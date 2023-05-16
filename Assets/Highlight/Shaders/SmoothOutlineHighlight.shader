Shader "Custom/SmoothOutlineHighlight"
{
    Properties 
    {
        _MainTex("_MainTex", 2D) = "white" {}
        _HighlightColor("Highlight Color", Color) = (1.0,1.0,1.0,0.0)
        _HighlightStrength("HighlightStrength", Range(1.0,4.0))=1.45
    }
    SubShader 
    {
        Tags {"RenderType"="Opaque"}
        CGPROGRAM
        #pragma surface surf Lambert
        sampler2D _MainTex;
        float4 _HighlightColor;
        float _HighlightStrength;
        
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float3 viewDir;
        };
        void surf(Input In, inout SurfaceOutput o)
        {
           fixed4 c = tex2D (_MainTex, In.uv_MainTex);
           float lc = saturate(dot(normalize(In.viewDir),o.Normal));
           half rim = 1.0 - lc;
           o.Albedo = c.rgb;
           o.Alpha = c.a;
           o.Emission = _HighlightStrength * (_HighlightColor.rgb*smoothstep(0.2,0.6,rim));
        }
        ENDCG        
    }
    Fallback "Diffuse"
}