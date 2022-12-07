using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TuringMachine.Data;
using TuringMachine.Model;
using TuringMachine.ViewModel.Form;

namespace TuringMachine.ViewModel
{
	// https://learn.microsoft.com/en-us/visualstudio/xaml-tools/xaml-designtime-data?view=vs-2022
	// https://badecho.com/index.php/2021/07/26/usable-design-time-data/
	// https://github.com/microsoft/WPF-Samples/tree/master/Sample%20Applications/DataBindingDemo
	public class ArchitectViewModel : ObservableObject {
		public ArchitectViewModel() {
			Variable = new VariableFormModel(Variables);
			Connection = new ConnectionFormModel(Connections);
		}

		public ObservableCollection<ConnectionEntry> Connections { get; } = new() {
			new ConnectionEntry("q0", "q1"),
			new ConnectionEntry("q0", "q1"),
		};
		public ObservableCollection<VariableEntry> Variables { get; } = new() {
			new VariableEntry("X", "Z", false),
			new VariableEntry("Y", "#", true)
		};

		public VariableFormModel Variable { get; }
		public ConnectionFormModel Connection { get; }


	}
	
}
