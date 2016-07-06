Shader "Custom/Multiply" {
	Properties {
		_ShadowTex ("Cookie", 2D) = "gray" {}
		_FalloffTex ("FallOff", 2D) = "white" {}
		_Angle("_Angle",float) = 1 //角度
	}
	Subshader {
		Tags {"Queue"="Transparent"}
		Pass {
			ZWrite Off
			ColorMask RGB
			Blend DstColor Zero
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 uvFalloff : TEXCOORD1;
				float2 uv:TEXCOORD2;
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
			};
			
			float4x4 _Projector;
			float4x4 _ProjectorClip;
			uniform float4 _ShadowTex_ST;
			v2f vert (appdata_tan v )
			{
				float4 vertex = v.vertex;
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, vertex);
				o.uvShadow = mul (_Projector, vertex);
				o.uvFalloff = mul (_ProjectorClip, vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _ShadowTex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			sampler2D _ShadowTex;
			sampler2D _FalloffTex;
			float _Angle;
			fixed4 frag (v2f i) : SV_Target
			{
				//float4 texS = tex2D(_ShadowTex, i.uv);
				fixed4 texS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				texS.a = 1.0-texS.a;

				fixed4 texF = tex2Dproj (_FalloffTex, UNITY_PROJ_COORD(i.uvFalloff));
				fixed4 res =  lerp(fixed4(1, 1, 1, 0), texS, texF.a);
				/*
				float2 uv = (i.uv - 0.5) * 2;
				//得出uv坐标到中心的距离
				
				float dis = distance(uv, half2(0, 0));
				//算出夹角
				float a1 = acos(uv.x / dis);
				//超出的话，隐藏掉
				if ((3.14 * _Angle / 360 - a1) < 0) res.a = 0;
				if (dis > 1) {
					res.rgb = 0;
					res.a = 0;
				}*/
				UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(1,1,1,1));
				return res;
			}
			ENDCG
		}
	}
}
