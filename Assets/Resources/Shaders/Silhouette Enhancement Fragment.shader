// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32899,y:32747,varname:node_4013,prsc:2|diff-9538-OUT,spec-177-OUT,gloss-3533-OUT,normal-9320-OUT,emission-7434-OUT;n:type:ShaderForge.SFN_Multiply,id:191,x:32142,y:32600,varname:node_191,prsc:2|A-2589-RGB,B-7243-OUT,C-8352-OUT;n:type:ShaderForge.SFN_Color,id:2589,x:31949,y:32600,ptovrint:False,ptlb:Fresnel Color,ptin:_FresnelColor,varname:node_2589,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:7243,x:31949,y:32752,varname:node_7243,prsc:2;n:type:ShaderForge.SFN_Add,id:9538,x:32433,y:32582,varname:node_9538,prsc:2|A-6507-OUT,B-191-OUT;n:type:ShaderForge.SFN_Slider,id:8352,x:31792,y:32903,ptovrint:False,ptlb:Silhouette Intensity,ptin:_SilhouetteIntensity,varname:node_8352,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:6507,x:32142,y:32339,varname:node_6507,prsc:2|A-4572-RGB,B-6816-RGB;n:type:ShaderForge.SFN_Color,id:6816,x:31949,y:32432,ptovrint:False,ptlb:Maintex Color,ptin:_MaintexColor,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:4572,x:31949,y:32247,ptovrint:True,ptlb:Maintex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:177,x:32276,y:32799,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:3533,x:32276,y:32901,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:4118,x:32276,y:33149,ptovrint:False,ptlb:Maintex Normal Intensity,ptin:_MaintexNormalIntensity,varname:node_9862,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:9320,x:32433,y:32992,varname:node_9320,prsc:2|A-6272-OUT,B-585-RGB,T-4118-OUT;n:type:ShaderForge.SFN_Vector3,id:6272,x:31947,y:32984,varname:node_6272,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Tex2d,id:5157,x:31893,y:33320,ptovrint:False,ptlb:Maintex Emission Map,ptin:_MaintexEmissionMap,varname:node_2964,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:585,x:31893,y:33122,ptovrint:False,ptlb:Maintex Normal Map,ptin:_MaintexNormalMap,varname:node_7940,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Lerp,id:7434,x:32634,y:33293,varname:node_7434,prsc:2|A-2305-OUT,B-442-OUT,T-9260-OUT;n:type:ShaderForge.SFN_Slider,id:9260,x:31736,y:33676,ptovrint:False,ptlb:Maintex Emission Intensity,ptin:_MaintexEmissionIntensity,varname:node_3275,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector3,id:2305,x:32098,y:33245,varname:node_2305,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Color,id:8344,x:31893,y:33503,ptovrint:False,ptlb:maintex Emission Color,ptin:_maintexEmissionColor,varname:node_8344,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:832,x:32098,y:33337,varname:node_832,prsc:2|A-5157-RGB,B-8344-RGB;n:type:ShaderForge.SFN_TexCoord,id:8706,x:31303,y:33940,varname:node_8706,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:4093,x:30942,y:33946,varname:node_4093,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:6899,x:31697,y:34061,ptovrint:False,ptlb:Emission Noise,ptin:_EmissionNoise,varname:node_876,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False|UVIN-9343-OUT;n:type:ShaderForge.SFN_Slider,id:1930,x:31540,y:34267,ptovrint:False,ptlb:Emission Noise Intensity,ptin:_EmissionNoiseIntensity,varname:node_9917,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Multiply,id:4050,x:31127,y:34101,varname:node_4050,prsc:2|A-4093-T,B-9520-OUT;n:type:ShaderForge.SFN_Slider,id:9097,x:30586,y:34129,ptovrint:False,ptlb:Emission Noise U,ptin:_EmissionNoiseU,varname:_NoiseSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:1979,x:30586,y:34233,ptovrint:False,ptlb:Emission Noise V,ptin:_EmissionNoiseV,varname:_NoiseSpeed_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:9343,x:31498,y:34061,varname:node_9343,prsc:2|A-8706-UVOUT,B-7996-OUT;n:type:ShaderForge.SFN_Append,id:9520,x:30942,y:34091,varname:node_9520,prsc:2|A-9097-OUT,B-1979-OUT;n:type:ShaderForge.SFN_Multiply,id:7996,x:31303,y:34101,varname:node_7996,prsc:2|A-4050-OUT,B-3009-OUT;n:type:ShaderForge.SFN_Slider,id:3009,x:30970,y:34263,ptovrint:False,ptlb:Emission Noise Speed,ptin:_EmissionNoiseSpeed,varname:node_3757,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1,max:1;n:type:ShaderForge.SFN_Multiply,id:977,x:31917,y:34092,varname:node_977,prsc:2|A-6899-RGB,B-1930-OUT;n:type:ShaderForge.SFN_Lerp,id:5060,x:32331,y:33677,varname:node_5060,prsc:2|A-832-OUT,B-536-OUT,T-977-OUT;n:type:ShaderForge.SFN_Vector1,id:536,x:32098,y:33461,varname:node_536,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:7642,x:31749,y:35010,ptovrint:False,ptlb:Dissolve Noise,ptin:_DissolveNoise,varname:node_5424,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:7269,x:31254,y:34808,ptovrint:False,ptlb:Dissolve Amount,ptin:_DissolveAmount,varname:node_4039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:1597,x:31953,y:34916,varname:node_1597,prsc:2|A-4026-OUT,B-7642-R;n:type:ShaderForge.SFN_RemapRange,id:4026,x:31749,y:34808,varname:node_4026,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.6|IN-830-OUT;n:type:ShaderForge.SFN_OneMinus,id:830,x:31587,y:34808,varname:node_830,prsc:2|IN-7269-OUT;n:type:ShaderForge.SFN_Clamp01,id:4070,x:31929,y:34527,varname:node_4070,prsc:2|IN-8342-OUT;n:type:ShaderForge.SFN_Vector1,id:2221,x:32101,y:34674,varname:node_2221,prsc:2,v1:0;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:8342,x:31755,y:34527,varname:node_8342,prsc:2|IN-1597-OUT,IMIN-9773-OUT,IMAX-7652-OUT,OMIN-2674-OUT,OMAX-4648-OUT;n:type:ShaderForge.SFN_Slider,id:4648,x:31215,y:34628,ptovrint:False,ptlb:Dissolve Max,ptin:_DissolveMax,varname:node_7359,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:4,max:10;n:type:ShaderForge.SFN_Slider,id:2674,x:31215,y:34533,ptovrint:False,ptlb:Dissolve Min,ptin:_DissolveMin,varname:node_8221,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:-4,max:10;n:type:ShaderForge.SFN_Vector1,id:9773,x:31372,y:34389,varname:node_9773,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:7652,x:31372,y:34452,varname:node_7652,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2dAsset,id:9689,x:32273,y:34704,ptovrint:False,ptlb:Dissolve Ramp,ptin:_DissolveRamp,varname:node_4088,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:19f34113455b8304cad6c4dd09377655,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:568,x:32273,y:34527,varname:node_568,prsc:2|A-4070-OUT,B-2221-OUT;n:type:ShaderForge.SFN_Tex2d,id:6382,x:32540,y:34590,varname:node_1196,prsc:2,tex:19f34113455b8304cad6c4dd09377655,ntxv:0,isnm:False|UVIN-568-OUT,TEX-9689-TEX;n:type:ShaderForge.SFN_Lerp,id:442,x:32637,y:33696,varname:node_442,prsc:2|A-2305-OUT,B-5060-OUT,T-6382-RGB;proporder:2589-8352-6816-4572-177-3533-4118-585-5157-8344-9260-6899-1930-3009-9097-1979-9689-7642-7269-4648-2674;pass:END;sub:END;*/

