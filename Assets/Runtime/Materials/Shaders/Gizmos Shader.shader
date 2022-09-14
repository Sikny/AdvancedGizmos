Shader "Hidden/Gizmos Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1.0,1.0,1.0,1.0)
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
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewT : TEXCOORD1;
            };

            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos (v.vertex);
                o.normal = normalize(v.normal);
                o.viewT = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                const float3 light_color = fixed3(1.0, 1.0, 1.0);
                const float ambient_strength = 0.25;

                float3 ambient = ambient_strength * light_color;
                float3 lightDir = i.viewT;
                float diff = max(dot(i.normal, lightDir), 0.0) * 0.5f;
                float3 diffuse = diff * light_color;  // white color
                
                float3 result = (diffuse + ambient) * _Color;
                
                return float4(result, 1.0);
            }
            ENDCG
        }
    }
}
