// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:1,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.003,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32837,y:32424,varname:node_2865,prsc:2|emission-2497-OUT,alpha-3825-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:31998,y:32686,varname:node_6343,prsc:2|A-9487-RGB,B-7736-RGB;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31804,y:32854,ptovrint:True,ptlb:Maintex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:9487,x:31804,y:32686,ptovrint:False,ptlb:Maintex Color,ptin:_MaintexColor,varname:node_9487,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_TexCoord,id:1529,x:31191,y:32358,varname:node_1529,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:2393,x:30830,y:32364,varname:node_2393,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:876,x:31585,y:32479,ptovrint:False,ptlb:Noise Map,ptin:_NoiseMap,varname:node_876,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3603-OUT;n:type:ShaderForge.SFN_Color,id:1994,x:31585,y:32296,ptovrint:False,ptlb:Noise Color,ptin:_NoiseColor,varname:node_1994,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:8196,x:31995,y:32401,varname:node_8196,prsc:2|A-1994-RGB,B-8938-OUT;n:type:ShaderForge.SFN_Slider,id:9917,x:31428,y:32685,ptovrint:False,ptlb:Noise Intensity,ptin:_NoiseIntensity,varname:node_9917,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Clamp01,id:2497,x:32652,y:32523,varname:node_2497,prsc:2|IN-2251-OUT;n:type:ShaderForge.SFN_Multiply,id:8938,x:31805,y:32510,varname:node_8938,prsc:2|A-876-RGB,B-9917-OUT;n:type:ShaderForge.SFN_Lerp,id:844,x:32253,y:32686,varname:node_844,prsc:2|A-6343-OUT,B-8196-OUT,T-8938-OUT;n:type:ShaderForge.SFN_Lerp,id:2251,x:32471,y:32523,varname:node_2251,prsc:2|A-6456-RGB,B-844-OUT,T-3825-OUT;n:type:ShaderForge.SFN_Tex2d,id:6456,x:32252,y:32506,ptovrint:False,ptlb:Overlay,ptin:_Overlay,varname:node_6456,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9199,x:31015,y:32519,varname:node_9199,prsc:2|A-2393-T,B-7381-OUT;n:type:ShaderForge.SFN_Slider,id:1261,x:30474,y:32547,ptovrint:False,ptlb:Noise U,ptin:_NoiseU,varname:_NoiseSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:4764,x:30474,y:32651,ptovrint:False,ptlb:Noise V,ptin:_NoiseV,varname:_NoiseSpeed_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:3603,x:31386,y:32479,varname:node_3603,prsc:2|A-1529-UVOUT,B-209-OUT;n:type:ShaderForge.SFN_Append,id:7381,x:30830,y:32509,varname:node_7381,prsc:2|A-1261-OUT,B-4764-OUT;n:type:ShaderForge.SFN_Multiply,id:209,x:31191,y:32519,varname:node_209,prsc:2|A-9199-OUT,B-3757-OUT;n:type:ShaderForge.SFN_Slider,id:3757,x:30858,y:32681,ptovrint:False,ptlb:Noise Speed,ptin:_NoiseSpeed,varname:node_3757,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Lerp,id:3825,x:32458,y:32778,varname:node_3825,prsc:2|A-7736-A,B-6456-A,T-6456-A;proporder:9487-7736-876-1994-9917-6456-1261-4764-3757;pass:END;sub:END;*/

Shader "DCC/Crystal Shader UI" {
    Properties {
        _MaintexColor ("Maintex Color", Color) = (1,1,1,1)
        _MainTex ("Maintex", 2D) = "white" {}
        _NoiseMap ("Noise Map", 2D) = "white" {}
        _NoiseColor ("Noise Color", Color) = (1,1,1,1)
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.5
        _Overlay ("Overlay", 2D) = "black" {}
        _NoiseU ("Noise U", Range(-1, 1)) = 1
        _NoiseV ("Noise V", Range(-1, 1)) = 1
        _NoiseSpeed ("Noise Speed", Range(0, 1)) = 0.1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _MaintexColor;
            uniform sampler2D _NoiseMap; uniform float4 _NoiseMap_ST;
            uniform float4 _NoiseColor;
            uniform float _NoiseIntensity;
            uniform sampler2D _Overlay; uniform float4 _Overlay_ST;
            uniform float _NoiseU;
            uniform float _NoiseV;
            uniform float _NoiseSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _Overlay_var = tex2D(_Overlay,TRANSFORM_TEX(i.uv0, _Overlay));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_2393 = _Time;
                float2 node_3603 = (i.uv0+((node_2393.g*float2(_NoiseU,_NoiseV))*_NoiseSpeed));
                float4 _NoiseMap_var = tex2D(_NoiseMap,TRANSFORM_TEX(node_3603, _NoiseMap));
                float3 node_8938 = (_NoiseMap_var.rgb*_NoiseIntensity);
                float node_3825 = lerp(_MainTex_var.a,_Overlay_var.a,_Overlay_var.a);
                float3 emissive = saturate(lerp(_Overlay_var.rgb,lerp((_MaintexColor.rgb*_MainTex_var.rgb),(_NoiseColor.rgb*node_8938),node_8938),node_3825));
                float3 finalColor = emissive;
                return fixed4(finalColor,node_3825);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
