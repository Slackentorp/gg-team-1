// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1454,x:32719,y:32712,varname:node_1454,prsc:2|diff-771-OUT,clip-8471-OUT;n:type:ShaderForge.SFN_Tex2d,id:4798,x:30207,y:33307,ptovrint:False,ptlb:Dissolve Noise,ptin:_DissolveNoise,varname:node_5424,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:9243,x:29712,y:33105,ptovrint:False,ptlb:Dissolve Amount,ptin:_DissolveAmount,varname:node_4039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:8387,x:30411,y:33213,varname:node_8387,prsc:2|A-8898-OUT,B-4798-R;n:type:ShaderForge.SFN_RemapRange,id:8898,x:30207,y:33105,varname:node_8898,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.6|IN-8335-OUT;n:type:ShaderForge.SFN_OneMinus,id:8335,x:30045,y:33105,varname:node_8335,prsc:2|IN-9243-OUT;n:type:ShaderForge.SFN_Clamp01,id:5378,x:30387,y:32824,varname:node_5378,prsc:2|IN-6502-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:6502,x:30213,y:32824,varname:node_6502,prsc:2|IN-8387-OUT,IMIN-6360-OUT,IMAX-2554-OUT,OMIN-4654-OUT,OMAX-8950-OUT;n:type:ShaderForge.SFN_Slider,id:5420,x:30188,y:32353,ptovrint:False,ptlb:Dissolve Max,ptin:_DissolveMax,varname:node_7359,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:5,max:10;n:type:ShaderForge.SFN_Slider,id:9682,x:30188,y:32258,ptovrint:False,ptlb:Dissolve Min,ptin:_DissolveMin,varname:node_8221,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:-2,max:10;n:type:ShaderForge.SFN_Vector1,id:6360,x:29830,y:32686,varname:node_6360,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:2554,x:29830,y:32749,varname:node_2554,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:8950,x:29830,y:32880,varname:node_8950,prsc:2,v1:3;n:type:ShaderForge.SFN_Vector1,id:4654,x:29830,y:32813,varname:node_4654,prsc:2,v1:-3;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:8530,x:30220,y:32515,varname:node_8530,prsc:2|IN-9693-OUT,IMIN-6360-OUT,IMAX-2554-OUT,OMIN-9682-OUT,OMAX-5420-OUT;n:type:ShaderForge.SFN_Clamp01,id:8471,x:30394,y:32515,varname:node_8471,prsc:2|IN-8530-OUT;n:type:ShaderForge.SFN_Vector1,id:1963,x:30566,y:32662,varname:node_1963,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:3868,x:30738,y:32515,varname:node_3868,prsc:2|A-8471-OUT,B-1963-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:5757,x:30738,y:32662,ptovrint:False,ptlb:Dissolve Ramp,ptin:_DissolveRamp,varname:node_1715,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:19f34113455b8304cad6c4dd09377655,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:9693,x:29830,y:32554,varname:node_9693,prsc:2|IN-8387-OUT;n:type:ShaderForge.SFN_Multiply,id:9656,x:31190,y:32484,varname:node_9656,prsc:2|A-1586-RGB,B-7879-RGB;n:type:ShaderForge.SFN_Color,id:1586,x:31003,y:32322,ptovrint:False,ptlb:Dissolve Color,ptin:_DissolveColor,varname:node_3412,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7879,x:31003,y:32484,varname:node_6427,prsc:2,tex:19f34113455b8304cad6c4dd09377655,ntxv:0,isnm:False|UVIN-3868-OUT,TEX-5757-TEX;n:type:ShaderForge.SFN_Multiply,id:4968,x:32145,y:32865,varname:node_4968,prsc:2|A-7373-RGB,B-5027-RGB;n:type:ShaderForge.SFN_Lerp,id:771,x:32364,y:32726,varname:node_771,prsc:2|A-9656-OUT,B-4968-OUT,T-4968-OUT;n:type:ShaderForge.SFN_Tex2d,id:7373,x:31952,y:32786,ptovrint:False,ptlb:Maintex,ptin:_Maintex,varname:node_8182,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:5027,x:31952,y:32986,ptovrint:False,ptlb:Maintex Color,ptin:_MaintexColor,varname:node_5334,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;proporder:4798-9243-5420-9682-5757-1586-7373-5027;pass:END;sub:END;*/

