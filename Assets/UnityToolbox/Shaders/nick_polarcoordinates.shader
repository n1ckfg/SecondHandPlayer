// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Nick/PolarCoordinates" {

	Properties {
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader {
		Tags { "RenderType"="Opaque" }

		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;

	        struct v2f {
	            float4 pos : SV_POSITION;
	            float2 uv : TEXCOORD0;
	        };

			//Vertex Shader
	        v2f vert (float4 v : POSITION) {
	            v2f o;
	            o.pos = UnityObjectToClipPos(v);

	            // TexGen EyeLinear:
	            // use view space vertex position
	            o.uv = mul(UNITY_MATRIX_MV, v).xyz;
	            return o;
	        }

			//Fragment Shader
			fixed4 frag(v2f i) : COLOR {
				float3 n = normalize(i.normal);
				float PI = 3.14159265;
				float PI2 = PI * 2.0;
				float2 uvs = half2(0.5 + atan2(n.z, n.x) / pi2, acos(n.y) / PI);
				uvs = TRANSFORM_TEX(uvs, _MainTex);
				uvs.x = 1.0 - uvs.x;
				uvs.y = 1.0 - uvs.y;
				fixed4 col = tex2D(_MainTex, uvs);
				return col;
			}
			ENDCG
		}
	}
	
	FallBack "Diffuse"

}