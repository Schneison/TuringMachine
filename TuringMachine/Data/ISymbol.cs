using System;
using System.Collections.Generic;

namespace TuringMachine.Data;

public interface ISymbol : IPrintable {
	
	bool IsNone() {
		return false;
	}
	
}