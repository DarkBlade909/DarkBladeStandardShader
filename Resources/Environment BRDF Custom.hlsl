#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ImageBasedLighting.hlsl"

void EnvironmentBRDFCustomNode(out float3 BRDF, out half3 energyCompensation, float3 normalWS, float3 viewDirectionWS, half metallic, half roughness, Texture2D DFGCustom, SamplerState sample, half reflectance = 0.5, half3 albedo = 1, half f82 = 1, half cloth = 0)
{
    half NoV = abs(dot(normalWS, viewDirectionWS)) + 1e-5f;
  	half3 f0 = 0.16 * reflectance * reflectance * (1.0 - metallic) + albedo * metallic;

    const float lutRes = 64;
    float2 coordLUT = Remap01ToHalfTexelCoord(float2(sqrt(NoV), roughness), lutRes);
    float4 dfg = SAMPLE_TEXTURE2D_LOD(DFGCustom, sampler_BilinearClamp, coordLUT, 0);
    // BRDF = lerp(dfg.xxx, dfg.yyy, f0);
    // BRDF = f0 * dfg.z;
    BRDF = lerp(lerp(dfg.xxx, dfg.yyy, f0), f0 * dfg.z, cloth);
    energyCompensation = lerp(1.0 + f0 * (1.0 / dfg.y - 1.0), 1, cloth);

    // f82
    float f = 6.0 / 7.0;
    float3 schlick = lerp(f0, 1.0, pow(f, 5));
    BRDF -= schlick * (7.0 / pow(f, 6)) * (1.0 - f82) * metallic;
    BRDF *= lerp(f82, 1.0, metallic);

}