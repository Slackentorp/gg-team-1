// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33192,y:32622,varname:node_2865,prsc:2|emission-8922-OUT,alpha-3900-OUT;n:type:ShaderForge.SFN_Color,id:9038,x:31872,y:32677,ptovrint:False,ptlb:Maintex Color,ptin:_MaintexColor,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:2408,x:30465,y:32820,ptovrint:False,ptlb:Maintex U Speed,ptin:_MaintexUSpeed,varname:_OverlayUspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:3532,x:30465,y:32908,ptovrint:False,ptlb:Maintex V Speed,ptin:_MaintexVSpeed,varname:_OverlayVspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Append,id:9264,x:30734,y:32872,varname:node_9264,prsc:2|A-2408-OUT,B-3532-OUT;n:type:ShaderForge.SFN_TexCoord,id:4295,x:29181,y:32017,varname:node_4295,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:6146,x:31204,y:32879,varname:node_6146,prsc:2|A-4295-UVOUT,B-5284-OUT;n:type:ShaderForge.SFN_Multiply,id:5284,x:30985,y:33032,varname:node_5284,prsc:2|A-9264-OUT,B-4836-OUT;n:type:ShaderForge.SFN_Time,id:4694,x:30447,y:33113,varname:node_4694,prsc:2;n:type:ShaderForge.SFN_Slider,id:4099,x:30988,y:33487,ptovrint:False,ptlb:Fog 1 Map Intensity,ptin:_Fog1MapIntensity,varname:node_4099,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Lerp,id:7468,x:31991,y:33093,varname:node_7468,prsc:2|A-9199-OUT,B-9150-R,T-9150-R;n:type:ShaderForge.SFN_OneMinus,id:9199,x:31322,y:33488,varname:node_9199,prsc:2|IN-4099-OUT;n:type:ShaderForge.SFN_Multiply,id:6077,x:32094,y:32579,varname:node_6077,prsc:2|A-9150-RGB,B-9038-RGB;n:type:ShaderForge.SFN_Slider,id:3023,x:32032,y:33001,ptovrint:False,ptlb:Total Opacity Intensity,ptin:_TotalOpacityIntensity,varname:node_3023,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Multiply,id:7416,x:32382,y:32982,varname:node_7416,prsc:2|A-7533-OUT,B-3023-OUT;n:type:ShaderForge.SFN_Tex2d,id:9150,x:31601,y:32573,varname:node_9150,prsc:2,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False|UVIN-6146-OUT,TEX-9355-TEX;n:type:ShaderForge.SFN_Tex2d,id:1807,x:31605,y:32831,varname:node_1807,prsc:2,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False|UVIN-1652-UVOUT,TEX-9355-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:9355,x:31205,y:32535,ptovrint:False,ptlb:Maintex,ptin:_Maintex,varname:node_9355,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3831,x:32094,y:32747,varname:node_3831,prsc:2|A-1807-RGB,B-9038-RGB;n:type:ShaderForge.SFN_Lerp,id:4816,x:32323,y:32652,varname:node_4816,prsc:2|A-6077-OUT,B-3831-OUT,T-6077-OUT;n:type:ShaderForge.SFN_Multiply,id:5590,x:30734,y:33193,varname:node_5590,prsc:2|A-4694-T,B-9068-OUT;n:type:ShaderForge.SFN_Slider,id:9068,x:30290,y:33408,ptovrint:False,ptlb:Fog 2 speed,ptin:_Fog2speed,varname:node_9068,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.5,max:10;n:type:ShaderForge.SFN_Multiply,id:6740,x:30985,y:33193,varname:node_6740,prsc:2|A-9264-OUT,B-5590-OUT;n:type:ShaderForge.SFN_Add,id:1914,x:31204,y:33032,varname:node_1914,prsc:2|A-4295-UVOUT,B-6740-OUT;n:type:ShaderForge.SFN_Lerp,id:8426,x:31991,y:33227,varname:node_8426,prsc:2|A-9249-OUT,B-1807-R,T-1807-R;n:type:ShaderForge.SFN_Slider,id:3351,x:30988,y:33637,ptovrint:False,ptlb:Fog 2 Map Intensity,ptin:_Fog2MapIntensity,varname:_OpacityMap1Intensity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_OneMinus,id:9249,x:31322,y:33637,varname:node_9249,prsc:2|IN-3351-OUT;n:type:ShaderForge.SFN_Lerp,id:7533,x:32189,y:33093,varname:node_7533,prsc:2|A-7468-OUT,B-8426-OUT,T-7468-OUT;n:type:ShaderForge.SFN_Slider,id:4961,x:30290,y:33276,ptovrint:False,ptlb:Fog 1 speed,ptin:_Fog1speed,varname:_Noise2speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:10;n:type:ShaderForge.SFN_Multiply,id:4836,x:30734,y:33042,varname:node_4836,prsc:2|A-4961-OUT,B-4694-T;n:type:ShaderForge.SFN_Panner,id:1652,x:31418,y:33032,varname:node_1652,prsc:2,spu:1,spv:0|UVIN-1914-OUT,DIST-1872-OUT;n:type:ShaderForge.SFN_Slider,id:1872,x:30985,y:33359,ptovrint:False,ptlb:Fog 1 Offset,ptin:_Fog1Offset,varname:node_1872,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_VertexColor,id:1852,x:32149,y:33467,varname:node_1852,prsc:2;n:type:ShaderForge.SFN_Lerp,id:1602,x:32456,y:33269,varname:node_1602,prsc:2|A-4684-OUT,B-8915-OUT,T-1528-OUT;n:type:ShaderForge.SFN_Vector1,id:4684,x:32149,y:33396,varname:node_4684,prsc:2,v1:0;n:type:ShaderForge.SFN_OneMinus,id:1528,x:32357,y:33443,varname:node_1528,prsc:2|IN-1852-B;n:type:ShaderForge.SFN_Clamp01,id:8915,x:32382,y:33116,varname:node_8915,prsc:2|IN-7416-OUT;n:type:ShaderForge.SFN_Tex2d,id:5157,x:28680,y:32022,ptovrint:False,ptlb:Dissolve Noise,ptin:_DissolveNoise,varname:node_5424,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:54ed97f1c5b2ce54cb82c4c2c1cf007e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:8521,x:29267,y:32491,ptovrint:False,ptlb:Dissolve Amount,ptin:_DissolveAmount,varname:node_4039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:7781,x:28884,y:31928,varname:node_7781,prsc:2|A-803-OUT,B-5157-R;n:type:ShaderForge.SFN_RemapRange,id:803,x:28680,y:31820,varname:node_803,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.9|IN-3599-OUT;n:type:ShaderForge.SFN_OneMinus,id:3599,x:28518,y:31820,varname:node_3599,prsc:2|IN-8521-OUT;n:type:ShaderForge.SFN_Slider,id:8457,x:29433,y:31401,ptovrint:False,ptlb:Dissolve Max,ptin:_DissolveMax,varname:node_7359,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:5,max:10;n:type:ShaderForge.SFN_Slider,id:7200,x:29433,y:31306,ptovrint:False,ptlb:Dissolve Min,ptin:_DissolveMin,varname:node_8221,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:-5,max:10;n:type:ShaderForge.SFN_Vector1,id:8219,x:29075,y:31734,varname:node_8219,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:1473,x:29075,y:31797,varname:node_1473,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:7153,x:29075,y:31928,varname:node_7153,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:3801,x:29075,y:31861,varname:node_3801,prsc:2,v1:-2;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:3241,x:29465,y:31563,varname:node_3241,prsc:2|IN-6880-OUT,IMIN-8219-OUT,IMAX-1473-OUT,OMIN-7200-OUT,OMAX-8457-OUT;n:type:ShaderForge.SFN_Clamp01,id:922,x:30745,y:31704,varname:node_922,prsc:2|IN-496-OUT;n:type:ShaderForge.SFN_Vector1,id:1988,x:30838,y:32175,varname:node_1988,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:9506,x:31010,y:32028,varname:node_9506,prsc:2|A-922-OUT,B-1988-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:7263,x:31010,y:32175,ptovrint:False,ptlb:Dissolve Ramp,ptin:_DissolveRamp,varname:node_1715,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:19f34113455b8304cad6c4dd09377655,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:6880,x:29075,y:31602,varname:node_6880,prsc:2|IN-7781-OUT;n:type:ShaderForge.SFN_Color,id:8858,x:31932,y:32107,ptovrint:False,ptlb:Dissolve Color,ptin:_DissolveColor,varname:node_3412,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1591,x:31932,y:32256,varname:node_1591,prsc:2,tex:19f34113455b8304cad6c4dd09377655,ntxv:0,isnm:False|UVIN-9506-OUT,TEX-7263-TEX;n:type:ShaderForge.SFN_Multiply,id:4479,x:32149,y:32256,varname:node_4479,prsc:2|A-8858-RGB,B-1591-RGB;n:type:ShaderForge.SFN_Lerp,id:7646,x:32552,y:32256,varname:node_7646,prsc:2|A-4816-OUT,B-6968-OUT,T-922-OUT;n:type:ShaderForge.SFN_Lerp,id:3900,x:32826,y:33156,varname:node_3900,prsc:2|A-1602-OUT,B-4684-OUT,T-6371-OUT;n:type:ShaderForge.SFN_Multiply,id:6968,x:32324,y:32256,varname:node_6968,prsc:2|A-9403-OUT,B-4479-OUT;n:type:ShaderForge.SFN_Slider,id:9403,x:31983,y:31940,ptovrint:False,ptlb:Dissolve Color Intensity,ptin:_DissolveColorIntensity,varname:node_9403,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:10;n:type:ShaderForge.SFN_Clamp01,id:8922,x:32732,y:32256,varname:node_8922,prsc:2|IN-7646-OUT;n:type:ShaderForge.SFN_Length,id:9764,x:30050,y:31718,varname:node_9764,prsc:2|IN-9841-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:4967,x:30397,y:31840,varname:node_4967,prsc:2|IN-9764-OUT,IMIN-7216-OUT,IMAX-779-OUT,OMIN-1877-OUT,OMAX-6540-OUT;n:type:ShaderForge.SFN_Vector1,id:7216,x:30050,y:31846,varname:node_7216,prsc:2,v1:-2;n:type:ShaderForge.SFN_Vector1,id:779,x:30050,y:31908,varname:node_779,prsc:2,v1:2;n:type:ShaderForge.SFN_RemapRange,id:1877,x:30050,y:31974,varname:node_1877,prsc:2,frmn:0,frmx:1,tomn:5,tomx:15|IN-8521-OUT;n:type:ShaderForge.SFN_ComponentMask,id:6371,x:31237,y:31969,varname:node_6371,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-9506-OUT;n:type:ShaderForge.SFN_Lerp,id:496,x:30573,y:31704,varname:node_496,prsc:2|A-5141-OUT,B-1110-OUT,T-9601-OUT;n:type:ShaderForge.SFN_Clamp01,id:9601,x:30570,y:31840,varname:node_9601,prsc:2|IN-4967-OUT;n:type:ShaderForge.SFN_Clamp01,id:1110,x:29641,y:31563,varname:node_1110,prsc:2|IN-3241-OUT;n:type:ShaderForge.SFN_Vector1,id:5141,x:30361,y:31670,varname:node_5141,prsc:2,v1:0;n:type:ShaderForge.SFN_RemapRange,id:6540,x:30050,y:32149,varname:node_6540,prsc:2,frmn:0,frmx:1,tomn:-10,tomx:3|IN-8521-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5729,x:29468,y:31718,varname:node_5729,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-4295-U;n:type:ShaderForge.SFN_RemapRange,id:9730,x:29641,y:31718,varname:node_9730,prsc:2,frmn:0,frmx:1,tomn:-1.5,tomx:1.5|IN-5729-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5008,x:29468,y:31883,varname:node_5008,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-4295-V;n:type:ShaderForge.SFN_RemapRange,id:2900,x:29641,y:31883,varname:node_2900,prsc:2,frmn:0,frmx:1,tomn:-2,tomx:1|IN-5008-OUT;n:type:ShaderForge.SFN_Append,id:9841,x:29866,y:31718,varname:node_9841,prsc:2|A-9730-OUT,B-2900-OUT;proporder:9038-2408-3532-4099-3023-9355-9068-3351-4961-1872-5157-8521-8457-7200-8858-7263-9403;pass:END;sub:END;*/

