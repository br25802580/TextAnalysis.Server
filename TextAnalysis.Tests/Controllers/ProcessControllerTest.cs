using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAnalysis;
using TextAnalysis.Controllers;
using System.Web.Http.Results;
using TextAnalysis.Models;

namespace TextAnalysis.Tests.Controllers
{
    [TestClass]
    public class ProcessControllerTest
    {
        [TestMethod]
        public void SegmentWithShortcut()
        {
            // Arrange
            var inputText = "Hello Mr. Cohen";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void SegmentWithReservedWord()
        {
            // Arrange
            var inputText = "She have an M.A. degree";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void SegmentWithRegex()
        {
            // Arrange
            var inputText = "There is a meeting on 01.02.18";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void SegmentWithSomeLines()
        {
            // Arrange
            var inputText = "David went to the house. He told his mom that he is hungry. She told him to fix himself some dinner";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void SegmentWithAutoNumber()
        {
            // Arrange
            var inputText = "Several questions. 1. What's the time? 2. What's your name?";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void SegmentWithQuestionSign()
        {
            // Arrange
            var inputText = "How are you? I'm great";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void SegmentWithExclamationSign()
        {
            // Arrange
            var inputText = "I love you! I love you! I love you!";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void SegmentWithNewLines()
        {
            // Arrange
            var inputText = "Father\r\nMother.\r\nChildren";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void RemoveUnneccessarySpaces()
        {
            // Arrange
            var inputText = "Father\r\n  \r\nChildren";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void AddSpaceWhenNeccessary()
        {
            // Arrange
            var inputText = "Mr.Cohen";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result[0][3] == ' ');
        }

        [TestMethod]
        public void SomeExceptionalWordsInTheSameSentence()
        {
            // Arrange
            var inputText = "Mr. Cohen And Mr. Levi are friends";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result[0][3] == ' ');
        }

        [TestMethod]
        public void ConsecutiveCharacters()
        {
            // Arrange
            var inputText = "Hello Ziv... How are you???";

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public void HugeData()
        {
            // Arrange
            string inputText = string.Empty;

            for (int i = 0; i < 5000; i++)
                inputText += "Hello. ";

            // Act
             var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count == 5000);
        }

        [TestMethod]
        public void EmptyText()
        {
            // Arrange
            string inputText = string.Empty;

            // Act
             var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void TextWithSpaces()
        {
            // Arrange
            string inputText = string.Empty;

                inputText += "   ";

            // Act
             var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void UndefinedText()
        {
            // Arrange
            string inputText = string.Empty;

            inputText += null;

            // Act
            var result = GetResult(inputText);

            // Assert
            Assert.IsTrue(result.Count == 0);
        }

        private IList< string> GetResult(string inputText)
        {
            // Arrange
            ProcessController controller = new ProcessController();
            ProcessRequest request = new ProcessRequest(inputText);
           
            // Act
            var actionResult = controller.Post(request);
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
           
            // Assert
            Assert.IsNotNull(response);

            return response.Content;
        }
    }
}
