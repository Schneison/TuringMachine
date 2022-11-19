using System.Text;
using NUnit.Framework;
using TuringMachine.Resources;

namespace TuringMachine.UnitTests.Resources; 

[TestFixture]
public class ScriptParserTest {
	private static string CreateValidScript() {
		var builder = new StringBuilder();
		builder.AppendLine("init: first");
		builder.AppendLine("accept: last");
		builder.AppendLine("name: valid");
		builder.AppendLine("");
		builder.AppendLine("first,0");
		builder.AppendLine("last,1,>");
		
		return builder.ToString();
	}
	
	private static string CreateEmptyScript() {
		var builder = new StringBuilder();
		builder.AppendLine("init: start");
		builder.AppendLine("accept: end");
		builder.AppendLine("name: empty");

		return builder.ToString();
	}
	
	private static string CreateInvalidHeader() {
		var builder = new StringBuilder();
		builder.AppendLine("init: first");

		return builder.ToString();
	}
	
	private static string CreateInvalidConnection() {
		var builder = new StringBuilder();
		builder.AppendLine("init: f");
		builder.AppendLine("accept: l");
		builder.AppendLine("name: invalid");
		builder.AppendLine("");
		builder.AppendLine("f,0,1");
		builder.AppendLine("l,1,2,>");
		builder.AppendLine("");
		builder.AppendLine("f,0");
		builder.AppendLine("l,1,>");
		
		return builder.ToString();
	}

	[Test]
	public void TestParse() {
		var parser = ScriptParser.CreateFromString(CreateEmptyScript());
		var graph = parser.Parse();
		// start market to start state is always added
		Assert.That(graph.Connections, Has.Count.EqualTo(1));
		parser = ScriptParser.CreateFromString(CreateValidScript());
		graph = parser.Parse();
		// graph should have 2 connections, start marker to start state and 1 connection from first to last
		Assert.That(graph.Connections, Has.Count.EqualTo(2));
		Assert.Catch<ScriptException>(() => {
			parser = ScriptParser.CreateFromString(CreateInvalidHeader());
			parser.Parse();
		});
		Assert.Catch<ScriptException>(() => {
			parser = ScriptParser.CreateFromString(CreateInvalidConnection());
			parser.Parse();
		});
	}
	
	[Test]
	public void DotForge() {
		var parser = ScriptParser.CreateFromString(CreateValidScript());
		var forge = new DotForge(parser.Parse());
		var dot = forge.Create();
		Assert.NotNull(dot);
	}
}