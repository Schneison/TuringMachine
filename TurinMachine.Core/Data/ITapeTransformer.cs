namespace TuringMachine.Data;

public interface ITapeTransformer {
    ISymbol ToTape(ISymbol input);
}