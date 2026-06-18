// Converts a float3 (h, s, v) into a float3 (r, g, b)
void HSVToRGB(out float3 RGB, float Hue, float Saturation, float Value)
{
    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);

    float3 p = abs(frac(Hue.xxx + K.xyz) * 6.0 - K.www);

    RGB = lerp(K.xxx, saturate(p - K.xxx), Saturation) * Value;
}