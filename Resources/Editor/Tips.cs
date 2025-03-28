﻿using System;
using UnityEngine;
using UnityEditor;

namespace DarkBlade {
	public static class DarkBladeTips {
		// Standard
		public static GUIContent standWorkflow = new GUIContent("Workflow", "Standard:\nDefault packing mode of visually separated texture slots for PBR maps, uses MAHS format.\n\nPacked: \nModular packing mode that combines all PBR texture slots into one and allows channel selection.");
		public static GUIContent standBlendMode = new GUIContent("Blending Mode", "Opaque:\nDefault blending mode, has no transparency support.\n\nCutout:\nPixels outside the alpha threshold will be discarded and not rendered.\n\nFade:\nCreates a smoothly blended transparency that takes all alpha values from 0-1 into account when determining opacity.\n\nTransparent:\nFunctions similarily to Fade, but maintains reflections and specular behavior on transparent areas (ie. glass).");
		public static GUIContent samplingMode = new GUIContent("Sampling Mode", "The method used for sampling textures throughout the shader.\n\nDefault:\nDefault sampling behavior, 1 sample per texture.\n\nStochastic:\nUses uvs to generate pseudo-random positions for sampling to alleviate tiling artifacts. 3 samples per texture.\n\nSupersampled:\nManually handles mip mapping to help alleviate artifacts when viewing a surface from sharp angles. 4 samples per texture.\n\nTriplanar:\nIgnores uvs and samples based on normal direction. Up to 3 samples per texture.");
		public static GUIContent uvSetLabel = new GUIContent("UV Set");
		public static GUIContent albedoText = new GUIContent("Base Color", "Unshaded base color (RGB) and transparency (A)");
		public static GUIContent alphaCutoffText = new GUIContent("Alpha Cutoff", "Threshold for alpha cutoff");
		public static GUIContent metallicMapText = new GUIContent("Metallic", "How metallic a surface should look. Higher values will decrease visibility of base color in favor of reflective color to simulate the behavior of metal surfaces in real life. Lower values will look more akin to plastic.");
		public static GUIContent metallicPackedText = new GUIContent("Metallic Strength", metallicMapText.tooltip);
		public static GUIContent roughnessText = new GUIContent("Roughness", "How rough or smooth the surface should look. Rougher surfaces scatter more light rather than directly reflecting it as a mirror image.");
		public static GUIContent roughnessPackedText = new GUIContent("Roughness Strength", roughnessText.tooltip);
		public static GUIContent highlightsText = new GUIContent("Specular Highlights", "GGX Specular Highlights.");
		public static GUIContent reflectionsText = new GUIContent("Cubemap Reflections", "Cubemap-based reflections.");
		public static GUIContent normalMapText = new GUIContent("Normal Map", "Determines the behavior of light on the surface. Primarily used to fake small details or smooth bevels on sharp edges.");
		public static GUIContent heightMapText = new GUIContent("Height Map", "Uses parallax occlusion mapping to generate fake 3d bumps on a surface.");
		public static GUIContent heightMapPackedText = new GUIContent("Height Strength", heightMapText.tooltip);
		public static GUIContent occlusionText = new GUIContent("Ambient Occlusion", "Adds extra shading to simulate contact shadows in crevices or corners where less light reaches (think how the DVD logo almost never perfectly bounces into the corner of the screen). Darker values will add darker shading.");
		public static GUIContent occlusionPackedText = new GUIContent("Occlusion Strength", occlusionText.tooltip);
		public static GUIContent emissionText = new GUIContent("Color", "Adds extra color value after all lighting calculations to simulate an emissive effect.");
		public static GUIContent detailMaskText = new GUIContent("Detail Mask", "Mask for secondary maps.");
		public static GUIContent detailAlbedoText = new GUIContent("Detail Base Color", "Secondary unshaded base color (RGB)");
		public static GUIContent detailNormalMapText = new GUIContent("Detail Normal Map", normalMapText.tooltip);
		public static GUIContent detailRoughnessMapText = new GUIContent("Detail Roughness", roughnessText.tooltip);
		public static GUIContent detailAOMapText = new GUIContent("Detail Occlusion", occlusionText.tooltip);
		public static GUIContent packedMapText = new GUIContent("Packed Map", "Texture containing roughness, metallic, ambient occlusion and optional height maps in the RGBA channels.");
		public static GUIContent reflCubeText = new GUIContent("Reflection Fallback", "Replace environment reflections below the luminance threshold with this cubemap");
		public static GUIContent reflOverrideText = new GUIContent("Reflection Override", "Override the primary reflection probe sample with this cubemap");
		public static GUIContent ssrText = new GUIContent("Screen Space Reflections", "Screen Space Reflections create reflections on surfaces based on what's visible on screen.");
		public static GUIContent edgeFadeText = new GUIContent("Edge Fade", "Fades SSR around the edges of the screen to avoid a hard cutoff visual.");
		public static GUIContent stepsText = new GUIContent("Parallax Samples", "Number of steps/samples used to calculate parallax height. More samples is more expensive, but looks smoother.");
		public static GUIContent maskText = new GUIContent("Mask");
		public static GUIContent heightMaskText = new GUIContent("Height Mask");
		public static GUIContent parallaxOfsText = new GUIContent("Parallax Offset", "Offsets the parallax height value up or down to hide artifacts.");
		public static GUIContent thicknessMapText = new GUIContent("Thickness Map", "Determines the thickness of the surface. Thinner areas will let more light through.");
		public static GUIContent alphaMaskText = new GUIContent("Alpha Mask", "Separate texture for handling opacity. Black is transparent, white is opaque.");
		public static GUIContent separateAlpha = new GUIContent("Separate Alpha", "Use a separate texture for alpha transparency, rather than the alpha channel of the base color.");
		public static GUIContent triplanarFalloff = new GUIContent("Triplanar Falloff", "Blending smoothness between samples at different angles.");
		public static GUIContent useSmoothness = new GUIContent("Smoothness", "Changes to a smoothness workflow rather than roughness. Useful for regular standard shader packed maps.");
		public static GUIContent useHeight = new GUIContent("Packed Height", "Toggles use of heightmapping when in packed mode. Do not enable this unless you have a heightmap in your packed texture (and want to use it).");
		public static GUIContent emissPulseWave = new GUIContent("Pulse", "Pulses the strength of the emission using the selected waveform.");
		public static GUIContent audioLinkEmission = new GUIContent("Audio Link", "Audio link integration. For more info check out https://github.com/llealloo/vrc-udon-audio-link.");
		public static GUIContent audioLinkEmissionMeta = new GUIContent("Audio Link Meta Pass", "Apply Audio Link to the meta pass. Enable if you're using realtime GI, otherwise keep disabled.");
		public static GUIContent audioLinkEmissionStrength = new GUIContent("Audio Link Strength", "Strength of the audio link effect.");
		public static GUIContent emissPulseStrength = new GUIContent("Pulse Strength", "Strength of the pulsing effect");
		public static GUIContent emissPulseSpeed = new GUIContent("Pulse Speed", "Speed of the pulsing effect");
		public static GUIContent rimBlend = new GUIContent("Blending");
		public static GUIContent rimCol = new GUIContent("Color");
		public static GUIContent rimStr = new GUIContent("Strength");
		public static GUIContent rimWidth = new GUIContent("Width");
		public static GUIContent rimEdge = new GUIContent("Sharpness");
		public static GUIContent scatterCol = new GUIContent("Subsurface Color");
		public static GUIContent scatterAlbedoTint = new GUIContent("Base Color Tint");
		public static GUIContent scatterIntensity = new GUIContent("Direct Strength");
		public static GUIContent scatterAmbient = new GUIContent("Indirect Strength");
		public static GUIContent scatterPow = new GUIContent("Power");
		public static GUIContent scatterDist = new GUIContent("Distance");
		public static GUIContent wrappingFactor = new GUIContent("Wrapping Factor");
		public static GUIContent detailSamplingMode = new GUIContent("Use Sampling Mode", "Uses the selected Sampling Mode from the shader variant section to sample the detail textures.");
		public static GUIContent culling = new GUIContent("Culling", "Off:\nRenders both back and front faces at all times. Only use this if absolutely necessary.\n\nFront:\nCulls front faces, only displaying back faces.\n\nBack:\nCulls back faces, only displaying front faces.");
		public static GUIContent queueOffset = new GUIContent("Render Queue Offset", "Offsets the render queue +/- this value.");
		public static GUIContent useFresnel = new GUIContent("Fresnel", "A rim light effect based on the lighting of the scene");
		public static GUIContent reflVertexColor = new GUIContent("Vertex Color Reflections", "Multiplies reflection color with vertex color. A good use for this is painting black vertex colors on areas of surfaces you don't want to have visible reflections.");
		public static GUIContent reflShadows = new GUIContent("Specular Occlusion", "Uses shadows from either realtime directional lights or baked lightmaps to occlude reflections");
		public static GUIContent gsaa = new GUIContent("Specular Antialiasing", "Increases the roughness on rounded corners of normals to hide sparkly artifacts from specular lighting");
		public static GUIContent bicubicLightmap = new GUIContent("Bicubic Lightmap Sampling", "Samples lightmaps using bicubic filtering to hide aliasing on shadows. Provides a smooth gradient on otherwise low resolution shadows.");
		public static GUIContent bakeryMode = new GUIContent("Bakery Mode", "Support for the third party lightmapping software Bakery. See Bakery documentation for further details.");
		public static GUIContent lightMeshText = new GUIContent("Mesh", "Light mesh data");
		public static GUIContent lightTex0Text = new GUIContent("Texture 0", "Light texture 0");
		public static GUIContent lightTex1Text = new GUIContent("Texture 1", "Light texture 1");
		public static GUIContent lightTex2Text = new GUIContent("Texture 2", "Light texture 2, intended for indirect light");
		public static GUIContent lightTex3Text = new GUIContent("Texture 3+","Light texture 3 and above, intended for static lights");
		public static GUIContent opaqueLightsText = new GUIContent("Opaque Lights");
		public static GUIContent occlusionALText = new GUIContent("Occlusion", "ShadowMask (RGBA) and Occlusion (G)");
		public static GUIContent occlusionUVSetText = new GUIContent("UV Set");
		public static GUIContent vertexBaseColorText = new GUIContent("Vertex Base Color", "Multiplies base color with vertex color.");
		public static GUIContent unityFogToggleText = new GUIContent("Unity Scene Fog", "Enables or disables unity scene fog on this material.");
		public static GUIContent mirrorNormalSwizzleText = new GUIContent("Normal Swizzle", "Determines the axis of offset when applying the normal maps to mirror based reflections. If reflections look broken or incorrect, try each of these options to see which matches the orientation of your surface.");

