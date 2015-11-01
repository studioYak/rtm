Shader "Custom/testShaderBV" {
    Properties {
    _Color ("Main Color", Color) = (1,0,0,0.5)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _BumpMap ("Normal", 2D) = "white" {}
}
SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="False" "RenderType"="Transparent"}
    LOD 400
    
//    CGPROGRAM
//      #pragma surface surf Lambert
//      struct Input {
//        float2 uv_MainTex;
//        float2 uv_BumpMap;
//      };
//      sampler2D _MainTex;
//      sampler2D _BumpMap;
//      void surf (Input IN, inout SurfaceOutput o) {
//        //o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
//        o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
//      }
//      ENDCG

    // extra pass that renders to depth buffer only
    Pass {
        ZWrite On
        ColorMask 0
    }

    // paste in forward rendering passes from Transparent/Diffuse
    UsePass "Transparent/Diffuse/FORWARD"
}
Fallback "VertexLit"
 }