Shader "Unlit/Dissolve" {
    Properties {
        _DissolveNoise ("Dissolve Noise", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 1
        _DissolveMax ("Dissolve Max", Range(-10, 10)) = 5
        _DissolveMin ("Dissolve Min", Range(-10, 10)) = -2
        _DissolveRamp ("Dissolve Ramp", 2D) = "white" {}
        _DissolveColor ("Dissolve Color", Color) = (1,1,1,1)
        _Maintex ("Maintex", 2D) = "white" {}
        _MaintexColor ("Maintex Color", Color) = (1,1,1,1)
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
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float4 _DissolveColor;
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform float4 _MaintexColor;
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
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_8387 = (((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r);
                float node_6360 = 0.0;
                float node_2554 = 1.0;
                float node_8471 = saturate((_DissolveMin + ( ((1.0 - node_8387) - node_6360) * (_DissolveMax - _DissolveMin) ) / (node_2554 - node_6360)));
                clip(node_8471 - 0.5);
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
                float2 node_3868 = float2(node_8471,0.0);
                float4 node_6427 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_3868, _DissolveRamp));
                float4 _Maintex_var = tex2D(_Maintex,TRANSFORM_TEX(i.uv0, _Maintex));
                float3 node_4968 = (_Maintex_var.rgb*_MaintexColor.rgb);
                float3 diffuseColor = lerp((_DissolveColor.rgb*node_6427.rgb),node_4968,node_4968);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
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
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float4 _DissolveColor;
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform float4 _MaintexColor;
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
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_8387 = (((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r);
                float node_6360 = 0.0;
                float node_2554 = 1.0;
                float node_8471 = saturate((_DissolveMin + ( ((1.0 - node_8387) - node_6360) * (_DissolveMax - _DissolveMin) ) / (node_2554 - node_6360)));
                clip(node_8471 - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float2 node_3868 = float2(node_8471,0.0);
                float4 node_6427 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_3868, _DissolveRamp));
                float4 _Maintex_var = tex2D(_Maintex,TRANSFORM_TEX(i.uv0, _Maintex));
                float3 node_4968 = (_Maintex_var.rgb*_MaintexColor.rgb);
                float3 diffuseColor = lerp((_DissolveColor.rgb*node_6427.rgb),node_4968,node_4968);
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
            Cull Back
            
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
            uniform float _DissolveMax;
            uniform float _DissolveMin;
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
            float4 frag(VertexOutput i) : COLOR {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_8387 = (((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r);
                float node_6360 = 0.0;
                float node_2554 = 1.0;
                float node_8471 = saturate((_DissolveMin + ( ((1.0 - node_8387) - node_6360) * (_DissolveMax - _DissolveMin) ) / (node_2554 - node_6360)));
                clip(node_8471 - 0.5);
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
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float4 _DissolveColor;
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform float4 _MaintexColor;
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
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_8387 = (((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r);
                float node_6360 = 0.0;
                float node_2554 = 1.0;
                float node_8471 = saturate((_DissolveMin + ( ((1.0 - node_8387) - node_6360) * (_DissolveMax - _DissolveMin) ) / (node_2554 - node_6360)));
                float2 node_3868 = float2(node_8471,0.0);
                float4 node_6427 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_3868, _DissolveRamp));
                float4 _Maintex_var = tex2D(_Maintex,TRANSFORM_TEX(i.uv0, _Maintex));
                float3 node_4968 = (_Maintex_var.rgb*_MaintexColor.rgb);
                float3 diffColor = lerp((_DissolveColor.rgb*node_6427.rgb),node_4968,node_4968);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
