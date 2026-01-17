#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float Time;
float FogDensity = 0.5;
float FogSpeed = 0.3;
float4 FogColor; // Add this parameter!

// SpriteBatch uses this built-in sampler
sampler2D SpriteTextureSampler : register(s0);

// SpriteBatch provides this matrix
matrix MatrixTransform;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

VertexShaderOutput SpriteVertexShader(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = mul(input.Position, MatrixTransform);
    output.Color = input.Color;
    output.TexCoord = input.TexCoord;
    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR0
{
    // Simple animated fog pattern
    float fog = sin(input.TexCoord.x * 10.0 + Time * FogSpeed) * cos(input.TexCoord.y * 10.0 + Time * FogSpeed);
    fog = (fog + 1.0) * 0.5;
    fog = fog * FogDensity;
    
    // Return FOG COLOR with alpha, not black!
    return float4(FogColor.rgb, fog * FogColor.a);
}

technique SpriteDrawing
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL SpriteVertexShader();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
}