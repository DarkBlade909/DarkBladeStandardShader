using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DarkBlade;

internal class DarkBladeStandardGUI: ShaderGUI
{

	public static Dictionary<Material, Toggles> foldouts = new Dictionary<Material, Toggles>();
	Toggles toggles = new Toggles(new string[] {
		"Primary Textures"
	}, 1);

	string versionLabel = "v1.0";

	MaterialProperty bumpMap = null;
	MaterialProperty bumpMapClear = null;
	MaterialProperty clearcoatRoughnessMap = null;
	MaterialProperty DFG = null;
	MaterialProperty detailAlbedo = null;
	MaterialProperty detailMRO = null;
	MaterialProperty detailNormal = null;
	MaterialProperty emissionMap = null;
	MaterialProperty emissionStrength = null;
	MaterialProperty mainTex = null;
	MaterialProperty metallicGloss = null;
	MaterialProperty color = null;
	MaterialProperty emissionColor = null;
	MaterialProperty clearcoatNormalStrength = null;
	MaterialProperty clearcoatRoughness = null;
	MaterialProperty clearcoatStrength = null;
	MaterialProperty cull = null;
	MaterialProperty cutoff = null;
	MaterialProperty toggleAnisotropy = null;
	MaterialProperty anisotropy = null;
	MaterialProperty anisotropicRotation = null;
	MaterialProperty anisotropicColor = null;
	MaterialProperty anisotropicMask = null;
	MaterialProperty anisotropicDirection = null;
	MaterialProperty anisotropicRotationMap = null;
	MaterialProperty toggleanisotropicDirection = null;
	MaterialProperty shadowmap = null;
	MaterialProperty detailAlbedoStrength = null;
	MaterialProperty detailNormalPower = null;
	MaterialProperty detailOcclusionStrength = null;
	MaterialProperty detailRoughBias = null;
	MaterialProperty detailRoughPower = null;
	MaterialProperty dstBlend = null;
	MaterialProperty gsaaThreshold = null;
	MaterialProperty gsaaVariance = null;
	MaterialProperty indirectSpec = null;
	MaterialProperty metallic = null;
	MaterialProperty mode = null;
	MaterialProperty occlusionStrength = null;
	MaterialProperty surfaceOptions = null;
	MaterialProperty normalPower = null;
	MaterialProperty properties = null;
	MaterialProperty roughness = null;
	MaterialProperty specularAO = null;
	MaterialProperty srcBlend = null;
	MaterialProperty toggleClearcoat = null;
	MaterialProperty toggleDetail = null;
	MaterialProperty toggleGSAA = null;
	MaterialProperty toggleUEFlip = null;
	MaterialProperty toggleUEFlipClearcoat = null;
	MaterialProperty toggleUseBaseNormal = null;
	MaterialProperty ZWrite = null;
	MaterialProperty flipBackfaceNormals = null;
	MaterialProperty monoSH = null;
	MaterialProperty lightmapSpecular = null;
	MaterialProperty nonlinearSH = null;
	MaterialProperty bicubicLightmap = null;
	MaterialProperty nonlinearLightProbeSH = null;
	MaterialProperty specularOcclusion = null;
	MaterialProperty specularOcclusionSharpness = null;

	MaterialEditor me;

	bool m_FirstTimeApply = true;

