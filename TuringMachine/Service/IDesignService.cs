using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringMachine.Data;
using TuringMachine.Model;

namespace TuringMachine.Service;

public interface IDesignService {
	void CompleteSetup(DesignSetupConfig designSetup);

	DesignSetupConfig Setup { get; }

	ConnectionEntry CreateConnection();
}

