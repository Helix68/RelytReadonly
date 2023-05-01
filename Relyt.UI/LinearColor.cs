using System;
using System.Drawing;

namespace Relyt.UI;

public struct LinearColor
{
	public static readonly LinearColor Transparent = new LinearColor(0.0f, 0.0f, 0.0f, 0.0f);
	public static readonly LinearColor White = new LinearColor(1.0f, 1.0f, 1.0f);
	public static readonly LinearColor Black = new LinearColor(0.0f, 0.0f, 0.0f);

	public static readonly LinearColor Red = new LinearColor(1.0f, 0.0f, 0.0f);
	public static readonly LinearColor Green = new LinearColor(0.0f, 1.0f, 0.0f);
	public static readonly LinearColor Blue = new LinearColor(0.0f, 0.0f, 1.0f);

	public float R;
	public float G;
	public float B;
	public float A;

	public LinearColor(float r, float g, float b, float a = 1.0f)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public LinearColor(float gray, float a = 1.0f)
	{
		R = G = B = gray;
		A = a;
	}

	public LinearColor(Color color)
	{
		R = color.R / 255.0f;
		G = color.G / 255.0f;
		B = color.B / 255.0f;
		A = color.A / 255.0f;
	}

	public override bool Equals(object? obj)
	{
		return obj is LinearColor color &&
			   R == color.R &&
			   G == color.G &&
			   B == color.B &&
			   A == color.A;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(R, G, B, A);
	}

	public static bool operator ==(LinearColor left, LinearColor right)
	{
		return left.R == right.R
			&& left.G == right.G
			&& left.B == right.B
			&& left.A == right.A;
	}

	public static bool operator !=(LinearColor left, LinearColor right)
	{
		return left.R != right.R
			|| left.G != right.G
			|| left.B != right.B
			|| left.A != right.A;
	}
}