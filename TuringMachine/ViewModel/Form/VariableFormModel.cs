using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TuringMachine.Model;

namespace TuringMachine.ViewModel.Form; 

// https://dotnetgenetics.blogspot.com/2021/02/wpf-crud-with-datagrid-mvvm-entity.html
public class VariableFormModel : ObservableObject {
	private VariableEntry? _entry;

	public VariableFormModel(ObservableCollection<VariableEntry> variables) {
		AddCommand = new RelayCommand(Add);
		ClearCommand = new RelayCommand(Clear);
		DeleteCommand = new RelayCommand(Delete);
		CancelCommand = new RelayCommand(Cancel);
		Variables = variables;
		_entry = Variables.Count > 0 ? Variables[0] : null;
	}

	public ObservableCollection<VariableEntry> Variables { get; }

	public RelayCommand AddCommand { get; }

	public RelayCommand ClearCommand { get; }

	public RelayCommand DeleteCommand { get; }

	public RelayCommand CancelCommand { get; }

	public VariableEntry? Entry {
		get => _entry;
		set => SetProperty(ref _entry, value);
	}

	public void Add() {
		Entry = new VariableEntry();
		Variables.Add(Entry);
	}

	public void Delete() {
		if (Entry == null) {
			return;
		}
		Variables.Remove(Entry);
	}

	public void Clear() {
		if (Entry == null) {
			return;
		}
		Entry.Name = "";
		Entry.Values = "";
		Entry.Blacklist = false;
	}

	public void Cancel() {

	}
}