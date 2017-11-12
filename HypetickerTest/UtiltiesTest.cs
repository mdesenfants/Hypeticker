using Hypeticker.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HypetickerTest
{
    [TestClass]
    public class UtiltiesTest
    {
        [TestMethod]
        public void CompanyNameTest()
        {
            var result = Company.GetCompany("kindred of 叱 and 𠮟 (2016) (namakajiri.net)");
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }
    }
}
