namespace ROS.Regression.Test.StepDefinitions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using ROS.Test.Shared.Helpers;
    using TechTalk.SpecFlow;

    /// <summary>
    /// This class contains the step definition for Enter fuel information scenerios.
    /// </summary>
    [Binding]
    public class EnterFuelDetailsStepDefinition
    {
        #region Properties

        private GeneralHelpers generalHelpers;
        private readonly ScenarioContext scenarioContext;

        private readonly IWebDriver instance;

        #endregion Properties

        #region Constructor

        public EnterFuelDetailsStepDefinition(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.instance = this.scenarioContext["Web_Driver"] as IWebDriver;
        }

        #endregion Constructor

        #region Stepdefinitions

        [Given(@"I select the ""(.*)"" as (.*)")]
        public void GivenISelectTheAs(string inputfield, string input)
        {
            this.generalHelpers.SelectElementFromDropdown(inputfield, input, this.instance);
        }

        [Given(@"I enter the ""(.*)"" as (.*)")]
        public void GivenIEnterTheAs(string inputfield, string input)
        {
            this.generalHelpers.EnterInput(inputfield, input, this.instance);
        }

        [When(@"I click Submit")]
        public void WhenIClickSubmit()
        {
            this.generalHelpers.ClickElement("Continue button", this.instance);
        }

        [Then(@"I should be see the ""(.*)"" page")]
        public void ThenIShouldBeSeeThePage(string pagetitle)
        {
            Assert.AreEqual(
               pagetitle,
               this.generalHelpers.GetTextFromElement("Sustainability page heading", this.instance),
               "The page title '{0}' is incorrect",
               pagetitle);
        }

        #endregion Stepdefinitions
    }
}