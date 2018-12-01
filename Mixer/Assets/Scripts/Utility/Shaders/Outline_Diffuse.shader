Shader "Outline_Diffuse"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 0, 0, 1)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		Cull Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM

			#pragma vertex vertexFunc
			#pragma fragment fragmentFunc
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			fixed4 _Color;
				
			struct vertexToFragment
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
			};

			vertexToFragment vertexFunc(appdata_base v)
			{
				vertexToFragment o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				
				return o;
			}

			

			fixed4 fragmentFunc(vertexToFragment i) : COLOR
			{
				half4 baseColor = tex2D(_MainTex, i.uv);	// get this pixel's color				
				half4 outlineColor = _Color;				// get the outline color

				// get alphas for surrounding pixels
				fixed upAlpha = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
				fixed downAlpha = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
				fixed rightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
				fixed leftAlpha = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0)).a;

				// if this pixel is transparent, but has ANY neighboring A > 0 pixels, render the outline
				if (((upAlpha + downAlpha + rightAlpha + leftAlpha) > 0) && (baseColor.a == 0))
				{
					outlineColor.rgb *= outlineColor.a;
					return outlineColor;
				}
					
				// otherwise, render normally
				else
				{
					baseColor.rgb *= baseColor.a;
					return baseColor;
				}
			}

			ENDCG
		}
	}
}
