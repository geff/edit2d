#define BLURFACTOR 1 //Multipler for the velocity
#define COLOR_SAMPLES 15 //number of samples used to blur the color(should be an odd value)
#define VELOCITY_SAMPLES 13 //number of samples used to blur the velocity(should be divible by 4 + 1)
#define SAMPLE_WIDTH 1.5 //Number of pixes used by increment function during velocity blur


float2 viewport : register(c0);
float4x4 MatrixTransform : register(c1);

sampler SamplerColor : register(s0) = sampler_state
{
	MagFilter = LINEAR;
	MinFilter = LINEAR;
	
	MaxAnisotropy = 16;
	
	AddressU = Clamp;
	AddressV = Clamp;
};

sampler SamplerVel : register(s1) = sampler_state
{
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = NONE;
	
	AddressU = Clamp;
	AddressV = Clamp;
};

sampler SamplerLastVel : register(s2) = sampler_state
{
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = NONE;

	AddressU = Clamp;
	AddressV = Clamp;
};

struct VertexShaderInput
{
	float3 P : POSITION0; //Position
	float3 LP : POSITION1; //Last Positioo
	float2 T : TEXCOORD0; //Tex coord
};

struct VertexShaderVelocityOutput
{
	float4 P : POSITION0; //Position
	float2 V : TEXCOORD0; //Velocity
	float2 T : TEXCOORD1;
};

struct VertexShaderColorOutput
{
	float4 P : POSITION0;
	float2 T : TEXCOORD0;
};

//Tranforms vector2 to orthographic space
float4 OrthoTransform(float2 vec)
{
	float4 outp;
	
    //vec = mul(vec, transpose(MatrixTransform));
	
	//The following 2 lines are the HLSL equivalent of the following XNA line
	// outp = Vector2.Transform(vec,Matrix.CreateOrthographicOffCenter(0,viewport.x,viewport.y,0,0,1);
	outp.x = vec.x * (2/viewport.x) - 1;
	outp.y = vec.y * (-2/viewport.y) + 1;
	
	
	
	outp.zw = float2(0,1);

    outp.xy = mul(outp.xy, transpose(MatrixTransform));
	
	return outp;
}

//Tranforms input positions and calculates the currect velocity
VertexShaderVelocityOutput VertexShaderVelocity(VertexShaderInput input)
{
	VertexShaderVelocityOutput output;
	
	//Shift position to lineup texels with pixels
	// Read http://msdn.microsoft.com/en-us/library/bb219690(VS.85).aspx for more info
	input.P.xy -= .5f;
	input.LP.xy -= .5f;
	
	float4 position = OrthoTransform(input.P.xy);
	float4 lastPosition = OrthoTransform(input.LP.xy);
	
	float2 velocity = position - lastPosition;
	velocity /= 2;
	
	output.P = position;
	output.V = velocity; //Velocity is passed to pixel shader
	output.T = input.T;
	
	return output;
}

//Writes the velocity information to the texture
float4 PixelShaderVelocity(VertexShaderVelocityOutput input) : COLOR0
{

	float alpha = tex2D(SamplerColor,input.T).w;
	
	if(alpha < .01)//we don't want to override the current velocity if the alpha is small
	{
		clip(-1);
	}
	
	float2 vel = input.V * ceil(alpha);
		
#ifdef USING_COLOR
	//Since color textures don't support negative values shift the values to the positive range
	vel *= RANGE_EXPAND;
	vel += ((float)128)/255;
#endif

	return float4(vel.x, vel.y , 0, 1);
}

//Simple translation of Position to screen space
VertexShaderColorOutput VertexShaderSimple(VertexShaderInput input)
{
	VertexShaderColorOutput output;

	//Shift position to lineup texels with pixels
	// Read http://msdn.microsoft.com/en-us/library/bb219690(VS.85).aspx for more info
	input.P.xy -= .75;
	
	output.P = OrthoTransform(input.P.xy);
	
	output.T = input.T;
	
	return output;
}

//Blurs Velocity to color blur lines
float4 PixelShaderBlur(VertexShaderColorOutput input) : COLOR0
{

	#define pixX (SAMPLE_WIDTH/viewport.x)
	#define pixY (SAMPLE_WIDTH/viewport.y)
	
	float3 color = tex2D(SamplerColor,input.T).xyz;
	
	for(int ii=1;ii<=((VELOCITY_SAMPLES-1)/4);++ii)
	{
		color += tex2D(SamplerColor,input.T-float2(ii*pixX,0)).xyz;
		color += tex2D(SamplerColor,input.T+float2(ii*pixX,0)).xyz;
		color += tex2D(SamplerColor,input.T-float2(0,ii*pixY)).xyz;
		color += tex2D(SamplerColor,input.T+float2(0,ii*pixY)).xyz;
	}
	
	return float4(color/VELOCITY_SAMPLES,1);
	
	#undef pixX
	#undef pixY	
}


float4 PixelShaderColor(VertexShaderColorOutput input) : COLOR0
{
	return tex2D(SamplerColor,input.T);
}

//Blur input color according to the current velocity
float4 PixelShaderCombine(VertexShaderColorOutput input) : COLOR0
{
	float2 inc = tex2D(SamplerVel,input.T).xy;
	
#ifdef USING_COLOR
	inc -= ((float)128)/255;
	inc /= RANGE_EXPAND;
#endif

	inc.y = - inc.y;

	inc *= BLURFACTOR;
	
	float3 color = tex2D(SamplerColor, input.T).xyz;
	
	for(int ii=1;ii<=(COLOR_SAMPLES-1)/2;++ii)
	{
		color += tex2D(SamplerColor, input.T + inc * ii/COLOR_SAMPLES).xyz;
		color += tex2D(SamplerColor, input.T - inc * ii/COLOR_SAMPLES).xyz;
	}
	
	return float4((color/COLOR_SAMPLES),1);
}

technique Technique1
{
	pass ColorPass
	{ 
		ZENABLE = false;
		CullMode = None;
		AlphaBlendEnable = true;
		SrcBlend = SrcAlpha;
		DestBlend = InvSrcAlpha;
		Fillmode = Solid;
		
		VertexShader = compile vs_2_0 VertexShaderSimple();
		PixelShader = compile ps_2_0 PixelShaderColor();
	}
	
	pass Velocity
	{
		ZENABLE = false;
		CullMode = None;
		AlphaBlendEnable = false;
		FillMode = Solid;
		
		VertexShader = compile vs_2_0 VertexShaderVelocity();
		PixelShader = compile ps_2_0 PixelShaderVelocity();
	}
	
	pass BlurVelocity
	{
		ZENABLE = false;
		CullMode = None;
		AlphaBlendEnable = false;
		Fillmode = Solid;
		
		VertexShader = compile vs_2_0 VertexShaderSimple();
		PixelShader = compile ps_2_0 PixelShaderBlur();
	}
	
	
	pass Combine
	{
		ZENABLE = false;
		CullMode = None;
		AlphaBlendEnable = false;
		Fillmode = Solid;
		
		VertexShader = compile vs_2_0 VertexShaderSimple();
		PixelShader = compile ps_2_0 PixelShaderCombine();
	}
	
	
}
