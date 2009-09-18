//-----------------------------------------------------------
float2   ViewportSize    : register(c0);
float2   TextureSize     : register(c1);
float4x4 MatrixTransform : register(c2);
sampler  TextureSampler  : register(s0);
//-----------------------------------------------------------

Texture EdgePassTexture;
sampler EdgePassSampler = sampler_state
{
    Texture = <EdgePassTexture>;   
	MaxAnisotropy = 16;
	MinFilter = Anisotropic;
	MagFilter = Anisotropic;
	MipFilter = Anisotropic;
};

Texture NormalMapTexture;
sampler NormalMapSampler = sampler_state
{
    Texture = <NormalMapTexture>;    
	MaxAnisotropy = 16;
};


Texture SpriteBatchTexture;
sampler SpriteBatchSampler = sampler_state
{
    Texture = <SpriteBatchTexture>;    
};

//--- Selection
float timeMS;
bool isSelected;
//---

//--- Background
float4 gradientColor1;
float4 gradientColor2;
//---

float2   myEntiteSize;
float2   myEntitePosition;
float2   myTextureSize;
float2 blipPos;
bool isInBackground = false;
float blurFactor = 0.0f; 
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

const float2 neighbours[8] = {
	-1,-1,
	0,-1,
	1,-1,
	-1,0,
	1,0,
	-1,1,
	0,1,
	1,1
};

//--- Edge pass
bool initEdgePass = false;
//---


//-----------------------------------------------------------
//			VERTEX SHADER
//-----------------------------------------------------------
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

//-----------------------------------------------------------
//-----------------------------------------------------------
float GetEdgeValuePass(float2 texCoord : TEXCOORD0)
{
	float edgeValue = 1;
	float4 color = tex2D(EdgePassSampler, texCoord);

	if(initEdgePass)
	{
		if(color.a   < 1)
			edgeValue = 0;
	}
	else if(color.b   < 1)
	{
			//edgeValue = color.a;
			edgeValue = color.b;
			//edgeValue = 0.33;
	}
	else
	{
		//edgeValue = 0.66;
		
		
		int j = 0;
		for(j = 0; j < 8; j++)
		{
			//if(edgeValue != 1)
			{
				float2 newTexCoord = texCoord + neighbours[j]/ myTextureSize;
				
				if(newTexCoord.x>= 0 && newTexCoord.x <= 1 && newTexCoord.y >= 0 && newTexCoord.y<= 1)
				{
					float4 clr = tex2D(EdgePassSampler, newTexCoord);
					
					//if(clr.a+0.1  < edgeValue)
					if(clr.b+0.1 < edgeValue)
					{
						//if(edgeValue > clr.a-0.1)
							//edgeValue = clr.a-0.2;
							edgeValue = clr.b+0.1;
							//edgeValue = min(clr.a+0.1, edgeValue);
					}
				}
				else
				{
					edgeValue = 0;
				}
			}
		}
	}

	if(edgeValue < 0)
		edgeValue = 0;
	
	//edgeValue = texCoord.x;
	return edgeValue;
}

//-----------------------------------------------------------
//-----------------------------------------------------------
float GetEdgeValue(float2 texCoord : TEXCOORD0, float length)
{
	float edgeValue = 0;
	float nbValue = 0;
	
	if(tex2D(TextureSampler, texCoord).a != 0)
	{
		float2 fullTexCoord = texCoord * myTextureSize;
		
		//float length =5;
		
		for(float x = -length; x <=length; x+=2)
		{
			for(float y = -length; y <= length; y+=2)
			{
				
				float2 newTexCoord = fullTexCoord +float2(x,y);
				
				bool calcDist = false;
				
				if(newTexCoord.x > 0 && newTexCoord.y > 0 && newTexCoord.x < myTextureSize.x-0 && newTexCoord.y < myTextureSize.y-0)
				{
					float2 neewCoord = newTexCoord / myTextureSize;
					
					if(tex2D(TextureSampler, neewCoord).a < 1)
						calcDist = true;
				}
				else
				{
					calcDist = true;
				}
				
				if(calcDist)
				{
					float dist = distance(fullTexCoord, newTexCoord);
					
					nbValue++;
					//if(dist < edgeValue)
					edgeValue += dist;
				}
			}
		}
	}

	//if(edgeValue == 100)
	//	edgeValue = 0;
	//else
	if(nbValue >0)
		edgeValue /= nbValue;
	
	return edgeValue;
}

