#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float Time;
float3 FogColor = float3(0.3, 0.3, 0.3);
float FogDensity = 0.6;
float Zoom = 1;

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

float random(float2 st)
{
    return frac(sin(dot(st, float2(12.9898, 79.279))) * 43758.5453123);
}

float noise(float2 st)
{
    float2 i = floor(st);
    float2 f = frac(st);
    float2 u = f * f * (3.0 - 2.0 * f);
    
    float a = random(i);
    float b = random(i + float2(1.0, 0.0));
    float c = random(i + float2(0.0, 1.0));
    float d = random(i + float2(1.0, 1.0));
    
    return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
}

VertexShaderOutput MainVS(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = mul(input.Position, MatrixTransform);
    output.Color = input.Color;
    output.TexCoord = input.TexCoord;
    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR0
{
    float2 st = input.TexCoord;
    
    // Add animation by moving the coordinates
    float2 movingSt = st;
    movingSt.x += Time * 0.02; // Slow horizontal drift
    movingSt.y += Time * 0.01; // Even slower vertical drift
    
    // Create FBM with animation
    float n = 0.0;
    n += noise(movingSt * Zoom * 0.5) * 0.5;
    n += noise(movingSt * Zoom * 1.0 + 5.0) * 0.25;
    n += noise(movingSt * Zoom * 2.0 + 10.0) * 0.125;
    
    n = n / 0.875;
    
    // Vertical gradient
    float vertical = 1.0 - st.y;
    vertical = vertical * vertical;
    
    // Combine
    float fog = n * vertical * FogDensity;
    
    return float4(FogColor, fog);
}
float4 MainPSs(VertexShaderOutput input) : COLOR0
{
    return float4(1, 0, 0, 1);
}

technique Fog
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
}