float pow5_2(float x)
{
	float x2 = x * x;
	return x2 * x2 * x;
}

half3 F_Schlick_2(half u, half3 f0)
{
	return f0 + (1.0 - f0) * pow(1.0 - u, 5.0);
}

float F_Schlick_2(float f0, float f90, float VoH)
{
	return f0 + (f90 - f0) * pow5_2(1.0 - VoH);
}

half Fd_Burley_2(half roughness, half NoV, half NoL, half LoH)
{
	// Burley 2012, "Physically-Based Shading at Disney"
	half f90 = 0.5 + 2.0 * roughness * LoH * LoH;
	float lightScatter = F_Schlick_2(1.0, f90, NoL);
	float viewScatter  = F_Schlick_2(1.0, f90, NoV);
	return lightScatter * viewScatter;
}

half D_GGX_2(half NoH, half roughness)
{
	half a = NoH * roughness;
	half k = roughness / (1.0 - NoH * NoH + a * a);
	return k * k * (1.0 / PI);
}

// float D_GGX_Anisotropic_2_1(float NoH, half LoH, float3 h, float3 t, float3 b, float at, float ab)
// {
// 	half ToH = dot(t, h);
// 	half BoH = dot(b, h);
// 	half LoH2 = LoH * LoH;
// 	half a2 = at * ab;
// 	float3 v = float3(ab * ToH, at * BoH, a2 * NoH);
// 	float v2 = dot(v, v);
// 	half w2 = SafeDiv(a2 * a2 * a2, v2 * v2);
// 	return w2 * (LoH2 * 4);	
// }

// float D_GGX_Anisotropic_2_1(float NoH, float3 h, float3 t, float3 b, float at, float ab)
// {
// 	float ToH = dot(t, h);
// 	float BoH = dot(b, h);
// 	float a2 = at * ab;
// 	float3 v = float3(ab * ToH, at * BoH, a2 * NoH);
// 	float v2 = dot(v, v);
// 	float w2 = a2 / v2;
// 	return a2 * w2 * w2 * (1.0 / PI);
// }

float D_GGX_Anisotropic_2(float NoH, float3 h, float3 t, float3 b, float at, float ab) { // Filamented Anisotropic
    float ToH = dot(t, h);
    float BoH = dot(b, h);
    float a2 = at * ab;
    float3 v = half3(ab * ToH, at * BoH, a2 * NoH);
    float v2 = dot(v, v);
    float w2 = a2 / v2;
    return a2 * w2 * w2 * (1.0 / PI);
}

float V_SmithGGXCorrelatedFast_2(half NoV, half NoL, half roughness)
{
	half a = roughness;
	float GGXV = NoL * (NoV * (1.0 - a) + a);
	float GGXL = NoV * (NoL * (1.0 - a) + a);
	return 0.5 / (GGXV + GGXL);
}

float V_SmithGGXCorrelated_2(half NoV, half NoL, half roughness)
{
	#ifdef QUALITY_LOW
		return V_SmithGGXCorrelatedFast_2(NoV, NoL, roughness);
	#else
		half a2 = roughness * roughness;
		float GGXV = NoL * sqrt(NoV * NoV * (1.0 - a2) + a2);
		float GGXL = NoV * sqrt(NoL * NoL * (1.0 - a2) + a2);
		return 0.5 / (GGXV + GGXL);
	#endif
}

float V_SmithGGXCorrelated_Anisotropic_2(float at, float ab, float ToV, float BoV, float ToL, float BoL, float NoV, float NoL)
{
	float lambdaV = NoL * length(float3(at * ToV, ab * BoV, NoV));
	float lambdaL = NoV * length(float3(at * ToL, ab * BoL, NoL));
	float v = 0.5 / (lambdaV + lambdaL);
	return saturate(v);
}

void BurleyDiffuseAndGGXSpecularLightAnisotropicNode(out half3 diffuse, out half3 specular, half3 color, float3 direction, half attenuation, float3 normal, float3 viewDirectionWS, float3 tangentWS, float3 bitangentWS, half roughness, half metallic, half3 albedo = 1, half reflectance = 0.5, half energyCompensation = 1, half anisotropy = 0)
{
	diffuse = 0;
	specular = 0;
	#if defined(UNITY_PASS_FORWARDBASE ) || defined(UNITY_PASS_FORWARDADD) || defined(PREVIEW)
		half clampedRoughness = max(roughness * roughness, 0.002);

		// Filamented Anisotropic GGX definitions

		half3 v = viewDirectionWS;
		half3 l = direction;
		half3 n = normal;
		half3 h = normalize(l + v);
		half3 b = bitangentWS;
		half3 t = tangentWS;

		half NoH = saturate(dot(normal, h));

		float at = max(clampedRoughness * (1.0 + anisotropy), 0.001);
		float ab = max(clampedRoughness * (1.0 - anisotropy), 0.001);

		// END

		half3 f0 = 0.16 * reflectance * reflectance * (1.0 - metallic) + albedo * metallic;
		half NoV = dot(n, v) + 1e-5f;
		half ToV = dot(t, v) + 1e-5f;
		half BoV = dot(b, v) + 1e-5f;
		half NoL = saturate(dot(n, l));
		half ToL = dot(t, l);
		half BoL = dot(b, l);


		// float3 h = normalize(direction + viewDirectionWS);
		// half h = viewDirectionWS;
		half LoH = dot(direction, h);
		// half b = normalize(cross(normal, tangentWS));

		// half NoH = dot(normal, h);
		// half t = tangentWS;

		UNITY_BRANCH
		if (NoL > 0)
		{
			diffuse = NoL * attenuation * color;

			#if !defined(QUALITY_LOW)
				diffuse *= Fd_Burley_2(roughness, NoV, NoL, LoH);
			#endif

			#ifndef _SPECULARHIGHLIGHTS_OFF
				half3 F = F_Schlick_2(LoH, f0) * energyCompensation;
				// half D = D_GGX_2(NoH, clampedRoughness);-
				half D = D_GGX_Anisotropic_2(NoH, h, t, b, at, ab);
				// half V = V_SmithGGXCorrelated_2(NoV, NoL, clampedRoughness);
				half V = V_SmithGGXCorrelated_Anisotropic_2(at, ab, ToV, BoV, ToL, BoL, NoV, NoL);

				specular = max(0.0, (D * V) * F) * diffuse * PI * energyCompensation;
				// specular = D;
			#endif
		}
	#endif
}