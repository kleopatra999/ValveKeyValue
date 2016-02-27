﻿using System.Collections;
using NUnit.Framework;

namespace ValveKeyValue.Test
{
    class LegacyDepotDataSubsetTestCase
    {
        [Test]
        public void IsNotNull()
        {
            Assert.That(data, Is.Not.Null);
        }

        [Test]
        public void HasFourItems()
        {
            Assert.That(data.Items, Is.Not.Null);
            Assert.That(data.Items, Has.Count.EqualTo(4));
        }

        [TestCaseSource(nameof(ItemsTestCaseData))]
        public void HasItems(string key, string expectedValue)
        {
            var value = data[key];
            Assert.That(value.Name, Is.EqualTo(key));
            Assert.That(value.Items, Is.Empty);
            Assert.That((string)value.Value, Is.EqualTo(expectedValue));
        }

        [TestCaseSource(nameof(ItemsTestCaseData))]
        public void HasItemWithValueCast(string key, string expectedValue)
        {
            var value = data[key];
            Assert.That((string)value, Is.EqualTo(expectedValue));
        }

        [TestCaseSource(nameof(GarbageItemsTestCaseData))]
        public void DoesNotHaveGarbageItems(string key)
        {
            var value = data[key];
            Assert.That(value, Is.Null);
        }

        [TestCaseSource(nameof(GarbageItemsTestCaseData))]
        public void DoesNotHaveGarbageWithValueCast(string key)
        {
            var value = data[key];
            Assert.That((string)value, Is.Null);
        }

        static IEnumerable ItemsTestCaseData
        {
            get
            {
                yield return new TestCaseData("0", "A10D0BCD94CE6105D0E2256FE06B2B22");
                yield return new TestCaseData("1", "60EF870FB4B9A2EBB5E511BC8CEC8858");
                yield return new TestCaseData("3", "AA4CA0B6300E96774BC3C2B55C3388C9");
                yield return new TestCaseData("7", "636DC4B351732EDC6022021B89124CC9");
            }
        }

        static IEnumerable GarbageItemsTestCaseData
        {
            get
            {
                yield return "2";
                yield return "asdasd";
                yield return "123";
                yield return "hello there";
            }
        }

        KVObject data;

        [OneTimeSetUp]
        public void SetUp()
        {
            using (var stream = TestDataHelper.OpenResource("legacydepotdata_subset.vdf"))
            {
                data = KVSerialiser.Deserialize(stream);
            }
        }
    }
}
