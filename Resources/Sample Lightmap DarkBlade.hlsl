half D_GGX(half NoH, half roughness)
{
    half a = NoH * roughness;
    half k = roughness / (1.0 - NoH * NoH + a * a);
    return k * k * (1.0 / UNITY_PI);
}

// Bicubic from Core RP

float2 BSpline3MiddleLeft(float2 x)
{
    return 0.16666667 + x * (0.5 + x * (0.5 - x * 0.5));
}

float2 BSpline3MiddleRight(float2 x)
{
    return 0.66666667 + x * (-1.0 + 0.5 * x) * x;
}

float2 BSpline3Rightmost(float2 x)
{
    return 0.16666667 + x * (-0.5 + x * (0.5 - x * 0.16666667));
}

void BicubicFilter(float2 fracCoord, out float2 weights[2], out float2 offsets[2])
{
    float2 r  = BSpline3Rightmost(fracCoord);
    float2 mr = BSpline3MiddleRight(fracCoord);
    float2 ml = BSpline3MiddleLeft(fracCoord);
    float2 l  = 1.0 - mr - ml - r;

    weights[0] = r + mr;
    weights[1] = ml + l;
    offsets[0] = -1.0 + mr * rcp(weights[0]);
    offsets[1] =  1.0 + l * rcp(weights[1]);
}

float4 TexelSizeFromTexture2D(Texture2D t)
{
    float4 texelSize;
    t.GetDimensions(texelSize.x, texelSize.y);
    texelSize.zw = 1.0 / texelSize.xy;
    return texelSize;
}

float4 SampleTexture2DBicubic(Texture2D tex, SamplerState smp, float2 coord, float4 texSize, float2 maxCoord)
{
    float2 xy = coord * texSize.xy + 0.5;
    float2 ic = floor(xy);
    float2 fc = frac(xy);

    float2 weights[2], offsets[2];
    BicubicFilter(fc, weights, offsets);

    return weights[0].y * (weights[0].x * tex.SampleLevel(smp, min((ic + float2(offsets[0].x, offsets[0].y) - 0.5) * texSize.zw, maxCoord), 0.0)  +
                           weights[1].x * tex.SampleLevel(smp, min((ic + float2(offsets[1].x, offsets[0].y) - 0.5) * texSize.zw, maxCoord), 0.0)) +
           weights[1].y * (weights[0].x * tex.SampleLevel(smp, min((ic + float2(offsets[0].x, offsets[1].y) - 0.5) * texSize.zw, maxCoord), 0.0)  +
                           weights[1].x * tex.SampleLevel(smp, min((ic + float2(offsets[1].x, offsets[1].y) - 0.5) * texSize.zw, maxCoord), 0.0));
}

float shEvaluateDiffuseL1Geomerics(float L0, float3 L1, float3 n)
{
    // average energy
    float R0 = L0;
    
    // avg direction of incoming light
    float3 R1 = 0.5f * L1;
    
    // directional brightness
    float lenR1 = length(R1);
    
    // linear angle between normal and direction 0-1
    //float q = 0.5f * (1.0f + dot(R1 / lenR1, n));
    //float q = dot(R1 / lenR1, n) * 0.5 + 0.5;
    float q = dot(normalize(R1), n) * 0.5 + 0.5;
    q = saturate(q); // Thanks to ScruffyRuffles for the bug identity.
    
    // power for q
    // lerps from 1 (linear) to 3 (cubic) based on directionality
    float p = 1.0f + 2.0f * lenR1 / R0;
    
    // dynamic range constant
    // should vary between 4 (highly directional) and 0 (ambient)
    float a = (1.0f - lenR1 / R0) / (1.0f + lenR1 / R0);
    
    return R0 * (a + (1.0f - a) * (p + 1.0f) * pow(q, p));
}

