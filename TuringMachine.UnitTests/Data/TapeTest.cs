using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

[TestFixture]
public class TapeTest
{

	[Test]
	public void Empty()
    {
	    var tape = Tape.CreateEmpty();
		Assert.That(tape, Has.Length.EqualTo(0));
        Assert.Multiple(() =>
        {
            Assert.That(tape.Head, Is.EqualTo(SymbolNone));
            Assert.That(tape.ToPrint(), Is.Empty);
        });
        Assert.Multiple(() =>
        {
	        Assert.That(tape.Head, Is.EqualTo(SymbolNone));
	        Assert.That(tape.ToPrint(), Is.Empty);
        });
    }

    [Test]
	public void ApplyMutation()
	{
		var trxLeft = MockMove(SymbolA, Direction.Left);
		var trxNone = MockMove(Direction.None);
		var trxRight = MockMove(Direction.Right);

		var tape = Tape.CreateBlank();
		Assert.That(tape, Has.Length.EqualTo(1));
		Assert.That(tape.Head, Is.EqualTo(SymbolBlank));

		// <- _
		tape.ApplyMutation(trxLeft);
		Assert.That(tape, Has.Length.EqualTo(2));

		// _ -> a
		tape.ApplyMutation(trxRight);
		Assert.That(tape, Has.Length.EqualTo(2));
		Assert.That(tape.ToPrint(), Is.EqualTo(SymbolBlank.ToPrint() + "a"));

		// _a ->
		Assert.Throws<NotSupportedException>(() =>
		{
			// Should fail because a is no blank
			tape.ApplyMutation(trxRight);
		});
		// tape.ApplyTransaction(trxRight);
		// Assert.That(tape, Has.Length.EqualTo(3));
	}
}