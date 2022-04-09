Shader "Nick/Composite/Darken" {
 
Properties {
    _MainTex ("Texture", 2D) = ""
}
 
SubShader {
    Tags {Queue = Transparent}
    Blend DstColor Zero
    ZWrite Off
	Fog {Mode Off}    
    
    Pass {
        SetTexture[_MainTex]
    } 
}
 
}