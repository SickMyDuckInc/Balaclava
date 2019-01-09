﻿Shader "Custom/GlassShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Transparency("Transparency", Range(0, 1)) = 0.5
		_Emisive("Emision Value", Range(0,10)) = 0.5
	}
		SubShader
	{
		Tags {
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent" }
		LOD 200

		Cull Off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input
		{
		float2 uv_MainTex;
		};

		fixed4 _Color;
		half _Transparency;
		half _Emisive;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;
			o.Albedo = c.rgb;
			o.Emission = c.rgb * _Emisive;
			o.Alpha = _Transparency;
		}
		ENDCG
	}
		FallBack "Diffuse"
}