using NUnit.Framework;
using TuringMachine.Data;

namespace TuringMachine.UnitTests.Data;

[TestFixture]
public class MeshTest
{
	[Test]
	public void BuilderValidation()
	{
		var builder = Mesh.CreateBuilder();
		Assert.Catch<NotSupportedException>(() =>
		{
			builder.FromTape(2).WithTransition(TrxFrom(SymbolA, SymbolA)).Create();
		});
		Assert.Catch<NotSupportedException>(() =>
		{
			builder.FromTape(1).WithTransition(TrxFromPairs((SymbolA, SymbolA), (SymbolA, SymbolA))).Create();
		});
	}

	[Test]
	public void Accepts()
	{
		var mesh = Mesh.CreateBuilder()
			.WithTransition(TrxFrom(SymbolA, SymbolA)).Create();
		var other = Mesh.CreateBuilder()
			.FromTape(2).WithTransition(TrxFromPairs((SymbolA, SymbolA), (SymbolA, SymbolA))).Create();
		Assert.Multiple(() =>
		{
			Assert.That(mesh.Accepts(Tuple.Create(SymbolA)), Is.True);
			Assert.That(mesh.Accepts(Tuple.Create(SymbolB)), Is.False);
			Assert.That(other.Accepts(Tuple.Create(SymbolA)), Is.False);
			Assert.That(other.Accepts(Tuple.Create(SymbolA, SymbolA)), Is.True);
			Assert.That(other.Accepts(Tuple.Create(SymbolB, SymbolA)), Is.False);
		});
	}

	[Test]
	public void Index()
	{
		var mesh = Mesh.CreateBuilder()
			.WithTransition(TrxFrom(SymbolA, SymbolA)).Create();
		Assert.Multiple(() =>
		{
			Assert.Catch<KeyNotFoundException>(() => { Assert.That(mesh[(SymbolA, SymbolA)], Is.Not.Null); });
			Assert.That(mesh[Tuple.Create(SymbolA)], Is.Not.Null);
		});
	}

	[Test]
	public void Find()
	{
		var mesh = Mesh.CreateBuilder()
			.WithTransition(TrxFrom(SymbolA, SymbolA)).Create();
		mesh.Find(Array.Empty<ISymbol>());
	}
}