Shader "Nick/Basic Surface Shader" {

	Properties{
		_MainTex("Texture", 2D) = "white"{ }
		_BumpTex("Bump Tex", 2D) = "bump"{ }
		_SpecTex("Specular Tex", 2D) = "black"{ }
		_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1.0)
		_SpecPower("Specular Power", Range(0, 1)) = 0.5
		//_EmitTex("Emissive Tex", 2D) = "black"{ }
		//_EmitPower("Emit Power", Range(0,2)) = 1.0 //glow
		//_AlphaTex("Alpha Tex", 2D) = "black"{ }
		//_Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5
	}
	
	SubShader{
		Tags {"RenderType" = "Opaque" "Queue" = "AlphaTest"}
		
		CGPROGRAM
		#pragma surface surf BlinnPhong// alphatest:_Cutoff
		//#pragma exclude_renderers flash
		
		sampler2D _MainTex;
		sampler2D _BumpTex;
		sampler2D _SpecTex;
		float _SpecPower;
		//sampler2D _EmitTex;
		//float _EmitPower;
		//sampler2D _AlphaTex;
								
		struct Input{
			float2 uv_MainTex; //(1.0, 1.0) U, V
			float2 uv_BumpTex;
			float2 uv_SpecTex;
			//float2 uv_EmitTex;
			//float2 uv_AlphaTex;
			//float4 color : COLOR; //(1.0, 1.0, 1.0, 1.0) R, G, B, A
		};
	
		void surf(Input IN, inout SurfaceOutput o){
			//o.Albedo = 1;
			fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 bumpTex = tex2D(_BumpTex, IN.uv_BumpTex);
			fixed4 specTex = tex2D(_SpecTex, IN.uv_SpecTex);
			//fixed4 emitTex = tex2D(_EmitTex, IN.uv_EmitTex);
			//fixed4 alphaTex = tex2D(_AlphaTex, IN.uv_AlphaTex);
									
			o.Albedo = mainTex.rgb;
			o.Normal = UnpackNormal(bumpTex);
			o.Specular = _SpecPower;
			o.Gloss = specTex.rgb;
			//o.Emission = emitTex.rgb * _EmitPower;
			//o.Alpha = alphaTex.a;
		}
		
		ENDCG
	}

	Fallback "Diffuse"
	
}