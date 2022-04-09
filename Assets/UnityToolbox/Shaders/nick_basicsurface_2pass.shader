Shader "Nick/Basic Surface Shader 2pass"{

	Properties{
		_MainTex("Texture", 2D) = "white"{ }
		_BumpTex("Bump Tex", 2D) = "bump"{ }

		_SpecTex1("Specular Tex 1", 2D) = "black"{ }
		_SpecPower1("Specular Power 1", Float) = 0.5
		_SpecFalloff1("Specular Falloff 1", Float) = 1.0

		_SpecTex2("Specular Tex 2", 2D) = "black"{ }
		_SpecPower2("Specular Power 2", Float) = 0.5
		_SpecFalloff2("Specular Falloff 2", Float) = 1.0
		
		_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1.0)
		
		//_EmitTex("Emissive Tex", 2D) = "black"{ }
		//_EmitPower("Emit Power", Range(0,2)) = 1.0 //glow
		//_AlphaTex("Alpha Tex", 2D) = "black"{ }
		//_Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5
	}

	SubShader{
		Tags {"RenderType" = "Opaque" "Queue" = "AlphaTest"}

		Cull Off 
		
		// PASS 1-2
		CGPROGRAM
		#pragma surface surf BlinnPhong //alphatest:_Cutoff
		//#pragma exclude_renderers flash
		
		sampler2D _MainTex;
		
		sampler2D _BumpTex;
		
		sampler2D _SpecTex1;
		float _SpecFalloff1;
		float _SpecPower1;
		
		//sampler2D _EmitTex;
		//float _EmitPower;
		//sampler2D _AlphaTex;
								
		struct Input{
			float2 uv_MainTex; //(1.0, 1.0) U, V
			float2 uv_BumpTex;
			float2 uv_SpecTex1;
			//float2 uv_EmitTex;
			//float2 uv_AlphaTex;
		//float4 color : COLOR; //(1.0, 1.0, 1.0, 1.0) R, G, B, A
		};
	
		void surf(Input IN, inout SurfaceOutput o){
			//o.Albedo = 1;
			fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 bumpTex = tex2D(_BumpTex, IN.uv_BumpTex);
			fixed4 specTex1 = tex2D(_SpecTex1, IN.uv_SpecTex1);
			//fixed4 emitTex = tex2D(_EmitTex, IN.uv_EmitTex);
			//fixed4 alphaTex = tex2D(_AlphaTex, IN.uv_AlphaTex);
									
			o.Albedo = mainTex.rgb;
			o.Normal = UnpackNormal(bumpTex);
			_SpecColor += specTex1 * _SpecFalloff1;
			o.Specular = _SpecPower1;
			o.Gloss = specTex1.rgb;
			//o.Emission = emitTex.rgb * _EmitPower;
			//o.Alpha = alphaTex.a;
		}
		
		ENDCG
		
		// PASS 2-2
		Blend One One //SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf BlinnPhong
		
		sampler2D _SpecTex2;
		
		float _SpecFalloff2;
		
		float _SpecPower2;
								
		struct Input{
			float2 uv_SpecTex2;
		};
	
		void surf(Input IN, inout SurfaceOutput o){
			fixed4 specTex2 = tex2D(_SpecTex2, IN.uv_SpecTex2);

			_SpecColor += specTex2 * _SpecFalloff2;
					
			o.Specular = _SpecPower2;
			o.Gloss = specTex2.rgb;
		}
		
		ENDCG
	}

	Fallback "Diffuse"
	
}