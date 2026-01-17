#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float Time = 1.0;

static const float3 FOG_COLOR = float3(1, 1, 1);
static const float ZOOM = 6.0;
static const float INTENSITY = 0.3;
static const float CONTRAST = 5.0;
static const float SPEED = 1.0;


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

VertexShaderOutput MainVS(VertexShaderInput input)
{
    VertexShaderOutput output;
    output.Position = mul(input.Position, MatrixTransform);
    output.Color = input.Color;
    output.TexCoord = input.TexCoord;
    return output;
}

float random(float2 st)
{
    return frac(sin(dot(st, float2(12.9898, 79.279))) * 43758.5453123);
}
float hash(float2 p)
{
    float3 p3 = frac(float3(p.xyx) * 0.1031);
    p3 += dot(p3, p3.yzx + 33.33);
    return frac((p3.x + p3.y) * p3.z);
}
float2 random2(float2 st)
{
    // Same as GLSL version
    st = float2(dot(st, float2(127.1, 311.7)),
                dot(st, float2(269.5, 183.3)));
    
    return -1.0 + 2.0 * frac(sin(st) * 7.0);
}

float noise(float2 st)
{
    float2 i = floor(st); // Integer cell coordinates
    float2 f = frac(st); // Fraction within cell (0-1)
    
    // Smootherstep interpolation (same as GLSL)
    float2 u = f * f * (3.0 - 2.0 * f); // Creates smooth curve
    
    // Get gradients at 4 corners of cell
    float a = dot(random2(i + float2(0.0, 0.0)), f - float2(0.0, 0.0));
    float b = dot(random2(i + float2(1.0, 0.0)), f - float2(1.0, 0.0));
    float c = dot(random2(i + float2(0.0, 1.0)), f - float2(0.0, 1.0));
    float d = dot(random2(i + float2(1.0, 1.0)), f - float2(1.0, 1.0));
    
    // Bilinear interpolation (mix = lerp in HLSL)
    return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
}

float fractal_brownian_motion(float2 coord)
{
    float value = 0.0;
    float scale = 0.5; // Start with 0.5
    
    // Octave 1
    value += noise(coord) * scale;
    coord *= 2.0;
    scale *= 0.5;
    
    // Octave 2  
    value += noise(coord) * scale;
    coord *= 2.0;
    scale *= 0.5;
    
    // Octave 3
    value += noise(coord) * scale;
    coord *= 2.0;
    scale *= 0.5;
    
    // Octave 4
    value += noise(coord) * scale;
    
    // Convert from approx -1..1 to 0..1 properly
    // value ranges from -1 to 1 approximately
    return (value * 0.5) + 0.5; // Better scaling
}

float4 MainPS(VertexShaderOutput input) : COLOR0
{
    float2 st = input.TexCoord;
    
    float timeToUse = Time * SPEED;
    float zoomToUse = ZOOM;
    float intensityToUse = INTENSITY;
    float3 fogColorToUse = FOG_COLOR;
    
    float2 pos = st * zoomToUse;
    float2 timeOffset = float2(timeToUse * -0.5, timeToUse * -0.3);
    float2 motion = float2(
        fractal_brownian_motion(pos + timeOffset),
        fractal_brownian_motion(pos + timeOffset + float2(5.2, 1.3))
    );
    
    float2 displacedPos = pos + motion;
    float final = fractal_brownian_motion(displacedPos);

    float3 bgColor = float3(0.0, 0.0, 0.0);
    
    final = final - 0.5;
    final = final * CONTRAST;
    final = final * 0.5 + 0.5;
    final = saturate(final);
    final *= intensityToUse;
    
    float3 color = lerp(bgColor, fogColorToUse, final);
    
    return float4(color, 0);
}

technique Fog
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
}