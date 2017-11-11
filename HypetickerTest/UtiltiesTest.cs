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
            var result = ProductMatcher.GetCompany("kindred");
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }
    }
}
