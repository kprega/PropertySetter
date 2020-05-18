using System;
using PropertySetter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tekla.Structures.Model;

namespace PropertySetter.UnitTests
{
    [TestClass]
    public class BeamMappingUnitTests
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
            mapping = new BeamMapping();
            modelObject = TestObjectsHandler.GetBeam();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            modelObject.Delete();
        }

        #endregion

        [TestMethod]
        public void SetEndPoint_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(
                mapping.Functions["END_X"].Invoke("1000,1", modelObject) &&
                mapping.Functions["END_Y"].Invoke("1500,2", modelObject) &&
                mapping.Functions["END_Z"].Invoke("2000,3", modelObject));
        }

        [TestMethod]
        public void SetStartPoint_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(
                mapping.Functions["START_X"].Invoke("2000,3", modelObject) &&
                mapping.Functions["START_Y"].Invoke("1000,1", modelObject) &&
                mapping.Functions["START_Z"].Invoke("1500,2", modelObject));
        }
    }
}