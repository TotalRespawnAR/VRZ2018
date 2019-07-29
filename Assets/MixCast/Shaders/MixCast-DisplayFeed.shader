/*======= (c) Blueprint Reality Inc., 2017. All rights reserved =======*/
Shader "MixCast/Display Feed" {
	Properties{
		_MainTex("Base(RGB)", 2D) = "white"{}
	}

		SubShader{
			Tags{ "Queue" = "Transparent-10" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

			Cull Off
			ColorMask RGB
			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha

			Pass{
			CGPROGRAM

	#pragma target 3.0
	#pragma exclude_renderers d3d9

	#pragma multi_compile __ SKIP_COLORSPACE_CONVERT
	#pragma multi_compile __ UNITY_COLORSPACE_GAMMA
	#pragma multi_compile LIGHTING_OFF LIGHTING_FLAT
	#pragma multi_compile AA_OFF AA_ON

	#pragma vertex vert
	#pragma fragment frag

	#include "UnityCG.cginc"
	#include "./Includes/MixCastLighting.cginc"

		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _MainTex_TexelSize;
		float4 _MainTex_Transform;

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 pos : SV_POSITION;
			float3 worldPos : TEXCOORD1;
			float3 worldNormal : TEXCOORD2;
		};

		v2f vert(appdata_full v)
		{
			v2f o;

			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.pos = UnityObjectToClipPos(v.vertex);

			o.worldPos = mul(unity_ObjectToWorld, v.vertex);
			o.worldNormal = -UNITY_MATRIX_IT_MV[2].xyz;

			return o;
		}

		struct frag_output {
			float4 col:COLOR;
		};

		frag_output frag(v2f i) {
			frag_output o;

			float2 uvs = i.uv;
			uvs = uvs * _MainTex_Transform.xy + _MainTex_Transform.zw;

			clip(0.5 - abs(uvs.x - 0.5));	//instead of clamping the texture, just cut it off at the edges

			o.col = tex2D(_MainTex, uvs);

	#ifdef AA_ON
			o.col.a = 0.5 * o.col.a +
				(tex2D(_MainTex, uvs + float2(_MainTex_TexelSize.x, _MainTex_TexelSize.y)).a +
				tex2D(_MainTex, uvs + float2(-_MainTex_TexelSize.x, _MainTex_TexelSize.y)).a +
				tex2D(_MainTex, uvs + float2(_MainTex_TexelSize.x, -_MainTex_TexelSize.y)).a +
				tex2D(_MainTex, uvs + float2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y)).a) * 0.5 / 4;
	#endif

			o.col.rgb = ApplyLighting(o.col.rgb, i.worldPos, i.worldNormal);

	#ifndef UNITY_COLORSPACE_GAMMA
	#ifndef SKIP_COLORSPACE_CONVERT
			o.col.rgb = GammaToLinearSpace(o.col.rgb);
	#endif
	#endif

			clip(o.col.a - 0.01);

			return o;
		}


		ENDCG
		}
	}
}