		// Uber
		public static GUIContent renderModeLabel = new GUIContent("Shading", "Enables or disables shading. If you aren't using any shading features, disabling this can provide a huge performance boost");
		public static GUIContent blendModeLabel = new GUIContent("Blending Mode", "Opaque:\nDefault blending mode, has no transparency support.\n\nCutout:\nPixels outside the alpha threshold will be discarded and not rendered.\n\nDithered:\nUses a dithering pattern to apply cutout blending in a way that somewhat mimics the semi-transparency of fade.\n\nFade:\nCreates a smoothly blended transparency that takes all alpha values from 0-1 into account when determining opacity.\n\nTransparent:\nFunctions similarily to Fade, but maintains reflections and specular behavior on transparent areas (ie. glass).");
		public static GUIContent mirrorBehaviorLabel = new GUIContent("Mirror Behavior", "Only Reflection:\nOnly displays the material in mirror reflections.\n\nTextured Reflection:\nDisplays an alternative base color texture in mirror reflections.\n\nNo Reflection:\nOnly displays the material outside of mirror reflections.");
		public static GUIContent cubeModeLabel = new GUIContent("Main Texture Type", "2D:\nDefault base color texture.\n\nCubemap:\nUses a cubemap for the base color. Looks neat when combined with shading.\n\nCombined:\nUses both a cubemap and 2D texture for the base color, interpolating between them via mask or slider value.");
		public static GUIContent cullingModeLabel = new GUIContent("Culling", culling.tooltip);
		public static GUIContent useAlphaMaskLabel = new GUIContent("Alpha Source", "Base Color Alpha:\nUses the alpha channel of the base color texture to determine opacity.\n\nAlpha Mask:\nUses a separate alpha mask texture to determine opacity.");
		public static GUIContent maskLabel = new GUIContent("Mask");
		public static GUIContent baseColorLabel = new GUIContent("Base Color", "Unshaded base color.");
		public static GUIContent baseColor2Label = new GUIContent("Base Color 2");
		public static GUIContent mirrorTexLabel = new GUIContent("Base Color (Mirror)", "The base color texture that appears in mirrors.");
		public static GUIContent emissTexLabel = new GUIContent("Emission Map", "Adds extra color value after all lighting calculations to simulate an emissive effect.");
		public static GUIContent normalTexLabel = new GUIContent("Normal", normalMapText.tooltip);
		public static GUIContent metallicTexLabel = new GUIContent("Metallic", metallicMapText.tooltip);
		public static GUIContent roughnessTexLabel = new GUIContent("Roughness", roughnessText.tooltip);
		public static GUIContent occlusionTexLabel = new GUIContent("Occlusion", occlusionText.tooltip);
		public static GUIContent heightTexLabel = new GUIContent("Height", heightMapText.tooltip);
		public static GUIContent reflCubeLabel = new GUIContent("Cubemap");
		public static GUIContent shadowRampLabel = new GUIContent("Ramp");
		public static GUIContent specularTexLabel = new GUIContent("Specular Map");
		public static GUIContent primaryMapsLabel = new GUIContent("Primary Maps");
		public static GUIContent detailMapsLabel = new GUIContent("Detail Maps");
		public static GUIContent dissolveTexLabel = new GUIContent("Dissolve Map", "Dissolves areas of the material where the brightness of this texture falls below the dissolve amount value.");
		public static GUIContent dissolveRimTexLabel = new GUIContent("Rim Color");
		public static GUIContent colorLabel = new GUIContent("Color");
		public static GUIContent packedTexLabel = new GUIContent("Packed Texture");
		public static GUIContent cubemapLabel = new GUIContent("Cubemap");
		public static GUIContent thicknessTexLabel = new GUIContent("Thickness Map", thicknessMapText.tooltip);
		public static GUIContent tintLabel = new GUIContent("Tint");
		public static GUIContent filteringLabel = new GUIContent("PBR Filtering");
		public static GUIContent smoothTexLabel = new GUIContent("Smoothness");
		public static GUIContent alphaMaskLabel = new GUIContent("Alpha Mask");
		public static GUIContent curveTexLabel = new GUIContent("Curvature");
		public static GUIContent detailLabel = new GUIContent("Detail Maps");
		public static GUIContent subsurfLabel = new GUIContent("Subsurface");
		public static GUIContent diffuseLabel = new GUIContent("Diffuse");
		public static GUIContent eRimLabel = new GUIContent("Enviro. Rim");
		public static GUIContent basicRimLabel = new GUIContent("Basic Rim");
		public static GUIContent shadowLabel = new GUIContent("Shadows");
		public static GUIContent matcapLabel = new GUIContent("Matcap");
		public static GUIContent matcapPrimaryMask = new GUIContent("Primary Matcap");
		public static GUIContent matcapSecondaryMask = new GUIContent("Secondary Matcap");
		public static GUIContent specLabel = new GUIContent("Specular");
		public static GUIContent reflLabel = new GUIContent("Reflections");
		public static GUIContent matcapBlendLabel = new GUIContent("Matcap Blend", "Interpolates between the two matcaps.");
		public static GUIContent anisoBlendLabel = new GUIContent("Specular Blend", "Interpolates between GGX and anisotropic specular when using the combined specular mode.");
		public static GUIContent emissLabel = new GUIContent("Emission");
		public static GUIContent emissPulseLabel = new GUIContent("Emission Pulse");
		public static GUIContent filterLabel = new GUIContent("Filtering");
		public static GUIContent olThickLabel = new GUIContent("Outline Thickness");
		public static GUIContent refractLabel = new GUIContent("Refraction");
		public static GUIContent nearClipLabel = new GUIContent("Enable", "Clips pixels closer to the camera than the specified range.");
		public static GUIContent maskingModeLabel = new GUIContent("Mode", "Separate:\nEach mask texture is a separate texture sample. This can be very expensive when using a lot of them.\n\nPacked:\nPacks all masks into 4 textures for a significant performance boost.");
		public static GUIContent enableMaskTransformLabel = new GUIContent("Enable Transforms", "Enables scale/offset/scrolling options for all masks. Keep this disabled unless you really need to use those options, as it can be expensive.");
		public static GUIContent staticLightDirToggle = new GUIContent("Static Direction", "");
		public static GUIContent directAO = new GUIContent("Direct Occlusion", "Applies ambient occlusion shading to direct lighting.");
		public static GUIContent indirectAO = new GUIContent("Indirect Occlusion", "Applies ambient occlusion to indirect ambient lighting.");
		public static GUIContent disneyDiffuse = new GUIContent("Disney Term", "A rim lighting effect calculated based on the light in the scene and the roughness of the surface.");
		public static GUIContent shStr = new GUIContent("Spherical Harmonics", "Applies spherical harmonic shading based on ambient light data, gives a more standard-like appearance.");
		public static GUIContent nonlinearSHToggle = new GUIContent("Nonlinear SH", "Uses a different method of calculating Spherical Harmonics shading");
		public static GUIContent rtDirectCont = new GUIContent("Direct Intensity", "Strength of the direct lighting from realtime lights.");
		public static GUIContent rtIndirectCont = new GUIContent("Indirect Intensity", "Strength of realtime GI.");
		public static GUIContent vLightCont = new GUIContent("Vertex Intensity", "Strength of vertex lights (aka non-important lights).");
		public static GUIContent addCont = new GUIContent("Additive Intensity", "Strength of realtime point and spotlights.");
		public static GUIContent clampAdditive = new GUIContent("Clamp Additive", "Clamps the brightness of each individual point/spot light affecting the material. This is not an overall brightness clamp, meaning if you have two lights, each will be clamped to the specified value, then added together for the total brightness.");
		public static GUIContent directCont = new GUIContent("Direct Intensity", "This value will be the portion of indirect lighting used for direct lighting calculations.");
		public static GUIContent indirectCont = new GUIContent("Indirect Intensity", "This value will be the portion of indirect lighting used for indirect lighting calculations.");
		public static GUIContent shadowConditions = new GUIContent("Conditions", "The conditions under which shadows will be rendered.");
		public static GUIContent detailShadowMap = new GUIContent("Detail Shadow Map", "Forces shadows to be rendered anywhere the texture is not white.");
		public static GUIContent rtSelfShadow = new GUIContent("Directional Light Shadows", "Renders shadows from directional lights. These can be ugly due to unity calculating them at a fairly low resolution.");
		public static GUIContent attenSmoothing = new GUIContent("Smooth Attenuation", "Attempts to smooth out the attenuation value to avoid sharp artifacts.");
		public static GUIContent lightingBasedIOR = new GUIContent("Specular Occlusion", "Darkens reflections inside shadowed areas of the material.");
		public static GUIContent realtimeSpec = new GUIContent("Realtime Light Only", "Only applies specular highlights when a realtime light is present in the scene.");
		public static GUIContent specBiasOverride = new GUIContent("Manual Bias", "Manually controls the specularity bias. This tints the specular highlight based on either the base color, or the light color.");
		public static GUIContent manualSpecBright = new GUIContent("Ignore Environment", "The brightness and color of the specular highlight will be calculated solely based on the strength and tint properties above, rather than the environment lighting in the scene.");
		public static GUIContent clearCoatRough = new GUIContent("Clearcoat Roughness Map", "Roughness map used for clearcoat.");
		public static GUIContent clearCoatNormal = new GUIContent("Clearcoat Normal Map", "Normal map used for clearcoat.");
		public static GUIContent anisotropicMask = new GUIContent("Anisotropic Mask", "Mask map used to mask the anisotropy.");
		public static GUIContent anisotropicDirection = new GUIContent("Anisotropic Direction", "Vector map used for anisotropic direction.");
		public static GUIContent shadowMap = new GUIContent("Shadow Map", "Baked Shadow map, useful for areas that need to be in shadow all the time.");
		public static GUIContent crossMode = new GUIContent("Crossfade Mode", "While the environment lighting is brighter than the threshold value, emission will be off. As lighting gets darker, crossing the threshold value, it will get fade on and reach maximum brightness.");
		public static GUIContent reactThresh = new GUIContent("Threshold", "The lighting brightness threshold that determines when emission will be on or off.");
		public static GUIContent crossFade = new GUIContent("Strength", "The value + and - the threshold value where emission will begin to fade off or on.");
		public static GUIContent postFiltering = new GUIContent("Post Filtering", "Filtering will be applied to the final output color instead of the base color.");
		public static GUIContent stencilMode = new GUIContent("Stencil Mode", "Will only display the outline outside the mesh.");
		public static GUIContent applyOutlineEmiss = new GUIContent("Apply Emission", "Applies settings from the emission tab to the outline. This will only apply the emission colorpicker tint, however if Base Color Tint is also checked, the emission map will be applied as well.");
		public static GUIContent applyOutlineLighting = new GUIContent("Apply Shading", "Applies lighting brightness and shadows to the outline. This will also enable light reactivity with the outline emission.");
		public static GUIContent applyAlbedoTint = new GUIContent("Base Color Tint", "Tints the outline with the base color texture. This will also enable the emission map if Apply Emission is checked.");
		public static GUIContent ignoreFilterMask = new GUIContent("Ignore Filter Mask", "Filtering will be applied to the entire outline, rather than just the areas allowed by the Filtering Mask.");
		public static GUIContent useVertexColor = new GUIContent("Vertex Color Thickness", "Multiplies outline thickness by the mesh vertex colors. The darker the color, the thinner the outline.");
		public static GUIContent outlineRange = new GUIContent("Min Range", "Parts of the outline closer to the camera than this value will not be rendered.");
		public static GUIContent colorPreservation = new GUIContent("Tint Clamp", "Clamps the maximum brightness of the material to the base color plus reflections, specular, and subsurface scattering.");
		public static GUIContent matcapNormal = new GUIContent("Normal", "By default the matcap will use the normal maps in the textures tab, but this can be used to overwrite them (or mix using the option below).");
		public static GUIContent matcapNormalMix = new GUIContent("Mix Normals", "Mixes the normal maps from the textures tab into the normal map slot in this tab.");
		public static GUIContent clipEdge = new GUIContent("Clip Edge", "Clips a small border around the edge of the flipbook to remove lines from the edge fo the frame. Most obvious when the flipbook is scrolling.");
		public static GUIContent aces = new GUIContent("ACES", "Cheap approximated ACES tonemapping.");
		public static GUIContent mirrorMode = new GUIContent("Mirror Mode", "Enable if using this material as a VRChat mirror. Doing so will replace cubemap reflections with the reflections from the mirror.");

