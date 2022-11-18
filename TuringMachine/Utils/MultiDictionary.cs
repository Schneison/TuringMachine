using System.Collections.Generic;

namespace TuringMachine.Utils;

public class MultiDictionary<TKey, TValue> : Dictionary<TKey, List<TValue>> where TKey : notnull {

	public void Add(TKey key, TValue value) {
		if (!this.TryGetValue(key, out var values)) {
			values = new List<TValue>();
			this.Add(key, values);
		}
		values.Add(value);
	}

}