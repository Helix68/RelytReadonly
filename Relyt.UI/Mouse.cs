using System;
using System.Numerics;

namespace Relyt.UI;

/// <summary>
/// Represents a mouse cursor in code.
/// This class is static because there 
/// is only ever one mouse cursor.
/// </summary>
public static class Mouse
{
	/// <summary>
	/// Returns the mouse position on the desktop.
	/// </summary>
	public static Vector2 GetAbsolutePosition()
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Sets the mouse position on the desktop.
	/// </summary>
	/// <param name="vector">The new mouse position.</param>
	public static void SetAbsolutePosition(Vector2 vector)
	{

	}

	/// <summary>
	/// Gets the mouse position relative to the specified window.
	/// </summary>
	/// <param name="window">The window to get the mouse position relative to.</param>
	/// <returns></returns>
	public static Vector2 GetPositionRelativeTo(Window window)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Sets the position of the mouse relative to the location of a window.
	/// </summary>
	/// <param name="window">The window.</param>
	public static void SetPositionRelativeTo(Window window)
	{

	}

	/// <summary>
	/// Gets the mouse position relative to the specified monitor.
	/// </summary>
	/// <param name="monitor">The monitor to get the mouse position relative to.</param>
	/// <returns></returns>
	public static Vector2 GetPositionRelativeTo(Monitor monitor)
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Sets the position of the mouse relative to the location of a monitor.
	/// </summary>
	/// <param name="monitor">The monitor.</param>
	public static void SetPositionRelativeTo(Monitor monitor)
	{

	}

	/// <summary>
	/// Gets the monitor that currently contains the mouse pointer.
	/// </summary>
	/// <returns>The monitor.</returns>
	public static Monitor GetMonitorContainingMouse()
	{
		throw new NotImplementedException();
	}
}
