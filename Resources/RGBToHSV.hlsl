// Converts a float3 (r, g, b) into a float3 (h, s, v)
void RGBToHSV(out float Hue, out float Saturation, out float Value, float3 RGB)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 p = lerp(float4(RGB.bg, K.w, K.z), float4(RGB.gb, K.x, K.y), step(RGB.b, RGB.g));
    float4 q = lerp(float4(p.xyw, RGB.r), float4(RGB.r, p.yzx), step(p.x, RGB.r));

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    float3 hsv = float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
    Hue = hsv.r;
    Saturation = hsv.g;
    Value = hsv.b;
}