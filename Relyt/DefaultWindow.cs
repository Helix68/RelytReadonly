using Relyt.UI;

using System.Drawing;
using System.Windows.Forms;

namespace Relyt;

public class BorderlessWindow : Window
{
	public BorderlessWindow()
	{
		Content = new EditableTextBox();
		UnderlyingForm.FormBorderStyle = FormBorderStyle.None;
	}

	protected override void DrawWindow(DrawingContext context, RectangleF rect)
	{
		context.DrawRectangle(rect, new LinearColor(0.9f, 0.9f, 0.9f));
		context.DrawRectangleOutline(rect, new LinearColor(0.0f, 0.0f, 0.0f), 10.0f);

		base.DrawWindow(context, RectangleF.Inflate(rect, -100.0f, -100.0f));
	}
}
