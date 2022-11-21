namespace TuringMachine.Data;

public record Design(int TapeCount, string Name) {
    
    public static readonly Design EXAMPLE = new Design(1, "");
}