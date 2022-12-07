namespace TuringMachine.Data;

/// <summary>
/// Describes the basic setup configuration for a turing machine, all contained values are consistent over the whole design cycle.
/// </summary>
/// <param name="TapeCount">Tape amount of the machine</param>
/// <param name="Name">Name of the machine, only used top define file name and name of the graph.</param>
public record DesignSetupConfig(int TapeCount, string Name) {
	/// <summary>
	/// Mockup value 
	/// </summary>
	public static readonly DesignSetupConfig EXAMPLE = new(1, "");
	/// <summary>
	/// None value used in all cases where no design setup is available or null would be used.
	/// </summary>
	public static readonly DesignSetupConfig EMPTY = new (0, "");

	/// <summary>
	/// Checks if the given config is valid.
	/// </summary>
	/// <returns></returns>
	public bool IsValid() {
		return TapeCount != 0 && !string.IsNullOrEmpty(Name);
	}
}