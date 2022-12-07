using TuringMachine.Data;
using TuringMachine.Model;

namespace TuringMachine.Controller;

/**
 *	Describes functions used for the design of the turing machine transitions and variables.
 *	This controller is mainly used by the designer view model.
 */
public class DesignController {
	public ConnectionEntry CreateConnection(DesignSetupConfig designSetup) {
		ConnectionEntry entry = new();
		for (var i = 0; i < designSetup.TapeCount; i++) {
			entry.Mutations.Add(new MutationEntry());
		}
		return entry;
	}
	
}