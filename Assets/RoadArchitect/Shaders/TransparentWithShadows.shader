Shader "RoadArchitect/TranShadow" { 
    Properties { 
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) TransGloss (A)", 2D) = "white" {}
        _ShadowIntensity ("Shadow Intensity", Range (0, 1)) = 0.6
    } 

    SubShader {
        Tags {
            "Queue"="AlphaTest" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent"
        }

        LOD 300

        CGPROGRAM
        #pragma surface surf Lambert alpha
        #pragma exclude_renderers d3d11 xbox360

        sampler2D _MainTex;
        float4 _Color;
        float _ShadowIntensity;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = tex.rgb * _Color.rgb;
            o.Alpha = tex.a * _Color.a;
        }
        ENDCG

        // Shadow Pass
        Pass {
            Blend SrcAlpha OneMinusSrcAlpha 
            Name "ShadowPass"
            Tags {"LightMode" = "ForwardBase"}
              
            CGPROGRAM 
            #pragma exclude_renderers d3d11 xbox360
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma fragmentoption ARB_fog_exp2
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
 
            struct v2f { 
                float4 pos : SV_POSITION;
                float2 uv_MainTex : TEXCOORD0;
                UNITY_LIGHTING_COORDS(1, 2)
            };

            float4 _MainTex_ST;

            sampler2D _MainTex;
            float4 _Color;
            float _ShadowIntensity;
 
            v2f vert (appdata_full v)
            {
                v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.pos); // Transfer fog data from vertex shader to fragment shader
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half atten = LIGHT_ATTENUATION(i.light);
                half4 c;
                c.rgb = 0;
                c.a = (1 - atten) * _ShadowIntensity * tex2D(_MainTex, i.uv_MainTex).a; 
                return c;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