//-----------------------------------------------------------
//-----------------------------------------------------------
float4 GetColorSobel(float2 texCoord : TEXCOORD0 )
{
	float4 OUT;

	 const int NUM = 9;
    const float threshold = 0.05;

    const float2 c[NUM] = {
            float2(-0.07287, 0.07287), 
            float2( 0.00 ,     0.07287),
            float2( 0.07287, 0.07287),
            float2(-0.07287, 0.00 ),
            float2( 0.0,       0.0),
            float2( 0.07287, 0.07287 ),
            float2(-0.07287,-0.07287),
            float2( 0.00 ,    -0.07287),
            float2( 0.07287,-0.07287),
    };

    float4 col[NUM];    
    int i;

	// it stores the samples of texture to col array.
    for (i=0; i < NUM; i++) 
	{
     	float2 newTexCoord =  texCoord + c[i];
		col[i] = tex2D(TextureSampler, newTexCoord);
	}
	
	float3 rgb2lum = float3(0.30, 0.9, 0.5);
    
    float lum[NUM];
    for (i = 0; i < NUM; i++) {
      lum[i] = dot(col[i].xyz, rgb2lum);
    }

	//Sobel filter computes new value at the central position by sum the weighted neighbors.
    float x = lum[2]+  lum[8]+2*lum[5]-lum[0]-2*lum[3]-lum[6];
    float y = lum[6]+2*lum[7]+  lum[8]-lum[0]-2*lum[1]-lum[2];

	//show the points which values are over the threshold and hide others. Final result is the product of col[5] and edge detector value. Brightness adjusts the brightness of the image.
    float edge =(x*x + y*y < threshold)? 1.0:0.0;

		float Brightness = 1.5;
	//final output
    OUT.xyz = Brightness * col[5].xyz * edge.xxx;
    OUT.w = 1.0;
	
	return OUT;
}

//-----------------------------------------------------------
//-----------------------------------------------------------
float4 ConvertToGray(float4 color)
{
	//return (float4)((color.r+color.g+color.b) / (768));
	
	return float4(0,0,0,color.a);
}

//-----------------------------------------------------------
//-----------------------------------------------------------
void SelectColor(inout float4 color)
{
	if(isSelected)
	{
		float4 clr1 =lerp(color, float4(0,0.3,1,1), 0.35);
		float4 clr2 =lerp(color, float4(0,0.3,1,1), 0.75);
		color = lerp(clr1,clr2, (1+cos(6.28/1000*timeMS))/2);
	}
}

/*
//-----------------------------------------------------------
//-----------------------------------------------------------
void EdgePixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
	//float4 edgeValue = tex2D(EdgePassSampler, texCoord);
	float4 spriteColor =tex2D(TextureSampler, texCoord);

	//color*=spriteColor;
	if(spriteColor.a = 1)
	{
		if(edgeValue.a<-1)
		{
			color= float4(edgeValue.a,edgeValue.a,edgeValue.a,1);
		}
		else
		{
			//color*=spriteColor;
		}
	}
	else
	{
		color = float4(1,1,1,1);
	}
	
	SelectColor(color);
}*/

