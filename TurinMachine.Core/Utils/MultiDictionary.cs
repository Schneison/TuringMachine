namespace TuringMachine.Utils;

/// <summary>
///     A dictionary implementation that can be used to associate a key with a list of values.
/// </summary>
/// <typeparam name="TKey">key type</typeparam>
/// <typeparam name="TValue">value type</typeparam>
public class MultiDictionary<TKey, TValue> : Dictionary<TKey, List<TValue>> where TKey : notnull {
    /// <summary>
    ///     Adds a value to the list of values associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">Value to add.</param>
    public void Add(TKey key, TValue value) {
        if (!TryGetValue(key, out var values)) {
            values = new List<TValue>();
            Add(key, values);
        }

        values.Add(value);
    }

    /// <summary>
    ///     Removes the first occurrence of a specific object from the list of values associated with the specified key.
    /// </summary>
    /// <param name="key">Key of the element to remove.</param>
    /// <param name="value">Value to remove.</param>
    public void Remove(TKey key, TValue value) {
        if (TryGetValue(key, out var values)) {
            values.Remove(value);
        }
    }


    /// <summary>
    ///     Removes all the values associated with the specified key.
    /// </summary>
    /// <param name="key">Key of the elements to remove.</param>
    public void RemoveAll(TKey key) {
        Remove(key);
    }

    /// <summary>
    ///     Checks if the specified key has is associated with the specified value.
    /// </summary>
    /// <param name="key">Key of the element to check.</param>
    /// <param name="value">Value to check.</param>
    public bool Contains(TKey key, TValue value) {
        return TryGetValue(key, out var values) && values.Contains(value);
    }

    /// <summary>
    ///     Checks if any key is associated with the specified value.
    /// </summary>
    /// <param name="value">Value to check.</param>
    public bool ContainsValue(TValue value) {
        return Values.Any(values => values.Contains(value));
    }
}