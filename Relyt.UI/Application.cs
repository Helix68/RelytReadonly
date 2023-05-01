using System.Collections.Generic;
using System.Diagnostics;

using FormsApp = System.Windows.Forms.Application;

namespace Relyt.UI;

/// <summary>
/// Represents an application.
/// Manages all windows.
/// When all windows are closed, the application exits.
/// </summary>
public class Application
{
	private static bool _needsExit;
	internal static readonly List<Window> Windows = new List<Window>();

	public static void Run()
	{
		FormsApp.ApplicationExit += OnFromsAppExit;

		while (!_needsExit)
		{
			FormsApp.DoEvents();

			foreach (Window window in Windows)
			{
				window.DrawWindowInternal();
			}
		}
	}

	private static void OnFromsAppExit(object? sender, System.EventArgs e)
	{
		_needsExit = true;
	}

	public static void Exit()
	{
		_needsExit = true;
		FormsApp.Exit();
	}
}
