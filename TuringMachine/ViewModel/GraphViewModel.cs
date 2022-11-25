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
        Items.Add(new ConnectionElement((NodeElement)Items[0], (NodeElement)Items[1]));
        Items.Add(new ConnectionElement((NodeElement)Items[1], (NodeElement)Items[2]));
        Items.Add(new ConnectionElement((NodeElement)Items[2], (NodeElement)Items[0]));
        Items.Add(new ConnectionElement((NodeElement)Items[0], (NodeElement)Items[0]));
    }

    private RelayCommand AddNodeCommand { get; }
    private RelayCommand RemoveNodeCommand { get; }

    public ObservableCollection<ObservableObject> Items { get; } = new() {
        new NodeElement(new Vector2(50, 70), "q0", true, false),
        new NodeElement(new Vector2(300, 170), "q1", false, true),
        new NodeElement(new Vector2(200, 50), "q2", false, false)
    };

    private void AddNode() {
        throw new NotImplementedException();
    }

    private void RemoveNode() {
        throw new NotImplementedException();
    }
}