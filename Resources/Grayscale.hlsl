void Grayscale(float3 Color, out float Gray)
{
	Gray = max(max(Color.r,Color.g),Color.b);
}