	public void FindProperties(MaterialProperty[] props, Material mat)
	{
		bumpMap = FindProperty("_BumpMap", props);
		bumpMapClear = FindProperty("_BumpMapClear", props);
		clearcoatRoughnessMap = FindProperty("_ClearcoatRoughnessMap", props);
		DFG = FindProperty("_DFG", props);
		detailAlbedo = FindProperty("_DetailAlbedo", props);
		detailMRO = FindProperty("_DetailMRO", props);
		detailNormal = FindProperty("_DetailNormal", props);
		mainTex = FindProperty("_MainTex", props);
		metallicGloss = FindProperty("_MetallicGloss", props);
		color = FindProperty("_Color", props);
		emissionColor = FindProperty("_EmissionColor", props);
		emissionMap = FindProperty("_EmissionMap", props);
		emissionStrength = FindProperty("_EmissionStrength", props);
		clearcoatNormalStrength = FindProperty("_ClearcoatNormalStrength", props);
		clearcoatRoughness = FindProperty("_ClearcoatRoughness", props);
		clearcoatStrength = FindProperty("_ClearcoatStrength", props);
		cull = FindProperty("_Cull", props);
		cutoff = FindProperty("_Cutoff", props);
		toggleAnisotropy = FindProperty("_Toggle_ANISOTROPY", props);
		anisotropy = FindProperty("_Anisotropy", props);
		anisotropicRotation = FindProperty("_AnisotropicRotation", props);
		anisotropicRotationMap = FindProperty("_AnisotropicRotationMap", props);
		anisotropicColor = FindProperty("_AnisotropicColor", props);
        anisotropicMask = FindProperty("_AnisotropicMask", props);
		anisotropicDirection = FindProperty("_AnisotropicDirection", props);
		toggleanisotropicDirection = FindProperty("_Toggle_USEANISODIRECTION", props);
		shadowmap = FindProperty("_ShadowMap", props);
		detailAlbedoStrength = FindProperty("_DetailAlbedoStrength", props);
		detailNormalPower = FindProperty("_DetailNormalPower", props);
		detailOcclusionStrength = FindProperty("_DetailOcclusionStrength", props);
		detailRoughBias = FindProperty("_DetailRoughBias", props);
		detailRoughPower = FindProperty("_DetailRoughPower", props, false);
		dstBlend = FindProperty("_DstBlend", props);
		gsaaThreshold = FindProperty("_GSAAThreshold", props);
		gsaaVariance = FindProperty("_GSAAVariance", props);
		indirectSpec = FindProperty("_IndirectSpec", props);
		metallic = FindProperty("_Metallic", props);
		mode = FindProperty("_Mode", props);
		normalPower = FindProperty("_NormalPower", props);
		occlusionStrength = FindProperty("_OcclusionStrength", props);
		properties = FindProperty("_Properties", props);
		roughness = FindProperty("_Roughness", props);
		specularAO = FindProperty("_SpecularAO", props);
		srcBlend = FindProperty("_SrcBlend", props);
		surfaceOptions = FindProperty("_SurfaceOptions", props);
		toggleClearcoat = FindProperty("_Toggle_CLEARCOAT", props);
		toggleDetail = FindProperty("_Toggle_DETAIL", props);
		toggleGSAA = FindProperty("_Toggle_GSAA", props);
		toggleUEFlip = FindProperty("_Toggle_UEFlip", props);
		toggleUEFlipClearcoat = FindProperty("_Toggle_UEFlipClearcoat", props);
		toggleUseBaseNormal = FindProperty("_Toggle_USEBASENORMAL", props);
		ZWrite = FindProperty("_ZWrite", props);
		flipBackfaceNormals = FindProperty("_Toggle_FLIPBACKFACENORMALS", props);
		monoSH = FindProperty("_Toggle_MONOSH", props);
		lightmapSpecular = FindProperty("_Toggle_LIGHTMAPSPECULAR", props);
		nonlinearSH = FindProperty("_Toggle_BAKERYNLSH", props);
		bicubicLightmap = FindProperty("_Toggle_BICUBICLIGHTMAP", props);
		nonlinearLightProbeSH = FindProperty("_Toggle_NLLIGHTPROBESH", props);
		specularOcclusion = FindProperty("_SpecularOcclusion", props);
		specularOcclusionSharpness = FindProperty("_SpecularOcclusionSharpness", props);
	}

	public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
	{
		me = materialEditor;
		Material material = materialEditor.target as Material;

		FindProperties(props, material);

		// Make sure that needed setup (ie keywords/renderqueue) are set up if we're switching some existing
		// material to a standard shader.
		// Do this before any GUI code has been issued to prevent layout issues in subsequent GUILayout statements (case 780071)

		// Add mat to foldout dictionary if it isn't in there yet
		if (!foldouts.ContainsKey(material))
			foldouts.Add(material, toggles);

		// Use default labelWidth
		EditorGUIUtility.labelWidth = 0f;

		// Detect any changes to the material
		EditorGUI.BeginChangeCheck();
		{

			// Primary properties
			DGUI.CenteredText("DarkBlade Standard Shader", 18, 2,2);
			DGUI.VersionLabel("v1.0",12,0,0);
			DGUI.Space4();
			// Core Shader Variant
			DGUI.BoldLabel("Shader Variant");
			DoVariantArea(material);
			DGUI.Space2();
			DGUI.BoldLabel("Main Settings");
			DGUI.PropertyGroup(() => {
				DoPrimaryArea(material);
			});
			DGUI.Space2();
			DGUI.BoldLabel("Detail");
			DGUI.PropertyGroup(() => {
				DoSecondaryArea(material);
			});
			DGUI.Space2();
			DGUI.BoldLabel("Clearcoat");
			DGUI.PropertyGroup(() => {
				DoDistortionArea(material);
			});
			DGUI.Space2();
			DGUI.BoldLabel("Anisotropy");
			DGUI.PropertyGroup(() => {
				DoAnisotropyArea(material);
			});
			DGUI.Space2();
			DGUI.BoldLabel("Lightmap");
			DGUI.PropertyGroup(() => {
			DoLightmapArea(material);
			});
			DGUI.Space2();
			DGUI.BoldLabel("Render Settings");
			DGUI.PropertyGroup(() => {
				DoRenderingArea(material);
			});
		}

		DGUI.Space8();
	}

