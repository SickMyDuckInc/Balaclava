﻿Shader "Custom/WallsShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
		_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess("Shininess", Range(0.03, 1)) = 0.078125
		_NormalStrength("NormalStrength", Range(0, 2)) = 0.078125
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf BlinnPhong fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _BumpMap;
		half _Shininess;
		half _NormalStrength;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
        };

        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 specTex = tex2D(_MainTex, IN.uv_MainTex);
			o.Gloss = specTex.r;
			o.Specular = _Shininess * specTex.g;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
			fixed3 Dmg = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

			Dmg.xy *= _NormalStrength;

			o.Normal = normalize(Dmg);

            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Specular"
}
