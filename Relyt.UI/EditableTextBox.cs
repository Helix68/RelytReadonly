using SharpDX.Mathematics.Interop;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

using DWrite = SharpDX.DirectWrite;

namespace Relyt.UI;

/// <summary>
/// Displays text that can be edited.
/// </summary>
public class EditableTextBox : LeafWidget
{
	private readonly StringBuilder _textBuilder = new StringBuilder("Hello World");
	private DWrite.TextLayout _textLayout;
	private int _caretPosition;

	public EditableTextBox()
	{
		_caretPosition = 2;
		RefreshTextLayout();
	}

	public override void OnDraw(DrawingContext context, RectangleF rect)
	{
		context.DrawRectangle(rect, new LinearColor(0.8f));

		// Draw the text
		context.DrawTextLayout(_textLayout, rect, LinearColor.Black);

		// Draw the caret
		DWrite.HitTestMetrics metrics = _textLayout.HitTestTextPosition(_caretPosition, false, out _, out _);
		RectangleF caretRect = new RectangleF(rect.Left + metrics.Left, rect.Top + metrics.Top, 2.0f, metrics.Height);
		context.DrawRectangle(caretRect, LinearColor.Red);
	}

	public override bool OnKeyDown(KeyEventArgs e)
	{
		switch (e.KeyCode)
		{
			case Keys.Left:
				{
					if (_caretPosition > 0)
					{
						_caretPosition--;
					}
				}
				return true;

			case Keys.Right:
				{
					if (_caretPosition < _textBuilder.Length)
					{
						_caretPosition++;
					}
				}
				return true;

			case Keys.Back:
				{
					if (_caretPosition > 0)
					{
						_caretPosition--;
						_textBuilder.Remove(_caretPosition, 1);
						RefreshTextLayout();
					}
				}
				return true;

			case Keys.Enter:
				{
					_textBuilder.Insert(_caretPosition, '\n');
					_caretPosition++;
					RefreshTextLayout();
				}
				return true;

			default:
				return false;
		}
	}

	public override bool OnCharDown(char c)
	{
		if (!char.IsControl(c))
		{
			_textBuilder.Insert(_caretPosition, c);
			_caretPosition++;

			RefreshTextLayout();
		}

		return true;
	}

	public override bool OnMouseDown(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			Vector2 mouseLocation = e.GetMousePositionRelativeTo(this);
			DWrite.HitTestMetrics metrics = _textLayout.HitTestPoint(mouseLocation.X, mouseLocation.Y, out RawBool isTrailingHit, out RawBool isInside);
			_caretPosition = isTrailingHit ? metrics.TextPosition + 1 : metrics.TextPosition;

			return true;
		}

		return false;
	}

	[MemberNotNull(nameof(_textLayout))]
	private void RefreshTextLayout()
	{
		_textLayout = DrawingContext.CreateTextLayout(_textBuilder.ToString());
	}
}
