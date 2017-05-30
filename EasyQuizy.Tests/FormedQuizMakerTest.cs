using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyQuizy.Models.FormedQuizLogic;

namespace EasyQuizy.Tests
{
    [TestClass]
    public class FormedQuizMakerTest
    {
        bool[] isChoosen = { true, false, false, true, false };
        [TestMethod]
        public void IsChoosenArrayWithoutDublicate_()
        {
            bool[] result = { true, false, true };

            FormedQuizMaker fqm = new FormedQuizMaker(null, isChoosen, 1, 1);
            Assert.AreEqual(result[1], fqm.GetIsChoosenArray()[1]);
        }
        [TestMethod]
        public void FindChoosenQuizes_()
        {
            int[] quizes = { 1, 2, 3 };
            FormedQuizMaker fqm = new FormedQuizMaker(quizes, isChoosen, 1, 1);
            int[] result = fqm.FindChoosenQuizes();
            Assert.AreEqual(result[1], 3);
        }
    }
}
