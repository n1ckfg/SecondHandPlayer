Shader "Nick/VideoAlpha" {

	//http://forum.unity3d.com/threads/chroma-key-shader.48938/
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaOffsetX ("Alpha Offset X", float) = 0.5
		_AlphaOffsetY ("Alpha Offset Y", float) = 0
		_Cutoff ("Cutoff", Range (0,1)) = .5
	}
   
	SubShader {
		AlphaTest Less [_Cutoff]
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
			half4 d = tex2D(_MainTex, alphaTexUV);

			o.Albedo = c.rgb;
			o.Alpha = (d.r*-1)+1;
		}
		ENDCG
	}

	FallBack "Diffuse"

}
