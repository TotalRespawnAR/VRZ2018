// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Hologram-NoDiffuse" 
{
    Properties
    {
        _ScanLineTex    ("Scanline Tex", 2D)        = "white" {}
        _RayScanTex     ("Ray Scan Tex", 2D)        = "white" {}
        _HologramHalf   ("Ray Scan Tex", 2D)        = "white" {}
        _Color          ("Base Colour", Color)      = (0, 0, 1, 0.5)
        _Translucency   ("Translucency", float)     = 0.5
        _FloorY         ("FloorY", float)           = 0.0
    }
    
    SubShader
    {
        
        Tags { "Queue"="Transparent" "RenderType"="Transparent"}
            
        LOD 200             
        AlphaTest Off
        Lighting Off
        Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		ZTest LEqual
        
        
CGPROGRAM
      #pragma surface surf Lambert alpha:fade
      struct Input
      {
          fixed4 screenPos;
          fixed3 worldPos;
      };
      sampler2D _ScanLineTex;
      sampler2D _RayScanTex;
      sampler2D _HologramHalf;
      fixed4    _Color;
      fixed     _Translucency;
      fixed     _FloorY;
      
      void surf (Input IN, inout SurfaceOutput o) 
      {
          const fixed _WorldMinHeight = 1.0f/2.75;
          fixed Visibility = tex2D(_HologramHalf, fixed2(0, (IN.worldPos.y-_FloorY) * _WorldMinHeight)).r;
          clip(Visibility-0.01);
          
          fixed2 screenUV = IN.screenPos.xy / IN.screenPos.w;
          screenUV.y += _Time[0]*0.1;
          fixed2 rayUV    = fixed2(0, (screenUV.y*2) + _Time[1]*0.5);
          screenUV.y     *= 30;
          
          o.Albedo = ((tex2D(_ScanLineTex, screenUV).r*2)*_Color.rgb + tex2D(_RayScanTex, rayUV).r)*Visibility*_Translucency;
		  o.Alpha = _Color.a;
      }
      ENDCG
    }
}