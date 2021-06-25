#define MaxSteps 512
#define SurfDist 0.0001

void Raymarch_float( Texture3D sdf, SamplerState samplerState, float3 worldPosition, float3 worldViewDir, float surfaceOffset, out float3 position, out float3 normal, out float depthOffset, out float mask)
{
	position = worldPosition;
	normal = float3(0, 1, 0);
	depthOffset = 0;
	mask = 0;

	float4 c = 0;

	[loop]
	for (int i = 0; i < MaxSteps; i++)
	{
		c = sdf.Sample(samplerState, position);

		if ((c.r - surfaceOffset) < SurfDist)
		{
			mask = 1;
			break;
		}
		else
		{
			position += worldViewDir * c.r;

			if (position.x < 0 || position.x > 1 ||
				position.y < 0 || position.y > 1 ||
				position.z < 0 || position.z > 1)
			{
				break;
			}

			depthOffset += c.r;
		}
	}

	normal = c.wyz;
}