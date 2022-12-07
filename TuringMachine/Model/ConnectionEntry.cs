using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TuringMachine.Data;

namespace TuringMachine.Model;

public class ConnectionEntry : ObservableObject {
	private string _currentState;
	private string _nextState;
	private ObservableCollection<MutationEntry> _mutations = new();

	public ConnectionEntry() {
		_currentState = "";
		_nextState = "";
	}

	public ConnectionEntry(string currentState, string nextState) {
		_currentState = currentState;
		_nextState = nextState;
		_mutations.Add(new MutationEntry("Input", "Output", Direction.Left));
	}

	public ObservableCollection<MutationEntry> Mutations {
		get => _mutations;
		set => SetProperty(ref _mutations, value);
	}

	public string CurrentState {
		get => _currentState;
		set => SetProperty(ref _currentState, value);
	}

	public string NextState {
		get => _nextState;
		set => SetProperty(ref _nextState, value);
	}

	public override string ToString() {
		return $"{nameof(_currentState)}: {_currentState}, {nameof(_nextState)}: {_nextState}, {nameof(_mutations)}: {_mutations}";
	}
}

public class MutationEntry : ObservableObject {
	private string _input;
	private string _output;
	private Direction _direction;

	public MutationEntry() {
		_input = "Test";
		_output = "";
		_direction = Direction.None;
	}

	public MutationEntry(string input, string output, Direction direction) {
		_input = input;
		_output = output;
		_direction = direction;
	}

	public string Input {
		get => _input;
		set => SetProperty(ref _input, value);
	}

	public string Output {
		get => _output;
		set => SetProperty(ref _output, value);
	}

	public Direction Direction {
		get => _direction;
		set => SetProperty(ref _direction, value);
	}

	public override string ToString() {
		return $"{nameof(_input)}: {_input}, {nameof(_output)}: {_output}, {nameof(_direction)}: {_direction}";
	}
}