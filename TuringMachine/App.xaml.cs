using System;
using System.Windows;
using TuringMachine.Resources;

namespace TuringMachine;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
	/// <summary>
	///    The main entry point for the application.
	/// </summary>
	/// <param name="e">Event args</param>
	protected override void OnActivated(EventArgs e) {
		base.OnActivated(e);
		
		var scriptGraph = ScriptParser.CreateFromPath("3_1_band.txt").Parse();
		var forge = new DotForge(scriptGraph);
		forge.ToFile("myFile.dot");
	}
}