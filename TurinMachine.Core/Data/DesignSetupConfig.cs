namespace TuringMachine.Data;

public record DesignSetupConfig(int TapeCount, string Name) {
	public static readonly DesignSetupConfig EXAMPLE = new(1, "");
	public static readonly DesignSetupConfig EMPTY = new (0, "");
}