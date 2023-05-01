using Relyt.UI;

namespace Relyt;

internal class Program
{
	static void Main(string[] args)
	{
		BorderlessWindow rootWindow = new BorderlessWindow();
		rootWindow.Show();

		Application.Run();
	}
}
