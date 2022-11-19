using NUnit.Framework;
using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data; 

[TestFixture]
public class SymbolTest {
	[Test]
	public void None()
    {
        Assert.Multiple(() =>
        {
            Assert.Catch<NotSupportedException>(() => SymbolNone.ToPrint());
            Assert.That(SymbolNone, Is.True);
        });
    }
}