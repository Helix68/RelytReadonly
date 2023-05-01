using SharpDX.Mathematics.Interop;

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

	// Think of the _selectionStart and _selectionEnd of the start and end places that the
	// user dragged there cursor when creating the selection.
	private int _selectionStart = -1;
	private int _selectionEnd = -1;

	// Take into account of the fact that the _selectionEnd could come before _selectionStart
	// if the user selected a part of text then move their cursor to the left or up causing
	// the _selectionStart to become the end and the _selectionStart to become the start.
	public int SelectionStart => _selectionStart < _selectionEnd ? _selectionStart : _selectionEnd;
	public int SelectionEnd => _selectionStart < _selectionEnd ? _selectionEnd : _selectionStart;

	public bool HasSelection => _selectionStart != -1 && _selectionEnd != -1 && _selectionStart != _selectionEnd;

	public EditableTextBox()
	{
		_caretPosition = 2;
		RefreshTextLayout();
	}

	public override void OnDraw(DrawingContext context, RectangleF rect)
	{
		context.DrawRectangle(rect, new LinearColor(0.8f));

		// Do not draw the caret when there is a selection
		if (HasSelection)
		{
			DWrite.HitTestMetrics[] metrics = _textLayout.HitTestTextRange(SelectionStart, SelectionEnd - SelectionStart, rect.X, rect.Y);
			for (int i = 0; i < metrics.Length; i++)
			{
				DWrite.HitTestMetrics lineMetrics = metrics[i];
				RectangleF selectionRect = new RectangleF(lineMetrics.Left, lineMetrics.Top, lineMetrics.Width, lineMetrics.Height);
				context.DrawRectangle(selectionRect, LinearColor.Blue);
			}
		}
		else
		{
			// Draw the caret
			DWrite.HitTestMetrics metrics = _textLayout.HitTestTextPosition(_caretPosition, false, out _, out _);
			RectangleF caretRect = new RectangleF((rect.Left + metrics.Left) - 1.0f, rect.Top + metrics.Top, 2.0f, metrics.Height);
			context.DrawRectangle(caretRect, LinearColor.Red);
		}

		// Draw the text
		context.DrawTextLayout(_textLayout, rect, LinearColor.Black);
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

	public void ClearSelection()
	{
		_selectionStart = -1;
		_selectionEnd = -1;
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

	private bool _isSelecting = false;

	public override bool OnMouseDown(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			ClearSelection();

			Vector2 mouseLocation = e.GetMousePositionRelativeTo(this);
			DWrite.HitTestMetrics metrics = _textLayout.HitTestPoint(mouseLocation.X, mouseLocation.Y, out RawBool isTrailingHit, out RawBool isInside);
			_caretPosition = isTrailingHit ? metrics.TextPosition + 1 : metrics.TextPosition;

			_selectionStart = metrics.TextPosition;
			_isSelecting = true;
			return true;
		}

		return false;
	}

	public override bool OnMouseUp(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			Vector2 mouseLocation = e.GetMousePositionRelativeTo(this);
			DWrite.HitTestMetrics metrics = _textLayout.HitTestPoint(mouseLocation.X, mouseLocation.Y, out RawBool isTrailingHit, out RawBool isInside);
			_selectionEnd = metrics.TextPosition;
			_isSelecting = false;
			return true;
		}

		return false;
	}

	public override bool OnMouseMove(MouseEventArgs e)
	{
		if (_isSelecting)
		{
			Vector2 mouseLocation = e.GetMousePositionRelativeTo(this);
			DWrite.HitTestMetrics metrics = _textLayout.HitTestPoint(mouseLocation.X, mouseLocation.Y, out RawBool isTrailingHit, out RawBool isInside);
			_selectionEnd = metrics.TextPosition;
			return true;
		}

		return false;
	}

	[MemberNotNull(nameof(_textLayout))]
	private void RefreshTextLayout()
	{
		_textLayout = DrawingContext.CreateTextLayout(_textBuilder.ToString(), Rect.Size);
	}
}
