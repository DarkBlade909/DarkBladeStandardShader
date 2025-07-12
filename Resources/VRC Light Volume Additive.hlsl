void VRCLightVolumeAdditive(float3 NormalWS, float3 PositionWS, out float3 Diffuse, out float3 AmbientDir, out float3 AmbientCol, out float3 R, out float3 G, out float3 B)
{

    float3 L0, L1r, L1g, L1b;
    #ifdef PREVIEW
        Diffuse = 0.15;
        AmbientDir = 0;
        AmbientCol = 0;
        R = 0;
        G = 0;
        B = 0;
    #else
        LightVolumeAdditiveSH(PositionWS, L0, L1r, L1g, L1b);
        Diffuse = LightVolumeEvaluate(NormalWS, L0, L1r, L1g, L1b);
        AmbientDir = normalize(L1r+L1g+L1b);
        AmbientCol = Diffuse;
        R = L1r;
        G = L1g;
        B = L1b;
    #endif
}