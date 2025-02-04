namespace ROS.Regression.Test.StepDefinitions
{
    using NUnit.Framework;
    using NUnit.Framework.Legacy;
    using OpenQA.Selenium;
    using ROS.Test.Shared.Helpers;
    using TechTalk.SpecFlow;

    /// <summary>
    /// This class contains the step definitions for supplier page tests.
    /// </summary>
    [Binding]
    public class SupplierPageStepDefinitions
    {
        #region Properties

        private GeneralHelpers generalHelpers;
        private readonly ScenarioContext scenarioContext;

        private readonly IWebDriver instance;

        #endregion Properties

        #region Constructor

        public SupplierPageStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.instance = this.scenarioContext["Web_Driver"] as IWebDriver;
        }

        #endregion Constructor

        #region Stepdefinitions

        [Then(@"I should see the ""(.*)""")]
        public void ThenIShouldSeeThe(string pagetitle)
        {
            ClassicAssert.AreEqual(
                pagetitle,
                this.generalHelpers.GetTextFromElement("Supplier page heading", this.instance),
                "The page title '{0}' is incorrect",
                pagetitle);
        }

        #endregion Stepdefinitions
    }
}
