Shader "ObjectEffects/SelectedTileEffectBottom" 
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_EdgeColor("Edge Color", Color) = (0,0,0,0)
	}
	SubShader 
	{
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
		}

		Pass
		{
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _EdgeColor;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.uv = v.uv *_MainTex_ST.xy + _MainTex_ST.zw;
				// *_MainTex_ST.xy + _MainTex_ST.zw;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}



			float4 frag(v2f i) : COLOR
			{
				float4 texColor = tex2D(_MainTex, i.uv);

				return texColor;
			}
			ENDCG
		}
	}
}
