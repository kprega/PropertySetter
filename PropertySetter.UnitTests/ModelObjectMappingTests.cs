using System;
using Tekla.Structures.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PropertySetter.UnitTests
{
    /// <summary>
    /// Testing class for <see cref="ModelObjectMapping"/>
    /// </summary>
    [TestClass]
    public class ModelObjectMappingTests
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
            mapping = new ModelObjectMapping();
            modelObject = TestObjectsHandler.GetBeam();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            modelObject.Delete();
        }

        #endregion

        [TestMethod]
        public void SetPhase_ChangedValueIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["PHASE"].Invoke("10", modelObject));
        }
    }
}
