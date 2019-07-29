Shader "Hidden/BPR/Background Blit PMA"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ForegroundTex("FG Texture", 2D) = "clear" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			ZTest Off
			Lighting Off
			ZWrite Off
			Blend Off
			ColorMask RGB

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;

				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _ForegroundTex;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 finalSceneCol = tex2D(_MainTex, i.uv);
				fixed4 fgSceneCol = tex2D(_ForegroundTex, i.uv);
				fixed3 bgSceneCol = (finalSceneCol.rgb - fgSceneCol.rgb) / (1 - fgSceneCol.a);
				
				fixed4 col;
				col.rgb = bgSceneCol;
				col.a = 1;
				return col;
			}
			ENDCG
		}
	}
}