//-----------------------------------------------------------
//-----------------------------------------------------------
void EdgePassPixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
	float4 colorTex = tex2D(TextureSampler, texCoord);
	//float4 colorTexEdge = tex2D(EdgePassSampler, texCoord);
	
	
	//if(colorTex.a  = 1)
	{
		float edgeValue = GetEdgeValuePass(texCoord);
		
		if(edgeValue < 1)
		 {
			//color.r = edgeValue;
			//color.g =edgeValue;
			//color.b = 1;
			//color.a = edgeValue;
			
			color.r = 0;
			color.g = edgeValue;
			color.b = edgeValue;
			color.a = 1;
		 }
		 else
		 {
			color = float4(1,0,1,1);
		 }
	}
	//else
	//{
	//	color = float4(1,0,1,0);
	//}
	 	 
	 //color.a = tex2D(TextureSampler, texCoord).a;
	 
	
	//texCoord += colorTexEdge.rg/200;
	
	//color = tex2D(EdgePassSampler, texCoord);
	
	//color = colorTexEdge+float4(0.1,0.1,0.1,0);
	 //color = float4(edgeValue,edgeValue,edgeValue, 1);
	 
	 //color=colorTex;
}

//-----------------------------------------------------------
//-----------------------------------------------------------
void SobelPixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
	color = GetColorSobel(texCoord);
	 
	 //color.a = tex2D(TextureSampler, texCoord).a;
	 
	SelectColor(color);
}


 
float4 NormalMapPixelShader(in float2 uv:TEXCOORD0) : COLOR
{
	//float textureSize = 256.0f;
	float texelSize =  1.0f / myTextureSize.x ; //size of one texel;
	float normalStrength = 8;

    float tl = abs(tex2D (EdgePassSampler, uv + texelSize * float2(-1, -1)).x);   // top left
    float  l = abs(tex2D (EdgePassSampler, uv + texelSize * float2(-1,  0)).x);   // left
    float bl = abs(tex2D (EdgePassSampler, uv + texelSize * float2(-1,  1)).x);   // bottom left
    float  t = abs(tex2D (EdgePassSampler, uv + texelSize * float2( 0, -1)).x);   // top
    float  b = abs(tex2D (EdgePassSampler, uv + texelSize * float2( 0,  1)).x);   // bottom
    float tr = abs(tex2D (EdgePassSampler, uv + texelSize * float2( 1, -1)).x);   // top right
    float  r = abs(tex2D (EdgePassSampler, uv + texelSize * float2( 1,  0)).x);   // right
    float br = abs(tex2D (EdgePassSampler, uv + texelSize * float2( 1,  1)).x);   // bottom right
 
    // Compute dx using Sobel:
    //           -1 0 1 
    //           -2 0 2
    //           -1 0 1
    float dX = tr + 2*r + br -tl - 2*l - bl;
 
    // Compute dy using Sobel:
    //           -1 -2 -1 
    //            0  0  0
    //            1  2  1
    float dY = bl + 2*b + br -tl - 2*t - tr;
 
    // Build the normalized normal
    float4 N = float4(normalize(float3(dX, 1.0f / normalStrength, dY)), 1.0f);
 
    //convert (-1.0 , 1.0) to (0.0 , 1.0), if needed
    return N * 0.5f + 0.5f;
}

//-----------------------------------------------------------
//-----------------------------------------------------------
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
		
	SelectColor(color);
}

//-----------------------------------------------------------
//-----------------------------------------------------------
void BlurPixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
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
		
	SelectColor(color);
}

//-----------------------------------------------------------
//-----------------------------------------------------------
void GradientPixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
	color = lerp(gradientColor1, gradientColor2, texCoord.y);
}

