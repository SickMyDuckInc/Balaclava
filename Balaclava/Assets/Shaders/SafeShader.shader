Shader "Custom/SafeShader"
	{
		Properties
		{
			_Color("Color", Color) = (1,1,1,1)
			_MainTex("Albedo (RGB)", 2D) = "white" {}
			_RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
			_RimPower("Rim Power", Range(0.5,15.0)) = 3.0
			_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
			_Shininess("Shininess", Range(0.03, 1)) = 0.078125
		}
			SubShader
			{
				Tags { "RenderType" = "Opaque" }
				LOD 200

				CGPROGRAM
				// Physically based Standard lighting model, and enable shadows on all light types
				#pragma surface surf BlinnPhong fullforwardshadows

				// Use shader model 3.0 target, to get nicer looking lighting
				#pragma target 3.0

				sampler2D _MainTex;
				sampler2D _BumpMap;
				half _Shininess;
				float4 _RimColor;
				float _RimPower;

				struct Input
				{
					float2 uv_MainTex;
					float3 viewDir;
				};

				fixed4 _Color;

				// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
				// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
				// #pragma instancing_options assumeuniformscaling
				UNITY_INSTANCING_BUFFER_START(Props)
					// put more per-instance properties here
				UNITY_INSTANCING_BUFFER_END(Props)

				void surf(Input IN, inout SurfaceOutput o)
				{
					// Albedo comes from a texture tinted by color
					fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					fixed4 specTex = tex2D(_MainTex, IN.uv_MainTex);
					o.Gloss = specTex.r;
					o.Specular = _Shininess * specTex.g;
					o.Albedo = c.rgb;
					half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
					o.Emission = _RimColor.rgb * pow(rim, _RimPower);
					o.Alpha = c.a;
					
				}
				ENDCG
			}
				FallBack "Specular"
	}
