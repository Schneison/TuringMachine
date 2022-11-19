using System;

namespace TuringMachine.Data;

public class Character : ISymbol, IEquatable<Character> {
	private readonly char _value;

	private Character(char value) {
		_value = value;
	}

	public bool Equals(Character? other) {
		if (ReferenceEquals(null, other)) {
			return false;
		}

		if (ReferenceEquals(this, other)) {
			return true;
		}

		return _value == other._value;
	}

	public string ToPrint() {
		return _value.ToString();
	}

	public override bool Equals(object? obj) {
		if (ReferenceEquals(null, obj)) {
			return false;
		}

		if (ReferenceEquals(this, obj)) {
			return true;
		}

		if (obj.GetType() != GetType()) {
			return false;
		}

		return Equals((Character)obj);
	}

	public override int GetHashCode() {
		return _value.GetHashCode();
	}

	public static Character FromChar(char symbol) {
		return new Character(symbol);
	}
}