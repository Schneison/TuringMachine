using System;
using System.Numerics;
using TuringMachine.Behaviors;

namespace TuringMachine.Model; 

public class NodeElement : GraphElement, IDraggable {
    public static readonly int StateRadius = 25;
    public static readonly Vector2 StateCenter = new(StateRadius);
    private bool _isFinal;
    private bool _isStart;
    private string _name;
    private Vector2 _position;

    public NodeElement() : this(new Vector2(0, 0), "q0", false, false) {
    }

    public NodeElement(Vector2 position, string name, bool isStart, bool isFinal) {
        _position = position;
        _name = name;
        _isStart = isStart;
        _isFinal = isFinal;
    }

    public float PositionX {
        get => _position.X;
        set => SetProperty(ref _position.X, value);
    }

    public float PositionY {
        get => _position.Y;
        set => SetProperty(ref _position.Y, value);
    }

    public string Name {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public bool IsStart {
        get => _isStart;
        set => SetProperty(ref _isStart, value);
    }

    public bool IsFinal {
        get => _isFinal;
        set => SetProperty(ref _isFinal, value);
    }

    public Vector2 Position => _position;

    public void Dropped() {
        Console.WriteLine("Dropped");
    }

    public void Moved(double x, double y) {
        PositionX = (float)x;
        PositionY = (float)y;
    }

    public override string ToString() {
        return $"Name: {Name}, Position: {_position}, IsStart: {IsStart}, IsFinal: {IsFinal}";
    }

    public override bool Equals(object? obj) {
        if (obj is NodeElement node) {
            return node.Name == Name && node.IsStart == IsStart && node.IsFinal == IsFinal;
        }

        return false;
    }

    public override int GetHashCode() {
        return HashCode.Combine(Name, IsStart, IsFinal);
    }
}