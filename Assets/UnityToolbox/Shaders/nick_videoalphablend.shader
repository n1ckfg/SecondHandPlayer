Shader "Nick/VideoAlphaBlend" {
   
   Properties {
		_MainTex ("Base (RGB | A)", 2D) = "white" {}
		_AlphaOffsetX ("Alpha offset X", float) = 0.5
		_AlphaOffsetY ("Alpha offset Y", float) = 0
   }

   SubShader {
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf Lambert
   
		sampler2D _MainTex;
		float _AlphaOffsetX;
		float _AlphaOffsetY;
   
		struct Input {
			float2 uv_MainTex;
		};
   
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			float2 alphaTexUV = IN.uv_MainTex.xy + float2(_AlphaOffsetX, _AlphaOffsetY);
			half4 d = tex2D (_MainTex, alphaTexUV);
			//o.Albedo = c.rgb;
			o.Emission = c.rgb;
			o.Alpha = d.r;
		}
		ENDCG
   }

   FallBack "Diffuse"

}
