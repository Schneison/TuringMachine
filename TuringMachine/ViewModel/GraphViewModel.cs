using System;
using System.Collections.ObjectModel;
using System.Numerics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TuringMachine.Model;

namespace TuringMachine.ViewModel;

public class GraphViewModel : ObservableObject {
	public GraphViewModel() {
		AddNodeCommand = new RelayCommand(AddNode);
		RemoveNodeCommand = new RelayCommand(RemoveNode);
		Items.Add(new ConnectionElement((StateElement)Items[0], (StateElement)Items[1]));
		Items.Add(new ConnectionElement((StateElement)Items[1], (StateElement)Items[2]));
		Items.Add(new ConnectionElement((StateElement)Items[2], (StateElement)Items[0]));
		Items.Add(new ConnectionElement((StateElement)Items[0], (StateElement)Items[0]));
	}

	public RelayCommand AddNodeCommand { get; }
	public RelayCommand RemoveNodeCommand { get; }

	public ObservableCollection<ObservableObject> Items { get; } = new() {
		new StateElement(new Vector2(50, 70), "q0", true, false),
		new StateElement(new Vector2(300, 170), "q1", false, true),
		new StateElement(new Vector2(200, 50), "q2", false, false)
	};

	private void AddNode() {
		throw new NotImplementedException();
	}

	private void RemoveNode() {
		throw new NotImplementedException();
	}
}