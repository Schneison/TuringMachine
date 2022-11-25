namespace TuringMachine.Data;

public interface ISymbol : IPrintable {
	bool IsNone() {
		return false;
	}
}