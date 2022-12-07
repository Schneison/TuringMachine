using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringMachine.Controller;
using TuringMachine.Data;
using TuringMachine.Model;

namespace TuringMachine.UnitTests.Controller;

[TestFixture]
internal class DesignControllerTest {
	[Test]
	public void CreateConnection() {
		var designSetup = new DesignSetupConfig(1, "");
		var controller = new DesignController();
		var entry = controller.CreateConnection(designSetup);
		Assert.That(entry.Mutations, Has.Count.EqualTo(designSetup.TapeCount));

	}
}

