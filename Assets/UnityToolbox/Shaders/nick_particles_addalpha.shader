// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Nick/Particles/AddAlpha"{

	Properties{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_AddTex ("Additive Texture", 2D) = "black" {}
	}

	// ---- Fragment program cards
	SubShader{
		Tags{ "Queue"="Transparent +100" "IgnoreProjector"="True" "RenderType"="Transparent" }
		
		AlphaTest Greater .01
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		
		BindChannels{
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}
	
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma fragmentoption ARB_fog_exp2

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _TintColor;
			
			struct appdata_t{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v){
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			half4 frag (v2f i) : COLOR{
				return 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord);
			}
			ENDCG 
		}
		
	    Pass {
    		Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma fragmentoption ARB_fog_exp2

			#include "UnityCG.cginc"

			sampler2D _AddTex;
			
			struct appdata_t{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			float4 _AddTex_ST;

			v2f vert (appdata_t v){
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_AddTex);
				return o;
			}

			half4 frag (v2f i) : COLOR{
				return 2.0f * i.color * tex2D(_AddTex, i.texcoord);
			}
			ENDCG 
	    } 		
 	}
 }
