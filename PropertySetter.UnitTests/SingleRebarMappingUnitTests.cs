using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tekla.Structures.Model;

namespace PropertySetter.UnitTests
{
    /// <summary>
    /// Testing class for <see cref="SingleRebarMapping"/>
    /// </summary>
    [TestClass]
    public class SingleRebarMappingUnitTests
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
            mapping = new SingleRebarMapping();
            modelObject = TestObjectsHandler.GetSingleRebar();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            (modelObject as SingleRebar).Father.Delete();
        }

        #endregion

        [TestMethod]
        public void SetSize_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["SIZE"].Invoke("10", modelObject));
        }
    }
}
