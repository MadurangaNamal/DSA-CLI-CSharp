namespace Practice.Models.Samples;

public record Point(int X, int Y);

public abstract record Shape(bool IsRound);

public record Circle(decimal Radius) : Shape(true);

public record Rectangle(int Length, int Wdith) : Shape(false);
