using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;


namespace SendGridEventsModel.Tests
{
    [TestClass]
    public class EventsTest
    {
        string _jsonData;
        JArray _dataList;

        [TestInitialize]
        public void Initialize()
        {

            _jsonData = File.ReadAllText("json.txt");
            _dataList = JArray.Parse(_jsonData);

        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }

        [TestMethod]
        public void FirstEventTest()
        {
            JToken tok = _dataList[0];
            Event e = Event.CreateFromToken(tok);

            Assert.AreEqual("john.doe@sendgrid.com",e.Email);
            Assert.AreEqual(1386636112,DateTimeToUnixTimestamp(e.EventDate));
            Assert.AreEqual("<142d9f3f351.7618.254f56@sendgrid.com>",e.smtpID);
            Assert.AreEqual("processed",e.Event1);
            Assert.AreEqual("category1,category2,category3",e.Category);
            Assert.AreEqual(123456,e.JobID);
            Assert.AreEqual("\"id\": \"001\",\"purchase\": \"PO1452297845\"",e.UniqueArguments);

        }

        [TestMethod]
        public void SecondEventTest()
        {
            JToken tok = _dataList[1];
            Event e = Event.CreateFromToken(tok);

            Assert.AreEqual("not an email address", e.Email);
            Assert.AreEqual(1386636115, DateTimeToUnixTimestamp(e.EventDate));
            Assert.AreEqual("<4FB29F5D.5080404@sendgrid.com>", e.smtpID);
            Assert.AreEqual("Invalid", e.Reason);
            Assert.AreEqual("dropped", e.Event1);
            Assert.AreEqual("category1,category2,category3", e.Category);
            Assert.AreEqual(123456, e.JobID);
            Assert.AreEqual("\"id\": \"001\",\"purchase\": \"PO1452297845\"", e.UniqueArguments);
        }

        [TestMethod]
        public void ThirdEventTest()
        {
            JToken tok = _dataList[2];
            Event e = Event.CreateFromToken(tok);

            Assert.AreEqual("john.doe@sendgrid.com", e.Email);
            Assert.AreEqual(1386636113, DateTimeToUnixTimestamp(e.EventDate));
            Assert.AreEqual("<142d9f3f351.7618.254f56@sendgrid.com>", e.smtpID);
            Assert.IsTrue(String.IsNullOrEmpty( e.Reason));
            Assert.AreEqual("delivered", e.Event1);
            Assert.AreEqual("category1,category2,category3", e.Category);
            Assert.AreEqual(123456, e.JobID);
            Assert.AreEqual("\"id\": \"001\",\"purchase\": \"PO1452297845\"", e.UniqueArguments);
        }

        [TestMethod]
        public void FourthEventTest()
        {
            JToken tok = _dataList[3];
            Event e = Event.CreateFromToken(tok);

            Assert.AreEqual("john.smith@sendgrid.com", e.Email);
            Assert.AreEqual(1386636127, DateTimeToUnixTimestamp(e.EventDate));
            Assert.IsTrue(String.IsNullOrEmpty( e.smtpID));
            Assert.IsTrue(String.IsNullOrEmpty(e.Reason));
            Assert.AreEqual("open", e.Event1);
            Assert.AreEqual("category1,category2,category3", e.Category);
            Assert.AreEqual(123456, e.JobID);
            Assert.AreEqual("174.127.33.234", e.IP);
            Assert.AreEqual("\"id\": \"001\",\"purchase\": \"PO1452297845\"", e.UniqueArguments);
            Assert.AreEqual(e.UserAgent,"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36");
        }

        [TestMethod]
        public void FifthEventTest()
        {
            JToken tok = _dataList[4];
            Event e = Event.CreateFromToken(tok);

            Assert.AreEqual("john.doe@sendgrid.com", e.Email);
            Assert.AreEqual(1386637216, DateTimeToUnixTimestamp(e.EventDate));
            Assert.IsTrue(String.IsNullOrEmpty(e.smtpID));
            Assert.IsTrue(String.IsNullOrEmpty(e.Reason));
            Assert.AreEqual("click", e.Event1);
            Assert.AreEqual("category1,category2,category3", e.Category);
            Assert.AreEqual(123456, e.JobID);
            Assert.AreEqual("174.56.33.234", e.IP);
            Assert.AreEqual("http://www.google.com/", e.URL);
            Assert.AreEqual("\"id\": \"001\",\"purchase\": \"PO1452297845\"", e.UniqueArguments);
            Assert.AreEqual(e.UserAgent, "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36");
        }

