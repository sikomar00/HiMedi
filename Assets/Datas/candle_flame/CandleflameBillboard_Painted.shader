// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EdShaders/CandleFlameBillboard_Painted"
{
	Properties
	{
		_BrightnessMultiplier("Brightness Multiplier", Float) = 1
		_Noise("Noise", 2D) = "black" {}
		_WindStrength("Wind Strength", Range( 0 , 6)) = 1
		_FlameFlickerSpeed("Flame Flicker Speed", Float) = 0
		_NoiseScale("Noise Scale", Float) = 1
		_Flame("Flame", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Overlay"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		Blend One One
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nometa noforwardadd vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _BrightnessMultiplier;
		uniform sampler2D _Flame;
		uniform sampler2D _Noise;
		uniform float _FlameFlickerSpeed;
		uniform float _NoiseScale;
		uniform float4 _Noise_ST;
		uniform float _WindStrength;
		uniform float4 _Flame_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			//Calculate new billboard vertex position and normal;
			float3 upCamVec = float3( 0, 1, 0 );
			float3 forwardCamVec = -normalize ( UNITY_MATRIX_V._m20_m21_m22 );
			float3 rightCamVec = normalize( UNITY_MATRIX_V._m00_m01_m02 );
			float4x4 rotationCamMatrix = float4x4( rightCamVec, 0, upCamVec, 0, forwardCamVec, 0, 0, 0, 0, 1 );
			v.normal = normalize( mul( float4( v.normal , 0 ), rotationCamMatrix ));
			v.vertex.x *= length( unity_ObjectToWorld._m00_m10_m20 );
			v.vertex.y *= length( unity_ObjectToWorld._m01_m11_m21 );
			v.vertex.z *= length( unity_ObjectToWorld._m02_m12_m22 );
			v.vertex = mul( v.vertex, rotationCamMatrix );
			v.vertex.xyz += unity_ObjectToWorld._m03_m13_m23;
			//Need to nullify rotation inserted by generated surface shader;
			v.vertex = mul( unity_WorldToObject, v.vertex );
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult54 = (float2(-_FlameFlickerSpeed , -_FlameFlickerSpeed));
			float3 ase_worldPos = i.worldPos;
			float2 appendResult51 = (float2(ase_worldPos.x , ase_worldPos.y));
			float2 uv_Noise = i.uv_texcoord * _Noise_ST.xy + _Noise_ST.zw;
			float2 panner93 = ( 1.0 * _Time.y * appendResult54 + ( ( appendResult51 * 0.1 ) + ( _NoiseScale * uv_Noise ) ));
			float2 uv_Flame = i.uv_texcoord * _Flame_ST.xy + _Flame_ST.zw;
			o.Emission = ( _BrightnessMultiplier * tex2D( _Flame, ( i.uv_texcoord + ( i.uv_texcoord.y * i.uv_texcoord.y * ( (tex2D( _Noise, panner93 )).rg - float2( 0.5,0.5 ) ) * _WindStrength ) ) ) * tex2D( _Flame, uv_Flame ).a ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
2567;29;1906;1014;3574.75;817.8156;1.621037;True;False
Node;AmplifyShaderEditor.CommentaryNode;21;-3460.047,-328.2268;Float;False;1741.255;731.4139;;18;100;102;95;97;29;98;28;93;52;54;41;38;107;25;94;39;106;60;Flame UV Movement;0.4338235,1,0.5548681,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;60;-3377.918,-239.1424;Float;False;706.9712;210.6665;World Space Variation;4;104;105;51;49;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;49;-3349.832,-189.4632;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;105;-3011.731,-101.5707;Float;False;Constant;_Float0;Float 0;14;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;51;-3161.999,-166.4515;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-2844.029,-170.4705;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;94;-3362.715,109.3148;Float;False;0;28;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;-3370.997,248.9985;Float;False;Property;_FlameFlickerSpeed;Flame Flicker Speed;5;0;Create;True;0;0;False;0;0;0.28;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;106;-2618.089,-44.24372;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-3263.208,15.84671;Float;False;Property;_NoiseScale;Noise Scale;6;0;Create;True;0;0;False;0;1;0.06;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;41;-3142.037,253.7603;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-3108.897,107.4705;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;107;-2946.325,-3.500575;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-2922.119,33.70701;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;54;-2986.484,234.6737;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;93;-2832.114,150.7149;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;28;-2658.014,145.9267;Float;True;Property;_Noise;Noise;3;0;Create;True;0;0;False;0;None;25e85efab831df14288665af17b3e78f;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;98;-2364.52,147.9248;Float;False;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;95;-2274.078,-42.39103;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;-2323.087,259.5416;Float;False;Property;_WindStrength;Wind Strength;4;0;Create;True;0;0;False;0;1;1;0;6;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;97;-2156.115,155.3293;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-2005.36,109.1126;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1400.663,-225.17;Float;False;Property;_BrightnessMultiplier;Brightness Multiplier;1;0;Create;True;0;0;False;0;1;1.68;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;100;-1897.634,-30.61203;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;125;-1201.527,-156.6162;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;129;-1853.21,467.6667;Float;True;Property;_Flame;Flame;7;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.WireNode;124;-1314.217,-146.5246;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;130;-1519.275,321.7737;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;74461c9fed875ff4da81779ced37c491;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1629.907,-86.09713;Float;True;Property;_FlameTexturewobble;Flame Texture wobble;2;0;Create;True;0;0;False;0;None;74461c9fed875ff4da81779ced37c491;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1296.148,-101.7134;Float;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;19;-1151.19,-147.7355;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;EdShaders/CandleFlameBillboard_Painted;False;False;False;False;True;True;True;True;True;False;True;True;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Overlay;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;True;Cylindrical;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;51;0;49;1
WireConnection;51;1;49;2
WireConnection;104;0;51;0
WireConnection;104;1;105;0
WireConnection;106;0;104;0
WireConnection;41;0;25;0
WireConnection;38;0;39;0
WireConnection;38;1;94;0
WireConnection;107;0;106;0
WireConnection;52;0;107;0
WireConnection;52;1;38;0
WireConnection;54;0;41;0
WireConnection;54;1;41;0
WireConnection;93;0;52;0
WireConnection;93;2;54;0
WireConnection;28;1;93;0
WireConnection;98;0;28;0
WireConnection;97;0;98;0
WireConnection;102;0;95;2
WireConnection;102;1;95;2
WireConnection;102;2;97;0
WireConnection;102;3;29;0
WireConnection;100;0;95;0
WireConnection;100;1;102;0
WireConnection;125;0;5;0
WireConnection;124;0;125;0
WireConnection;130;0;129;0
WireConnection;1;0;129;0
WireConnection;1;1;100;0
WireConnection;3;0;124;0
WireConnection;3;1;1;0
WireConnection;3;2;130;4
WireConnection;19;2;3;0
ASEEND*/
//CHKSM=9834771CCC933A9F037C94281AC0483575A17C02