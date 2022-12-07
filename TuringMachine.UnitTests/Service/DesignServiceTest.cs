using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringMachine.Data;
using TuringMachine.Model;
using TuringMachine.Service;

namespace TuringMachine.UnitTests.Service;

[TestFixture]
internal class DesignServiceTest {
	private readonly DesignSetupConfig _oneTape = new (1, "");

	[Test]
	public void CompleteSetup() {
		var service = new DesignService();
		service.CompleteSetup(_oneTape);
		Assert.That(service.Setup, Is.EqualTo(_oneTape));

	}

	[Test]
	public void CreateConnection() {
		var service = new DesignService();
		service.CompleteSetup(_oneTape);
		var entry = service.CreateConnection();
		Assert.That(entry.Mutations, Has.Count.EqualTo(1));

	}
}

