// http://forum.unity3d.com/threads/invisible-depth-mask.122873/

Shader "Nick/DepthMask" {

    SubShader {
        Tags { "Queue" = "Geometry-1" }
        Lighting Off
        Pass {
            ZWrite On
            ZTest LEqual
            ColorMask 0    
        }
    }

}