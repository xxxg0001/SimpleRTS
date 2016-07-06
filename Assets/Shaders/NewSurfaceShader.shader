Shader "RPG/SkillArea" {
	Properties{
		_Color("Main Color", Color) = (0,0,0,0)
		_MainTex("MainTexture (RGB)", 2D) = "white" {}
	_Angle("_Angle",float) = 1 //角度
	}
		SubShader
	{
		Tags{ "Queue" = "Geometry+10" }   //"LightMode" = "ForwardBase" "LightMode" = "ForwardAdd"
										  //最终颜色 = 源颜色 * 源透明值 + 目标颜色*（1 - 源透明值）
										  //最常用的透明混合方式。贴图alpha值高的部分，显示得实，而混合的背景很淡。而alpha值高的部分，贴图显示得淡，而背景现实得实。
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 150
		ZTest Always
		ZWrite off
		offset -100,-100
		Pass
	{
		Tags{ "LightMode" = "Vertex" }// 
		CGPROGRAM
#pragma vertex vert  
#pragma fragment frag  
#include "UnityCG.cginc"   
#include "Lighting.cginc" 
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	uniform float4 _MainTex_ST;
	v2f vert(appdata_tan v)
	{
		v2f o;
		float4 v2 = v.vertex;
		o.pos = mul(UNITY_MATRIX_MVP, v2);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}
	sampler2D _MainTex;
	float4 _Color;
	float _Angle;
	half4 frag(v2f i) : COLOR
	{
		float4 result = tex2D(_MainTex, i.uv);
		//暂时不明白这个含义
		float2 uv = (i.uv - 0.5) * 2;
		//得出uv坐标到中心的距离
		float dis = distance(uv,half2(0,0));
		//算出夹角
		float a1 = acos(uv.x / dis);
		//超出的话，隐藏掉
		if ((3.14 * _Angle / 360 - a1) < 0) result.a = 0;
		if (dis > 1) {
			result.rgb = 0;
			result.a = 0;
		}
		result.rgb = saturate(result.rgb);
		//result = float4 (a1,a1,a1,1);
		result *= _Color;
		//clip(result.a - 0.5);
		return result;
	}
		ENDCG
	}

	}
		Fallback "VertexLit"
}