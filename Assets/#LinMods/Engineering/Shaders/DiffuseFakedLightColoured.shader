Shader "Custom/DiffuseFakedLightColoured" 
{
    Properties
    {
        _MainTex        ("Card Front(RGB)", 2D)     = "white" {}
		_Color  	    ("Main Color", Color) 	    = (1,1,1,1)   
        _LightDirection ("Light Direction", Vector) = (0, 0, 1, 0)
    }
    
    SubShader
    {
        
        Tags { "Queue"="Geometry" "RenderType"="Opaque"}
            
        LOD 200             
        AlphaTest Off
        Lighting Off
        Blend Off
        Cull Back
        
        pass
        {            
CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4    _LightDirection;
            fixed4    _Color;
            
            struct appdata
            {
                fixed3 vertex    : POSITION;
                fixed2 texcoord  : TEXCOORD0;
                fixed3 colour    : COLOR;
                fixed3 normal    : NORMAL;
            };


            struct v2f
            {
                fixed4 pos     : SV_POSITION;
                fixed2 base_uv : TEXCOORD0;
                fixed3 colour  : COLOR;
            };

            fixed4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos     = UnityObjectToClipPos(fixed4(v.vertex, 1.0));
                o.base_uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                fixed brightness = clamp(dot(mul((float3x3)unity_ObjectToWorld, v.normal), _LightDirection), 0.4, 1.5);
                o.colour  = v.colour*brightness*_Color;
                
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                return fixed4(tex2D(_MainTex , i.base_uv).rgb*i.colour.rgb, 1.0);
            }
ENDCG
        }        
    }
}