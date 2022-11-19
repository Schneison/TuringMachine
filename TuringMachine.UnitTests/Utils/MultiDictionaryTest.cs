using TuringMachine.Utils;

namespace TuringMachine.UnitTests.Utils;

[TestFixture]
public class MultiDictionaryTest {
	[Test]
	public void Add() {
		var dict = new MultiDictionary<int, string> { { 1, "a" }, { 1, "b" }, { 2, "c" } };
		Assert.Multiple(() => {
			Assert.That(dict, Has.Count.EqualTo(2));
			Assert.That(dict[1], Has.Count.EqualTo(2));
			Assert.That(dict[2], Has.Count.EqualTo(1));
		});
		Assert.Multiple(() => {
			Assert.That(dict[1][0], Is.EqualTo("a"));
			Assert.That(dict[1][1], Is.EqualTo("b"));
			Assert.That(dict[2][0], Is.EqualTo("c"));
		});
	}

	[Test]
	public void Remove() {
		var dict = new MultiDictionary<int, string> { { 1, "a" }, { 1, "b" }, { 2, "c" } };
		dict.Remove(1, "a");
		Assert.Multiple(() => {
			Assert.That(dict, Has.Count.EqualTo(2));
			Assert.That(dict[1], Has.Count.EqualTo(1));
			Assert.That(dict[2], Has.Count.EqualTo(1));
		});
		Assert.Multiple(() => {
			Assert.That(dict[1][0], Is.EqualTo("b"));
			Assert.That(dict[2][0], Is.EqualTo("c"));
		});
	}

	[Test]
	public void RemoveAll() {
		var dict = new MultiDictionary<int, string> { { 1, "a" }, { 1, "b" }, { 2, "c" } };
		dict.RemoveAll(1);
		Assert.Multiple(() => {
			Assert.That(dict, Has.Count.EqualTo(1));
			Assert.Throws<KeyNotFoundException>(() => Assert.That(dict[1], Has.Count.EqualTo(0)));
			Assert.That(dict[2], Has.Count.EqualTo(1));
		});
		Assert.Multiple(() => { Assert.That(dict[2][0], Is.EqualTo("c")); });
	}

	[Test]
	public void Clear() {
		var dict = new MultiDictionary<int, string> { { 1, "a" }, { 1, "b" }, { 2, "c" } };
		dict.Clear();
		Assert.That(dict, Has.Count.EqualTo(0));
	}

	[Test]
	public void Contains() {
		var dict = new MultiDictionary<int, string> { { 1, "a" }, { 1, "b" }, { 2, "c" } };
		Assert.Multiple(() => {
			Assert.That(dict.Contains(1, "a"), Is.True);
			Assert.That(dict.Contains(1, "b"), Is.True);
			Assert.That(dict.Contains(2, "c"), Is.True);
			Assert.That(dict.Contains(1, "c"), Is.False);
			Assert.That(dict.Contains(2, "a"), Is.False);
			Assert.That(dict.Contains(3, "a"), Is.False);
		});
	}

	[Test]
	public void ContainsKey() {
		var dict = new MultiDictionary<int, string> { { 1, "a" }, { 1, "b" }, { 2, "c" } };
		Assert.Multiple(() => {
			Assert.That(dict.ContainsKey(1), Is.True);
			Assert.That(dict.ContainsKey(2), Is.True);
			Assert.That(dict.ContainsKey(3), Is.False);
		});
	}

	[Test]
	public void ContainsValue() {
		var dict = new MultiDictionary<int, string> { { 1, "a" }, { 1, "b" }, { 2, "c" } };
		Assert.Multiple(() => {
			Assert.That(dict.ContainsValue("a"), Is.True);
			Assert.That(dict.ContainsValue("b"), Is.True);
			Assert.That(dict.ContainsValue("c"), Is.True);
			Assert.That(dict.ContainsValue("d"), Is.False);
		});
	}

	[Test]
	public void TryGetValue() {
		var dict = new MultiDictionary<int, string> { { 1, "a" }, { 1, "b" }, { 2, "c" } };
		Assert.Multiple(() => {
			Assert.That(dict.TryGetValue(1, out var values), Is.True);
			Assert.That(values, Is.Not.Null);
			Assert.That(values, Has.Count.EqualTo(2));
			Assert.That(values![0], Is.EqualTo("a"));
			Assert.That(values![1], Is.EqualTo("b"));
			Assert.That(dict.TryGetValue(2, out values), Is.True);
			Assert.That(values, Is.Not.Null);
			Assert.That(values, Has.Count.EqualTo(1));
			Assert.That(values![0], Is.EqualTo("c"));
			Assert.That(dict.TryGetValue(3, out values), Is.False);
			Assert.That(values, Is.Null);
		});
	}
}