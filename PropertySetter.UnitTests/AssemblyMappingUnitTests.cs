using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tekla.Structures.Model;

namespace PropertySetter.UnitTests
{
    /// <summary>
    /// Testing class for <see cref="AssemblyMapping"/>
    /// </summary>
    [TestClass]
    public class AssemblyMappingUnitTests
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
            mapping = new AssemblyMapping();
            modelObject = TestObjectsHandler.GetAssembly();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            modelObject.Delete();
        }

        #endregion

        [TestMethod]
        public void SetStartNumber_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["ASSEMBLY_START_NUMBER"].Invoke("7", modelObject));
        }

        [TestMethod]
        public void SetPrefix_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["ASSEMBLY_PREFIX"].Invoke("ABC", modelObject));
        }

        [TestMethod]
        public void SetName_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["NAME"].Invoke("Sample name", modelObject));
        }
    }
}
