using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using DotNetGraph;
using DotNetGraph.Extensions;
using TuringMachine.Utils;

namespace TuringMachine;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
	protected override void OnActivated(EventArgs e) {
		base.OnActivated(e);
		var lines = File.ReadAllLines("3_1_band.txt");
		var segments = new List<List<string>>();
		var current = new List<string>();
		var last = "";
		foreach (var line in lines) {
			last = line;
			if (line == "") {
				segments.Add(current);
				current = new List<string>();
			}
			else {
				current.Add(line);
			}
		}

		if (last != "") {
			segments.Add(current);
		}

		var directedGraph = new DotGraph("MyDirectedGraph", true);

		// Parse transactions
		var turingMachine = ParseFile(segments);

		// Add edges / connections to the graph
		foreach (var (key, value) in turingMachine.Connections) {
			directedGraph.AddEdge(key.from, key.to, edge => {
				// Add divider to transitions of we have multi tape
				edge.Label = turingMachine.MultiTape
					? string.Join("\n----------\n", value)
					: string.Join("\n", value);
			});
		}

		// Create attributes for layout and font
		var attributes = new Dictionary<string, string> {
			["rankdir"] = "LR",
			["fontname"] = "\"Helvetica,Arial,sans-serif\""
		};

		// Convert graph to text
		var dot = directedGraph.Compile(true);
		// Split text graph in lines to add own attributes
		var result = dot.Split('\r', '\n').ToList();
		// Insert own attributes because the library does not support them 
		var attributesText = string.Join("\n", attributes.Select(x => $"\t{x.Key}={x.Value};"));
		// At important attributes to the start, so that they don't get overriden 
		result.Insert(1, "\tnode[shape=circle];");
		result.Insert(2, $"\t{turingMachine.End}[shape=doublecircle];");
		result.Insert(3, "\tstart[shape=square];");
		// Add none important attributes to the end
		result.Insert(result.Count - 1, attributesText);
		// Save it to a file
		File.WriteAllLines("myFile.dot", result);
	}

	private static TmGraph ParseFile(List<List<string>> segmentList) {
		var firstSegment = segmentList[0];
		var name = "";
		var startState = "";
		var endState = "";
		var multiTape = false;
		foreach (var segLine in firstSegment) {
			if (segLine.StartsWith("name")) {
				name = segLine.Replace("name:", "").Trim();
			}
			else if (segLine.StartsWith("init")) {
				startState = segLine.Replace("init:", "").Trim();
			}
			else if (segLine.StartsWith("accept")) {
				endState = segLine.Replace("accept:", "").Trim();
			}
		}

		// Parse transactions
		var connections = new MultiDictionary<(string from, string to), string> { { ("start", startState), "" } };
		foreach (var segment in segmentList.GetRange(1, segmentList.Count - 1)
			         .Where(transitions => transitions.Count != 0)) {
			// Transitions only consist of two lines, this has to be an error or comment
			if (segment.Count == 1) {
				Console.Out.Write("Could not analyse line: " + segment[0]);
				continue;
			}

			var first = segment[0].Split(',');
			var second = segment[1].Split(',');
			// Checks if the transaction is given for the right tape amount
			var singleTapeFirst = first.Length > 2;
			var singleTapeSecond = second.Length > 3;
			// Check if this transaction has the same tape amount like the other transactions
			if (singleTapeFirst != singleTapeSecond || (multiTape && !(singleTapeFirst && singleTapeSecond))) {
				throw new InvalidOperationException();
			}

			// Sets the tape to double if this transaction has a "double tape"
			multiTape |= singleTapeFirst;
			// Get start and end Node
			var start = first[0];
			var end = second[0];
			// Range from second to last element. (can be the same)
			var value = first[1..first.Length];
			value = value.Select(v => v.Replace("_", "□")).ToArray();
			// Range from second element to second last element. (can be the same)
			var valueReplace = second[1..^1];
			valueReplace = valueReplace.Select(v => v.Replace("_", "□")).ToArray();
			var move = second[^1];
			// Map script name to tm direction
			var realMove = move switch {
				">" => "R",
				"<" => "L",
				_ => "N"
			};
			var label = new string[value.Length];
			for (var i = 0; i < value.Length; i++) {
				label[i] = string.Join(" | ", value[i], valueReplace[i], realMove);
			}

			connections.Add((start, end), string.Join("\n", label));
		}

		return new TmGraph(connections, startState, endState, name, multiTape);
	}

	private record TmGraph(
		MultiDictionary<(string from, string to), string> Connections,
		string Start, string End,
		string Name, bool MultiTape
	);
}