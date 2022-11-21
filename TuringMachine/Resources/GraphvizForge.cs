using QuikGraph;
using QuikGraph.Graphviz;

namespace TuringMachine.Resources; 

public class GraphvizForge {
    public void Create() {
        var graphviz = new GraphvizAlgorithm<string, Edge<string>>(new AdjacencyGraph<string, Edge<string>>());
        graphviz.FormatVertex += (sender, args) => args.VertexFormat.Label = args.Vertex;
        graphviz.FormatEdge += (sender, args) => args.EdgeFormat.Label.Value = args.Edge.ToString();
        graphviz.Generate(new FileDotEngine(), "graph");
    }
}