		// Water
		public static GUIContent stochasticLabel = new GUIContent("Stochastic", "Uses uvs to generate pseudo-random positions for sampling to alleviate tiling artifacts. 3 samples per texture.");
		public static GUIContent parallaxOffsetLabel = new GUIContent("Parallax Offset", "Offsets the texture sample up or down in 3D space.");
		public static GUIContent foamNormal = new GUIContent("Create Normals", "Automatically generates normals for the foam based on the grayscale value of the foam texture.");
		public static GUIContent foamCrestStrength = new GUIContent("Crest Strength", "How visible foam is on the crests of gerstner waves.");
		public static GUIContent foamCrestThreshold = new GUIContent("Crest Threshold", "How high or low a gerstner wave must be to have crest foam.");
		public static GUIContent foamEdgeStrength = new GUIContent("Edge Strength", "How visible the foam is on the edges.");
		public static GUIContent foamPower = new GUIContent("Edge Power", "Intensity of the edge foam gradient.");
		public static GUIContent foamRoughness = new GUIContent("Roughness", "How rough the foam should be when calculating reflections and specular highlights.");
		public static GUIContent foamNoiseTexCrestStrength = new GUIContent("Crest Noise", "How strongly the noise texture should affect crest foam.");
		public static GUIContent foamNoiseTexStrength = new GUIContent("Edge Noise", "How strongly the noise texture should affect edge foam.");
		public static GUIContent causticsFade = new GUIContent("Depth Fade", "Determines how strongly caustics will fade out at greater depths.");
		public static GUIContent causticsSurfaceFade = new GUIContent("Surface Fade", "Determines how strongly caustics will fade out close to the surface.");
		public static GUIContent turbulence = new GUIContent("Strength", "Adds variation to the height of waves.");
		public static GUIContent waterNormalMap = new GUIContent("Normal Map", "Determines the behavior of light on the surface. Primarily used to fake small details or smooth bevels on sharp edges. Also serves as the source for refraction and distortion effects on textures.");
		public static GUIContent waterRoughness = new GUIContent("Roughness", roughnessText.tooltip);
		public static GUIContent waterMetallic = new GUIContent("Metallic", "How metallic a surface should look. Higher values will decrease visibility of base color in favor of reflective color to simulate the behavior of metal surfaces in real life. Lower values will look more akin to normnal water.");
		public static GUIContent blendNoise = new GUIContent("Blend Noise", "Each normal map (and some other textures) are sampled twice with different uvs. This texture will determine the pattern for blending between the two samples.");
		public static GUIContent detailMode = new GUIContent("Decal Mode", "Uses the alpha and UVs of the decal base color for blending and sampling these textures.");
		
