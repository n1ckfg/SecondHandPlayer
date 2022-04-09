// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Nick/Grabby" {
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}
        
        Pass {        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };
            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
            half4 frag(v2f i) : COLOR {
            	//cyan color
                return float4(0,1,1,1);
            }
            ENDCG
        }
        
        GrabPass {"_GrabTexture"}
        
        Pass {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            
            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
            
            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };
            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 uvProj : TEXCOORD1;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.uvProj = o.pos;
                return o;
            }
            half4 frag(v2f i) : COLOR {
            	i.uvProj /= i.uvProj.w;
            	i.uvProj = (i.uvProj + 1.0) * 0.5;
            	#if UNITY_UV_STARTS_AT_TOP
				if (_GrabTexture_TexelSize.y < 0)
				i.uvProj.y = 1 - i.uvProj.y;
				#endif
				
				//subtract blue, should see green, it worked!
            	float4 t = tex2D( _GrabTexture,  i.uvProj.xy)-float4(0,0,1,0);
                return t;
            }
            ENDCG
        }
    } 
}