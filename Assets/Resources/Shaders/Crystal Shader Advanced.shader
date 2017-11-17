// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32978,y:32411,varname:node_2865,prsc:2|diff-516-OUT,spec-798-OUT,gloss-1813-OUT,normal-2332-OUT,emission-5160-OUT,alpha-4613-OUT,refract-7881-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:31642,y:32104,varname:node_6343,prsc:2|A-7736-RGB,B-9520-OUT;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31448,y:32087,ptovrint:True,ptlb:Maintex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:1813,x:32380,y:32372,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_Slider,id:3681,x:31885,y:33231,ptovrint:False,ptlb:Maintex Refraction Intensity,ptin:_MaintexRefractionIntensity,varname:_RefractionIntensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6,max:1;n:type:ShaderForge.SFN_Multiply,id:7881,x:32607,y:33258,varname:node_7881,prsc:2|A-5909-OUT,B-6077-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5909,x:32403,y:33182,varname:node_5909,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2359-RGB;n:type:ShaderForge.SFN_Tex2d,id:2359,x:32042,y:33045,ptovrint:False,ptlb:Maintex Normal,ptin:_MaintexNormal,varname:_Refraction,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ba2cf90a8e40c0c4f9e9c35acf52acaf,ntxv:3,isnm:True|UVIN-1807-OUT;n:type:ShaderForge.SFN_TexCoord,id:4920,x:31684,y:32984,varname:node_4920,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:1807,x:31855,y:33045,varname:node_1807,prsc:2|A-4920-UVOUT,B-1736-OUT;n:type:ShaderForge.SFN_Vector1,id:1736,x:31684,y:33141,varname:node_1736,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:798,x:32537,y:32281,varname:node_798,prsc:2,v1:0;n:type:ShaderForge.SFN_Lerp,id:2332,x:32450,y:32929,varname:node_2332,prsc:2|A-4275-OUT,B-2359-RGB,T-3681-OUT;n:type:ShaderForge.SFN_Vector3,id:4275,x:32042,y:32924,varname:node_4275,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Multiply,id:6077,x:32403,y:33334,varname:node_6077,prsc:2|A-3681-OUT,B-5011-OUT;n:type:ShaderForge.SFN_Vector1,id:5011,x:32042,y:33310,varname:node_5011,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Slider,id:5069,x:32362,y:33661,ptovrint:False,ptlb:Opacity Intensity,ptin:_OpacityIntensity,varname:node_5069,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Tex2d,id:8902,x:31488,y:32480,ptovrint:False,ptlb:Maintex Emission Map,ptin:_MaintexEmissionMap,varname:node_2964,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:8572,x:32459,y:32553,varname:node_8572,prsc:2|A-5661-OUT,B-8493-OUT,T-548-OUT;n:type:ShaderForge.SFN_Slider,id:2279,x:31694,y:32712,ptovrint:False,ptlb:Emission Intensity Maintex,ptin:_EmissionIntensityMaintex,varname:node_3275,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_Vector3,id:5661,x:32046,y:32470,varname:node_5661,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Color,id:8911,x:31488,y:32671,ptovrint:False,ptlb:Maintex Emission Color,ptin:_MaintexEmissionColor,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:8493,x:32046,y:32578,varname:node_8493,prsc:2|A-7814-OUT,B-9623-OUT;n:type:ShaderForge.SFN_RemapRange,id:5960,x:32519,y:33764,varname:node_5960,prsc:2,frmn:0,frmx:1,tomn:1,tomx:0|IN-3907-OUT;n:type:ShaderForge.SFN_Multiply,id:4613,x:32874,y:33629,varname:node_4613,prsc:2|A-5069-OUT,B-4394-OUT;n:type:ShaderForge.SFN_Multiply,id:3907,x:32341,y:33764,varname:node_3907,prsc:2|A-3042-OUT,B-1201-OUT;n:type:ShaderForge.SFN_TexCoord,id:6383,x:30996,y:32514,varname:node_6383,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:9520,x:31448,y:32264,varname:node_9520,prsc:2|A-863-RGB,B-9487-RGB,T-1765-OUT;n:type:ShaderForge.SFN_Color,id:863,x:31157,y:32005,ptovrint:False,ptlb:Gradient Color A,ptin:_GradientColorA,varname:node_863,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:9487,x:31157,y:32191,ptovrint:False,ptlb:Gradient Color B,ptin:_GradientColorB,varname:node_9487,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:1765,x:31236,y:32897,varname:node_1765,prsc:2|IN-6383-V,IMIN-9740-OUT,IMAX-7528-OUT,OMIN-1457-OUT,OMAX-7039-OUT;n:type:ShaderForge.SFN_Slider,id:9740,x:30839,y:32683,ptovrint:False,ptlb:Min Color Gradient Input,ptin:_MinColorGradientInput,varname:node_9740,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:7528,x:30839,y:32780,ptovrint:False,ptlb:Max Color Gradient Input,ptin:_MaxColorGradientInput,varname:node_7528,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:7039,x:30839,y:32979,ptovrint:False,ptlb:Max Color Gradient Output,ptin:_MaxColorGradientOutput,varname:node_7039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:1457,x:30839,y:32881,ptovrint:False,ptlb:Min Color Gradient Output,ptin:_MinColorGradientOutput,varname:node_1457,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:9582,x:32175,y:32052,ptovrint:False,ptlb:Maintex Overlay,ptin:_MaintexOverlay,varname:node_9582,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Clamp01,id:7150,x:32537,y:32131,varname:node_7150,prsc:2|IN-8559-OUT;n:type:ShaderForge.SFN_Add,id:8559,x:32347,y:32131,varname:node_8559,prsc:2|A-9582-A,B-4055-OUT;n:type:ShaderForge.SFN_Slider,id:254,x:31858,y:32227,ptovrint:False,ptlb:Maintex Overlay Opacity,ptin:_MaintexOverlayOpacity,varname:node_3676,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:4055,x:32175,y:32227,varname:node_4055,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:0|IN-254-OUT;n:type:ShaderForge.SFN_Lerp,id:516,x:32529,y:31918,varname:node_516,prsc:2|A-6343-OUT,B-9582-RGB,T-7150-OUT;n:type:ShaderForge.SFN_Clamp01,id:7814,x:31642,y:32264,varname:node_7814,prsc:2|IN-9520-OUT;n:type:ShaderForge.SFN_Multiply,id:548,x:32046,y:32770,varname:node_548,prsc:2|A-2279-OUT,B-1765-OUT;n:type:ShaderForge.SFN_Multiply,id:9623,x:31694,y:32564,varname:node_9623,prsc:2|A-8902-RGB,B-8911-RGB;n:type:ShaderForge.SFN_TexCoord,id:3583,x:31932,y:33561,varname:node_3583,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:3042,x:32152,y:33764,varname:node_3042,prsc:2|IN-3583-V,IMIN-5786-OUT,IMAX-3519-OUT,OMIN-4090-OUT,OMAX-5536-OUT;n:type:ShaderForge.SFN_Slider,id:5786,x:31775,y:33730,ptovrint:False,ptlb:Min Opacity Gradient Input,ptin:_MinOpacityGradientInput,varname:_MinColorGradientInput_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:3519,x:31775,y:33827,ptovrint:False,ptlb:Max Opacity Gradient Input,ptin:_MaxOpacityGradientInput,varname:_MaxColorGradientInput_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:5536,x:31775,y:34026,ptovrint:False,ptlb:Max Opacity Gradient Output,ptin:_MaxOpacityGradientOutput,varname:_MaxColorGradientOutput_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4319745,max:1;n:type:ShaderForge.SFN_Slider,id:4090,x:31775,y:33928,ptovrint:False,ptlb:Min Opacity Gradient Output,ptin:_MinOpacityGradientOutput,varname:_MinColorGradientOutput_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Clamp01,id:5160,x:32647,y:32553,varname:node_5160,prsc:2|IN-8572-OUT;n:type:ShaderForge.SFN_Clamp01,id:4394,x:32688,y:33764,varname:node_4394,prsc:2|IN-5960-OUT;n:type:ShaderForge.SFN_Slider,id:4950,x:31779,y:34159,ptovrint:False,ptlb:Maintex Opacity Modifier,ptin:_MaintexOpacityModifier,varname:node_9390,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_RemapRange,id:1201,x:32116,y:34156,varname:node_1201,prsc:2,frmn:0,frmx:1,tomn:1,tomx:0|IN-4950-OUT;proporder:9582-254-863-9487-9740-7528-7039-1457-7736-1813-2359-3681-8911-8902-2279-5786-3519-5536-4090-5069-4950;pass:END;sub:END;*/

