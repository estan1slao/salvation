using NUnit.Framework;
using System.Numerics;

namespace Salvation_v2;

[TestFixture]
public class Globals_Should
{
    [Test]
    [TestCase(0f, 0f, 1f, 0f, 1f)]
    [TestCase(0f, 0f, -1f, 0f, 1f)]
    [TestCase(0f, 0f, 0f, 1f, 1f)]
    [TestCase(0f, 0f, 0f, -1f, 1f)]
    [TestCase(0f, 0f, 1f, 1f, 1.4142135623730950488f)]
    [TestCase(0f, 0f, -1f, 1f, 1.4142135623730950488f)]
    [TestCase(0f, 0f, -1f, -1f, 1.4142135623730950488f)]
    [TestCase(0f, 0f, 1f, -1f, 1.4142135623730950488f)]
    public void MeasuringDistance(float x1, float y1, float x2, float y2, float result)
    {
        var start = new Vector2(x1, y1);
        var end = new Vector2(x2, y2);
        Assert.AreEqual(Globals.GetDistance(start, end), result, 1e-6);
    }
}
