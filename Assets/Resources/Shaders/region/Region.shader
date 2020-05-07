// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ZoneShader" {
    Properties {
        _MainTex ("Base (RGBA)", 2D) = "white" {}
        _ResColor ("Res Color", Color) = (0,1,0,0.5)
        _ComColor ("Com Color", Color) = (0,0,1,0.5)
        _AgrColor ("Agr Color", Color) = (1,0,0,0.5)
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass {
            CGPROGRAM
               #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
           
            #include "UnityCG.cginc"
 
            float4 _ResColor;
            float4 _ComColor;
            float4 _AgrColor;
            sampler2D _MainTex;
 
            struct v2f {
                float4 pos : SV_POSITION;
                float4 scrPos : TEXCOORD2;
                float2 uv : TEXCOORD0;
            };
 
            float4 _MainTex_ST;
 
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.scrPos = ComputeScreenPos(o.pos);
                o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
                return o;
            }
 
            half4 frag (v2f i) : COLOR
            {
                float2 xypos = floor((i.scrPos.xy/i.scrPos.w) * _ScreenParams.xy);
                half4 texcol = tex2D (_MainTex, i.uv);
 
                // TODO: rewrite the following to avoid all those branches!
                half4 result;
                if (texcol[0] > texcol[1]) {
                    // 1 (green) is out of the running...
                    if (texcol[0] > texcol[2]) {
                        // red wins
                        result = texcol[0] > 0.1 ? _AgrColor : half4(0,0,0,0);
                    } else {
                        // blue wins
                        result = texcol[2] > 0.1 ? _ComColor : half4(0,0,0,0);
                    }
                } else {
                    // 0 (red) is out of the running...
                    if (texcol[1] > texcol[2]) {
                        // green wins
                        result = texcol[1] > 0.1 ? _ResColor : half4(0,0,0,0);
                    } else {
                        // blue wins
                        result = texcol[2] > 0.1 ? _ComColor : half4(0,0,0,0);
                }
            }
           
            return result;
            }
            ENDCG
        }
    }
    FallBack "Unlit"
}
 
