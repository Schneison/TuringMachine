namespace TuringMachine.Data;

/// <summary>
/// </summary>
public class Engine {
    private Machine _machine;

    public void Run(Design design) {
        _machine = new Machine(design);
    }
}