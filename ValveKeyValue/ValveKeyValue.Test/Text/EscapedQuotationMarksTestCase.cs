﻿using NUnit.Framework;

namespace ValveKeyValue.Test.Text
{
    class EscapedQuotationMarksTestCase
    {
        [Test]
        public void QuotedKeyReturnsQuotedValue()
        {
            Assert.That((string)data["name \"of\" key"], Is.EqualTo("value \"of\" key"));
        }

        KVObject data;

        [OneTimeSetUp]
        public void SetUp()
        {
            using (var stream = TestDataHelper.OpenResource("Text.escaped_quotation_marks.vdf"))
            {
                data = KVSerialiser.Deserialize(stream);
            }
        }
    }
}