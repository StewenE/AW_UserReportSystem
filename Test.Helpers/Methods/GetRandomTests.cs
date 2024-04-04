using Helpers.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Helpers.Methods {

	[TestClass]
    public class GetRandomTests : BaseTests {
        protected override Type? type => typeof(GetRandom);
		[TestMethod] public void BoolTest() => testRnd(GetRandom.Bool);
		[TestMethod] public void CharTest() => testRnd(() => GetRandom.Char(), GetRandom.Char);
		[TestMethod] public void DateTimeTest() => testRnd(() => GetRandom.DateTime(), (x, y) => GetRandom.DateTime(x, y));
		[TestMethod] public void DoubleTest() => testRnd(() => GetRandom.Double(), GetRandom.Double);
		[TestMethod] public void FloatTest() => testRnd(() => GetRandom.Float(), GetRandom.Float);
		[TestMethod] public void Int64Test() => testRnd(() => GetRandom.Int64(), GetRandom.Int64);
		[TestMethod] public void Int32Test() => testRnd(() => GetRandom.Int32(), GetRandom.Int32);
        [TestMethod] public void Int16Test() => testRnd(() => GetRandom.Int16(), GetRandom.Int16);
        [TestMethod] public void Int8Test() => testRnd(() => GetRandom.Int8(), GetRandom.Int8);
		[TestMethod] public void StringTest() => Assert.AreNotEqual(GetRandom.String(), GetRandom.String());
		[TestMethod] public void UInt64Test() => testRnd(() => GetRandom.UInt64(), GetRandom.UInt64);
		[TestMethod] public void UInt32Test() => testRnd(() => GetRandom.UInt32(), GetRandom.UInt32);
		[TestMethod] public void UInt16Test() => testRnd(() => GetRandom.UInt16(), GetRandom.UInt16);
		[TestMethod] public void UInt8Test() => testRnd(() => GetRandom.UInt8(), GetRandom.UInt8);
		[TestMethod] public void DecimalTest() => testRnd(() => GetRandom.Decimal(), GetRandom.Decimal);

		private static void testRnd<T>(Func<T> f1, Func<T, T, T>? f2 = null) where T : IComparable<T> {
            testRnd(f1, out T min, out T max);
            if(f2 is null) return;
            testRnd(() => f2(min, max), out T x, out T y);
            if(min.CompareTo(max) < 0) isBetween(x, min, max);
            else isBetween(x, max, min);
        }
		private static void testRnd<T>(Func<T> f) where T : IComparable<T> {
			T x = f();
			T y = f();
			while(x.CompareTo(y) == 0) y = f();
			Assert.AreNotEqual(x, y);
		}
		private static void testRnd<T>(Func<T> f, out T x, out T y) where T : IComparable<T> {
            x = f();
            y = f();
            while (x.CompareTo(y) == 0) y = f();
            Assert.AreNotEqual(x, y);
        }
        private static void isBetween<T>(T x, T min, T max) where T : IComparable<T> {
            Assert.IsTrue(x.CompareTo(max) <= 0);
            Assert.IsTrue(x.CompareTo(min) >= 0);
        }
    }
}
