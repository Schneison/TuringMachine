using System.Collections.Immutable;
using TuringMachine.Utils;

namespace TuringMachine.Resources;

using Segment = ImmutableList<string>;

/// <summary>
///     Parses a script that describes a turing machine.
/// </summary>
public class ScriptParser {
    /// <summary>
    ///     Creates a new parser with the given segments.
    /// </summary>
    /// <param name="segments">All segments of the graph.</param>
    private ScriptParser(ImmutableList<Segment> segments) {
        Segments = segments;
    }

    /// <summary>
    ///     All segments of the script.
    /// </summary>
    private ImmutableList<Segment> Segments { get; }

    /// <summary>
    ///     Creates a new script parser with the lines from the given file path.
    /// </summary>
    /// <param name="path">Specified path to the script file.</param>
    /// <returns>A new script parser.</returns>
    public static ScriptParser CreateFromPath(string path) {
        return CreateFromLines(File.ReadAllLines(path));
    }

    /// <summary>
    ///     Creates a new script parser with the given lines.
    /// </summary>
    /// <param name="txt">Script in one line.</param>
    /// <returns>A new script parser.</returns>
    public static ScriptParser CreateFromString(string txt) {
        return CreateFromLines(txt.Split('\n'));
    }

    /// <summary>
    ///     Creates a new script parser with the given lines.
    /// </summary>
    /// <param name="lines">All lines of the script.</param>
    /// <returns>A new script parser.</returns>
    public static ScriptParser CreateFromLines(IEnumerable<string> lines) {
        return new ScriptParser(CreateSegments(lines));
    }

    /// <summary>
    ///     Creates segments from a list of lines.
    /// </summary>
    /// <param name="lines">Lines read from the script.</param>
    /// <returns>A list of the different segments.</returns>
    private static ImmutableList<Segment> CreateSegments(IEnumerable<string> lines) {
        var segments = ImmutableList.CreateBuilder<Segment>();
        var current = ImmutableList.CreateBuilder<string>();
        foreach (var line in lines) {
            if (string.IsNullOrEmpty(line.Trim())) {
                segments.Add(current.ToImmutable());
                current.Clear();
            }
            else {
                current.Add(line);
            }
        }

        if (current.Count > 0) {
            segments.Add(current.ToImmutable());
        }

        return segments.ToImmutable();
    }

    /// <summary>
    ///     Parses the script.
    /// </summary>
    /// <returns>Parsed graph</returns>
    public ScriptGraph Parse() {
        var firstSegment = Segments[0];
        var header = new GraphHeader("", "", "");
        foreach (var segLine in firstSegment) {
            if (segLine.StartsWith("name")) {
                header = header with { Name = segLine.Replace("name:", "").Trim() };
            }
            else if (segLine.StartsWith("init")) {
                header = header with { StartState = segLine.Replace("init:", "").Trim() };
            }
            else if (segLine.StartsWith("accept")) {
                header = header with { EndState = segLine.Replace("accept:", "").Trim() };
            }
        }

        if (!header.IsValid()) {
            throw new ScriptException("Invalid header, missing name, init or accept.");
        }

        // Defines if the machine has more than one tape.
        var multiTape = false;

        // Parse transactions
        var connections = new MultiDictionary<Connection, string>
            { { new Connection("start", header.StartState), "" } };
        foreach (var segment in Segments.GetRange(1, Segments.Count - 1)
                     .Where(transitions => transitions.Count != 0)) {
            ParseConnection(segment, ref multiTape, connections);
        }

        return new ScriptGraph(connections, multiTape, header);
    }

    /// <summary>
    ///     Parses a connection from a segment.
    /// </summary>
    /// <param name="segment">Segment which should contain to lines.</param>
    /// <param name="multiTape">Reference to a boolean which defines if the resulting machine has more than one tape.</param>
    /// <param name="connections">All connections in a dict</param>
    /// <exception cref="ScriptException">Thrown if the parser encounters an error</exception>
    public static void ParseConnection(Segment segment, ref bool multiTape,
        MultiDictionary<Connection, string> connections) {
        // Transitions only consist of two lines, this has to be an error or comment
        if (segment.Count == 1) {
            Console.Out.Write("Could not analyse line: " + segment[0]);
            return;
        }

        var first = segment[0].Split(',');
        var second = segment[1].Split(',');
        // Checks if the transaction is given for the right tape amount
        var singleTapeFirst = first.Length > 2;
        var singleTapeSecond = second.Length > 3;
        // Check if this transaction has the same tape amount like the other transactions
        if (singleTapeFirst != singleTapeSecond || (multiTape && !(singleTapeFirst && singleTapeSecond))) {
            throw new ScriptException(
                "Transaction has different tape count than other transactions in the turing machine.");
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

        connections.Add(new Connection(start, end), string.Join("\n", label));
    }
}