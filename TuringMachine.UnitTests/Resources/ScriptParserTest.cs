using System.Text;
using NUnit.Framework;
using TuringMachine.Resources;

namespace TuringMachine.UnitTests.Resources; 

[TestFixture]
public class ScriptParserTest {
	private static string CreateValidScript() {
		var builder = new StringBuilder();
		builder.AppendLine("1 0 1 R");
		builder.AppendLine("1 1 1 R");
		builder.AppendLine("1 B 2 L");
		builder.AppendLine("2 0 2 L");
		
		return builder.ToString();
	}
	
	private static string CreateEmptyScript() {
		var builder = new StringBuilder();
		builder.AppendLine("init: start");
		builder.AppendLine("accept: end");
		builder.AppendLine("name: empty");

		return builder.ToString();
	}
	
	private static string CreateInvalidScript() {
		var builder = new StringBuilder();
		builder.AppendLine("1 0 1 R");
		builder.AppendLine("1 1 1 R");
		builder.AppendLine("1 B 2 L");
		builder.AppendLine("2 0 2 L");
		
		return builder.ToString();
	}
	
	[Test]
	public void TestParse() {
		var parser = ScriptParser.CreateFromString(CreateEmptyScript());
		Assert.DoesNotThrow(() => {
			var graph = parser.Parse();
			Assert.That(graph, Is.Not.Null);
		});
	}
}