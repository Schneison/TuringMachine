using DotNetGraph;
using DotNetGraph.Extensions;
using DotNetGraph.Node;

namespace TuringMachine.Resources;

/// <summary>
///     Forge class to create DotGraphs from script graphs.
/// </summary>
public class DotForge {
    /// <summary>
    ///     Dot attributes.
    /// </summary>
    private readonly Dictionary<string, string> _attributes = new();

    /// <summary>
    ///     Associated script graph.
    /// </summary>
    private readonly ScriptGraph _graph;

    public DotForge(ScriptGraph graph) {
        _graph = graph;
        // Add default attributes for layout and font
        WithAttribute("rankdir", "LR");
        WithAttribute("fontname", "\"Helvetica,Arial,sans-serif\"");
    }

    /// <summary>
    ///     Add a new attribute to the graph.
    /// </summary>
    /// <param name="key">Specified key</param>
    /// <param name="value">Specified value</param>
    /// <returns>Instance of the forge for chaining.</returns>
    public DotForge WithAttribute(string key, string value) {
        _attributes[key] = value;
        return this;
    }

    /// <summary>
    ///     Create a DotGraph from the associated script graph.
    /// </summary>
    /// <returns>A string containing the graph.</returns>
    public IEnumerable<string> Create() {
        var graph = new DotGraph(_graph.Header.Name, true);

        graph.AddNode("node", node => { node.Shape = DotNodeShape.Circle; });

        graph.AddNode(_graph.Header.EndState, node => { node.Shape = DotNodeShape.DoubleCircle; });

        graph.AddNode("start", node => { node.Shape = DotNodeShape.Square; });

        // Add edges / connections to the graph
        foreach (var (key, value) in _graph.Connections) {
            graph.AddEdge(key.FromState, key.ToState, edge => {
                // Add divider to transitions of we have multi tape
                edge.Label = _graph.MultiTape
                    ? string.Join("\n----------\n", value)
                    : string.Join("\n", value);
            });
        }

        // Convert graph to text
        var dot = graph.Compile(true);
        // Split text graph in lines to add own attributes
        var result = dot.Split('\r', '\n').ToList();
        // Insert own attributes because the library does not support them 
        var attributesText = string.Join("\n", _attributes.Select(x => $"\t{x.Key}={x.Value};"));
        // Add none important attributes to the end
        result.Insert(result.Count - 1, attributesText);
        return result;
    }

    /// <summary>
    ///     Create a DotGraph from the associated script graph and write it to a file.
    /// </summary>
    /// <param name="path">Path of the file</param>
    public void ToFile(string path) {
        File.WriteAllLines(path, Create());
    }
}