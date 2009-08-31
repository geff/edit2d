//-----------------------------------------------------------------------------
// SpriteBatch.fx
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------


// Input parameters.
float2   ViewportSize    : register(c0);
float2   TextureSize     : register(c1);
float4x4 MatrixTransform : register(c2);
sampler  TextureSampler  : register(s0);
float timeMS;
float4 gradientColor1;
float4 gradientColor2;
bool isSelected;

float2   myEntiteSize;
float2   myTextureSize;

// Vertex shader for rendering sprites on Windows.
void SpriteVertexShader(inout float4 position : POSITION0,
		  				inout float4 color    : COLOR0,
						inout float2 texCoord : TEXCOORD0)
{
   
    // Apply the matrix transform.
    position = mul(position, transpose(MatrixTransform));
    
	// Half pixel offset for correct texel centering.
	position.xy -= 0.5;

	// Viewport adjustment.
	position.xy /= ViewportSize;
	position.xy *= float2(2, -2);
	position.xy -= float2(1, -1);

	// Compute the texture coordinate.
	texCoord /= TextureSize;
}

float GetEdgeValue(float2 texCoord : TEXCOORD0)
{
	float edgeValue = 100;
	
	float2 fullTexCoord = texCoord * myTextureSize;
		
	for(float x = -5; x <= 5; x++)
	{
		for(float y = -5; y <= 5; y++)
		{
			
			float2 newTexCoord;
			
			newTexCoord.x = fullTexCoord.x + x;
			newTexCoord.y = fullTexCoord.y + y;
			 //= fullTexCoord + float2(x,y);
			bool calcDist = false;
			
			if(newTexCoord.x > 0 && newTexCoord.y > 0 && newTexCoord.x < myTextureSize.x-0 && newTexCoord.y < myTextureSize.y-0)
			{
				float2 neewCoord = newTexCoord / myTextureSize;
				
				if(tex2D(TextureSampler, neewCoord).a == 1)
					calcDist = true;
					
				//edgeValue = 1;
			}
			else
			{
				calcDist = true;
			}
			
			if(calcDist)
			{
				float dist = distance(fullTexCoord, newTexCoord);
				
				if(dist < edgeValue)
					edgeValue = dist;
					
				//edgeValue = min(edgeValue, dist);
			}
		}
	}
	
	if(edgeValue == 100)
		edgeValue = 0;
	
	return edgeValue;
}

// Pixel shader for rendering sprites (shared between Windows and Xbox).
void SpritePixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
    color *= tex2D(TextureSampler, texCoord);
    
    //texCoord.y = texCoord.y  + (sin(texCoord.y*100)*0.03*(1+cos(6.28/500*timeMS))/2); 
	//color = tex2D( TextureSampler , texCoord);

	if(GetEdgeValue(texCoord) >0)
	{
		color.r = 0;
		color.g = 0;
		color.b = 0;
	}

	if(isSelected)
	{
		float4 clr1 =lerp(color, float4(0,0.3,1,1), 0.35);
		float4 clr2 =lerp(color, float4(0,0.3,1,1), 0.75);
		color = lerp(clr1,clr2, (1+cos(6.28/1000*timeMS))/2);
	}
}

// Pixel shader for rendering sprites (shared between Windows and Xbox).
void BackgroundPixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
	color = tex2D(TextureSampler, texCoord);


	if(color.a == 1)
	{
		color.r = 0;
		color.g = 0;
		color.b = 0;
	}
	else
		color=(0.5,0.5,0.5,0.0);
}

bool isInBackground = false;
float blurFactor = 0.0015; 
const float2 offsets[12] = {
   -0.326212, -0.405805,
   -0.840144, -0.073580,
   -0.695914,  0.457137,
   -0.203345,  0.620716,
    0.962340, -0.194983,
    0.473434, -0.480026,
    0.519456,  0.767022,
    0.185461, -0.893124,
    0.507431,  0.064425,
    0.896420,  0.412458,
   -0.321940, -0.932615,
   -0.791559, -0.597705,
};

float4 ConvertToGray(float4 color)
{
	//return (float4)((color.r+color.g+color.b) / (768));
	
	return float4(0,0,0,color.a);
}


void SpritePixelShaderBlur(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{

	float4 clr = color;
    color = tex2D(TextureSampler, texCoord);
    float4 tex;
    
    if(isInBackground)
      color = ConvertToGray(color);
    
    for(int i = 0; i < 12; i++){
        tex = tex2D(TextureSampler, texCoord + blurFactor * offsets[i]);
        
        if(isInBackground)
			tex = ConvertToGray(tex);
      
        color += tex;
    }
    
    color /= 13;
    
    if(!isInBackground)
		color *= clr;
}



void GradientPixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
	color = lerp(gradientColor1, gradientColor2, texCoord.y);
}

technique Gradient
{
    pass
    {
        PixelShader = compile ps_1_1 GradientPixelShader();
    }
}
 
technique Background
{
    pass
    {
        //VertexShader = compile vs_1_1 SpriteVertexShader();
        PixelShader = compile ps_1_1 BackgroundPixelShader();
    }
}

technique Blur
{
    pass
    {
        //VertexShader = compile vs_2_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 SpritePixelShaderBlur();
    }
}

technique SpriteBatch
{
	pass
	{
		//VertexShader = compile vs_3_0 SpriteVertexShader();
		PixelShader  = compile ps_3_0 SpritePixelShader();
	}
}