		// Particles
		public static GUIContent softening = new GUIContent("Softening", "Fades out the edges of particles where they interesect with opaque geometry to hide harsh cutoffs.");
		public static GUIContent falloffMode = new GUIContent("Mode", "Per Particle:\nFalloff will be based on the distance from the center of the particle.\n\nPer Vertex:\nFalloff will be based on the distance from the vertex of a particle. Mostly useful for larger mesh particles.");
		public static GUIContent autoShift = new GUIContent("Auto Shift", "Automatically shifts the hue over time based on the speed parameter.");
		public static GUIContent flipbookBlending = new GUIContent("Flipbook Blending", "Fades between frames instead of immediately cutting to make the animation smoother.");

		// Taken
		public static GUIContent gradientRestriction = new GUIContent("Gradient Restriction", "Masks the rim effect inside the gradient, so it will only be visible where the gradient is.");
		public static GUIContent emissionGradRestrict = new GUIContent("Gradient Restriction", "Masks the emission inside the gradient, so it will only be visible where the gradient is.");
		public static GUIContent restrictionMask = new GUIContent("Restriction Mask", "White areas will be exempt from the gradient restriction.");
		public static GUIContent gradientAxis = new GUIContent("Axis", "The direction the gradient will be applied. Y will come from below, Z will come from the front or behind, and X will come from the left or right. These directions are based on the mesh root position.");
		public static GUIContent endPos = new GUIContent("End Position", "How far from the start position the gradient will reach.");
		public static GUIContent startPos = new GUIContent("Start Position", "The position on the chosen axis that the gradient will start, measured by the distance from the mesh origin.");
		public static GUIContent noiseSmoothing = new GUIContent("Smoothing", "Smoothens the edges of the noise patches.");
		public static GUIContent globalTint = new GUIContent("Global Tint", "Determines the color of the majority of effects in the shader.");
		public static GUIContent emissionAO = new GUIContent("Emission (AO)", "Takes an ambient occlusion map and inverts it to determine emission. This is the same technique used by bungie for the effect in Destiny.");
		public static GUIContent invertTint = new GUIContent("Invert Tint", "Uses an inverted copy of the base color texture to determine outline tint.");
	}
}