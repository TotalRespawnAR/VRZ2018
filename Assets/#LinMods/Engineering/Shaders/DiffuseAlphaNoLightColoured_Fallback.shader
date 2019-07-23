Shader "Custom/DiffuseAlphaNoLightColoured_Fallback" 
{
	Properties
	{
		_Color  	("Main Color", Color) 		   = (1,1,1,1)
		_MainTex 	("Base (RGB)", 2D)			   = "white" {}		
	}
	
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent"}
		LOD 200
		
		AlphaTest Off
		Blend SrcAlpha OneMinusSrcAlpha
		pass
		{
			Cull Back
			Lighting Off
            
            SetTexture [_MainTex]
			{
				ConstantColor [_Color]
				Combine texture*constant
			}
		}
		
	}
}