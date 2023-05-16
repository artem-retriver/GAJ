 Shader "Custom/CheatShader" {
     Properties
     {
     _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0,.1)) = 0.03
         _Color ("MultiplyColor", Color) = (1,1,1,1)
         _AdditiveColor("AdditiveColor",Color) = (1,1,1,1)
         _MainTex1("MainTex",2D) = "white"{}         
         _MainTex ("NewTex", 2D) = "white" {}
     }
      
     Category
     {
         SubShader
         {
         Blend SrcAlpha OneMinusSrcAlpha
                 ZWrite Off
                 ZTest Greater
                Tags { "Queue"="Overlay+1"
                 "RenderType"="Transparent"
                 "LightMode"="ForwardBase"}
             Pass{
            Cull front

            CGPROGRAM

            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            //color of the outline
            fixed4 _OutlineColor;
            //thickness of the outline
            float _OutlineThickness;

            //the object data that's available to the vertex shader
            struct appdata{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f{
                float4 position : SV_POSITION;
            };

            //the vertex shader
            v2f vert(appdata v){
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                float3 normal = mul((float3x3) UNITY_MATRIX_MV, v.normal);
                normal.x *= UNITY_MATRIX_P[0][0];
                normal.y *= UNITY_MATRIX_P[1][1];
                o.position.xy += normal.xy * _OutlineThickness;
                return o;
            }

            //the fragment shader
            fixed4 frag(v2f i) : SV_TARGET{
                return _OutlineColor;
            }

            ENDCG
        }
             Pass
             {
                 Blend SrcAlpha OneMinusSrcAlpha
                 ZWrite Off
                 ZTest Greater
                 CGPROGRAM
                  
                 #pragma vertex vert            
                 #pragma fragment frag
                 #include "UnityCG.cginc"
                 #include "UnityLightingCommon.cginc"
                 
           
                 struct v2f
                 {
                   float2 uv : TEXCOORD0; 
                   float4 vertex : SV_POSITION; 
                 };
                  v2f vert (appdata_base v)
                   {
                     v2f o;
                     o.vertex = UnityObjectToClipPos(v.vertex);
                     o.uv = v.texcoord;
                     return o;
                   }
            
                    sampler2D _MainTex;
                    fixed4 _Color;
                    fixed4 _AdditiveColor;
                    half4 frag (v2f i) : SV_Target
                   {
                       fixed4 col = tex2D(_MainTex, i.uv);
                       return (col *= _Color)+_AdditiveColor; 
                   }
                 ENDCG
                 SetTexture [_MainTex] {combine texture}          
            
             }
      
             Pass
             {
                Blend SrcAlpha OneMinusSrcAlpha
                ZTest Less
                SetTexture [_MainTex1] {combine texture}
             }  
         }
     }
      
     FallBack "Diffuse"
}