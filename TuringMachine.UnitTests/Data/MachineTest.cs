using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

[TestFixture]
public class MachineTest {
	[Test]
	public void Test() {
		var design = new Design(1);
		var machine = new Machine(design);
		machine.TryApplyTransition();
	}
}