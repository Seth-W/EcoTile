Shader "ObjectEffects/Outline" 
{
	Properties
	{
		_EdgeColor("Edge Color", Color) = (0,0,0,0)
	}
	SubShader 
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		ZTest Always
		Blend One One

		Pass
		{


			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.uv = v.uv;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(_Object2World, v.vertex).xyz);
				return o;
			}

			float4 _EdgeColor;

			float4 frag(v2f i) : SV_Target
			{
				float ndotv = 1 - dot(i.normal, i.viewDir) * 1.5;
				return _EdgeColor * ndotv;
			}
			ENDCG
		}
	}
}
