using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TuringMachine.Resources;
using TuringMachine.Service;
using TuringMachine.View;

namespace TuringMachine;

/// <summary>
///  Interaction logic for App.xaml
/// </summary>
public partial class App : Application {

	public IServiceProvider? ServiceProvider { get; private set; }

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

	protected override void OnStartup(StartupEventArgs e) {
		ServiceProvider = new ServiceCollection()
			.AddSingleton<IDesignService, DesignService>()
			.BuildServiceProvider();

		base.OnStartup(e);
	}

	public static IServiceProvider GetServiceProvider() {
		var app = ((App)Application.Current);
		var provider = app.ServiceProvider;
		if (provider == null) {
			throw new InvalidOperationException("Service provider requested before app was initialized");
		}

		return provider;
	}
}