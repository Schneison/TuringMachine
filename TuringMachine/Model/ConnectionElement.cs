﻿using System.ComponentModel;
using System.Globalization;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TuringMachine.Model;

/// <summary>
/// Connection element model for the graph
/// </summary>
public class ConnectionElement : GraphElement {
	private readonly PropertyChangedEventHandler _changedHandler;
	private bool _alternativeDirection;
	private StateElement _end;
	private StateElement _start;

	public ConnectionElement(StateElement start, StateElement end) : this(start, end, true) {
	}

	public ConnectionElement(StateElement start, StateElement end, bool alternativeDirection) {
		_start = start;
		_end = end;
		_alternativeDirection = alternativeDirection;
		_changedHandler = (_, _) => {
			OnPropertyChanged(nameof(PositionX));
			OnPropertyChanged(nameof(PositionY));
			OnPropertyChanged(nameof(TextOffset));
		};
		_start.PropertyChanged += _changedHandler;
		_end.PropertyChanged += _changedHandler;
	}

	public bool IsSelfLoop => _start.Equals(_end);

	public Vector2 StartPoint => (_start.Position - _end.Position) / 2
	                                                                   + Dir * StateElement.StateRadius;

	public Vector2 EndPoint => (_end.Position - _start.Position) / 2
	                                                                 + -Dir * StateElement.StateRadius;

	public Vector2 Center => (_start.Position + _end.Position) / 2 + StateElement.StateCenter;

	public Vector2 Dir => Vector2.Normalize(_end.Position - _start.Position);

	public Vector2 Perpendicular {
		get {
			var dir = Dir;
			return AlternativeDirection ? new Vector2(dir.Y, -dir.X) : new Vector2(-dir.Y, dir.X);
		}
	}

	public float PositionX => Center.X;

	public float PositionY => Center.Y;

	public Thickness TextOffset {
		get {
			var overflow = TextOverflow();
			return new Thickness(overflow.X, overflow.Y, 0, 0);
		}
	}

	public bool AlternativeDirection {
		get => _alternativeDirection;
		set => SetProperty(ref _alternativeDirection, value);
	}

	public StateElement Start {
		get => _start;
		set => SetProperty(ref _start, value);
	}

	public StateElement End {
		get => _end;
		set => SetProperty(ref _end, value);
	}

	public Vector2 TextOverflow() {
		return IsSelfLoop switch {
			true when _alternativeDirection => new Vector2(0, StateElement.StateRadius * 2),
			true => new Vector2(0, -StateElement.StateRadius * 2),
			_ => Perpendicular * 0
		};
	}

	public void OnRemoveNode() {
		_start.PropertyChanged -= _changedHandler;
		_end.PropertyChanged -= _changedHandler;
	}
}