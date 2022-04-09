Shader "Nick/Composite/AddMult" {
 
Properties {
    _MainTex1 ("Mult Texture", 2D) = ""
    _MainTex2 ("Add Texture", 2D) = ""
}
 
SubShader {
    Tags {Queue = Transparent}
    ZWrite Off
	Fog {Mode Off}
   
    Pass {
    	Blend Zero SrcColor
        SetTexture[_MainTex1]
    } 
    
    Pass {
    	Blend One One
        SetTexture[_MainTex2]
    } 

}
 
}