void SampleLightmapAndSpecularNode(out half3 Diffuse, out half3 Specular, out half3 Color, out half4 Direction, float3 ViewDirectionWS, float2 lightmapUV, float3 normalWS, half roughness)
{
	Diffuse = 0;
	Color = 0;
	Direction = 0;
	Specular = 0;

	half roughness2 = roughness * roughness;

	#if defined(LIGHTMAP_ON)
        #if defined(_BICUBIC_LIGHTMAP) && !defined(QUALITY_LOW)
            float4 texelSize = TexelSizeFromTexture2D(unity_Lightmap);
            half3 illuminance = SampleTexture2DBicubic(unity_Lightmap, custom_bilinear_clamp_sampler, lightmapUV, texelSize, 1.0).rgb;
        #else
            half3 illuminance = DecodeLightmap(unity_Lightmap.SampleLevel(custom_bilinear_clamp_sampler, lightmapUV, 0));
        #endif

		Color = illuminance;

        #if defined(DIRLIGHTMAP_COMBINED) || defined(_BAKERY_MONOSH)
            #if defined(_BICUBIC_LIGHTMAP) && !defined(QUALITY_LOW)
                half4 directionalLightmap = SampleTexture2DBicubic(unity_LightmapInd, custom_bilinear_clamp_sampler, lightmapUV, texelSize, 1.0);
            #else
                half4 directionalLightmap = unity_LightmapInd.SampleLevel(custom_bilinear_clamp_sampler, lightmapUV, 0);
            #endif
			Direction = directionalLightmap;
            #ifdef _BAKERY_MONOSH
                half3 L0 = illuminance;
                half4 nL1 = directionalLightmap * 2.0 - 1.0;
				Direction = nL1;
                half3 L1x = nL1.x * L0 * 2.0;
                half3 L1y = nL1.y * L0 * 2.0;
                half3 L1z = nL1.z * L0 * 2.0;
                #ifdef BAKERY_SHNONLINEAR
                    float lumaL0 = dot(L0, 1);
                    float lumaL1x = dot(L1x, 1);
                    float lumaL1y = dot(L1y, 1);
                    float lumaL1z = dot(L1z, 1);
                    float lumaSH = shEvaluateDiffuseL1Geomerics(lumaL0, float3(lumaL1x, lumaL1y, lumaL1z), normalWS);

                    half3 sh = L0 + normalWS.x * L1x + normalWS.y * L1y + normalWS.z * L1z;
                    float regularLumaSH = dot(sh, 1);
                    sh *= lerp(1, lumaSH / regularLumaSH, saturate(regularLumaSH * 16));
                #else
                    half3 sh = L0 + normalWS.x * L1x + normalWS.y * L1y + normalWS.z * L1z;
                #endif

                illuminance = sh;
                #ifdef _LIGHTMAPPED_SPECULAR
                {
                    half smoothnessLm = 1.0f - max(roughness2, 0.002);
                    smoothnessLm *= sqrt(saturate(length(nL1)));
                    half roughnessLm = 1.0f - smoothnessLm;
                    half3 dominantDir = nL1;
                    half3 halfDir = Unity_SafeNormalize(normalize(dominantDir) + ViewDirectionWS);
                    half nh = saturate(dot(normalWS, halfDir));
                    half spec = D_GGX(nh, roughnessLm);
                    sh = L0 + dominantDir.x * L1x + dominantDir.y * L1y + dominantDir.z * L1z;
                    
                    #ifdef _ANISOTROPY
                        // half at = max(roughnessLm * (1.0 + surf.Anisotropy), 0.001);
                        // half ab = max(roughnessLm * (1.0 - surf.Anisotropy), 0.001);
                        // giOutput.indirectSpecular += max(Filament::D_GGX_Anisotropic(nh, halfDir, sd.tangentWS, sd.bitangentWS, at, ab) * sh, 0.0);
                    #else
                        Specular += max(spec * sh, 0.0);
                    #endif
                }
                #endif
            #else
                half halfLambert = dot(normalWS, directionalLightmap.xyz - 0.5) + 0.5;
                illuminance = illuminance * halfLambert / max(1e-4, directionalLightmap.w);
            #endif
        #endif
        #if defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN)
            illuminance = SubtractMainLightWithRealtimeAttenuationFromLightmap(illuminance, unityLight.attenuation, float4(0,0,0,0), normalWS);
            unityLight.color = 0;
        #endif

        Diffuse = illuminance;

        // #if defined(_BAKERY_MONOSH)
        //     giOutput.indirectOcclusion = (dot(nL1, giInput.reflectVector) + 1.0) * L0 * 2.0;
        // #else
        //     giOutput.indirectOcclusion = illuminance;
        // #endif
	#endif
}