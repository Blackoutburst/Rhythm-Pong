Shader "Unlit/Arena"
{
    Properties
    {
		_Times ("Times", Float) = 0
		_Height ("Height", Range (0, 255)) = 5
		_Phase ("Phase", Range (0, 255)) = 5
		_Glow ("Glow", Range (0, 255)) = 10
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
			
			float _Times;
			float _Height;
			float _Phase;
			float _Glow;
			
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

				float3 lightColor = float3(1.0, 0.5, 0.0);

				float2 resolution = i.uv;

				float2 p = resolution;

	
				lightColor = float3(1.0, 0.6, 1.0);

				//Top phasing
				float sx = (_Height) * sin(_Phase * p.x + _Times * 2.0);
				float3 dx = (_Glow / abs(40.0 * p.y - sx - 40.0)) * lightColor * 2.0;

				//Top normal
				float3 dx2 = (_Glow / abs(40.0 * p.y - 40.0)) * lightColor * 2.0;


				//Bottom phasing
				float sx3 = (_Height) * sin(_Phase * p.x + _Times * 2.0);
				float3 dx3 = (_Glow / abs(40.0 * p.y - sx3)) * lightColor * 2.0;

				//Bottom normal
				float3 dx4 = (_Glow / abs(40.0 * p.y)) * lightColor * 2.0;


				lightColor = float3(0.0, 0.5, 1.0);
				//Left phasing
				float sy = (_Height) * sin(_Phase * p.y + _Times * 2.0);
				float3 dy = (_Glow / abs(40.0 * p.x - sy - 40.0)) * lightColor * 2.0;

				//Left normal
				float3 dy2 = (_Glow / abs(40.0 * p.x - 40.0)) * lightColor * 2.0;

				lightColor = float3(1.0, 0.5, 0.0);

				//Right phasing
				float sy2 = (_Height) * sin(_Phase * p.y + _Times * 2.0);
				float3 dy3 = (_Glow / abs(40.0 * p.x - sy2)) * lightColor * 2.0;

				//Right normal
				float3 dy4 = (_Glow / abs(40.0 * p.x)) * lightColor * 2.0;

				
				//Mid orange phasing
				float my = (_Height) * sin(_Phase * p.y + _Times * 2.0);
				float3 dmy = (_Glow / abs(40.0 * p.x - my - 20.0)) * lightColor * 2.0;

				lightColor = float3(0.0, 0.5, 1.0);
				
				//Mid blue phasing
				float my2 = (_Height) * -sin(_Phase * p.y + _Times * 2.0);
				float3 dmy2 = (_Glow / abs(40.0 * p.x - my2 - 20.0)) * lightColor * 2.0;

				float3 color = dx + dx2 + dx3 + dx4 + dy + dy2 + dy3 + dy4 + dmy + dmy2;


				return float4( color, 1.0 );
            }
            ENDCG
        }
    }
}
