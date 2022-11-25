using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using TuringMachine.Behaviors;

namespace TuringMachine.Model
{
    public class NodeElement : GraphElement, IDraggable
    {
        public const int StateRadius = 25;
        public static readonly Vector2 StateCenter = new(StateRadius);
        private Vector2 _position;
        private string _name;
        private bool _isStart;
        private bool _isFinal;

        public NodeElement() : this(new Vector2(0, 0), "q0", false, false) {
        }

        public NodeElement(Vector2 position, string name, bool isStart, bool isFinal) {
            _position = position;
            _name = name;
            _isStart = isStart;
            _isFinal = isFinal;
        }

        public Vector2 Position => _position;

        public float PositionX {
            get => _position.X;
            set => SetProperty(ref _position.X, value);
        }

        public float PositionY
        {
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

        public override string ToString() {
            return $"Name: {Name}, Position: {_position}, IsStart: {IsStart}, IsFinal: {IsFinal}";
        }

        public override bool Equals(object? obj) {
            if (obj is NodeElement node)
            {
                return node.Name == Name && node._position == _position && node.IsStart == IsStart && node.IsFinal == IsFinal;
            }

            return false;
        }

        public override int GetHashCode() {
            return HashCode.Combine(_position, Name, IsStart, IsFinal);
        }

        public void Dropped() {
            Console.WriteLine("Dropped");
        }

        public void Moved(double x, double y) {
            PositionX = (float)x;
            PositionY = (float)y;
        }
    }
}