Shader "DCC/Crystal Shader Advanced" {
    Properties {
        _MaintexOverlay ("Maintex Overlay", 2D) = "black" {}
        _MaintexOverlayOpacity ("Maintex Overlay Opacity", Range(0, 1)) = 1
        _GradientColorA ("Gradient Color A", Color) = (0,1,1,1)
        _GradientColorB ("Gradient Color B", Color) = (1,1,1,1)
        _MinColorGradientInput ("Min Color Gradient Input", Range(0, 1)) = 0
        _MaxColorGradientInput ("Max Color Gradient Input", Range(0, 1)) = 1
        _MaxColorGradientOutput ("Max Color Gradient Output", Range(0, 1)) = 1
        _MinColorGradientOutput ("Min Color Gradient Output", Range(0, 1)) = 0
        _MainTex ("Maintex", 2D) = "white" {}
        _Gloss ("Gloss", Range(0, 1)) = 0.8
        _MaintexNormal ("Maintex Normal", 2D) = "bump" {}
        _MaintexRefractionIntensity ("Maintex Refraction Intensity", Range(0, 1)) = 0.6
        _MaintexEmissionColor ("Maintex Emission Color", Color) = (1,1,1,1)
        _MaintexEmissionMap ("Maintex Emission Map", 2D) = "white" {}
        _EmissionIntensityMaintex ("Emission Intensity Maintex", Range(0, 1)) = 0.8
        _MinOpacityGradientInput ("Min Opacity Gradient Input", Range(0, 1)) = 0
        _MaxOpacityGradientInput ("Max Opacity Gradient Input", Range(0, 1)) = 1
        _MaxOpacityGradientOutput ("Max Opacity Gradient Output", Range(0, 1)) = 0.4319745
        _MinOpacityGradientOutput ("Min Opacity Gradient Output", Range(0, 1)) = 0
        _OpacityIntensity ("Opacity Intensity", Range(0, 1)) = 0.5
        _MaintexOpacityModifier ("Maintex Opacity Modifier", Range(0, 1)) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 n3ds wiiu 
            #pragma target 2.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Gloss;
            uniform float _MaintexRefractionIntensity;
            uniform sampler2D _MaintexNormal; uniform float4 _MaintexNormal_ST;
            uniform float _OpacityIntensity;
            uniform sampler2D _MaintexEmissionMap; uniform float4 _MaintexEmissionMap_ST;
            uniform float _EmissionIntensityMaintex;
            uniform float4 _MaintexEmissionColor;
            uniform float4 _GradientColorA;
            uniform float4 _GradientColorB;
            uniform float _MinColorGradientInput;
            uniform float _MaxColorGradientInput;
            uniform float _MaxColorGradientOutput;
            uniform float _MinColorGradientOutput;
            uniform sampler2D _MaintexOverlay; uniform float4 _MaintexOverlay_ST;
            uniform float _MaintexOverlayOpacity;
            uniform float _MinOpacityGradientInput;
            uniform float _MaxOpacityGradientInput;
            uniform float _MaxOpacityGradientOutput;
            uniform float _MinOpacityGradientOutput;
            uniform float _MaintexOpacityModifier;
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
                float4 screenPos : TEXCOORD7;
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
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
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_1807 = (i.uv0*1.0);
                float3 _MaintexNormal_var = UnpackNormal(tex2D(_MaintexNormal,TRANSFORM_TEX(node_1807, _MaintexNormal)));
                float3 normalLocal = lerp(float3(0,0,1),_MaintexNormal_var.rgb,_MaintexRefractionIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_MaintexNormal_var.rgb.rg*(_MaintexRefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
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
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = 0.0;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_1765 = (_MinColorGradientOutput + ( (i.uv0.g - _MinColorGradientInput) * (_MaxColorGradientOutput - _MinColorGradientOutput) ) / (_MaxColorGradientInput - _MinColorGradientInput));
                float3 node_9520 = lerp(_GradientColorA.rgb,_GradientColorB.rgb,node_1765);
                float4 _MaintexOverlay_var = tex2D(_MaintexOverlay,TRANSFORM_TEX(i.uv0, _MaintexOverlay));
                float3 diffuseColor = lerp((_MainTex_var.rgb*node_9520),_MaintexOverlay_var.rgb,saturate((_MaintexOverlay_var.a+(_MaintexOverlayOpacity*1.0+-1.0)))); // Need this for specular when using metallic
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
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
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
                float4 _MaintexEmissionMap_var = tex2D(_MaintexEmissionMap,TRANSFORM_TEX(i.uv0, _MaintexEmissionMap));
                float3 emissive = saturate(lerp(float3(0,0,0),(saturate(node_9520)*(_MaintexEmissionMap_var.rgb*_MaintexEmissionColor.rgb)),(_EmissionIntensityMaintex*node_1765)));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,(_OpacityIntensity*saturate((((_MinOpacityGradientOutput + ( (i.uv0.g - _MinOpacityGradientInput) * (_MaxOpacityGradientOutput - _MinOpacityGradientOutput) ) / (_MaxOpacityGradientInput - _MinOpacityGradientInput))*(_MaintexOpacityModifier*-1.0+1.0))*-1.0+1.0)))),1);
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 n3ds wiiu 
            #pragma target 2.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Gloss;
            uniform float _MaintexRefractionIntensity;
            uniform sampler2D _MaintexNormal; uniform float4 _MaintexNormal_ST;
            uniform float _OpacityIntensity;
            uniform sampler2D _MaintexEmissionMap; uniform float4 _MaintexEmissionMap_ST;
            uniform float _EmissionIntensityMaintex;
            uniform float4 _MaintexEmissionColor;
            uniform float4 _GradientColorA;
            uniform float4 _GradientColorB;
            uniform float _MinColorGradientInput;
            uniform float _MaxColorGradientInput;
            uniform float _MaxColorGradientOutput;
            uniform float _MinColorGradientOutput;
            uniform sampler2D _MaintexOverlay; uniform float4 _MaintexOverlay_ST;
            uniform float _MaintexOverlayOpacity;
            uniform float _MinOpacityGradientInput;
            uniform float _MaxOpacityGradientInput;
            uniform float _MaxOpacityGradientOutput;
            uniform float _MinOpacityGradientOutput;
            uniform float _MaintexOpacityModifier;
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
                float4 screenPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
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
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_1807 = (i.uv0*1.0);
                float3 _MaintexNormal_var = UnpackNormal(tex2D(_MaintexNormal,TRANSFORM_TEX(node_1807, _MaintexNormal)));
                float3 normalLocal = lerp(float3(0,0,1),_MaintexNormal_var.rgb,_MaintexRefractionIntensity);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (_MaintexNormal_var.rgb.rg*(_MaintexRefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
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
                float3 specularColor = 0.0;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_1765 = (_MinColorGradientOutput + ( (i.uv0.g - _MinColorGradientInput) * (_MaxColorGradientOutput - _MinColorGradientOutput) ) / (_MaxColorGradientInput - _MinColorGradientInput));
                float3 node_9520 = lerp(_GradientColorA.rgb,_GradientColorB.rgb,node_1765);
                float4 _MaintexOverlay_var = tex2D(_MaintexOverlay,TRANSFORM_TEX(i.uv0, _MaintexOverlay));
                float3 diffuseColor = lerp((_MainTex_var.rgb*node_9520),_MaintexOverlay_var.rgb,saturate((_MaintexOverlay_var.a+(_MaintexOverlayOpacity*1.0+-1.0)))); // Need this for specular when using metallic
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
                fixed4 finalRGBA = fixed4(finalColor * (_OpacityIntensity*saturate((((_MinOpacityGradientOutput + ( (i.uv0.g - _MinOpacityGradientInput) * (_MaxOpacityGradientOutput - _MinOpacityGradientOutput) ) / (_MaxOpacityGradientInput - _MinOpacityGradientInput))*(_MaintexOpacityModifier*-1.0+1.0))*-1.0+1.0))),0);
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
            #define _GLOSSYENV 1
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 n3ds wiiu 
            #pragma target 2.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Gloss;
            uniform sampler2D _MaintexEmissionMap; uniform float4 _MaintexEmissionMap_ST;
            uniform float _EmissionIntensityMaintex;
            uniform float4 _MaintexEmissionColor;
            uniform float4 _GradientColorA;
            uniform float4 _GradientColorB;
            uniform float _MinColorGradientInput;
            uniform float _MaxColorGradientInput;
            uniform float _MaxColorGradientOutput;
            uniform float _MinColorGradientOutput;
            uniform sampler2D _MaintexOverlay; uniform float4 _MaintexOverlay_ST;
            uniform float _MaintexOverlayOpacity;
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
                
                float node_1765 = (_MinColorGradientOutput + ( (i.uv0.g - _MinColorGradientInput) * (_MaxColorGradientOutput - _MinColorGradientOutput) ) / (_MaxColorGradientInput - _MinColorGradientInput));
                float3 node_9520 = lerp(_GradientColorA.rgb,_GradientColorB.rgb,node_1765);
                float4 _MaintexEmissionMap_var = tex2D(_MaintexEmissionMap,TRANSFORM_TEX(i.uv0, _MaintexEmissionMap));
                o.Emission = saturate(lerp(float3(0,0,0),(saturate(node_9520)*(_MaintexEmissionMap_var.rgb*_MaintexEmissionColor.rgb)),(_EmissionIntensityMaintex*node_1765)));
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _MaintexOverlay_var = tex2D(_MaintexOverlay,TRANSFORM_TEX(i.uv0, _MaintexOverlay));
                float3 diffColor = lerp((_MainTex_var.rgb*node_9520),_MaintexOverlay_var.rgb,saturate((_MaintexOverlay_var.a+(_MaintexOverlayOpacity*1.0+-1.0))));
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0.0, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
