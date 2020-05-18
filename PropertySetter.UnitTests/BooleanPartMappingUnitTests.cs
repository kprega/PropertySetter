using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tekla.Structures.Model;

namespace PropertySetter.UnitTests
{
    /// <summary>
    /// Testing class for <see cref="BooleanPartMapping"/>
    /// </summary>
    [TestClass]
    public class BooleanPartMappingUnitTests
    {
        static IMapping mapping;
        static ModelObject modelObject;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            mapping = new BooleanPartMapping();
            modelObject = TestObjectsHandler.GetBooleanPart();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            modelObject.Delete();
        }

        #endregion

        [TestMethod]
        public void SetAssemblyStartNumber_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["ASSEMBLY_START_NUMBER"].Invoke("7", modelObject));
        }

        [TestMethod]
        public void SetAssemblyPrefix_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["ASSEMBLY_PREFIX"].Invoke("ABC", modelObject));
        }

        [TestMethod]
        public void SetCastUnitType_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["CAST_UNIT_TYPE"].Invoke("1", modelObject));
        }

        [TestMethod]
        public void SetClass_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["CLASS"].Invoke("12", modelObject));
        }

        [TestMethod]
        public void SetFinish_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["FINISH"].Invoke("Paint", modelObject));
        }

        [TestMethod]
        public void SetMaterial_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["MATERIAL"].Invoke("S235JR", modelObject));
        }

        [TestMethod]
        public void SetName_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["NAME"].Invoke("Sample name", modelObject));
        }

        [TestMethod]
        public void SetPartStartNumber_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["PART_START_NUMBER"].Invoke("6", modelObject));
        }

        [TestMethod]
        public void SetPartPrefix_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["PART_PREFIX"].Invoke("DEF", modelObject));
        }

        [TestMethod]
        public void SetPartCambering_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["PartCambering"].Invoke("12.3", modelObject));
        }

        [TestMethod]
        public void SetPartShortening_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["PartShortening"].Invoke("34.5", modelObject));
        }

        [TestMethod]
        public void SetPartWarping1_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["Warping1"].Invoke("5.6", modelObject));
        }

        [TestMethod]
        public void SetPartWarping2_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["Warping2"].Invoke("6.7", modelObject));
        }

        [TestMethod]
        public void SetPartPouringPhase_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["POUR_PHASE"].Invoke("3", modelObject));
        }

        [TestMethod]
        public void SetPartProfile_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["PROFILE"].Invoke("300*300", modelObject));
        }
    }
}
