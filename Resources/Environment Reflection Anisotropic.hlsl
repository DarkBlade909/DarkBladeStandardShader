void EnvironmentReflectionAnisotropicNode(out half3 Specular, float3 normalWS, float3 positionWS, float3 viewDirectionWS, half roughness, float3 tangentWS, float3 bitangentWS, half anisotropy, half3 BRDF = 1, half energyCompensation = 1)
{
	#ifdef PREVIEW
	Specular = .15;
	return;
	#endif

	#ifdef UNITY_PASS_FORWARDBASE
		half roughness2 = roughness * roughness;

		// float3 anisotropicDirection = anisotropy >= 0.0 ? tangentWS : tangentWS;

		float3 anisotropicDirection = 0;
		if (anisotropy >= 0 )
		{
			anisotropicDirection = bitangentWS;
		}
		else
		{
			anisotropicDirection = tangentWS;
			anisotropy *= -1;	
		}

		float3 anisotropicTangent = cross(anisotropicDirection, viewDirectionWS);
		float3 anisotropicNormal = cross(anisotropicTangent, anisotropicDirection);
		float3 bentNormal = normalize(lerp(normalWS, anisotropicNormal, anisotropy));
		float3 reflectVector = reflect(-viewDirectionWS, bentNormal);

		// float3 reflectVector = reflect(-viewDirectionWS, normalWS);

		#if !defined(QUALITY_LOW)
			reflectVector = lerp(reflectVector, normalWS, roughness2);
		#endif

		#if !defined(_GLOSSYREFLECTIONS_OFF)
			Specular = CalculateIrradianceFromReflectionProbes(reflectVector, positionWS, roughness, 0, normalWS);

			Specular *= BRDF * energyCompensation;
		#endif
	#else
		Specular = 0;
	#endif
}