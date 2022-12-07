using System;
using System.IO;
using System.Windows;
using TuringMachine.Resources;
using TuringMachine.View;

namespace TuringMachine;

/// <summary>
///  Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	/// <param name="e">Event args</param>
	protected override void OnActivated(EventArgs e) {
		base.OnActivated(e);

		if (!File.Exists("2a.txt")) {
			return;
		}

		var scriptGraph = ScriptParser.CreateFromPath("2a.txt").Parse();
		var forge = new DotForge(scriptGraph);
		forge.ToFile("myFile.dot");
	}

	private void Application_Startup(object sender, StartupEventArgs e) {
		var newWindow = new ArchitectWindow();
		newWindow.Show();
	}
}