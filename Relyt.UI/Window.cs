using System;
using System.Drawing;
using System.Windows.Forms;

namespace Relyt.UI;

/// <summary>
/// Represents a window in the operating system.
/// </summary>
public class Window
{
	private Widget _content;
	private Form _form;
	private DrawingContext _drawingContext;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Window"/> class.
	/// </summary>
	public Window()
	{
		_form = new TransparentForm()
		{
			Text = "Relyt",
			Width = 1300,
			Height = 800,
		};

		_drawingContext = new DrawingContext(this);

		Application.Windows.Add(this);

		_form.ClientSizeChanged += OnFormClientSizeChanged;
		_form.KeyDown += OnFormKeyDown;
		_form.FormClosed += OnFormClosed;
		_form.Deactivate += OnFromDeactivated;
		_form.KeyPress += OnFormKeyPress;
		_form.MouseDown += OnFormMouseDown;
		_form.MouseUp += OnFormMouseUp;
		_form.MouseMove += OnFormMouseMove;
	}

	/// <summary>
	/// Gets or sets the title of the window.
	/// </summary>
	public string Title
	{
		get => _form.Text;
		set => _form.Text = value;
	}

	public Form UnderlyingForm => _form;
	public nint Handle => _form.Handle;

	public Widget Content
	{
		get => _content;
		set => _content = value;
	}

	/// <summary>
	/// Shows the window onto the screen.
	/// </summary>
	public void Show()
	{
		_form.Show();
		_form.DesktopLocation = new Point(100, 100);
	}

	internal void DrawWindowInternal()
	{
		_drawingContext.BeginDraw();
		DrawWindow(_drawingContext, new RectangleF(PointF.Empty, _form.ClientSize));
		_drawingContext.EndDraw();
	}

	/// <summary>
	/// Draw the window.
	/// </summary>
	protected virtual void DrawWindow(DrawingContext context, RectangleF rect)
	{
		_content.DrawWidget(context, rect);
	}

	private void OnFormClientSizeChanged(object? sender, EventArgs e)
	{
		_drawingContext.Resize(_form.ClientSize);
		DrawWindowInternal();
	}

	private void OnFormKeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			Application.Exit();
		}

		_content?.OnKeyDown(e);
	}

	private void OnFormKeyPress(object? sender, KeyPressEventArgs e)
	{
		_content?.OnCharDown(e.KeyChar);
	}

	private void OnFormClosed(object? sender, FormClosedEventArgs e)
	{
		Application.Exit();
	}

	private void OnFromDeactivated(object? sender, EventArgs e)
	{
		Application.Exit();
	}

	private void OnFormMouseDown(object? sender, MouseEventArgs e)
	{
		_content?.OnMouseDown(e);
	}

	private void OnFormMouseUp(object? sender, MouseEventArgs e)
	{
		_content?.OnMouseUp(e);
	}

	private void OnFormMouseMove(object? sender, MouseEventArgs e)
	{
		_content?.OnMouseMove(e);
	}
}

public class TransparentForm : Form
{
	public TransparentForm()
	{
		SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		this.BackColor = Color.Transparent;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		
	}

	protected override void OnPaintBackground(PaintEventArgs e)
	{

	}
}