	void DoVariantArea(Material material)
	{
		DGUI.PropertyGroup(() => {
			me.ShaderProperty(mode, DarkBladeTips.standBlendMode);
			if (mode.floatValue == 1 || mode.floatValue == 6)
				me.ShaderProperty(cutoff, DarkBladeTips.alphaCutoffText);
			EditorGUI.BeginChangeCheck();
			SetupMaterialWithBlendMode(material);

			// MGUI.SpaceN2();
			// if (MGUI.PropertyButton("Unity Standard Packing Format")){
			// 	ApplyStandardPackingFormat(mat);
			// }
		});
	}
	void DoPrimaryArea(Material material)
	{
		DGUI.PropertyGroup(() => {
			me.TexturePropertySingleLine(DarkBladeTips.albedoText, mainTex, color);
			me.TexturePropertySingleLine(DarkBladeTips.packedMapText, metallicGloss);
			me.ShaderProperty(metallic, "Metallic");
			me.ShaderProperty(roughness, "Roughness");
			me.ShaderProperty(occlusionStrength, "Occlusion Strength");
			me.TexturePropertySingleLine(DarkBladeTips.normalMapText, bumpMap, normalPower);
			me.ShaderProperty(toggleUEFlip, "Flip Green Channel (UE)");
			me.TexturePropertySingleLine(DarkBladeTips.emissionText, emissionMap, emissionColor);
			me.ShaderProperty(emissionStrength, "Emission Strength");

		});
		EditorGUI.BeginChangeCheck();
		DGUI.PropertyGroup(() => {
			DGUI.TextureSO(me, mainTex);
		});
	}
	void DoSecondaryArea(Material material)
	{
		DGUI.PropertyGroup(() => {
			me.ShaderProperty(toggleDetail, "Detail Map");
		});
		if (toggleDetail.floatValue == 1)
			DGUI.PropertyGroup(() => {
			me.TexturePropertySingleLine(DarkBladeTips.albedoText, detailAlbedo);
			me.ShaderProperty(detailAlbedoStrength, "Albedo Strength");
			me.TexturePropertySingleLine(DarkBladeTips.packedMapText, detailMRO);
			me.ShaderProperty(detailRoughPower, "Roughness Strength");
			me.ShaderProperty(detailRoughBias, "Roughness Bias");
			me.ShaderProperty(detailOcclusionStrength, "Occlusion Strength");
			me.TexturePropertySingleLine(DarkBladeTips.normalMapText, detailNormal, detailNormalPower);
			});
		EditorGUI.BeginChangeCheck();
		if (toggleDetail.floatValue == 1)
			DGUI.PropertyGroup(() => {
				DGUI.TextureSO(me, detailAlbedo);
			});
	}
	void DoDistortionArea(Material material)
	{
		DGUI.PropertyGroup(() => {
			me.ShaderProperty(toggleClearcoat, "Clearcoat");
		});
		EditorGUI.BeginChangeCheck();
		if (toggleClearcoat.floatValue == 1)
			DGUI.PropertyGroup(() => {
				me.ShaderProperty(clearcoatStrength, "Clearcoat Strength");
				me.TexturePropertySingleLine(DarkBladeTips.clearCoatRough, clearcoatRoughnessMap);
				me.ShaderProperty(clearcoatRoughness, "Clearcoat Roughness");
				me.ShaderProperty(toggleUseBaseNormal, "Use Custom Normal");
				if (toggleUseBaseNormal.floatValue == 1)
					DGUI.PropertyGroup(() => {
						me.TexturePropertySingleLine(DarkBladeTips.clearCoatNormal, bumpMapClear, clearcoatNormalStrength);
						me.ShaderProperty(toggleUEFlipClearcoat, "Flip Green Channel (UE)");
					});
			});
	}

