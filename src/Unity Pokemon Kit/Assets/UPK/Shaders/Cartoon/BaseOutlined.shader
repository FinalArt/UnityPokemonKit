// Create an outlined cartoon style for Material
// @ModifiedBy : FinalArt (08/2015)
// @OriginalAuthor : Ippokratis Bournellis (asset : Toon Shader Free)

Shader "Cartoon/BaseOutlined"
{
    Properties 
    {
		_Outline ("Outline width", Float) = 1
		_OutlineColor ("Outline color", Color) = (0,0,0,1)
		_Brightness ("Brightness (1 = neutral)", Float) = 1.0
		_MainTex ("Detail", 2D) = "white" {}
		_ToonShade ("Shade", 2D) = "white" {}
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
		LOD 250 
        Lighting Off
        Fog { Mode Off }
        
        UsePass "Cartoon/Base/BASE"
        	
        Pass
        {
            Cull Front
            ZWrite On
            CGPROGRAM
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma glsl_no_auto_normalization
            #pragma vertex vert
 			#pragma fragment frag
			
            struct appdata_t 
            {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f 
			{
				float4 pos : SV_POSITION;
			};

            fixed _Outline;

            
            v2f vert (appdata_t v) 
            {
                v2f o;
			    o.pos = v.vertex;
			    o.pos.xyz += v.normal.xyz *_Outline*0.01;
			    o.pos = mul(UNITY_MATRIX_MVP, o.pos);
			    return o;
            }
            
            fixed4 _OutlineColor;
            
            fixed4 frag(v2f i) :COLOR 
			{
		    	return _OutlineColor;
			}
            
            ENDCG
        }
    }
    CustomEditor "MaterialEditor"
    Fallback "Diffuse"
}