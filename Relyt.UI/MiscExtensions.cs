using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Relyt.UI;

public static class MiscExtensions
{
	public static Vector2 GetMousePositionRelativeTo(this MouseEventArgs e, Widget widget)
	{
		Point mouseLocation = e.Location;
		RectangleF widgetRect = widget.DrawRect;
		return new Vector2(mouseLocation.X - widgetRect.X, mouseLocation.Y - widgetRect.Y);
	}
}
