// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Nick/ReflectiveSimple" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Cube ("Cubemap", Cube) = "" { /* used to be TexGen CubeReflect */ }
    _ReflectionWeight ("Reflection Weight", float) = 0.5
}
SubShader { 
    Pass { 
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        
        struct v2f {
            float4 pos : SV_POSITION;
            float3 uv : TEXCOORD0;
        };

        v2f vert (float4 v : POSITION, float3 n : NORMAL)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v);

            // TexGen CubeReflect:
            // reflect view direction along the normal,
            // in view space
            float3 viewDir = normalize(ObjSpaceViewDir(v));
            o.uv = reflect(-viewDir, n);
            o.uv = mul(UNITY_MATRIX_MV, float4(o.uv,0));
            return o;
        }

        samplerCUBE _Cube;
        sampler2D _MainTex;
        float _ReflectionWeight;

        half4 frag (v2f i) : SV_Target
        {
            return tex2D(_MainTex,i.uv) + (_ReflectionWeight * texCUBE(_Cube, i.uv));
        }
        ENDCG 
    } 
}
}