// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4226,x:32719,y:32712,varname:node_4226,prsc:2|diff-5042-OUT,normal-7158-OUT,emission-2851-OUT,clip-3835-OUT;n:type:ShaderForge.SFN_Tex2d,id:8182,x:32017,y:32719,ptovrint:False,ptlb:Maintex,ptin:_Maintex,varname:node_8182,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:40b1262c30d5d91458d07070563c7b55,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4716,x:31272,y:33493,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_4716,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e85634610e2a69e4389c1ed95866018d,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:7509,x:32071,y:33551,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_7509,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:5334,x:32017,y:32919,ptovrint:False,ptlb:Maintex Color,ptin:_MaintexColor,varname:node_5334,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:8829,x:32071,y:33746,ptovrint:False,ptlb:Maintex Emissions,ptin:_MaintexEmissions,varname:node_8829,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9044118,c2:0.7675933,c3:0.458856,c4:1;n:type:ShaderForge.SFN_Multiply,id:8140,x:32318,y:33623,varname:node_8140,prsc:2|A-7509-RGB,B-8829-RGB;n:type:ShaderForge.SFN_Multiply,id:9249,x:32612,y:33565,varname:node_9249,prsc:2|A-8140-OUT,B-8187-OUT;n:type:ShaderForge.SFN_Slider,id:8187,x:32039,y:33929,ptovrint:False,ptlb:Emission Intensity,ptin:_EmissionIntensity,varname:node_8187,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_ComponentMask,id:6227,x:31570,y:33473,varname:node_6227,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-4716-RGB;n:type:ShaderForge.SFN_Multiply,id:832,x:31764,y:33428,varname:node_832,prsc:2|A-5030-OUT,B-6227-OUT;n:type:ShaderForge.SFN_Slider,id:5030,x:31318,y:33326,ptovrint:False,ptlb:Normal Intensity,ptin:_NormalIntensity,varname:_NormalIntensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3,max:10;n:type:ShaderForge.SFN_Append,id:7158,x:32032,y:33335,varname:node_7158,prsc:2|A-832-OUT,B-6918-OUT;n:type:ShaderForge.SFN_Vector1,id:6918,x:31764,y:33572,varname:node_6918,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:714,x:32210,y:32798,varname:node_714,prsc:2|A-8182-RGB,B-5334-RGB;n:type:ShaderForge.SFN_TexCoord,id:7088,x:31134,y:33968,varname:node_7088,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:8877,x:30773,y:33974,varname:node_8877,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:2940,x:31528,y:34089,ptovrint:False,ptlb:Emission Noise,ptin:_EmissionNoise,varname:node_876,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False|UVIN-6377-OUT;n:type:ShaderForge.SFN_Slider,id:4194,x:31371,y:34295,ptovrint:False,ptlb:Emission Noise Intensity,ptin:_EmissionNoiseIntensity,varname:node_9917,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:9273,x:30958,y:34129,varname:node_9273,prsc:2|A-8877-T,B-1498-OUT;n:type:ShaderForge.SFN_Slider,id:7918,x:30417,y:34157,ptovrint:False,ptlb:Emission Noise U,ptin:_EmissionNoiseU,varname:_NoiseSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:5043,x:30417,y:34261,ptovrint:False,ptlb:Emission Noise V,ptin:_EmissionNoiseV,varname:_NoiseSpeed_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:6377,x:31329,y:34089,varname:node_6377,prsc:2|A-7088-UVOUT,B-5889-OUT;n:type:ShaderForge.SFN_Append,id:1498,x:30773,y:34119,varname:node_1498,prsc:2|A-7918-OUT,B-5043-OUT;n:type:ShaderForge.SFN_Multiply,id:5889,x:31134,y:34129,varname:node_5889,prsc:2|A-9273-OUT,B-581-OUT;n:type:ShaderForge.SFN_Slider,id:581,x:30801,y:34291,ptovrint:False,ptlb:Emission Noise Speed,ptin:_EmissionNoiseSpeed,varname:node_3757,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Multiply,id:450,x:31748,y:34120,varname:node_450,prsc:2|A-2940-RGB,B-4194-OUT;n:type:ShaderForge.SFN_Lerp,id:5654,x:33055,y:33677,varname:node_5654,prsc:2|A-9249-OUT,B-4020-OUT,T-450-OUT;n:type:ShaderForge.SFN_Vector1,id:4020,x:32781,y:33685,varname:node_4020,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:1245,x:30272,y:33240,ptovrint:False,ptlb:Dissolve Noise,ptin:_DissolveNoise,varname:node_5424,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:7383,x:29777,y:33038,ptovrint:False,ptlb:Dissolve Amount,ptin:_DissolveAmount,varname:node_4039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:3641,x:30476,y:33146,varname:node_3641,prsc:2|A-9795-OUT,B-1245-R;n:type:ShaderForge.SFN_RemapRange,id:9795,x:30272,y:33038,varname:node_9795,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.6|IN-2316-OUT;n:type:ShaderForge.SFN_OneMinus,id:2316,x:30110,y:33038,varname:node_2316,prsc:2|IN-7383-OUT;n:type:ShaderForge.SFN_Clamp01,id:3835,x:30452,y:32757,varname:node_3835,prsc:2|IN-1594-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:1594,x:30278,y:32757,varname:node_1594,prsc:2|IN-3641-OUT,IMIN-323-OUT,IMAX-2574-OUT,OMIN-7395-OUT,OMAX-8335-OUT;n:type:ShaderForge.SFN_Slider,id:8391,x:30253,y:32286,ptovrint:False,ptlb:Dissolve Max,ptin:_DissolveMax,varname:node_7359,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:5,max:10;n:type:ShaderForge.SFN_Slider,id:8265,x:30253,y:32191,ptovrint:False,ptlb:Dissolve Min,ptin:_DissolveMin,varname:node_8221,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:-2,max:10;n:type:ShaderForge.SFN_Vector1,id:323,x:29895,y:32619,varname:node_323,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:2574,x:29895,y:32682,varname:node_2574,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:7836,x:31255,y:32417,varname:node_7836,prsc:2|A-3412-RGB,B-6427-RGB;n:type:ShaderForge.SFN_Color,id:3412,x:31068,y:32255,ptovrint:False,ptlb:Dissolve Color,ptin:_DissolveColor,varname:node_3412,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:8335,x:29895,y:32813,varname:node_8335,prsc:2,v1:3;n:type:ShaderForge.SFN_Vector1,id:7395,x:29895,y:32746,varname:node_7395,prsc:2,v1:-3;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:896,x:30285,y:32448,varname:node_896,prsc:2|IN-6035-OUT,IMIN-323-OUT,IMAX-2574-OUT,OMIN-8265-OUT,OMAX-8391-OUT;n:type:ShaderForge.SFN_Clamp01,id:1543,x:30459,y:32448,varname:node_1543,prsc:2|IN-896-OUT;n:type:ShaderForge.SFN_Vector1,id:9535,x:30631,y:32595,varname:node_9535,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:9930,x:30803,y:32448,varname:node_9930,prsc:2|A-1543-OUT,B-9535-OUT;n:type:ShaderForge.SFN_Tex2d,id:6427,x:31068,y:32417,varname:node_6427,prsc:2,tex:19f34113455b8304cad6c4dd09377655,ntxv:0,isnm:False|UVIN-9930-OUT,TEX-1715-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:1715,x:30803,y:32595,ptovrint:False,ptlb:Dissolve Ramp,ptin:_DissolveRamp,varname:node_1715,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:19f34113455b8304cad6c4dd09377655,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:6035,x:29895,y:32487,varname:node_6035,prsc:2|IN-3641-OUT;n:type:ShaderForge.SFN_Lerp,id:5042,x:32429,y:32659,varname:node_5042,prsc:2|A-7836-OUT,B-714-OUT,T-714-OUT;n:type:ShaderForge.SFN_Lerp,id:2851,x:32490,y:33281,varname:node_2851,prsc:2|A-4209-OUT,B-5654-OUT,T-5654-OUT;n:type:ShaderForge.SFN_Multiply,id:4209,x:32157,y:33165,varname:node_4209,prsc:2|A-7836-OUT,B-9133-OUT;n:type:ShaderForge.SFN_Slider,id:9133,x:31633,y:33168,ptovrint:False,ptlb:Dissolve Emission Intensity,ptin:_DissolveEmissionIntensity,varname:node_9133,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:8182-5334-4716-5030-7509-8829-8187-2940-4194-7918-5043-581-1245-1715-3412-9133-7383-8391-8265;pass:END;sub:END;*/

