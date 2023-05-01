using SharpDX;
using SharpDX.Mathematics.Interop;

using System.Drawing;
using System.Numerics;

namespace Relyt.UI;

internal static class SharpDXExtensions
{
	public static RawPoint ToSharpDX(this Point point)
	{
		return new RawPoint(point.X, point.Y);
	}

	public static RawVector2 ToSharpDX(this PointF point)
	{
		return new RawVector2(point.X, point.Y);
	}

	public static RawRectangleF ToSharpDX(this RectangleF rect)
	{
		return new RawRectangleF(rect.Left, rect.Top, rect.Right, rect.Bottom);
	}

	public static RawVector2 ToSharpDX(this Vector2 vector)
	{
		return new RawVector2(vector.X, vector.Y);
	}

	public static Size2 ToSharpDX(this Size vector)
	{
		return new Size2(vector.Width, vector.Height);
	}

	public static RawColor4 ToSharpDX(this LinearColor color)
	{
		return new RawColor4(color.R, color.G, color.B, color.A);
	}

	public static RawColor4 ToSharpDX(this Color color)
	{
		return ToSharpDX(new LinearColor(color));
	}
}
