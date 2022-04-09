Shader "Nick/HalfLambert" {
	
	Properties {
        _Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf HalfLambert
		// ~ ~ ~ ~ ~ 
		fixed4 LightingHalfLambert(SurfaceOutput s, fixed3 lightDir, fixed atten) {
			fixed n = dot(s.Normal, lightDir);
			n *= 0.5;
			n += 0.5;
			n *= n;
			
			fixed diff = max(0, n);
			
			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten * 2);
			c.a = s.Alpha;
			return c;
		}
		// ~ ~ ~ ~ ~ 

        half4 _Color;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	 
	FallBack "Diffuse"

}
