using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

[TestFixture]
public class AlphabetTest {
	[Test]
	public void Accepts() {
		var alphabet = Alphabet.CreateBuilder()
			.WithSymbol(SymbolA).Create();
		Assert.Multiple(() => {
			Assert.That(alphabet, Has.Length.EqualTo(1));
			Assert.That(alphabet, Contains.Item(SymbolA));
			Assert.That(alphabet, Is.Not.Contains(SymbolB));
		});
		Assert.Multiple(() => {
			Assert.That(alphabet.Contains(SymbolA), Is.True);
			Assert.That(alphabet.Contains(SymbolB), Is.False);
		});
		Assert.Multiple(() => {
			Assert.That(alphabet.Accepts("a"), Is.True);
			Assert.That(alphabet.Accepts("A"), Is.False);
			Assert.That(alphabet.Accepts("b"), Is.False);
		});
		var other = alphabet.ToBuilder()
			.WithSymbol(SymbolB).WithoutSymbol(SymbolA).Create();
		Assert.Multiple(() => {
			Assert.That(other, Is.Not.EqualTo(alphabet));
			Assert.That(other, Has.Length.EqualTo(1));
			Assert.That(other, Contains.Item(SymbolB));
			Assert.That(other, Is.Not.Contains(SymbolA));
		});
		Assert.Multiple(() => {
			Assert.That(other.Contains(SymbolB), Is.True);
			Assert.That(other.Contains(SymbolA), Is.False);
		});
		Assert.Multiple(() => {
			Assert.That(other.Accepts("b"), Is.True);
			Assert.That(other.Accepts("A"), Is.False);
			Assert.That(other.Accepts("a"), Is.False);
		});
	}
}