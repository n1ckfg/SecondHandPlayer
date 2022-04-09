Shader "Nick/Composite/Lighten" {
 
Properties {
    _MainTex ("Texture", 2D) = ""
}
 
SubShader {
    Tags {Queue = Transparent}
    Blend OneMinusDstColor One
    ZWrite Off
	Fog {Mode Off}    
    
    Pass {
        SetTexture[_MainTex]
    } 
}
 
}