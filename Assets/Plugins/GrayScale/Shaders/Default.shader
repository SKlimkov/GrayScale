Shader "Sprites/DefaultCustom"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture (RGB)", 2D) = "white" {}
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			LOD 200
			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			Blend One OneMinusSrcAlpha

			Pass
			{
				CGPROGRAM

	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				sampler2D	_MainTex;

				float GetLuminance(fixed4 tex)
				{
					return (dot(tex, fixed4(0.2126, 0.7152, 0.0722, 0)));
				}

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color;
	#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
	#endif
					return OUT;
				}

				fixed4 frag(v2f IN) : COLOR
				{
					fixed4 output;
					fixed4 mainTex = tex2D(_MainTex, IN.texcoord) * IN.color;

					output = mainTex;

					output.rgb *= output.a;
					return output;
				}

				ENDCG
			}
		}
			FallBack "Diffuse"
}