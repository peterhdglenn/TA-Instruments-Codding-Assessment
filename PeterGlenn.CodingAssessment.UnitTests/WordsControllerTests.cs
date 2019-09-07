using NUnit.Framework;
using Moq;
using PeterGlenn.CodingAssessment.Controllers;
using PeterGlenn.CodingAssessment.Application;
using System.Collections.Generic;
using System;

namespace PeterGlenn.CodingAssessment.UnitTests
{
    public class WordsControllerTests
    {
        private WordsController _target;
        private Mock<IWordsApplication> _mockApplication;

        [SetUp]
        public void Setup()
        {
            _mockApplication = new Mock<IWordsApplication>();
        }

        [Test]
        public void MatchingWords_When_A_Valid_Word_Is_Passed_In_The_Controller_Calls_The_Application_And_Returns_The_Results()
        {
            //Assign
            var mockResults = new List<string>(new[] { "THE", "HE" });
            _mockApplication.Setup(x => x.GetMatchingWords(It.IsAny<string>())).Returns(mockResults);

            //Act
            _target = new WordsController(_mockApplication.Object);
            var actual = _target.MatchingWords("word");


            //Assert
            var expected = new List<string>(new[] { "THE", "HE" });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach(x =>
            {
                Assert.IsTrue(actual.Contains(x));
            });
            _mockApplication.Verify(x => x.GetMatchingWords(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void MatchingWords_When_Null_Is_Passed_In_Returns_An_Empty_List_Without_Calling_The_Application()
        {
            //Act
            _target = new WordsController(_mockApplication.Object);
            var actual = _target.MatchingWords(null);

            //Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 0);
            _mockApplication.Verify(x => x.GetMatchingWords(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void MatchingWords_Will_RePackage_An_Exception_Thrown_By_The_Application()
        {
            //Assign
            var thrownMessage = DateTime.Now.Ticks;
            _mockApplication.Setup(x => x.GetMatchingWords(It.IsAny<string>())).Throws(new ApplicationException(thrownMessage.ToString()));

            //Act
            Exception actual = null;
            _target = new WordsController(_mockApplication.Object);
            try
            {
                var discard = _target.MatchingWords("test");
            }
            catch (Exception ex)
            {
                actual = ex;
            }

            //Assert
            var expectedMessage = "Controller - " + thrownMessage;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedMessage, actual.Message);
            _mockApplication.Verify(x => x.GetMatchingWords(It.IsAny<string>()), Times.Once());
        }
    }
}