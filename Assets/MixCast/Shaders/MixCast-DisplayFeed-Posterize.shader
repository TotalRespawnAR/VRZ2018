/*======= (c) Blueprint Reality Inc., 2017. All rights reserved =======*/
Shader "MixCast/Display Feed (Posterize)" {

	Properties{
		_Color("Color", Color) = (0.5, 0.5, 0.5, 1)
		_MainTex("Base(RGB)", 2D) = "white"{}

		_PosterizeGamma("Posterize Gamma", Float) = 0.6
		_PosterizeLevels("Posterize Levels", Float) = 8
	}

		SubShader{
		Tags{ "Queue" = "Transparent-10" "IgnoreProjector" = "True" "RenderType" = "Opaque" }

		ColorMask RGB
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass{
		CGPROGRAM

#pragma target 3.0
#pragma exclude_renderers d3d9

#pragma multi_compile __ PXL_FLIP_X
#pragma multi_compile __ PXL_FLIP_Y
#pragma multi_compile LIGHTING_OFF LIGHTING_FLAT

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#include "./Includes/MixCastLighting.cginc"

	fixed4 _Color;
	sampler2D _MainTex;
	float4 _MainTex_Transform;

	float _PosterizeGamma;
	float _PosterizeLevels;

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

		o.uv = v.texcoord;
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

		float4 col = tex2D(_MainTex, uvs);
		col.rgb = ApplyLighting(col.rgb, i.worldPos, i.worldNormal);

		//Posterize result
		col.rgb = pow(col.rgb, _PosterizeGamma);
		col.rgb *= _PosterizeLevels;
		col.rgb = floor(col.rgb);
		col.rgb /= _PosterizeLevels;
		col.rgb = pow(col.rgb, 1.0 / _PosterizeGamma);

#ifndef UNITY_COLORSPACE_GAMMA
		_Color.rgb = pow(_Color.rgb, 0.4545454545454545);
#endif
		o.col = float4(2.0 * _Color.rgb * col.rgb, _Color.a * col.a);

		clip(o.col.a - 0.01);

		return o;
	}


	ENDCG
	}
	}
}