Shader "DCC/Silhouette Enhancement Fragment" {
    Properties {
        _FresnelColor ("Fresnel Color", Color) = (0,1,1,1)
        _SilhouetteIntensity ("Silhouette Intensity", Range(0, 10)) = 1
        _MaintexColor ("Maintex Color", Color) = (1,1,1,1)
        _MainTex ("Maintex", 2D) = "white" {}
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        _MaintexNormalIntensity ("Maintex Normal Intensity", Range(0, 1)) = 0
        _MaintexNormalMap ("Maintex Normal Map", 2D) = "bump" {}
        _MaintexEmissionMap ("Maintex Emission Map", 2D) = "white" {}
        _maintexEmissionColor ("maintex Emission Color", Color) = (0,1,1,1)
        _MaintexEmissionIntensity ("Maintex Emission Intensity", Range(0, 1)) = 0
        _EmissionNoise ("Emission Noise", 2D) = "white" {}
        _EmissionNoiseIntensity ("Emission Noise Intensity", Range(0, 1)) = 0.5
        _EmissionNoiseSpeed ("Emission Noise Speed", Range(0, 1)) = 0.1
        _EmissionNoiseU ("Emission Noise U", Range(-1, 1)) = 1
        _EmissionNoiseV ("Emission Noise V", Range(-1, 1)) = 1
        _DissolveRamp ("Dissolve Ramp", 2D) = "white" {}
        _DissolveNoise ("Dissolve Noise", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 1
        _DissolveMax ("Dissolve Max", Range(-10, 10)) = 4
        _DissolveMin ("Dissolve Min", Range(-10, 10)) = -4
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 2.0
            uniform float4 _FresnelColor;
            uniform float _SilhouetteIntensity;
            uniform float4 _MaintexColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float _MaintexNormalIntensity;
            uniform sampler2D _MaintexEmissionMap; uniform float4 _MaintexEmissionMap_ST;
            uniform sampler2D _MaintexNormalMap; uniform float4 _MaintexNormalMap_ST;
            uniform float _MaintexEmissionIntensity;
            uniform float4 _maintexEmissionColor;
            uniform sampler2D _EmissionNoise; uniform float4 _EmissionNoise_ST;
            uniform float _EmissionNoiseIntensity;
            uniform float _EmissionNoiseU;
            uniform float _EmissionNoiseV;
            uniform float _EmissionNoiseSpeed;
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
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
                float3 _MaintexNormalMap_var = UnpackNormal(tex2D(_MaintexNormalMap,TRANSFORM_TEX(i.uv0, _MaintexNormalMap)));
                float3 normalLocal = lerp(float3(0,0,1),_MaintexNormalMap_var.rgb,_MaintexNormalIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
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
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuseColor = ((_MainTex_var.rgb*_MaintexColor.rgb)+(_FresnelColor.rgb*(1.0-max(0,dot(normalDirection, viewDirection)))*_SilhouetteIntensity)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 node_2305 = float3(0,0,0);
                float4 _MaintexEmissionMap_var = tex2D(_MaintexEmissionMap,TRANSFORM_TEX(i.uv0, _MaintexEmissionMap));
                float node_536 = 0.0;
                float4 node_4093 = _Time;
                float2 node_9343 = (i.uv0+((node_4093.g*float2(_EmissionNoiseU,_EmissionNoiseV))*_EmissionNoiseSpeed));
                float4 _EmissionNoise_var = tex2D(_EmissionNoise,TRANSFORM_TEX(node_9343, _EmissionNoise));
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_9773 = 0.0;
                float2 node_568 = float2(saturate((_DissolveMin + ( ((((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r) - node_9773) * (_DissolveMax - _DissolveMin) ) / (1.0 - node_9773))),0.0);
                float4 node_1196 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_568, _DissolveRamp));
                float3 emissive = lerp(node_2305,lerp(node_2305,lerp((_MaintexEmissionMap_var.rgb*_maintexEmissionColor.rgb),float3(node_536,node_536,node_536),(_EmissionNoise_var.rgb*_EmissionNoiseIntensity)),node_1196.rgb),_MaintexEmissionIntensity);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 2.0
            uniform float4 _FresnelColor;
            uniform float _SilhouetteIntensity;
            uniform float4 _MaintexColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float _MaintexNormalIntensity;
            uniform sampler2D _MaintexEmissionMap; uniform float4 _MaintexEmissionMap_ST;
            uniform sampler2D _MaintexNormalMap; uniform float4 _MaintexNormalMap_ST;
            uniform float _MaintexEmissionIntensity;
            uniform float4 _maintexEmissionColor;
            uniform sampler2D _EmissionNoise; uniform float4 _EmissionNoise_ST;
            uniform float _EmissionNoiseIntensity;
            uniform float _EmissionNoiseU;
            uniform float _EmissionNoiseV;
            uniform float _EmissionNoiseSpeed;
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
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
                float3 _MaintexNormalMap_var = UnpackNormal(tex2D(_MaintexNormalMap,TRANSFORM_TEX(i.uv0, _MaintexNormalMap)));
                float3 normalLocal = lerp(float3(0,0,1),_MaintexNormalMap_var.rgb,_MaintexNormalIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuseColor = ((_MainTex_var.rgb*_MaintexColor.rgb)+(_FresnelColor.rgb*(1.0-max(0,dot(normalDirection, viewDirection)))*_SilhouetteIntensity)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 2.0
            uniform float4 _FresnelColor;
            uniform float _SilhouetteIntensity;
            uniform float4 _MaintexColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _MaintexEmissionMap; uniform float4 _MaintexEmissionMap_ST;
            uniform float _MaintexEmissionIntensity;
            uniform float4 _maintexEmissionColor;
            uniform sampler2D _EmissionNoise; uniform float4 _EmissionNoise_ST;
            uniform float _EmissionNoiseIntensity;
            uniform float _EmissionNoiseU;
            uniform float _EmissionNoiseV;
            uniform float _EmissionNoiseSpeed;
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float3 node_2305 = float3(0,0,0);
                float4 _MaintexEmissionMap_var = tex2D(_MaintexEmissionMap,TRANSFORM_TEX(i.uv0, _MaintexEmissionMap));
                float node_536 = 0.0;
                float4 node_4093 = _Time;
                float2 node_9343 = (i.uv0+((node_4093.g*float2(_EmissionNoiseU,_EmissionNoiseV))*_EmissionNoiseSpeed));
                float4 _EmissionNoise_var = tex2D(_EmissionNoise,TRANSFORM_TEX(node_9343, _EmissionNoise));
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_9773 = 0.0;
                float2 node_568 = float2(saturate((_DissolveMin + ( ((((1.0 - _DissolveAmount)*1.2+-0.6)+_DissolveNoise_var.r) - node_9773) * (_DissolveMax - _DissolveMin) ) / (1.0 - node_9773))),0.0);
                float4 node_1196 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_568, _DissolveRamp));
                o.Emission = lerp(node_2305,lerp(node_2305,lerp((_MaintexEmissionMap_var.rgb*_maintexEmissionColor.rgb),float3(node_536,node_536,node_536),(_EmissionNoise_var.rgb*_EmissionNoiseIntensity)),node_1196.rgb),_MaintexEmissionIntensity);
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffColor = ((_MainTex_var.rgb*_MaintexColor.rgb)+(_FresnelColor.rgb*(1.0-max(0,dot(normalDirection, viewDirection)))*_SilhouetteIntensity));
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metallic, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