        [TestMethod]
        public void SixthEventTest()
        {
            JToken tok = _dataList[5];
            Event e = Event.CreateFromToken(tok);

            Assert.AreEqual("asdfasdflksjfe@sendgrid.com", e.Email);
            Assert.AreEqual("5.1.1",e.Status);
            Assert.AreEqual("550 5.1.1 The email account that you tried to reach does not exist. Please try double-checking the recipient's email address for typos or unnecessary spaces. Learn more at http: //support.google.com/mail/bin/answer.py?answer=6596 do3si8775385pbc.262 - gsmtp ", e.Reason);
            Assert.AreEqual(1386637483, DateTimeToUnixTimestamp(e.EventDate));
            Assert.AreEqual("<142da08cd6e.5e4a.310b89@localhost.localdomain>",e.smtpID);
            Assert.AreEqual("bounce", e.Event1);
            Assert.AreEqual("category1,category2,category3", e.Category);
            Assert.AreEqual(123456, e.JobID);
            Assert.AreEqual("bounce",e.BounceType);
            Assert.IsTrue(String.IsNullOrEmpty(e.IP));
            Assert.IsTrue(String.IsNullOrEmpty(e.URL));
            Assert.AreEqual("\"id\": \"001\",\"purchase\": \"PO1452297845\"", e.UniqueArguments);
            Assert.IsTrue(String.IsNullOrEmpty(e.UserAgent));
        }

        [TestMethod]
        public void SeventhEventTest()
        {
            JToken tok = _dataList[6];
            Event e = Event.CreateFromToken(tok);

            Assert.AreEqual("john.doe@gmail.com", e.Email);
            Assert.IsTrue(String.IsNullOrEmpty(e.Status));
            Assert.IsTrue(String.IsNullOrEmpty(e.Reason));
            Assert.AreEqual(1386638248, DateTimeToUnixTimestamp(e.EventDate));
            Assert.IsTrue(String.IsNullOrEmpty(e.smtpID));
            Assert.AreEqual("unsubscribe", e.Event1);
            Assert.AreEqual("category1,category2,category3", e.Category);
            Assert.AreEqual(123456, e.JobID);
            Assert.IsTrue(String.IsNullOrEmpty( e.BounceType));
            Assert.IsTrue(String.IsNullOrEmpty(e.IP));
            Assert.IsTrue(String.IsNullOrEmpty(e.URL));
            Assert.AreEqual("\"id\": \"001\",\"purchase\": \"PO1452297845\"", e.UniqueArguments);
            Assert.IsTrue(String.IsNullOrEmpty(e.UserAgent));
        }
        [TestMethod]
        public void NoCategoryAndUniqueArgumentTest()
        {
            JToken tok = _dataList[7];
            Event e = Event.CreateFromToken(tok);

            Assert.AreEqual("john.doe@gmail.com", e.Email);
            Assert.IsTrue(String.IsNullOrEmpty(e.Status));
            Assert.IsTrue(String.IsNullOrEmpty(e.Reason));
            Assert.AreEqual(1386638248, DateTimeToUnixTimestamp(e.EventDate));
            Assert.IsTrue(String.IsNullOrEmpty(e.smtpID));
            Assert.AreEqual("unsubscribe", e.Event1);
            Assert.IsNull(e.Category);
            Assert.AreEqual(123456, e.JobID);
            Assert.IsTrue(String.IsNullOrEmpty(e.BounceType));
            Assert.IsTrue(String.IsNullOrEmpty(e.IP));
            Assert.IsTrue(String.IsNullOrEmpty(e.URL));
            Assert.IsNull( e.UniqueArguments);
            Assert.IsTrue(String.IsNullOrEmpty(e.UserAgent));
        }
    }
}
