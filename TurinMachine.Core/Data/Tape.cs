namespace TuringMachine.Data;

/// <summary>
///  A tape containing a sequence of symbols which are defined by a alphabet of the turing machine.
///  The tape is theoretically infinite in both directions.
/// </summary>
public class Tape : IPrintable {
	/// <summary>
	///  List of symbols on the tape.
	///  <p />
	///  A linked list is used for fast access to the current symbol and the symbols on the left and right side.
	/// </summary>
	private readonly LinkedList<ISymbol> _cells = new();

	/// <summary>
	///  Current position of the head on the tape.
	///  <br />
	///  This can be negative, if the head the tape is empty.
	/// </summary>
	private int _head;

	/// <summary>
	///  Reference to the node pointed by the head.
	/// </summary>
	private LinkedListNode<ISymbol>? _headSymbol;

	/// <summary>
	///  Create a new tape with the given amount of blank symbols.
	/// </summary>
	private Tape(int defaultSize) {
		_head = defaultSize > 0 ? 0 : -1;
		// Initialises the tape with the given amount of blank character
		for (var i = 0; i < defaultSize; i++) {
			_cells.AddLast(SymbolManager.Blank);
		}

		_headSymbol = _cells.First;
	}

	/// <summary>
	///  Symbol at the current position of the head.
	/// </summary>
	public ISymbol Head => _headSymbol?.Value ?? SymbolManager.None;

	/// <summary>
	///  Length of the tape.
	/// </summary>
	public int Length => _cells.Count;


	/// <summary>
	///  Checks if the tape has no content.
	/// </summary>
	public bool Empty => _headSymbol == null && _head < 0;

	/// <summary>
	///  Creates a string representation of the tape.
	/// </summary>
	/// <returns>String representation of the tape</returns>
	public string ToPrint() {
		return string.Join("", from cell in _cells select cell.ToPrint());
	}


	/// <summary>
	///  Creates a new tape which already contains one blank symbol.
	/// </summary>
	/// <returns>Tape with one blank</returns>
	public static Tape CreateBlank() {
		return new Tape(1);
	}

	/// <summary>
	///  Create a new tape with no content.
	/// </summary>
	/// <returns></returns>
	public static Tape CreateEmpty() {
		return new Tape(0);
	}

	/// <summary>
	///  Applies the given mutation to the tape.
	///  <br />
	///  This can change the current symbol under the head and move the head.
	/// </summary>
	/// <param name="mutation">Mutation to be applied</param>
	/// <exception cref="NotSupportedException">The mutation or its action is invalid for this tape</exception>
	public void ApplyMutation(Mutation mutation) {
		var dir = mutation.Dir;
		if (!mutation.Matches(Head)) {
			throw new NotSupportedException(
				"Tried to apply mutation to a tape which did not match the input criteria of the transaction itself.");
		}

		var symbol = mutation.ToTape(Head);
		// Check if valid symbol was returned
		if (symbol.IsNone()) {
			throw new NotSupportedException(
				"Tried to apply invalid mutation to the tape. Output symbol was 'none'.");
		}

		Alter(symbol, dir);
	}

	/// <summary>
	///  Places the given symbol under the head, moves the head into the given direction
	///  and expands the tape if necessary.
	///  <p />
	///  This is mainly used by the <see cref="ApplyMutation" /> method.
	///  <p />
	///  <b>
	///   Note that this method does not check if the given symbol is valid for the tape.
	///  </b>
	/// </summary>
	/// <param name="symbol">Specified symbol</param>
	/// <param name="direction">Specified direction</param>
	/// <exception cref="InvalidOperationException">If invalid input was given.</exception>
	public void Alter(ISymbol symbol, Direction direction) {
		SetSymbol(symbol);
		MoveHead(direction);
	}

	/// <summary>
	///  Same as <see cref="Alter(ISymbol, Direction)" /> but moves the head to the given position instead
	///  of moving it in a direction.
	/// </summary>
	/// <param name="symbol">Specified symbol</param>
	/// <param name="newPos">Specified position</param>
	/// <exception cref="InvalidOperationException">If invalid input was given.</exception>
	public void Alter(ISymbol symbol, int newPos) {
		SetSymbol(symbol);
		MoveHead(newPos);
	}


	/// <summary>
	///  Moves the head in the given direction.
	/// </summary>
	/// <param name="dir">Specified direction</param>
	/// <returns>True if the head was moved, false if the tape was at an invalid state.</returns>
	/// <exception cref="NotSupportedException">If invalid input was given.</exception>
	private bool MoveHead(Direction dir) {
		return dir switch {
			Direction.Left => MoveHead(_head - 1),
			Direction.Right => MoveHead(_head + 1),
			Direction.None => true,
			_ => throw new NotSupportedException()
		};
	}

	/// <summary>
	///  Moves the head to the given position.
	///  If the head is moved to a position which is not yet on the tape, blank symbols are added.
	///  <br />
	///  The absolute difference between the new position and the current position can only be 1 or zero.
	/// </summary>
	/// <param name="newPos"></param>
	/// <returns>True if the head was moved, false if the tape was at an invalid state.</returns>
	/// <exception cref="InvalidOperationException">If invalid input was given or tape in invalid state.</exception>
	private bool MoveHead(int newPos) {
		if (newPos == _head) {
			return true;
		}

		if (newPos == -1) {
			_headSymbol = _cells.AddFirst(SymbolManager.Blank);
			return true;
		}

		if (Math.Abs(newPos - _head) > 1) {
			throw new InvalidOperationException("Tried to move to far to the right or left on the tape!");
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

	/// <summary>
	///  Sets the symbol under the head to the given symbol.
	/// </summary>
	/// <param name="symbol">The specified symbol.</param>
	/// <exception cref="InvalidOperationException">If specified symbol is invalid.</exception>
	private void SetSymbol(ISymbol symbol) {
		if (_headSymbol == null || symbol.IsNone()) {
			throw new InvalidOperationException("Tape head is out of sync with the tape itself.");
		}

		_headSymbol.Value = symbol;
	}

	public override string ToString() {
		return $"Tape({ToPrint()})";
	}
}