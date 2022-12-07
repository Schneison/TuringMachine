using System;
using TuringMachine.Data;
using TuringMachine.Model;

namespace TuringMachine.Service;

/**
 *	Describes functions used for the design of the turing machine transitions and variables.
 *	This controller is mainly used by the designer view model.
 */
public class DesignService : IDesignService {
	public void CompleteSetup(DesignSetupConfig designSetup) {
		Setup = designSetup;
	}

	public DesignSetupConfig Setup { get; private set; } = DesignSetupConfig.EMPTY;

	public ConnectionEntry CreateConnection() {
		ConnectionEntry entry = new();
		for (var i = 0; i < Setup.TapeCount; i++) {
			entry.Mutations.Add(new MutationEntry());
		}
		return entry;
	}
	
}