using System.Drawing;
using System.Windows.Forms;

namespace Relyt.UI;

/// <summary>
/// Represents a single widget in a widget tree.
/// </summary>
public class Widget
{
	/// <summary>
	/// The rect that this widget was last drawn in.
	/// </summary>
	public RectangleF DrawRect { get; private set; }

	public Point MouseLocation { get; set; }

	public void DrawWidget(DrawingContext context, RectangleF rect)
	{
		DrawRect = rect;
		OnDraw(context, rect);
	}

	public virtual void OnDraw(DrawingContext context, RectangleF rect) { }

	public virtual bool OnKeyDown(KeyEventArgs e) => false;
	public virtual bool OnCharDown(char c) => false;
	public virtual bool OnMouseDown(MouseEventArgs e) => false;
	public virtual bool OnMouseUp(MouseEventArgs e) => false;
	public virtual bool OnMouseMove(MouseEventArgs e) => false;
}
