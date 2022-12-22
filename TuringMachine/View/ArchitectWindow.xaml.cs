using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TuringMachine.ViewModel;

namespace TuringMachine.View;

/// <summary>
///     Interaction logic for ArchitectWindow.xaml
/// </summary>
public partial class ArchitectWindow : Window {
	public ArchitectWindow() {
		InitializeComponent();
		DataContext = App.GetServiceProvider().GetRequiredService<ArchitectViewModel>();
	}
}