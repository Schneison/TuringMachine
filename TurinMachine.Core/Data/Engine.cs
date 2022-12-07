namespace TuringMachine.Data;

/// <summary>
/// </summary>
public class Engine {
	private Machine _machine;

	public void Run(DesignSetupConfig designSetup) {
		_machine = new Machine(designSetup);
	}
}