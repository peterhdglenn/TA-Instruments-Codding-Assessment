using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System;
using PeterGlenn.CodingAssessment.Application;
using PeterGlenn.CodingAssessment.Repositories;

namespace PeterGlenn.CodingAssessment.UnitTests
{
    public class WordsApplicationTests
    {
        private WordsApplication _target;
        private Mock<IWordsRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IWordsRepository>();
        }

        [Test]
        public void GetMatchingWords_Returns_A_List_Of_One_When_There_Is_Only_One_Match()
        {
            //Assign
            var inputWord = "INPUT";
            var mockResults = new List<string>(new[] {"THE", "PIN", "HAPPY" });
            _mockRepository.Setup(x => x.GetWordsList()).Returns(mockResults);

            //Act
            _target = new WordsApplication(_mockRepository.Object);
            var actual = _target.GetMatchingWords(inputWord);


            //Assert
            var expected = new List<string>(new[] { "PIN" });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach(x =>
            {
                Assert.IsTrue(actual.Contains(x));
            });
            _mockRepository.Verify(x => x.GetWordsList(), Times.Once());
        }

        [Test]
        public void GetMatchingWords_Returns_A_List_Of_Two_When_There_Are_Two_Matches()
        {
            //Assign
            var inputWord = "INPUT";
            var mockResults = new List<string>(new[] { "THE", "PIN", "HAPPY", "INPUT" });
            _mockRepository.Setup(x => x.GetWordsList()).Returns(mockResults);

            //Act
            _target = new WordsApplication(_mockRepository.Object);
            var actual = _target.GetMatchingWords(inputWord);


            //Assert
            var expected = new List<string>(new[] { "PIN", "INPUT" });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach(x =>
            {
                Assert.IsTrue(actual.Contains(x));
            });
            _mockRepository.Verify(x => x.GetWordsList(), Times.Once());
        }

        [Test]
        public void GetMatchingWords_Returns_An_Empty_List_When_There_Are_No_Matches()
        {
            //Assign
            var inputWord = "INPUT";
            var mockResults = new List<string>(new[] { "THE", "HAPPY", "ARE" });
            _mockRepository.Setup(x => x.GetWordsList()).Returns(mockResults);

            //Act
            _target = new WordsApplication(_mockRepository.Object);
            var actual = _target.GetMatchingWords(inputWord);


            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count);
            _mockRepository.Verify(x => x.GetWordsList(), Times.Once());
        }

        [Test]
        public void GetMatchingWords_Ignores_Non_Alpha_Character_In_Input()
        {
            //Assign
            var inputWord = "I;NP1UT&";
            var mockResults = new List<string>(new[] { "THE", "PIN", "HAPPY" });
            _mockRepository.Setup(x => x.GetWordsList()).Returns(mockResults);

            //Act
            _target = new WordsApplication(_mockRepository.Object);
            var actual = _target.GetMatchingWords(inputWord);


            //Assert
            var expected = new List<string>(new[] { "PIN" });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach(x =>
            {
                Assert.IsTrue(actual.Contains(x));
            });
            _mockRepository.Verify(x => x.GetWordsList(), Times.Once());
        }

        [Test]
        public void GetMatchingWords_Ignores_Non_Alpha_Character_In_WordList()
        {
            //Assign
            var inputWord = "INPUT";
            var mockResults = new List<string>(new[] { "TH;E", "P$*!;IN", "HA*PPY" });
            _mockRepository.Setup(x => x.GetWordsList()).Returns(mockResults);

            //Act
            _target = new WordsApplication(_mockRepository.Object);
            var actual = _target.GetMatchingWords(inputWord);


            //Assert
            var expected = new List<string>(new[] { "P$*!;IN" });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach(x =>
            {
                Assert.IsTrue(actual.Contains(x));
            });
            _mockRepository.Verify(x => x.GetWordsList(), Times.Once());
        }

        [Test]
        public void GetMatchingWords_Ignores_Case_In_Input()
        {
            //Assign
            var inputWord = "iNpUt";
            var mockResults = new List<string>(new[] { "THE", "PIN", "HAPPY" });
            _mockRepository.Setup(x => x.GetWordsList()).Returns(mockResults);

            //Act
            _target = new WordsApplication(_mockRepository.Object);
            var actual = _target.GetMatchingWords(inputWord);


            //Assert
            var expected = new List<string>(new[] { "PIN" });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach(x =>
            {
                Assert.IsTrue(actual.Contains(x));
            });
            _mockRepository.Verify(x => x.GetWordsList(), Times.Once());
        }

        [Test]
        public void GetMatchingWords_Case_In_WordList()
        {
            //Assign
            var inputWord = "INPUT";
            var mockResults = new List<string>(new[] { "tHe", "PiN", "hApPy" });
            _mockRepository.Setup(x => x.GetWordsList()).Returns(mockResults);

            //Act
            _target = new WordsApplication(_mockRepository.Object);
            var actual = _target.GetMatchingWords(inputWord);


            //Assert
            var expected = new List<string>(new[] { "PiN" });
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);
            expected.ForEach(x =>
            {
                Assert.IsTrue(actual.Contains(x));
            });
            _mockRepository.Verify(x => x.GetWordsList(), Times.Once());
        }


        [Test]
        public void GetMatchingWords_Will_Pass_Along_Any_Exception_Thrown_By_Repository_So_Controller_Can_Log_And_Repackage()
        {
            //Assign
            var thrownMessage = DateTime.Now.Ticks.ToString();
            _mockRepository.Setup(x => x.GetWordsList()).Throws(new ApplicationException(thrownMessage));

            //Act
            Exception actual = null;
            _target = new WordsApplication(_mockRepository.Object);
            try
            {
                var discard = _target.GetMatchingWords("test");
            }
            catch (Exception ex)
            {
                actual = ex;
            }

            //Assert
            var expectedMessage = thrownMessage;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedMessage, actual.Message);
            _mockRepository.Verify(x => x.GetWordsList(), Times.Once());
        }
    }
}