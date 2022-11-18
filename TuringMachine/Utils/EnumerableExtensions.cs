using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TuringMachine.Utils;

public static class EnumerableExtensions
{
	public static ITuple ToTuple<T>(this IEnumerable<T> str)
	{
		var asArray = str.ToArray();
		return CreateTuple(asArray);
	}

	public static ITuple CreateTuple<T>(T[] array)
	{
		return array.Length switch
		{
			0 => throw new InvalidOperationException("Tried to create tuple from array which was empty."),
			1 => Tuple.Create(array[0]),
			2 => Tuple.Create(array[0], array[1]),
			3 => Tuple.Create(array[0], array[1], array[2]),
			4 => Tuple.Create(array[0], array[1], array[2], array[3]),
			5 => Tuple.Create(array[0], array[1], array[2], array[3], array[4]),
			6 => Tuple.Create(array[0], array[1], array[2], array[3], array[4], array[5]),
			7 => Tuple.Create(array[0], array[1], array[2], array[3], array[4], array[5], array[6]),
			_ => throw new InvalidOperationException("Tried to create tuple from array which was to large."),
		};
	}
}