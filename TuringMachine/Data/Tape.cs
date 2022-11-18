using System;
using System.Collections.Generic;
using System.Linq;

namespace TuringMachine.Data;

public class Tape : IPrintable {
	private readonly LinkedList<ISymbol> _cells = new();

	private int _head;
	private LinkedListNode<ISymbol>? _headSymbol;

	private Tape(int defaultSize) {
		_head = defaultSize > 0 ? 0 : -1;
		// Initialises the tape with the given amount of blank character
		for (var i = 0; i < defaultSize; i++) {
			_cells.AddLast(SymbolManager.Blank);
		}

		_headSymbol = _cells.First;
	}

	public ISymbol Head => _headSymbol?.Value ?? SymbolManager.None;

	public int Length => _cells.Count;

	public bool Empty => _headSymbol == null && _head < 0;

	public string ToPrint() {
		return string.Join("", from cell in _cells select cell.ToPrint());
	}

	public static Tape CreateBlank() {
		return new Tape(1);
	}

	public static Tape CreateEmpty() {
		return new Tape(0);
	}

	public void ApplyMutation(Mutation mutation) {
		var dir = mutation.Dir;
		if (!mutation.Matches(Head)) {
			throw new NotSupportedException(
				"Tried to apply transaction to a tape which did not match the input criteria of the transaction itself.");
		}

		var symbol = mutation.ToTape(Head);
		SetSymbol(symbol);
		MoveHead(dir);
	}

	private bool MoveHead(Direction dir) {
		return dir switch {
			Direction.Left => MoveHead(_head - 1),
			Direction.Right => MoveHead(_head + 1),
			Direction.None => true,
			_ => throw new NotSupportedException()
		};
	}

	private bool MoveHead(int newPos) {
		if (newPos == _head) {
			return true;
		}

		if (newPos < 0) {
			_headSymbol = _cells.AddFirst(SymbolManager.Blank);
			return true;
		}

		if (newPos - _head > 1) {
			throw new InvalidOperationException("Tried to move to far to the right on the tape!");
		}

		_head += 1;
		if (_head == _cells.Count) {
			_headSymbol = _cells.AddLast(SymbolManager.Blank);
		}
		else {
			// Tape is empty add one blank symbol 
			if (Empty) {
				_headSymbol = _cells.AddLast(SymbolManager.Blank);
			}

			if (_headSymbol?.Next == null) {
				throw new InvalidOperationException("Tape head is out of sync with the tape itself.");
			}

			_headSymbol = _headSymbol.Next!;
		}

		return false;
	}

	private void SetSymbol(ISymbol symbol) {
		if (_headSymbol == null) {
			throw new InvalidOperationException("Tape head is out of sync with the tape itself.");
		}

		_headSymbol.Value = symbol;
	}

	public override string ToString() {
		return $"Tape({ToPrint()})";
	}
}