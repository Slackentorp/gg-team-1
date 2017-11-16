Shader "Custom/OverlayBlendMode_Effect" 
{
	Properties 
		{
			_MainTex ("Albedo (RGB)", 2D) = "white" {}
			_BlendTex ("Blend Texture", 2D) = "white" {}
			_perlinTex ("Perlin Texture", 2D) = "white" {}
			_perlinTex2 ("Perlin Texture2", 2D) = "white" {}
			_Opacity ("Blend Opacity", Range(0, 1)) = 1.0
			_perlinOpacity ("Perlin Opacity", Range(0, 1)) = 1.0
			_ScratchesYSpeed ("Scratches Y Speed", Float) = 10.0
			_ScratchesXSpeed ("Scratches X Speed", Float) = 10.0
			_RandomValue ("Random Value", Float) = 1.0
			_FadeValue ("Fade Value" , Float) = 1.0
			_FadeSpeed ("Fade Speed", Float) = 1.0
			//_Contrast ("Contrast", Float) = 3.0
		}
	SubShader 
	{
		Pass
			{
				CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#pragma framentoption ARB_Precision_hint_fastest
				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				uniform sampler2D _BlendTex;
				uniform sampler2D _perlinTex;
				uniform sampler2D _perlinTex2;
				fixed _Opacity;
				fixed _perlinOpacity;
				fixed _ScratchesYSpeed;
				fixed _ScratchesXSpeed;
				fixed _RandomValue;
				fixed _FadeValue;
				fixed _FadeSpeed;
				//fixed _Contrast;fsdaf

				fixed OverlayBlendMode(fixed basePixel, fixed blendPixel)
				{
					if(basePixel < 0.5)
					{
						return (2.0 * basePixel * blendPixel);
					}
					else
					{
						return (1.0 - 2.0 * (1.0 - basePixel) * (1.0 - blendPixel));
					}
				}

				fixed4 frag(v2f_img i) : COLOR 
				{
					fixed3 constantWhite = fixed3(1,1,1);

					half2 scratchesUV = half2(i.uv.x + (_RandomValue * _SinTime.z *
					_ScratchesXSpeed), i.uv.y + (_Time.x * _ScratchesYSpeed));

					float fadeBlend = 1;

					//half2 dustUV = half2(i.uv.x + (_RandomValue * (_SinTime.z * 
					//_ScratchesXSpeed)), i.uv.y + (_RandomValue * (_SinTime.z * _ScratchesYSpeed)));

					//fixed4 scratchesTex = tex2D(_BlendTex, scratchesUV);

					fixed4 renderTex = tex2D(_MainTex, i.uv);
					fixed4 blendTex = tex2D(_BlendTex, i.uv);
					fixed4 perlinTex = tex2D(_perlinTex, scratchesUV);
					fixed4 perlinTex2 = tex2D(_perlinTex2, scratchesUV);
						
					fixed4 blendedImage = renderTex;
					fixed4 blendedImage2 = renderTex;
					fixed4 fadeImage;
					//fixed4 blendedImage3 = renderTex;

					fadeImage = lerp(perlinTex.a, perlinTex2.a, fadeBlend);

					blendedImage.r = OverlayBlendMode(blendTex.r, perlinTex.r);
					blendedImage.g = OverlayBlendMode(blendTex.g, perlinTex.g);
					blendedImage.b = OverlayBlendMode(blendTex.b, perlinTex.b);

					blendedImage = lerp(renderTex, blendedImage, _perlinOpacity);

					blendedImage2.r = OverlayBlendMode(renderTex.r, blendedImage.r);
					blendedImage2.g = OverlayBlendMode(renderTex.g, blendedImage.g);
					blendedImage2.b = OverlayBlendMode(renderTex.b, blendedImage.b);

					renderTex = lerp(renderTex, blendedImage2, _Opacity);
					
					renderTex.rgb *= lerp(blendTex, constantWhite, (1));

					return renderTex;
				}
				ENDCG
			}
		
	}
}