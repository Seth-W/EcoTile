Shader "ObjectEffects/SelectedTileEffectTop" 
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
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Pass
		{
			Blend One One
			Cull Off
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
				float4 color : TEXCOORD1;
				float xz : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _EdgeColor;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.uv = v.uv *_MainTex_ST.xy + _MainTex_ST.zw;
				o.xz = v.vertex.x * v.vertex.z;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float yValue = v.vertex.y * v.vertex.y;
				o.color = float4(v.vertex.yyy, 1);
				return o;
			}



			float4 frag(v2f i) : SV_Target
			{
				i.uv += _Time;
				float4 texColor = tex2D(_MainTex, i.uv);

				return -i.color * _EdgeColor;
			}
			ENDCG
		}
	}
}
