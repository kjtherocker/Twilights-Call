Shader "Custom/Dissolve" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SecTex ("Albedo (RGB)", 2D) = "white" {}
        _SecColor ("Color", Color) = (1,1,1,1)
        _SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
        _SliceAmount("Slice Amount", Range(0.0, 1.0)) = 0
 
        _BurnSize("Burn Size", Range(0.0, 1.0)) = 0.15
        _BurnRamp("Burn Ramp (RGB)", 2D) = "white" {}
        _BurnColor("Burn Color", Color) = (1,1,1,1)
 
        _EmissionAmount("Emission amount", float) = 2.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off
        CGPROGRAM
        #pragma surface surf Lambert addshadow
        #pragma target 3.0
 
        fixed4 _Color;
        fixed4 _SecColor;
        sampler2D _MainTex;
        sampler2D _SecTex;
        sampler2D _SliceGuide;
        sampler2D _BumpMap;
        sampler2D _BurnRamp;
        fixed4 _BurnColor;
        float _BurnSize;
        float _SliceAmount;
        float _EmissionAmount;
 
        struct Input {
            float2 uv_MainTex;
            float2 uv_SecTex;
        };
 
 
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 c2 = tex2D (_SecTex, IN.uv_SecTex) * _SecColor;
            half test = tex2D(_SliceGuide, IN.uv_MainTex).rgb - _SliceAmount;

            
            
            
            fixed4 Result =  !test;
           
           if(test < 0)
           {
             o.Albedo = c2.rgb;
           }
           else
           {
             o.Albedo = c.rgb;
           }
           
             
            if (test < _BurnSize && _SliceAmount > 0) 
            {
            
            
                o.Emission = tex2D(_BurnRamp, float2(test * (1 / _BurnSize), 0)) * _BurnColor * _EmissionAmount * c2;
            }

            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}