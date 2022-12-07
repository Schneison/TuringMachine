using System;
using TuringMachine.Data;
using TuringMachine.Model;

namespace TuringMachine.Service;

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