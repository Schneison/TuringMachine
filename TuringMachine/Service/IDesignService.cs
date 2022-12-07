using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringMachine.Data;
using TuringMachine.Model;

namespace TuringMachine.Service;

/// <summary>
/// Describes functions used for the design of the turing machine transitions and variables.
/// This service is mainly used by the designer view model.
/// </summary>
public interface IDesignService {
	/// <summary>
	/// Supplies a setup config to the design service, should be called after the initial setup of the turing machine design cycle.
	/// </summary>
	/// <param name="designSetup">Specified config, contains information about the machine supplied by the user.</param>
	void CompleteSetup(DesignSetupConfig designSetup);

	/// <summary>
	/// Returns the current setup config, if no setup has been completed, returns an empty config.
	/// </summary>
	DesignSetupConfig Setup { get; }

	/// <summary>
	/// Creates new connection entry, with mutations for each tape.
	/// </summary>
	ConnectionEntry CreateConnection();
}

