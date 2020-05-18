using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tekla.Structures.Model;

namespace PropertySetter.UnitTests
{
    /// <summary>
    /// Testing class for <see cref="BaseRebarGroupMapping"/>
    /// </summary>
    [TestClass]
    public class BaseRebarGroupMappingUnitTests
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
            mapping = new BaseRebarGroupMapping();
            modelObject = TestObjectsHandler.GetRebarGroup();
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

        [TestMethod]
        public void SetSize_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["SIZE"].Invoke("10", modelObject));
        }

        [TestMethod]
        public void SetExactSpacings_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["CC_EXACT"].Invoke("10", modelObject));
        }

        [TestMethod]
        public void SetTargetSpacings_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["CC_TARGET"].Invoke("10", modelObject));
        }

        [TestMethod]
        public void SetBarsNumber_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["NUMBER"].Invoke("3", modelObject));
        }
    }
}
