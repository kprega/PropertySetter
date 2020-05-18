using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tekla.Structures.ModelInternal;
using Tekla.Structures.Model;

namespace PropertySetter.UnitTests
{
    /// <summary>
    /// Testing class for <see cref="ReinforcementMapping"/>
    /// </summary>
    [TestClass]
    public class ReinforcementMappingUnitTests
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
            mapping = new ReinforcementMapping();
            modelObject = TestObjectsHandler.GetSingleRebar();
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            (modelObject as SingleRebar).Father.Delete();
        }

        #endregion

        [TestMethod]
        public void SetClass_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["CLASS"].Invoke("7", modelObject));
        }

        [TestMethod]
        public void SetGrade_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["GRADE"].Invoke("S500", modelObject));
        }

        [TestMethod]
        public void SetName_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["NAME"].Invoke("Test name", modelObject));
        }

        [TestMethod]
        public void SetPrefix_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["PREFIX"].Invoke("Px", modelObject));
        }

        [TestMethod]
        public void SetStartNumber_ValueChangedIsValid_ReturnsTrue()
        {
            Assert.IsTrue(mapping.Functions["START_NUMBER"].Invoke("0", modelObject));
        }
    }
}
