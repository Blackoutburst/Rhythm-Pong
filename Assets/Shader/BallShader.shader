
Shader "Unlit/BallShader"
{
    Properties
    {
		_RColor ("Red Color", Range (0, 255)) = 0
		_GColor ("Green Color", Range (0, 255)) = 150
		_BColor ("Blue Color", Range (0, 255)) = 255
		_AColor ("Alpha Color", Range (0, 255)) = 255
		_Size ("Size", Range (0, 1)) = 1
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
		Blend One One
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			float _RColor;
			float _GColor;
			float _BColor;
			float _AColor;
			float _Size;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {
				float2 lightLocation = float2(0.5, 0.5);
				float distance = length(lightLocation - i.uv);
				float attenuation = (1 - _Size)/distance;
				float4 lightcolor = float4(attenuation, attenuation, attenuation, 0) * float4(_RColor, _GColor, _BColor, _AColor);

				return lightcolor;
            }
            ENDCG
        }
    }
}
