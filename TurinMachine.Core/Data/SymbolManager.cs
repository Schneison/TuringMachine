using TuringMachine.Config;

namespace TuringMachine.Data;

public static class SymbolManager {
	/// Represents a blank but not empty symbol on the tape.
	public static readonly ISymbol Blank = new SymbolBlank();

	/// Dummy value which should be used instead of null for all symbol values.
	/// <p />
	/// This value will raise an error if you should try to place it onto the tape or if you try to print it.
	public static readonly ISymbol None = new SymbolNone();

	public static ISymbol FromChar(char value) {
		return Character.FromChar(value);
	}

	/**
	 * Implementation for the user defined blank symbol which will be retrieved from the config.
	 */
	private class SymbolBlank : ISymbol {
		public string ToPrint() {
			return ConfigManager.Config.Blank.ToString();
		}
	}

	/**
	 * Dummy implementation for a symbol, should only be used as an replacement of the null value.
	 * <p />
	 * This class will raise an error if you should try to place it onto the tape or if you try to print it.
	 */
	private class SymbolNone : ISymbol {
		public string ToPrint() {
			throw new NotSupportedException();
		}

		public bool IsNone() {
			return true;
		}
	}
}