Shader "DCC/Double Sided Standard" {
    Properties {
        _Maintex ("Maintex", 2D) = "white" {}
        _MaintexColor ("Maintex Color", Color) = (1,1,1,1)
        _Normal ("Normal", 2D) = "bump" {}
        _NormalIntensity ("Normal Intensity", Range(0, 10)) = 0.3
        _Emission ("Emission", 2D) = "white" {}
        _MaintexEmissions ("Maintex Emissions", Color) = (0.9044118,0.7675933,0.458856,1)
        _EmissionIntensity ("Emission Intensity", Range(0, 5)) = 0
        _EmissionNoise ("Emission Noise", 2D) = "white" {}
        _EmissionNoiseIntensity ("Emission Noise Intensity", Range(0, 1)) = 1
        _EmissionNoiseU ("Emission Noise U", Range(-1, 1)) = 1
        _EmissionNoiseV ("Emission Noise V", Range(-1, 1)) = 1
        _EmissionNoiseSpeed ("Emission Noise Speed", Range(0, 1)) = 0.1
        _DissolveNoise ("Dissolve Noise", 2D) = "white" {}
        _DissolveRamp ("Dissolve Ramp", 2D) = "white" {}
        _DissolveColor ("Dissolve Color", Color) = (1,1,1,1)
        _DissolveEmissionIntensity ("Dissolve Emission Intensity", Range(0, 1)) = 0
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0
        _DissolveMax ("Dissolve Max", Range(-10, 10)) = 5
        _DissolveMin ("Dissolve Min", Range(-10, 10)) = -2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float4 _MaintexColor;
            uniform float4 _MaintexEmissions;
            uniform float _EmissionIntensity;
            uniform float _NormalIntensity;
            uniform sampler2D _EmissionNoise; uniform float4 _EmissionNoise_ST;
            uniform float _EmissionNoiseIntensity;
            uniform float _EmissionNoiseU;
            uniform float _EmissionNoiseV;
            uniform float _EmissionNoiseSpeed;
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform float4 _DissolveColor;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float _DissolveEmissionIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = float3((_NormalIntensity*_Normal_var.rgb.rg),1.0);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_3641 = (((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r);
                float node_323 = 0.0;
                float node_2574 = 1.0;
                float node_7395 = (-3.0);
                clip(saturate((node_7395 + ( (node_3641 - node_323) * (3.0 - node_7395) ) / (node_2574 - node_323))) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - 0;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float2 node_9930 = float2(saturate((_DissolveMin + ( ((1.0 - node_3641) - node_323) * (_DissolveMax - _DissolveMin) ) / (node_2574 - node_323))),0.0);
                float4 node_6427 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_9930, _DissolveRamp));
                float3 node_7836 = (_DissolveColor.rgb*node_6427.rgb);
                float4 _Maintex_var = tex2D(_Maintex,TRANSFORM_TEX(i.uv0, _Maintex));
                float3 node_714 = (_Maintex_var.rgb*_MaintexColor.rgb);
                float3 diffuseColor = lerp(node_7836,node_714,node_714);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(i.uv0, _Emission));
                float node_4020 = 0.0;
                float4 node_8877 = _Time;
                float2 node_6377 = (i.uv0+((node_8877.g*float2(_EmissionNoiseU,_EmissionNoiseV))*_EmissionNoiseSpeed));
                float4 _EmissionNoise_var = tex2D(_EmissionNoise,TRANSFORM_TEX(node_6377, _EmissionNoise));
                float3 node_5654 = lerp(((_Emission_var.rgb*_MaintexEmissions.rgb)*_EmissionIntensity),float3(node_4020,node_4020,node_4020),(_EmissionNoise_var.rgb*_EmissionNoiseIntensity));
                float3 emissive = lerp((node_7836*_DissolveEmissionIntensity),node_5654,node_5654);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float4 _MaintexColor;
            uniform float4 _MaintexEmissions;
            uniform float _EmissionIntensity;
            uniform float _NormalIntensity;
            uniform sampler2D _EmissionNoise; uniform float4 _EmissionNoise_ST;
            uniform float _EmissionNoiseIntensity;
            uniform float _EmissionNoiseU;
            uniform float _EmissionNoiseV;
            uniform float _EmissionNoiseSpeed;
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform float4 _DissolveColor;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float _DissolveEmissionIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = float3((_NormalIntensity*_Normal_var.rgb.rg),1.0);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_3641 = (((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r);
                float node_323 = 0.0;
                float node_2574 = 1.0;
                float node_7395 = (-3.0);
                clip(saturate((node_7395 + ( (node_3641 - node_323) * (3.0 - node_7395) ) / (node_2574 - node_323))) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float2 node_9930 = float2(saturate((_DissolveMin + ( ((1.0 - node_3641) - node_323) * (_DissolveMax - _DissolveMin) ) / (node_2574 - node_323))),0.0);
                float4 node_6427 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_9930, _DissolveRamp));
                float3 node_7836 = (_DissolveColor.rgb*node_6427.rgb);
                float4 _Maintex_var = tex2D(_Maintex,TRANSFORM_TEX(i.uv0, _Maintex));
                float3 node_714 = (_Maintex_var.rgb*_MaintexColor.rgb);
                float3 diffuseColor = lerp(node_7836,node_714,node_714);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_3641 = (((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r);
                float node_323 = 0.0;
                float node_2574 = 1.0;
                float node_7395 = (-3.0);
                clip(saturate((node_7395 + ( (node_3641 - node_323) * (3.0 - node_7395) ) / (node_2574 - node_323))) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float4 _MaintexColor;
            uniform float4 _MaintexEmissions;
            uniform float _EmissionIntensity;
            uniform sampler2D _EmissionNoise; uniform float4 _EmissionNoise_ST;
            uniform float _EmissionNoiseIntensity;
            uniform float _EmissionNoiseU;
            uniform float _EmissionNoiseV;
            uniform float _EmissionNoiseSpeed;
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform float4 _DissolveColor;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float _DissolveEmissionIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_3641 = (((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r);
                float node_323 = 0.0;
                float node_2574 = 1.0;
                float2 node_9930 = float2(saturate((_DissolveMin + ( ((1.0 - node_3641) - node_323) * (_DissolveMax - _DissolveMin) ) / (node_2574 - node_323))),0.0);
                float4 node_6427 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_9930, _DissolveRamp));
                float3 node_7836 = (_DissolveColor.rgb*node_6427.rgb);
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(i.uv0, _Emission));
                float node_4020 = 0.0;
                float4 node_8877 = _Time;
                float2 node_6377 = (i.uv0+((node_8877.g*float2(_EmissionNoiseU,_EmissionNoiseV))*_EmissionNoiseSpeed));
                float4 _EmissionNoise_var = tex2D(_EmissionNoise,TRANSFORM_TEX(node_6377, _EmissionNoise));
                float3 node_5654 = lerp(((_Emission_var.rgb*_MaintexEmissions.rgb)*_EmissionIntensity),float3(node_4020,node_4020,node_4020),(_EmissionNoise_var.rgb*_EmissionNoiseIntensity));
                o.Emission = lerp((node_7836*_DissolveEmissionIntensity),node_5654,node_5654);
                
                float4 _Maintex_var = tex2D(_Maintex,TRANSFORM_TEX(i.uv0, _Maintex));
                float3 node_714 = (_Maintex_var.rgb*_MaintexColor.rgb);
                float3 diffColor = lerp(node_7836,node_714,node_714);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
