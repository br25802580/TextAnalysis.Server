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
            ProcessController controller = new ProcessController();
            var inputText = "Hello Mr. Cohen";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void SegmentWithReservedWord()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "She have an M.A. degree";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void SegmentWithRegex()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "There is a meeting on 01.02.18";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void SegmentWithSomeLines()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "David went to the house. He told his mom that he is hungry. She told him to fix himself some dinner";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void SegmentWithAutoNumber()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "Several questions. 1. What's the time? 2. What's your name?";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void SegmentWithQuestionNumber()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "How are you? I'm great";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void SegmentWithNewLine()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "Father\r\nMother.\r\nChildren";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void RemoveUnneccessarySpaces()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "Father\r\n  \r\nChildren";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void AddSpaceWhenNeccessary()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "Mr.Cohen";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result[0][3] == ' ');
        }

        [TestMethod]
        public void SomeExceptionalWordsInTheSameSentence()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "Mr. Cohen And Mr. Levi are friends";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result[0][3] == ' ');
        }

        [TestMethod]
        public void PointsSign()
        {
            // Arrange
            ProcessController controller = new ProcessController();
            var inputText = "I like the following children: Yosef, Oren and Ziv";
            ProcessRequest request = new ProcessRequest(inputText);

            // Act
            var actionResult = controller.Post(request);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<IList<string>>;
            Assert.IsNotNull(response);

            var result = response.Content;
            Assert.IsTrue(result.Count == 2);
        }
    }
}