	void DoAnisotropyArea(Material material)
	{
		DGUI.PropertyGroup(() => {
			me.ShaderProperty(toggleAnisotropy, "Anisotropy");
		});
		EditorGUI.BeginChangeCheck();
		if (toggleAnisotropy.floatValue == 1)
			DGUI.PropertyGroup(() => {
				me.ShaderProperty(anisotropy, "Anisotropy");
				me.ShaderProperty(anisotropicRotation, "Anisotropic Rotation");
				me.TexturePropertySingleLine(DarkBladeTips.anisotropicRotationMap, anisotropicRotationMap);
				me.ShaderProperty(anisotropicColor, "Anisotropic Color");
				me.TexturePropertySingleLine(DarkBladeTips.anisotropicMask, anisotropicMask);
				me.ShaderProperty(toggleanisotropicDirection, "Use Anisotropic Direction Map");
				if (toggleanisotropicDirection.floatValue == 1)
					DGUI.PropertyGroup(() => 
					{
						me.TexturePropertySingleLine(DarkBladeTips.anisotropicDirection, anisotropicDirection);
					});
				if (toggleanisotropicDirection.floatValue == 1)
					DGUI.PropertyGroup(() =>
					{
						DGUI.TextureSO(me, anisotropicDirection);
					});
				});
	}
	void DoLightmapArea(Material material)
	{
        me.ShaderProperty(monoSH, "MonoSH");
		if (monoSH.floatValue == 1)
			material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
				if (monoSH.floatValue == 1)
					DGUI.PropertyGroup(() => {
						me.ShaderProperty(lightmapSpecular, "Lightmap Specular");
						me.ShaderProperty(nonlinearSH, "Bakery Non-Linear SH");
					});
				me.ShaderProperty(bicubicLightmap, "Bicubic Lightmap Sampling");
				me.ShaderProperty(nonlinearLightProbeSH, "Non-Linear Light Probe SH");
				me.ShaderProperty(specularOcclusion, "Specular Occlusion");
				me.ShaderProperty(specularOcclusionSharpness, "Specular Occlusion Sharpness");
	}

	void DoRenderingArea(Material material)
	{
		me.ShaderProperty(cull, "Culling Mode");
		me.ShaderProperty(flipBackfaceNormals, "Flip Backface Normals");
		me.ShaderProperty(indirectSpec, "Indirect Specular");
		me.ShaderProperty(specularAO, "Specular Occlusion");
		me.TexturePropertySingleLine(DarkBladeTips.shadowMap, shadowmap);
		me.ShaderProperty(toggleGSAA, "Geometric Specular Anti-Aliasing");
		if (toggleGSAA.floatValue == 1)
			DGUI.PropertyGroup(() => {
				me.ShaderProperty(gsaaThreshold, "GSAA Threshold");
				me.ShaderProperty(gsaaVariance, "GSAA Variance");
			});
	}

	public static void SetupMaterialWithBlendMode(Material material)
	{

		switch (material.GetInt("_Mode"))
		{
			case 0: // opaque
				material.SetOverrideTag("RenderType", "");
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				material.SetInt("_ZWrite", 1);
				material.SetInt("_AlphaToMask", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHAFADE_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.DisableKeyword("_ALPHAMODULATE_ON");
				material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry + material.GetInt("_QueueOffset");
				break;
			case 1: // cutout
				material.SetOverrideTag("RenderType", "TransparentCutout");
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				material.SetInt("_ZWrite", 1);
				material.SetInt("_AlphaToMask", 0);
				material.EnableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHAFADE_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.DisableKeyword("_ALPHAMODULATE_ON");
				material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest + material.GetInt("_QueueOffset");	
				break;
			case 2: // fade
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				material.SetInt("_ZWrite", 0);
				material.SetInt("_AlphaToMask", 1);
				material.DisableKeyword("_ALPHATEST_ON");
				material.EnableKeyword("_ALPHAFADE_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.DisableKeyword("_ALPHAMODULATE_ON");
				material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + material.GetInt("_QueueOffset");
				break;
			case 3: // premultiply
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				material.SetInt("_ZWrite", 0);
				material.SetInt("_AlphaToMask", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHAFADE_ON");
				material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
				material.DisableKeyword("_ALPHAMODULATE_ON");
				material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + material.GetInt("_QueueOffset");
				break;
			case 4: // additive
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
				material.SetInt("_ZWrite", 0);
				material.SetInt("_AlphaToMask", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.EnableKeyword("_ALPHAFADE_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.DisableKeyword("_ALPHAMODULATE_ON");
				material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + material.GetInt("_QueueOffset");
				break;
			case 5: // multiply
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				material.SetInt("_ZWrite", 0);
				material.SetInt("_AlphaToMask", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHAFADE_ON");
				material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
				material.EnableKeyword("_ALPHAMODULATE_ON");
				material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + material.GetInt("_QueueOffset");
				break;
			case 6: // transclipping
				material.SetOverrideTag("RenderType", "TransparentCutout");
				material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				material.SetInt("_ZWrite", 1);
				material.SetInt("_AlphaToMask", 0);
				material.DisableKeyword("_ALPHATEST_ON");
				material.DisableKeyword("_ALPHAFADE_ON");
				material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
				material.DisableKeyword("_ALPHAMODULATE_ON");
				material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest + 10 + material.GetInt("_QueueOffset");
				break;
		}
	}
}