//-----------------------------------------------------------
//-----------------------------------------------------------
void NightPixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
	/*
	float maxEdgeValue =6;
	float edgeValue = GetEdgeValue(texCoord, maxEdgeValue);
	
	color = (0,0,0,0);
	
	if(edgeValue > 0 )
	 {
		color+=dot(float2(0.5-texCoord.x,0.5-texCoord.y), float2(edgeValue, edgeValue));
	 }
	 
	 color.a = tex2D(TextureSampler, texCoord).a;
	*/

	float4 colorTex = tex2D(TextureSampler, texCoord);
	float4 colorNormalMap = normalize(2*tex2D(NormalMapSampler, texCoord)-1);
	
	float l = 150;
	float angle = 6.28/2000 * timeMS;
	float2 lightPos = float2(l*cos(angle), l*sin(angle));
	float2 texelPos = myTextureSize * (texCoord - 0.5);
	float2 vec = normalize(-lightPos + texelPos);

	float dt = saturate(dot(colorNormalMap.xz, vec));
	
	color*=colorTex*0.75+0.25*saturate(dot(colorNormalMap.xz, vec));
	
	//color = float4(dt,dt,dt,1);
	
	SelectColor(color);
}

//-----------------------------------------------------------
//-----------------------------------------------------------
void SpritePixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{

	//NightPixelShader(color, texCoord);
	//texCoord.y = texCoord.y  + (sin(texCoord.y*100)*0.03*(1+cos(6.28/500*timeMS))/2); 
	/*
	if(isInBackground)
	{
		BackgroundPixelShader(color, texCoord);
	}
	else
	{
		color *= tex2D(TextureSampler, texCoord);
	}
	
	if(blurFactor>0.0f)
	{
		BlurPixelShader(color, texCoord);
		//color = float4(0,0,0,0);
	}
	*/
    
    //color *= tex2D(SpriteBatchSampler, texCoord);
    color=float4(1,1,1,0);
	//color = tex2D( TextureSampler , texCoord);
	//SelectColor(color);
}

//-----------------------------------------------------------
//-----------------------------------------------------------
void EdgePixelShader(inout float4 color : COLOR0, float2 texCoord : TEXCOORD0)
{
	float4 clr =  tex2D( TextureSampler , texCoord);
	
	if(clr.a  != 1)
	{
		color = float4(1,1,1,1);
	}
	else
	{
		float nbPass = 15;
		float edgeValue = GetEdgeValue(texCoord,nbPass) / nbPass;
	
		if(edgeValue <= 0)
			edgeValue = 1;
			
		color=float4(edgeValue,edgeValue,edgeValue, 1);
	}
}

//-----------------------------------------------------------
//			TECHNIQUES
//-----------------------------------------------------------
technique NormalMap
{
    pass HeightMap
    {
        PixelShader = compile ps_3_0 EdgePixelShader();
    }
    pass NormalMap
    {
        PixelShader = compile ps_3_0 NormalMapPixelShader();
    }
}
//-----------------------------------------------------------
//-----------------------------------------------------------
technique EdgePass
{
    pass
    {
        PixelShader = compile ps_3_0 EdgePassPixelShader();
    }
}
//-----------------------------------------------------------
//-----------------------------------------------------------
technique Edge
{
    pass
    {
        PixelShader = compile ps_3_0 EdgePixelShader();
    }
}
//-----------------------------------------------------------
//-----------------------------------------------------------
technique Gradient
{
    pass
    {
        PixelShader = compile ps_3_0 GradientPixelShader();
    }
}
//-----------------------------------------------------------
//-----------------------------------------------------------
technique Night
{
    pass
    {
        PixelShader = compile ps_3_0 NightPixelShader();
    }
}
//-----------------------------------------------------------
//-----------------------------------------------------------
technique Background
{
    pass
    {
        //VertexShader = compile vs_1_1 SpriteVertexShader();
        PixelShader = compile ps_3_0 BackgroundPixelShader();
    }
}
//-----------------------------------------------------------
//-----------------------------------------------------------
technique Blur
{
    pass
    {
        //VertexShader = compile vs_2_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 BlurPixelShader();
    }
}
//-----------------------------------------------------------
//-----------------------------------------------------------
technique SpriteBatch
{
	pass
	{
		//VertexShader = compile vs_3_0 SpriteVertexShader();
		PixelShader  = compile ps_3_0 SpritePixelShader();
	}
}