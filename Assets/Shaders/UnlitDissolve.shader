// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/DissolveEffectShader"
{
	Properties
	{
        _PrimaryColor ("Primary color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_EdgeColour1 ("Edge colour 1", Color) = (1.0, 1.0, 1.0, 1.0)
		_EdgeColour2 ("Edge colour 2", Color) = (1.0, 1.0, 1.0, 1.0)
		_Level ("Dissolution level", Range (0.0, 1.0)) = 0.1
		_Edges ("Edge width", Range (0.0, 1.0)) = 0.1
		_TimeOffset ("Time offset", Range (0.0, 1.0)) = 0.5
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
        	Lighting Off
        	ZWrite Off
        	Fog { Mode Off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile DUMMY PIXELSNAP_ON
			
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
                fixed4 color : COLOR0;
			};

            float4 _PrimaryColor;
			sampler2D _MainTex;
			sampler2D _NoiseTex;
			float4 _EdgeColour1;
			float4 _EdgeColour2;
			float _Level;
			float _Edges;
			float _TimeOffset;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = _PrimaryColor;

				#ifdef PIXELSNAP_ON
                o.vertex = UnityPixelSnap (o.vertex);
                #endif

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                _Level = _Level + 3*(sin(_Time.w + _TimeOffset)/15);

				// sample the texture
				float cutout = tex2D(_NoiseTex, i.uv).r;
				// This is for using with an image texture.
				// fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col = _PrimaryColor;

				if (cutout < _Level)
					discard;

				if(cutout < col.a && cutout < _Level + _Edges)
					col = _EdgeColour1;
					// The following is for having double colored edges.
					// col =lerp(_EdgeColour1, _EdgeColour2, (cutout-_Level)/_Edges );

				return col;
			}
			ENDCG
		}
	}
}