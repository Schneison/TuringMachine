using TuringMachine.Utils;

namespace TuringMachine.UnitTests.Utils;

[TestFixture(new int[0])]
[TestFixture(new[] { 1 })]
[TestFixture(new[] { 1, 2 })]
[TestFixture(new[] { 1, 2, 5 })]
[TestFixture(new[] { 1, 2, 5, 7 })]
[TestFixture(new[] { 1, 2, 5, 7, 8 })]
[TestFixture(new[] { 1, 2, 5, 7, 8, 9 })]
[TestFixture(new[] { 1, 2, 5, 7, 8, 9, 1 })]
[TestFixture(new[] { 1, 2, 5, 7, 8, 9, 1, 4 })]
public class EnumerableExtensionsTest {
	private readonly int[] _testValues;

	public EnumerableExtensionsTest(int[] testValues) {
		_testValues = testValues;
	}

	[Test]
	public void ToTuple() {
		if (_testValues.Length is 0 or > 7) {
			Assert.Catch<InvalidOperationException>(() => _testValues.ToTuple());
			return;
		}

		var tuple = _testValues.ToTuple();
		Assert.That(tuple, Has.Length.EqualTo(_testValues.Length));
		for (var i = 0; i < _testValues.Length; i++) {
			Assert.That(tuple[i], Is.EqualTo(_testValues[i]));
		}
	}
}