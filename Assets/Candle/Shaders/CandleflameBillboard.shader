// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EdShaders/CandleFlameBillboard"
{
	Properties
	{
		_BrightnessMultiplier("Brightness Multiplier", Float) = 1
		_CoreColour("Core Colour", Color) = (0.9779412,0.9248784,0.2085316,0)
		_OuterColour("Outer Colour", Color) = (0.9632353,0.3477807,0.07082614,0)
		_BaseColour("Base Colour", Color) = (0.07082614,0.1877624,0.9632353,0)
		_Noise("Noise", 2D) = "black" {}
		_WindStrength("Wind Strength", Range( 0 , 6)) = 1
		_FlameFlickerSpeed("Flame Flicker Speed", Float) = 0
		_NoiseScale("Noise Scale", Float) = 1
		_FlameTexture("Flame Texture", 2D) = "white" {}
		_FakeGlow("Fake Glow", Range( 0 , 1)) = 0.3
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
		uniform float4 _CoreColour;
		uniform sampler2D _FlameTexture;
		uniform sampler2D _Noise;
		uniform float _FlameFlickerSpeed;
		uniform float _NoiseScale;
		uniform float4 _Noise_ST;
		uniform float _WindStrength;
		uniform float4 _OuterColour;
		uniform float4 _BaseColour;
		uniform float4 _FlameTexture_ST;
		uniform float _FakeGlow;

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
			float4 tex2DNode1 = tex2D( _FlameTexture, ( i.uv_texcoord + ( i.uv_texcoord.y * i.uv_texcoord.y * ( (tex2D( _Noise, panner93 )).rg - float2( 0.5,0.5 ) ) * _WindStrength ) ) );
			float2 uv_FlameTexture = i.uv_texcoord * _FlameTexture_ST.xy + _FlameTexture_ST.zw;
			o.Emission = ( _BrightnessMultiplier * ( ( _CoreColour * tex2DNode1.r ) + ( _OuterColour * tex2DNode1.g ) + ( tex2DNode1.b * _BaseColour ) + ( tex2D( _FlameTexture, uv_FlameTexture ).a * _FakeGlow * _OuterColour ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
2567;29;1906;1014;4589.273;2191.455;3.909162;True;False
Node;AmplifyShaderEditor.CommentaryNode;21;-3460.047,-328.2268;Float;False;1741.255;731.4139;;18;100;102;95;97;29;98;28;93;52;54;41;38;107;25;94;39;106;60;Flame UV Movement;0.4338235,1,0.5548681,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;60;-3377.918,-239.1424;Float;False;706.9712;210.6665;World Space Variation;4;104;105;51;49;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;49;-3349.832,-189.4632;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;51;-3161.999,-166.4515;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;105;-3011.731,-101.5707;Float;False;Constant;_Float0;Float 0;14;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-2844.029,-170.4705;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;106;-2618.089,-44.24372;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-3263.208,15.84671;Float;False;Property;_NoiseScale;Noise Scale;8;0;Create;True;0;0;False;0;1;0.06;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;94;-3362.715,109.3148;Float;False;0;28;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;-3370.997,248.9985;Float;False;Property;_FlameFlickerSpeed;Flame Flicker Speed;7;0;Create;True;0;0;False;0;0;0.28;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;107;-2946.325,-3.500575;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-3108.897,107.4705;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NegateNode;41;-3142.037,253.7603;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;54;-2986.484,234.6737;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-2922.119,33.70701;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;93;-2832.114,150.7149;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;74;-977.0093,-236.5681;Float;False;454.6583;358.3214;;4;13;12;86;123;G - Outer Colour;0.1586208,1,0,1;0;0
Node;AmplifyShaderEditor.SamplerNode;28;-2658.014,145.9267;Float;True;Property;_Noise;Noise;5;0;Create;True;0;0;False;0;None;25e85efab831df14288665af17b3e78f;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;98;-2364.52,147.9248;Float;False;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;12;-945.8568,-173.5083;Float;False;Property;_OuterColour;Outer Colour;3;0;Create;True;0;0;False;0;0.9632353,0.3477807,0.07082614,0;0.5808823,0.3087042,0.01708479,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;-2323.087,259.5416;Float;False;Property;_WindStrength;Wind Strength;6;0;Create;True;0;0;False;0;1;1;0;6;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;97;-2156.115,155.3293;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;123;-755.8403,28.41349;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;95;-2274.078,-42.39103;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;111;-1651.01,265.9981;Float;True;Property;_FlameTexture;Flame Texture;9;0;Create;True;0;0;False;0;None;74461c9fed875ff4da81779ced37c491;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.WireNode;120;-1007.546,58.14272;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-2005.36,109.1126;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;128;-1460.251,181.6631;Float;False;1;0;SAMPLER2D;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.WireNode;122;-1037.276,107.6912;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;100;-1897.634,-30.61203;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;127;-1646.396,148.2526;Float;False;1;0;SAMPLER2D;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.CommentaryNode;73;-979.9876,-639.0322;Float;False;465.3604;352.7877;;2;11;10;R - Core Colour;1,0,0,1;0;0
Node;AmplifyShaderEditor.SamplerNode;1;-1629.907,-86.09713;Float;True;Property;_FlameTexturewobble;Flame Texture wobble;2;0;Create;True;0;0;False;0;None;74461c9fed875ff4da81779ced37c491;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;121;-1037.276,720.11;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;113;-967.1047,630.779;Float;False;486.3594;355.9586;;2;114;116;Fake Glow;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;112;-1361.392,278.2718;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;74461c9fed875ff4da81779ced37c491;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;75;-977.5981,184.7019;Float;False;486.3594;355.9586;;2;8;7;B - Base Colour;0,0.2965517,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-332.9388,-213.6685;Float;False;Property;_BrightnessMultiplier;Brightness Multiplier;1;0;Create;True;0;0;False;0;1;1.68;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;10;-965.8353,-579.967;Float;False;Property;_CoreColour;Core Colour;2;0;Create;True;0;0;False;0;0.9779412,0.9248784,0.2085316,0;0.8603911,0.9191176,0.310878,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-930.5865,358.8116;Float;False;Property;_BaseColour;Base Colour;4;0;Create;True;0;0;False;0;0.07082614,0.1877624,0.9632353,0;0.6131055,0.6152667,0.9264706,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;86;-947.6709,-3.466096;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;87;-1034.738,-322.4019;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;126;-1070.329,695.5719;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;116;-954.1796,849.7026;Float;False;Property;_FakeGlow;Fake Glow;10;0;Create;True;0;0;False;0;0.3;0.57;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;89;-1051.479,254.9862;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;119;-979.7988,787.496;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;125;-133.8032,-145.1147;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-658.5818,-443.3548;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-664.0324,-58.83775;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-636.3212,267.9926;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-625.8271,714.0696;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;124;-246.4933,-135.0231;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-367.1033,-78.05847;Float;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-228.424,-90.21187;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;19;-83.46525,-136.234;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;EdShaders/CandleFlameBillboard;False;False;False;False;True;True;True;True;True;False;True;True;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Overlay;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;True;Cylindrical;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;51;0;49;1
WireConnection;51;1;49;2
WireConnection;104;0;51;0
WireConnection;104;1;105;0
WireConnection;106;0;104;0
WireConnection;107;0;106;0
WireConnection;38;0;39;0
WireConnection;38;1;94;0
WireConnection;41;0;25;0
WireConnection;54;0;41;0
WireConnection;54;1;41;0
WireConnection;52;0;107;0
WireConnection;52;1;38;0
WireConnection;93;0;52;0
WireConnection;93;2;54;0
WireConnection;28;1;93;0
WireConnection;98;0;28;0
WireConnection;97;0;98;0
WireConnection;123;0;12;0
WireConnection;120;0;123;0
WireConnection;102;0;95;2
WireConnection;102;1;95;2
WireConnection;102;2;97;0
WireConnection;102;3;29;0
WireConnection;128;0;111;0
WireConnection;122;0;120;0
WireConnection;100;0;95;0
WireConnection;100;1;102;0
WireConnection;127;0;128;0
WireConnection;1;0;127;0
WireConnection;1;1;100;0
WireConnection;121;0;122;0
WireConnection;112;0;111;0
WireConnection;86;0;1;2
WireConnection;87;0;1;1
WireConnection;126;0;112;4
WireConnection;89;0;1;3
WireConnection;119;0;121;0
WireConnection;125;0;5;0
WireConnection;11;0;10;0
WireConnection;11;1;87;0
WireConnection;13;0;12;0
WireConnection;13;1;86;0
WireConnection;8;0;89;0
WireConnection;8;1;7;0
WireConnection;114;0;126;0
WireConnection;114;1;116;0
WireConnection;114;2;119;0
WireConnection;124;0;125;0
WireConnection;9;0;11;0
WireConnection;9;1;13;0
WireConnection;9;2;8;0
WireConnection;9;3;114;0
WireConnection;3;0;124;0
WireConnection;3;1;9;0
WireConnection;19;2;3;0
ASEEND*/
//CHKSM=0AD20F6417CFF5F64435AB9FC9FFCA9A74C5E7FD