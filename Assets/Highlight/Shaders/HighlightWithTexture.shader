Shader "Custom/HighlightWithTexture"
{
    Properties 
    {
        _MainTex("MainTex", 2D) = "white" {}
        _AdditionalTex("Additional Texture",2D) = "white"{}
        _AdditionalColor("Additional Color", Color) = (1, 1, 1, 1)
        _HighlightConcentration("Highlight Concentration", Range(0.5,5.0)) = 0.9
    }
    SubShader 
    {
        Tags {"RenderType"="Opaque"}
        CGPROGRAM
        #pragma surface surf Lambert
        sampler2D _MainTex;
        sampler2D _AdditionalTex;
        float4 _AdditionalColor;
        float _HighlightConcentration;
        
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float3 viewDir;
        };
        void surf(Input In, inout SurfaceOutput o)
        {
           fixed4 c = tex2D (_MainTex, In.uv_MainTex);
           fixed4 additional = tex2D(_AdditionalTex, In.uv_MainTex) * _AdditionalColor;
           o.Albedo = c.rgb;
           o.Alpha = c.a;
           o.Emission = additional.rgb  * pow(1.0 -saturate(dot(normalize(In.viewDir),o.Normal)),1/_HighlightConcentration);
        }
        ENDCG        
    }
    Fallback "Diffuse"
}
