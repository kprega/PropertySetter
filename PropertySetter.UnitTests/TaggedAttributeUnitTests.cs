using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tekla.Structures.Model;

namespace PropertySetter.UnitTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TaggedAttributeUnitTests
    {
        static ModelObject modelObject;


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get => TestContextInstance;
            set => TestContextInstance = value;
        }

        public TestContext TestContextInstance { get; set; }

        #region Additional test attributes

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            modelObject = TestObjectsHandler.GetBeam();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            modelObject.Delete();
        }

        #endregion

        [TestMethod]
        public void Parse_NoPercentSymbolsFound_ReturnsParameterString()
        {
            var testString = "test string";
            var parseResult = TaggedAttribute.Parse(testString, modelObject);
            Assert.AreEqual(parseResult, testString);
        }

        [TestMethod]
        public void Parse_PercentSymbolsFound_ReturnsParameterString()
        {
            var beam = modelObject as Beam;
            var testString = "%NAME% Test";
            var expectedResult = $"{beam.Name} Test";
            var parseResult = TaggedAttribute.Parse(testString, modelObject);
            Assert.AreEqual(parseResult, expectedResult);
        }

        [TestMethod]
        public void Parse_AttributeNotPresent_ReturnsEmptyString()
        {
            var testString = "%Warping1%";
            var expectedResult = string.Empty;
            var parseResult = TaggedAttribute.Parse(testString, modelObject);
            Assert.AreEqual(parseResult, expectedResult);
        }

        [TestMethod]
        public void Parse_NumericValueAttributePresent_ReturnsNumberAsString()
        {
            var testString = "%CLASS_ATTR%";
            var expectedResult = (modelObject as Beam).Class;
            var parseResult = TaggedAttribute.Parse(testString, modelObject);
            Assert.AreEqual(parseResult, expectedResult);
        }

        [TestMethod]
        public void Parse_PhaseAttributePresent_ReturnsNumberAsString()
        {
            var testString = "N[%PHASE%]-";
            Phase p;
            modelObject.GetPhase(out p);
            var expectedResult = $"N[{p.PhaseNumber.ToString()}]-";
            var parseResult = TaggedAttribute.Parse(testString, modelObject);
            Assert.AreEqual(expectedResult, parseResult);
        }
    }
}
