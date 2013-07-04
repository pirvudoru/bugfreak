﻿using System.Collections.Generic;
using NUnit.Framework;

namespace AgileErrorReporting.Tests
{
    [TestFixture]
    public class FormErrorReportSerializerTests
    {
        private FormErrorReportSerializer _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new FormErrorReportSerializer();
        }

        [Test]
        public void Serialize_Always_FormatsStringProperties()
        {
            var errorReport = new ErrorReport
                                  {
                                      Message = "test message",
                                      Source = "source",
                                      StackTrace = "stack trace"
                                  };

            var result = _subject.Serialize(errorReport);

            Assert.AreEqual("message=test message&source=source&stackTrace=stack trace", result);
        }

        [Test]
        public void Serialize_WhenPropertyIsNull_SetsEmpty()
        {
            var errorReport = new ErrorReport
                                  {
                                      Message = null,
                                      Source = null,
                                      StackTrace = null
                                  };

            var result = _subject.Serialize(errorReport);

            Assert.AreEqual("message=&source=&stackTrace=", result);
        }

        [Test]
        public void Serialize_Always_AddsDataFromAdditionalData()
        {
            var errorReport = new ErrorReport
                                  {
                                      Message = "test message",
                                      Source = "source",
                                      StackTrace = "stack trace"
                                  };
            errorReport.AdditionalData.Add(new KeyValuePair<string, string>("OS", "4.2"));

            var result = _subject.Serialize(errorReport);

            Assert.AreEqual("message=test message&source=source&stackTrace=stack trace&OS=4.2", result);
        }
    }
}
