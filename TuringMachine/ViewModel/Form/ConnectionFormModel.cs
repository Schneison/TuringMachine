using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TuringMachine.Data;
using TuringMachine.Model;

namespace TuringMachine.ViewModel.Form
{
    public class ConnectionFormModel : ObservableObject {
	    private ConnectionEntry? _entry;

	    public ConnectionFormModel(ObservableCollection<ConnectionEntry> connections) {
		    AddCommand = new RelayCommand(Add);
		    ClearCommand = new RelayCommand(Clear);
		    DeleteCommand = new RelayCommand(Delete);
		    CancelCommand = new RelayCommand(Cancel);
			Connections = connections;
			_entry = Connections.Count > 0 ? Connections[0] : null;
	    }

	    public ObservableCollection<ConnectionEntry> Connections { get; }

	    public RelayCommand AddCommand { get; }

	    public RelayCommand ClearCommand { get; }

	    public RelayCommand DeleteCommand { get; }

	    public RelayCommand CancelCommand { get; }

	    public ConnectionEntry? Entry {
		    get => _entry;
		    set => SetProperty(ref _entry, value);
	    }

	    public void Add() {
		    Entry = new ConnectionEntry();
		    Connections.Add(Entry);
	    }

	    public void Delete() {
		    if (Entry == null) {
			    return;
		    }
		    Connections.Remove(Entry);
	    }

	    public void Clear() {
		    if (Entry == null) {
			    return;
		    }
		    Entry.CurrentState = "";
		    Entry.NextState = "";
		    Entry.Mutations = new ObservableCollection<MutationEntry>();
	    }

	    public void Cancel() {

	    }

	    public static IEnumerable<Direction> DirectionValues =>
		    Enum.GetValues(typeof(Direction))
			    .Cast<Direction>();
    }
}
