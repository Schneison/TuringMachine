using System.Windows.Controls;
using TuringMachine.ViewModel;

namespace TuringMachine.View; 

/// <summary>
///     Interaction logic for GraphView.xaml
/// </summary>
public partial class GraphView : UserControl {
    public GraphView() {
        InitializeComponent();
        DataContext = new GraphViewModel();
    }
}