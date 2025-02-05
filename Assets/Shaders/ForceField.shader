Shader "Custom/ForceFieldShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _FresnelPower ("Fresnel Power", Range(0,5)) = 1.0
        _Distortion ("Distortion", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _FresnelPower;
        half _Distortion;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            // Fresnel effect
            half fresnel = pow(1.0 - dot(IN.viewDir, o.Normal), _FresnelPower);
            o.Emission = c.rgb * fresnel;

            // Distortion effect
            float2 distortedUV = IN.uv_MainTex + (o.Normal.xy * _Distortion);
            fixed4 distortedColor = tex2D(_MainTex, distortedUV) * _Color;
            o.Albedo = lerp(o.Albedo, distortedColor.rgb, _Distortion);

            o.Alpha = c.a * fresnel;
        }
        ENDCG
    }
    FallBack "Transparent"
}
