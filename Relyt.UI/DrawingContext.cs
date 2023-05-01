using SharpDX.Mathematics.Interop;

using System.Drawing;

using D2D1 = SharpDX.Direct2D1;
using DWrite = SharpDX.DirectWrite;
using DXGI = SharpDX.DXGI;

namespace Relyt.UI;

/// <summary>
/// Class for drawing primitive shapes, images and text onto a window.
/// All widgets call into this class to draw themeselves.
/// </summary>
public class DrawingContext
{
	// The window that this drawing context is drawing onto.
	private readonly Window _window;

	internal static D2D1.Factory FactoryD2D1;
	internal static DWrite.Factory FactoryDWrite;

	static DrawingContext()
	{
		FactoryD2D1 = new D2D1.Factory();
		FactoryDWrite = new DWrite.Factory();
	}

	private D2D1.WindowRenderTarget _renderTarget;
	private D2D1.SolidColorBrush _solidColorBrush;

	/// <summary>
	/// Initializes a new instance of the <see cref="DrawingContext"/> class with the window to drawn on.
	/// </summary>
	/// <param name="window">The window to draw onto.</param>
	public DrawingContext(Window window)
	{
		_window = window;

		var renderTargetProps = new D2D1.RenderTargetProperties(new D2D1.PixelFormat(DXGI.Format.Unknown, D2D1.AlphaMode.Premultiplied));
		var hwndRenderTargetProps = new D2D1.HwndRenderTargetProperties()
		{
			Hwnd = window.Handle,
			PixelSize = window.UnderlyingForm.ClientSize.ToSharpDX(),
			PresentOptions = D2D1.PresentOptions.None
		};
		_renderTarget = new D2D1.WindowRenderTarget(FactoryD2D1, renderTargetProps, hwndRenderTargetProps);
		_solidColorBrush = new D2D1.SolidColorBrush(_renderTarget, new RawColor4(0.0f, 0.0f, 0.0f, 1.0f));
	}

	public static DWrite.TextLayout CreateTextLayout(string text, SizeF size)
	{
		DWrite.TextFormat textFormat = new DWrite.TextFormat(FactoryDWrite, "Cascadia Mono", DWrite.FontWeight.SemiLight, DWrite.FontStyle.Normal, 40.0f);
		return new DWrite.TextLayout(FactoryDWrite, text, textFormat, size.Width, size.Height);
	}

	internal void BeginDraw()
	{
		_renderTarget.BeginDraw();
		_renderTarget.Clear(new RawColor4(1.0f, 1.0f, 1.0f, 1.0f));
	}

	internal void EndDraw()
	{
		_renderTarget.EndDraw();
	}

	public void DrawRectangle(RectangleF rect, LinearColor color)
	{
		_renderTarget.FillRectangle(rect.ToSharpDX(), GetSolidColorBrush(color));
	}

	public void DrawRectangleOutline(RectangleF rect, LinearColor color, float strokeSize)
	{
		_renderTarget.StrokeWidth = strokeSize;
		_renderTarget.DrawRectangle(rect.ToSharpDX(), GetSolidColorBrush(color));
	}

	public void DrawRoundedRectangle(RectangleF rect, LinearColor color, float radiusX, float radiusY)
	{
		_renderTarget.FillRoundedRectangle(new D2D1.RoundedRectangle() { Rect = rect.ToSharpDX(), RadiusX = radiusX, RadiusY = radiusY }, GetSolidColorBrush(color));
	}

	public void DrawRoundedRectangleOutline(RectangleF rect, LinearColor color, float strokeSize, float radiusX, float radiusY)
	{
		_renderTarget.StrokeWidth = strokeSize;
		_renderTarget.DrawRoundedRectangle(new D2D1.RoundedRectangle() { Rect = rect.ToSharpDX(), RadiusX = radiusX, RadiusY = radiusY }, GetSolidColorBrush(color));
	}

	public void DrawTextLayout(DWrite.TextLayout textLayout, RectangleF rect, LinearColor color)
	{
		textLayout.MaxWidth = rect.Width;
		textLayout.MaxHeight = rect.Height;
		_renderTarget.DrawTextLayout(rect.Location.ToSharpDX(), textLayout, GetSolidColorBrush(color));
	}

	internal void Resize(Size clientSize)
	{
		_renderTarget.Resize(clientSize.ToSharpDX());
	}

	private D2D1.Brush GetSolidColorBrush(LinearColor color)
	{
		_solidColorBrush.Color = color.ToSharpDX();
		return _solidColorBrush;
	}
}
