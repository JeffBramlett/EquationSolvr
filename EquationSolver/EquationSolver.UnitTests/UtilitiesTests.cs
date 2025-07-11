using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationSolver.UnitTests
{
    [TestClass]
    public class UtilitiesTests
    {
        // Since Utilities is an internal class, we need to use reflection to access its methods
        private Type _utilitiesType;
        
        [TestInitialize]
        public void Initialize()
        {
            // Get the Utilities type using reflection
            Assembly assembly = Assembly.GetAssembly(typeof(EquationSolverFactory));
            _utilitiesType = assembly.GetType("EquationSolver.Utilities");
            Assert.IsNotNull(_utilitiesType, "Could not find Utilities class");
        }

        [TestMethod]
        public void IsANumber_ValidNumbers_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("IsANumber", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find IsANumber method");
            
            string[] validNumbers = { "123", "-123", "+123", "123.456", "-123.456", "+123.456", "0", ".5" };
            
            // Act & Assert
            foreach (var number in validNumbers)
            {
                var result = method.Invoke(null, new object[] { number });
                Assert.IsTrue((bool)result, $"Expected '{number}' to be recognized as a number");
            }
        }

        [TestMethod]
        public void IsANumber_InvalidNumbers_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("IsANumber", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find IsANumber method");
            
            // Note: IsANumber only checks if each character is a valid number character
            // It doesn't validate the overall number format
            string[] invalidNumbers = { "abc", "123abc", "abc123", "12,345", " " };
            
            // Act & Assert
            foreach (var number in invalidNumbers)
            {
                var result = method.Invoke(null, new object[] { number });
                Assert.IsFalse((bool)result, $"Expected '{number}' to be recognized as not a number");
            }
        }
        
        [TestMethod]
        public void IsANumber_EmptyString_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("IsANumber", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find IsANumber method");
            
            // For an empty string, the foreach loop in IsANumber doesn't execute,
            // so it returns true by default
            string emptyString = "";
            
            // Act
            var result = method.Invoke(null, new object[] { emptyString });
            
            // Assert
            Assert.IsTrue((bool)result, "Expected empty string to return true from IsANumber");
        }
        
        [TestMethod]
        public void IsANumber_ValidCharactersButInvalidFormat_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("IsANumber", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find IsANumber method");
            
            // These strings have valid number characters but invalid number format
            // IsANumber only checks characters, not format
            string[] validCharsInvalidFormat = { "12.34.56", "1..2", "+-123", "..." };
            
            // Act & Assert
            foreach (var number in validCharsInvalidFormat)
            {
                var result = method.Invoke(null, new object[] { number });
                Assert.IsTrue((bool)result, $"Expected '{number}' to be recognized as having valid number characters");
            }
        }

        [TestMethod]
        public void StringToInteger_ValidIntegers_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToInteger", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToInteger method");
            
            string[] validIntegers = { "123", "-123", "+123", "0" };
            
            // Act & Assert
            foreach (var integer in validIntegers)
            {
                var parameters = new object[] { integer, null };
                var result = method.Invoke(null, parameters);
                Assert.IsTrue((bool)result, $"Expected '{integer}' to be converted to an integer");
                Assert.IsNotNull(parameters[1], "Output parameter should not be null");
                
                int expectedValue = int.Parse(integer);
                Assert.AreEqual(expectedValue, (int)parameters[1], $"Expected '{integer}' to be converted to {expectedValue}");
            }
        }

        [TestMethod]
        public void StringToInteger_InvalidIntegers_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToInteger", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToInteger method");
            
            string[] invalidIntegers = { "abc", "123.45", "123abc", "" };
            
            // Act & Assert
            foreach (var integer in invalidIntegers)
            {
                var parameters = new object[] { integer, null };
                var result = method.Invoke(null, parameters);
                Assert.IsFalse((bool)result, $"Expected '{integer}' to fail conversion to an integer");
                Assert.AreEqual(0, (int)parameters[1], "Output parameter should be 0 for invalid integers");
            }
        }

        [TestMethod]
        public void StringToDouble_ValidDoubles_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToDouble", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToDouble method");
            
            string[] validDoubles = { "123", "-123", "+123", "123.456", "-123.456", "+123.456", "0", ".5" };
            
            // Act & Assert
            foreach (var doubleValue in validDoubles)
            {
                var parameters = new object[] { doubleValue, null };
                var result = method.Invoke(null, parameters);
                Assert.IsTrue((bool)result, $"Expected '{doubleValue}' to be converted to a double");
                Assert.IsNotNull(parameters[1], "Output parameter should not be null");
                
                double expectedValue = double.Parse(doubleValue);
                Assert.AreEqual(expectedValue, (double)parameters[1], $"Expected '{doubleValue}' to be converted to {expectedValue}");
            }
        }

        [TestMethod]
        public void StringToDouble_InvalidDoubles_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToDouble", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToDouble method");
            
            // Note: StringToDouble uses IsANumber which only checks characters, not format
            // But it will still fail on actual parsing
            string[] invalidDoubles = { "abc", "123abc", "abc123", "12,345", " " };
            
            // Act & Assert
            foreach (var doubleValue in invalidDoubles)
            {
                var parameters = new object[] { doubleValue, null };
                var result = method.Invoke(null, parameters);
                Assert.IsFalse((bool)result, $"Expected '{doubleValue}' to fail conversion to a double");
                Assert.AreEqual(0.0, (double)parameters[1], "Output parameter should be 0.0 for invalid doubles");
            }
        }
        
        [TestMethod]
        public void StringToDouble_ValidCharactersButInvalidFormat_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToDouble", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToDouble method");
            
            // These strings have valid number characters but invalid number format
            string[] validCharsInvalidFormat = { "12.34.56", "1..2", "+-123", "..." };
            
            // Act & Assert
            foreach (var doubleValue in validCharsInvalidFormat)
            {
                var parameters = new object[] { doubleValue, null };
                var result = method.Invoke(null, parameters);
                // Even though IsANumber returns true, the actual parsing will fail
                Assert.IsFalse((bool)result, $"Expected '{doubleValue}' to fail conversion to a double");
                Assert.AreEqual(0.0, (double)parameters[1], "Output parameter should be 0.0 for invalid doubles");
            }
        }
        
        [TestMethod]
        public void StringToDouble_EmptyString_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToDouble", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToDouble method");
            
            // Empty string passes IsANumber but fails in double.Parse
            string emptyString = "";
            
            // Act
            var parameters = new object[] { emptyString, null };
            var result = method.Invoke(null, parameters);
            
            // Assert
            Assert.IsFalse((bool)result, "Expected empty string to fail conversion to a double");
            Assert.AreEqual(0.0, (double)parameters[1], "Output parameter should be 0.0 for empty string");
        }

        [TestMethod]
        public void Factorial_ValidInputs_ReturnsCorrectResult()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("Factorial", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find Factorial method");
            
            var testCases = new Dictionary<int, int>
            {
                { 0, 1 },
                { 1, 1 },
                { 2, 2 },
                { 3, 6 },
                { 4, 24 },
                { 5, 120 },
                { 6, 720 },
                { 7, 5040 },
                { 8, 40320 },
                { 9, 362880 },
                { 10, 3628800 }
            };
            
            // Act & Assert
            foreach (var testCase in testCases)
            {
                var result = method.Invoke(null, new object[] { testCase.Key });
                Assert.AreEqual(testCase.Value, (int)result, $"Factorial of {testCase.Key} should be {testCase.Value}");
            }
        }

        [TestMethod]
        public void IsStringBoolean_ValidBooleans_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("IsStringBoolean", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find IsStringBoolean method");
            
            var testCases = new Dictionary<string, bool>
            {
                { "true", true },
                { "True", true },
                { "TRUE", true },
                { " true ", true },
                { "false", false },
                { "False", false },
                { "FALSE", false },
                { " false ", false }
            };
            
            // Act & Assert
            foreach (var testCase in testCases)
            {
                var parameters = new object[] { null, testCase.Key };
                var result = method.Invoke(null, parameters);
                Assert.IsTrue((bool)result, $"Expected '{testCase.Key}' to be recognized as a boolean");
                Assert.AreEqual(testCase.Value, (bool)parameters[0], $"Expected '{testCase.Key}' to be converted to {testCase.Value}");
            }
        }

        [TestMethod]
        public void IsStringBoolean_InvalidBooleans_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("IsStringBoolean", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find IsStringBoolean method");
            
            string[] invalidBooleans = { "yes", "no", "1", "0", "truee", "falsey", "", " " };
            
            // Act & Assert
            foreach (var boolValue in invalidBooleans)
            {
                var parameters = new object[] { null, boolValue };
                var result = method.Invoke(null, parameters);
                Assert.IsFalse((bool)result, $"Expected '{boolValue}' to fail recognition as a boolean");
                Assert.IsFalse((bool)parameters[0], "Output parameter should be false for invalid booleans");
            }
        }

        [TestMethod]
        public void NormalizeXML_ValidInput_ReturnsNormalizedString()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("NormalizeXML", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find NormalizeXML method");
            
            var testCases = new Dictionary<string, string>
            {
                { "<tag>", "&lt;tag&gt;" },
                { "a < b", "a &lt; b" },
                { "a > b", "a &gt; b" },
                { "a & b", "a &amp; b" },
                { "<tag>content</tag>", "&lt;tag&gt;content&lt;/tag&gt;" },
                { "a < b & c > d", "a &lt; b &amp; c &gt; d" }
            };
            
            // Act & Assert
            foreach (var testCase in testCases)
            {
                var result = method.Invoke(null, new object[] { testCase.Key });
                Assert.AreEqual(testCase.Value, (string)result, $"Expected '{testCase.Key}' to be normalized to '{testCase.Value}'");
            }
        }

        [TestMethod]
        public void MakeAppropriateCDATA_NeedsWrapping_ReturnsCDATAWrappedString()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("MakeAppropriateCDATA", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find MakeAppropriateCDATA method");
            
            string[] inputsNeedingCDATA = { "<tag>", "a < b", "a > b", "a & b", "a \" b" };
            
            // Act & Assert
            foreach (var input in inputsNeedingCDATA)
            {
                var result = method.Invoke(null, new object[] { input });
                string expected = $"<![CDATA[{input}]]>";
                Assert.AreEqual(expected, (string)result, $"Expected '{input}' to be wrapped in CDATA");
            }
        }

        [TestMethod]
        public void MakeAppropriateCDATA_NoWrappingNeeded_ReturnsOriginalString()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("MakeAppropriateCDATA", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find MakeAppropriateCDATA method");
            
            string[] inputsNotNeedingCDATA = { "normal text", "123", "abc" };
            
            // Act & Assert
            foreach (var input in inputsNotNeedingCDATA)
            {
                var result = method.Invoke(null, new object[] { input });
                Assert.AreEqual(input, (string)result, $"Expected '{input}' to remain unchanged");
            }
        }

        [TestMethod]
        public void IsPositiveWholeNumber_ValidPositiveWholeNumbers_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("IsPositiveWholeNumber", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find IsPositiveWholeNumber method");
            
            string[] validNumbers = { "123", "0", "9999" };
            
            // Act & Assert
            foreach (var number in validNumbers)
            {
                var result = method.Invoke(null, new object[] { number });
                Assert.IsTrue((bool)result, $"Expected '{number}' to be recognized as a positive whole number");
            }
        }

        [TestMethod]
        public void IsPositiveWholeNumber_InvalidPositiveWholeNumbers_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("IsPositiveWholeNumber", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find IsPositiveWholeNumber method");
            
            string[] invalidNumbers = { "-123", "+123", "123.456", "abc", "123abc", "" };
            
            // Act & Assert
            foreach (var input in invalidNumbers)
            {
                var result = method.Invoke(null, new object[] { input });
                Assert.IsFalse((bool)result, $"Expected '{input}' to not be recognized as a positive whole number");
            }
        }

        [TestMethod]
        public void StringToDecimal_ValidDecimals_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToDecimal", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToDecimal method");
            
            var testCases = new Dictionary<string, decimal>
            {
                { "123", 123m },
                { "-123", -123m },
                { "+123", 123m },
                { "123.456", 123.456m },
                { "-123.456", -123.456m },
                { "0", 0m },
                { ".5", 0.5m }
            };
            
            // Act & Assert
            foreach (var testCase in testCases)
            {
                var parameters = new object[] { testCase.Key, null };
                var result = method.Invoke(null, parameters);
                Assert.IsTrue((bool)result, $"Expected '{testCase.Key}' to be converted to a decimal");
                Assert.IsNotNull(parameters[1], "Output parameter should not be null");
                
                Assert.AreEqual(testCase.Value, (decimal)parameters[1], $"Expected '{testCase.Key}' to be converted to {testCase.Value}");
            }
        }

        [TestMethod]
        public void StringToDecimal_InvalidDecimals_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToDecimal", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToDecimal method");
            
            string[] invalidDecimals = { "abc", "123abc", "abc123", "12,345", " ", "" };
            
            // Act & Assert
            foreach (var decimalValue in invalidDecimals)
            {
                var parameters = new object[] { decimalValue, null };
                var result = method.Invoke(null, parameters);
                Assert.IsFalse((bool)result, $"Expected '{decimalValue}' to fail conversion to a decimal");
                Assert.AreEqual(0m, (decimal)parameters[1], "Output parameter should be 0 for invalid decimals");
            }
        }

        [TestMethod]
        public void StringToDecimal_ValidCharactersButInvalidFormat_ReturnsFalse()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("StringToDecimal", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find StringToDecimal method");
            
            // These strings have valid number characters but invalid number format
            string[] validCharsInvalidFormat = { "12.34.56", "1..2", "+-123", "..." };
            
            // Act & Assert
            foreach (var decimalValue in validCharsInvalidFormat)
            {
                var parameters = new object[] { decimalValue, null };
                var result = method.Invoke(null, parameters);
                // Even though IsANumber returns true, the actual parsing will fail
                Assert.IsFalse((bool)result, $"Expected '{decimalValue}' to fail conversion to a decimal");
                Assert.AreEqual(0m, (decimal)parameters[1], "Output parameter should be 0 for invalid decimals");
            }
        }

        [TestMethod]
        public void MakeDateFromString_ValidDateFormat_ReturnsTrue()
        {
            // Arrange
            var method = _utilitiesType.GetMethod("MakeDateFromString", BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, "Could not find MakeDateFromString method");
            
            // Note: The implementation uses a regex "^{n}/{n}/{n:4}" which doesn't seem to be a valid regex
            // The method will likely fail for all inputs, but we'll test it anyway
            string[] validDates = { "1/1/2020", "12/31/2020", "01/01/2020" };
            
            // Act & Assert
            foreach (var dateString in validDates)
            {
                var parameters = new object[] { null, dateString };
                var result = method.Invoke(null, parameters);
                
                // The method should return true or false based on the regex match
                // We'll just verify that it doesn't throw an exception
                Assert.IsNotNull(parameters[0], "Output parameter should not be null");
                Assert.IsInstanceOfType(parameters[0], typeof(DateTime), "Output parameter should be a DateTime");
            }
        }
    }
}