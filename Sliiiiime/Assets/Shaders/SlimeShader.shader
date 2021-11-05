Shader "Unlit/SlimeShader"
{
    Properties
    {
         _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _SlimeSize ("Slime size", Float) = 1
        _WaveSize ("Wave size", Float) = 1
        _WaveSpeed ("Wave speed", Float) = 1
    }
    SubShader
    {
         Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
               #pragma vertex vert
                 #pragma fragment frag
                 #pragma target 2.0
                 #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                     float4 vertex : POSITION;
                     float2 texcoord : TEXCOORD0;
                     UNITY_VERTEX_INPUT_INSTANCE_ID            };

            struct v2f
            {
                     float4 vertex : SV_POSITION;
                     float2 texcoord : TEXCOORD0;
                     UNITY_FOG_COORDS(1)
                     UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            Float _SlimeSize;
            Float _WaveSize;
            Float _WaveSpeed;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                     v2f o;
                     UNITY_SETUP_INSTANCE_ID(v);
                     UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                     o.vertex = UnityObjectToClipPos(v.vertex);
                     o.texcoord = v.texcoord//TRANSFORM_TEX(v.texcoord, _MainTex);
                     UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.texcoord);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                float distanceFromCenter = distance(float2(0.5,0.5),i.texcoord);
                
                float halfRange = (_WaveSize - (-_WaveSize)) / 2;
                float slimeSizeSineWave = (_SlimeSize + (-_WaveSize) + halfRange + sin(_Time * _WaveSpeed) * halfRange);  

                if(distanceFromCenter > slimeSizeSineWave)
                {
                 return fixed4(0,0,0,0);
                }
                else
                {
                    return col;
                }
            }
            ENDCG
        }
    }
}
