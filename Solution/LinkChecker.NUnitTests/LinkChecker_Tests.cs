using Link.Checker.ClassLibrary;

namespace LinkChecker.NUnitTests
{
    public class LinkChecker_Tests
    {
        LinkCheckerApp _linkChecker;

        [SetUp]
        public void Setup()
        {
            LinkCheckerApp _linkChecker = new LinkCheckerApp();
        }

        [Test]
        public void SiteWithoutProtocol_Test()
        {
            LinkCheckerApp linkChecker = new LinkCheckerApp();
            bool actualResult = linkChecker.CheckLinks("google.com");
            bool expectedResult = true;
            if (actualResult == expectedResult) { Assert.Pass(); } else { Assert.Fail(); }
        }

        [Test]
        public void SiteWithProtocol_Test()
        {
            LinkCheckerApp linkChecker = new LinkCheckerApp();
            bool actualResult = linkChecker.CheckLinks("https://google.com");
            bool expectedResult = true;
            if (actualResult == expectedResult) { Assert.Pass(); } else { Assert.Fail(); }
        }

        [Test]
        public void CheckHeadStatusCode_Test()
        {
            LinkCheckerApp linkChecker = new LinkCheckerApp();
            bool actualResult = linkChecker.CheckHeadStatusCode("google.com");
            bool expectedResult = true;
            if (actualResult == expectedResult) { Assert.Pass(); } else { Assert.Fail(); }
        }

        [Test]
        public void UnitTestSetup_Test()
        {
            LinkCheckerApp linkChecker = new LinkCheckerApp();
            bool actualResult = linkChecker.CheckLinks("google.com");
            bool expectedResult = true;
            if (actualResult == expectedResult) { Assert.Pass(); } else { Assert.Fail(); }
        }
    }
}