Shader "DCC/Fog Wall" {
    Properties {
        _MaintexColor ("Maintex Color", Color) = (1,1,1,1)
        _MaintexUSpeed ("Maintex U Speed", Float ) = 0
        _MaintexVSpeed ("Maintex V Speed", Float ) = 0.1
        _Fog1MapIntensity ("Fog 1 Map Intensity", Range(0, 1)) = 0.5
        _TotalOpacityIntensity ("Total Opacity Intensity", Range(0, 2)) = 1
        _Maintex ("Maintex", 2D) = "white" {}
        _Fog2speed ("Fog 2 speed", Range(0, 10)) = 1.5
        _Fog2MapIntensity ("Fog 2 Map Intensity", Range(0, 1)) = 0.5
        _Fog1speed ("Fog 1 speed", Range(0, 10)) = 0.5
        _Fog1Offset ("Fog 1 Offset", Range(0, 1)) = 0.5
        _DissolveNoise ("Dissolve Noise", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0
        _DissolveMax ("Dissolve Max", Range(-10, 10)) = 5
        _DissolveMin ("Dissolve Min", Range(-10, 10)) = -5
        _DissolveColor ("Dissolve Color", Color) = (1,1,1,1)
        _DissolveRamp ("Dissolve Ramp", 2D) = "white" {}
        _DissolveColorIntensity ("Dissolve Color Intensity", Range(0, 10)) = 10
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
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
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 2.0
            uniform float4 _MaintexColor;
            uniform float _MaintexUSpeed;
            uniform float _MaintexVSpeed;
            uniform float _Fog1MapIntensity;
            uniform float _TotalOpacityIntensity;
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform float _Fog2speed;
            uniform float _Fog2MapIntensity;
            uniform float _Fog1speed;
            uniform float _Fog1Offset;
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float4 _DissolveColor;
            uniform float _DissolveColorIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_9264 = float2(_MaintexUSpeed,_MaintexVSpeed);
                float4 node_4694 = _Time;
                float2 node_6146 = (i.uv0+(node_9264*(_Fog1speed*node_4694.g)));
                float4 node_9150 = tex2D(_Maintex,TRANSFORM_TEX(node_6146, _Maintex));
                float3 node_6077 = (node_9150.rgb*_MaintexColor.rgb);
                float2 node_1652 = ((i.uv0+(node_9264*(node_4694.g*_Fog2speed)))+_Fog1Offset*float2(1,0));
                float4 node_1807 = tex2D(_Maintex,TRANSFORM_TEX(node_1652, _Maintex));
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_7781 = (((1.0 - _DissolveAmount)*1.5+-0.6)+_DissolveNoise_var.r);
                float node_8219 = 0.0;
                float node_1473 = 1.0;
                float node_7216 = (-2.0);
                float node_1877 = (_DissolveAmount*10.0+5.0);
                float node_922 = saturate(lerp(0.0,saturate((_DissolveMin + ( ((1.0 - node_7781) - node_8219) * (_DissolveMax - _DissolveMin) ) / (node_1473 - node_8219))),saturate((node_1877 + ( (length(float2((i.uv0.r.r*3.0+-1.5),(i.uv0.g.r*3.0+-2.0))) - node_7216) * ((_DissolveAmount*13.0+-10.0) - node_1877) ) / (2.0 - node_7216)))));
                float2 node_9506 = float2(node_922,0.0);
                float4 node_1591 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_9506, _DissolveRamp));
                float3 emissive = saturate(lerp(lerp(node_6077,(node_1807.rgb*_MaintexColor.rgb),node_6077),(_DissolveColorIntensity*(_DissolveColor.rgb*node_1591.rgb)),node_922));
                float3 finalColor = emissive;
                float node_4684 = 0.0;
                float node_7468 = lerp((1.0 - _Fog1MapIntensity),node_9150.r,node_9150.r);
                fixed4 finalRGBA = fixed4(finalColor,lerp(lerp(node_4684,saturate((lerp(node_7468,lerp((1.0 - _Fog2MapIntensity),node_1807.r,node_1807.r),node_7468)*_TotalOpacityIntensity)),(1.0 - i.vertexColor.b)),node_4684,node_9506.r));
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
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 2.0
            uniform float4 _MaintexColor;
            uniform float _MaintexUSpeed;
            uniform float _MaintexVSpeed;
            uniform sampler2D _Maintex; uniform float4 _Maintex_ST;
            uniform float _Fog2speed;
            uniform float _Fog1speed;
            uniform float _Fog1Offset;
            uniform sampler2D _DissolveNoise; uniform float4 _DissolveNoise_ST;
            uniform float _DissolveAmount;
            uniform float _DissolveMax;
            uniform float _DissolveMin;
            uniform sampler2D _DissolveRamp; uniform float4 _DissolveRamp_ST;
            uniform float4 _DissolveColor;
            uniform float _DissolveColorIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float2 node_9264 = float2(_MaintexUSpeed,_MaintexVSpeed);
                float4 node_4694 = _Time;
                float2 node_6146 = (i.uv0+(node_9264*(_Fog1speed*node_4694.g)));
                float4 node_9150 = tex2D(_Maintex,TRANSFORM_TEX(node_6146, _Maintex));
                float3 node_6077 = (node_9150.rgb*_MaintexColor.rgb);
                float2 node_1652 = ((i.uv0+(node_9264*(node_4694.g*_Fog2speed)))+_Fog1Offset*float2(1,0));
                float4 node_1807 = tex2D(_Maintex,TRANSFORM_TEX(node_1652, _Maintex));
                float4 _DissolveNoise_var = tex2D(_DissolveNoise,TRANSFORM_TEX(i.uv0, _DissolveNoise));
                float node_7781 = (((1.0 - _DissolveAmount)*1.5+-0.6)+_DissolveNoise_var.r);
                float node_8219 = 0.0;
                float node_1473 = 1.0;
                float node_7216 = (-2.0);
                float node_1877 = (_DissolveAmount*10.0+5.0);
                float node_922 = saturate(lerp(0.0,saturate((_DissolveMin + ( ((1.0 - node_7781) - node_8219) * (_DissolveMax - _DissolveMin) ) / (node_1473 - node_8219))),saturate((node_1877 + ( (length(float2((i.uv0.r.r*3.0+-1.5),(i.uv0.g.r*3.0+-2.0))) - node_7216) * ((_DissolveAmount*13.0+-10.0) - node_1877) ) / (2.0 - node_7216)))));
                float2 node_9506 = float2(node_922,0.0);
                float4 node_1591 = tex2D(_DissolveRamp,TRANSFORM_TEX(node_9506, _DissolveRamp));
                o.Emission = saturate(lerp(lerp(node_6077,(node_1807.rgb*_MaintexColor.rgb),node_6077),(_DissolveColorIntensity*(_DissolveColor.rgb*node_1591.rgb)),node_922));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
