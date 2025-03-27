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
			Unity_GlossyEnvironmentData envData;
			envData.roughness = roughness;
			envData.reflUVW = BoxProjectedCubemapDirection(reflectVector, positionWS, unity_SpecCube0_ProbePosition, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);

			half3 probe0 = Unity_GlossyEnvironment(UNITY_PASS_TEXCUBE(unity_SpecCube0), unity_SpecCube0_HDR, envData);
			half3 reflectionSpecular = probe0;

			#if defined(UNITY_SPECCUBE_BLENDING)
				UNITY_BRANCH
				if (unity_SpecCube0_BoxMin.w < 0.99999)
				{
					envData.reflUVW = BoxProjectedCubemapDirection(reflectVector, positionWS, unity_SpecCube1_ProbePosition, unity_SpecCube1_BoxMin, unity_SpecCube1_BoxMax);
					float3 probe1 = Unity_GlossyEnvironment(UNITY_PASS_TEXCUBE_SAMPLER(unity_SpecCube1, unity_SpecCube0), unity_SpecCube1_HDR, envData);
					reflectionSpecular = lerp(probe1, probe0, unity_SpecCube0_BoxMin.w);
				}
			#endif
			Specular = reflectionSpecular;
			#if !defined(QUALITY_LOW)
				float horizon = min(1.0 + dot(reflectVector, normalWS), 1.0);
				Specular *= horizon * horizon;
			#endif

			Specular *= BRDF * energyCompensation;
		#endif
	#else
		Specular = 0;
	#endif
}