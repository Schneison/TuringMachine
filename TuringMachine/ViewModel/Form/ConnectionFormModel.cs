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
using TuringMachine.Service;

namespace TuringMachine.ViewModel.Form
{
    public class ConnectionFormModel : ObservableObject {
	    private ConnectionEntry? _entry;
	    private readonly IDesignService _service;


		public ConnectionFormModel(IDesignService service) {
		    AddCommand = new RelayCommand(Add);
		    ClearCommand = new RelayCommand(Clear);
		    DeleteCommand = new RelayCommand(Delete);
		    CancelCommand = new RelayCommand(Cancel);
			Connections = new ObservableCollection<ConnectionEntry>();
			this._service = service;
		}

		public void Init(ObservableCollection<ConnectionEntry> connections) {
			_entry = connections.Count > 0 ? connections[0] : null;
		    Connections = connections;
		    OnPropertyChanged(nameof(Connections));
	    }

		public ObservableCollection<ConnectionEntry> Connections { get; private set; }

	    public RelayCommand AddCommand { get; }

	    public RelayCommand ClearCommand { get; }

	    public RelayCommand DeleteCommand { get; }

	    public RelayCommand CancelCommand { get; }

	    public ConnectionEntry? Entry {
		    get => _entry;
		    set => SetProperty(ref _entry, value);
	    }

	    public void Add() {
		    Entry = _service.CreateConnection();
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
		    Entry = null;
	    }

	    public static IEnumerable<Direction> DirectionValues =>
		    Enum.GetValues(typeof(Direction))
			    .Cast<Direction>();
    }
}
