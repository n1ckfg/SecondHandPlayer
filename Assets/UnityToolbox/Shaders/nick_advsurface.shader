//TUTORIAL 6. Alpha map v2
Shader "Nick/Advanced Surface Shader"{ //Shader name goes here

	Properties{
		_MainTex("Texture", 2D) = "white"{ }
		_BumpMap1("Bump Map 1", 2D) = "bump"{ }
		_BumpMap2("Bump Map 2", 2D) = "bump"{ }
		_BumpBlend("Bump Blend", Float) = 0.5
		_SpecMap1("Specular Map 1", 2D) = "black"{ }
		_SpecPower1("Specular Power 1", Float) = 0.5
		_SpecAdjust1("Specular Adjust 1", Float) = 0.5
		_SpecMap2("Specular Map 2", 2D) = "black"{ }
		_SpecPower2("Specular Power 2", Float) = 0.5
		_SpecAdjust2("Specular Adjust 2", Float) = 0.5
		_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1.0)
		//_EmitMap("Emissive Map", 2D) = "black"{ }
		//_EmitPower("Emit Power", Range(0,2)) = 1.0 //glow
		//_AlphaMap("Alpha Map", 2D) = "black"{ }
		//_Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5
	}
	SubShader{
		Tags {"RenderType" = "Opaque" "Queue" = "AlphaTest"}
		
		// Pass 1
		CGPROGRAM
		#pragma surface surf BlinnPhong alphatest:_Cutoff
		
		sampler2D _MainTex;
		
		sampler2D _BumpMap1;
		sampler2D _BumpMap2;
		float _BumpBlend;
		
		sampler2D _SpecMap1;
		float _SpecPower1;
		float _SpecAdjust1;
		
		//sampler2D _EmitMap;
		//float _EmitPower;
		//sampler2D _AlphaMap;
								
		struct Input{
			float2 uv_MainTex; //(1.0, 1.0) U, V
			float2 uv_BumpMap1;
			float2 uv_BumpMap2;
			float2 uv_SpecMap1;
			//float2 uv_EmitMap;
			//float2 uv_AlphaMap;
		//float4 color : COLOR; //(1.0, 1.0, 1.0, 1.0) R, G, B, A
		};
	
		void surf(Input IN, inout SurfaceOutput o){
			//o.Albedo = 1;
			fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 bumpTex1 = tex2D(_BumpMap1, IN.uv_BumpMap1);
			fixed4 bumpTex2 = tex2D(_BumpMap2, IN.uv_BumpMap2);
			fixed4 specTex1 = tex2D(_SpecMap1, IN.uv_SpecMap1);
			//fixed4 emitTex = tex2D(_EmitMap, IN.uv_EmitMap);
			//fixed4 alphaTex = tex2D(_AlphaMap, IN.uv_AlphaMap);
								
			o.Albedo = mainTex.rgb;
			o.Normal = normalize(UnpackNormal(bumpTex1) + UnpackNormal(bumpTex2) * _BumpBlend);
			_SpecColor *= specTex1 * _SpecAdjust1;			
			o.Specular = _SpecPower1;
			o.Gloss = specTex1.rgb; 
			//o.Emission = emitTex.rgb * _EmitPower;
			//o.Alpha = alphaTex.a;
		}
		
		ENDCG
		
		Blend One One
		// Pass 2
		CGPROGRAM
		#pragma surface surf BlinnPhong alphatest:_Cutoff
		
		sampler2D _SpecMap2;
		float _SpecPower2;
		float _SpecAdjust2;
								
		struct Input{
			float2 uv_SpecMap2;
		};
	
		void surf(Input IN, inout SurfaceOutput o){
			fixed4 specTex2 = tex2D(_SpecMap2, IN.uv_SpecMap2);
			_SpecColor *= specTex2 * _SpecAdjust2;			
			o.Specular = _SpecPower2;
			o.Gloss = specTex2.rgb; 
		}
		
		ENDCG


	}
	
	Fallback "Diffuse"
	
}