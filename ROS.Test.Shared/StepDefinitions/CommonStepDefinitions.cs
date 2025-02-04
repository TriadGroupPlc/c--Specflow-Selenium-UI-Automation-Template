namespace ROS.Test.Shared.StepDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using ROS.Test.Shared.Helpers;
    using ROS.Test.Shared.Utilities;
    using TechTalk.SpecFlow;

    /// <summary>
    /// This class contains commonly used step definitions  in all specflow UI test projects.
    /// </summary>
    [Binding]

    public class CommonStepDefinitions
    {
        /// <summary>
        /// This class contains commonly used step definitions in all specflow UI test projects.
        /// </summary>

        #region Properties

        private GeneralHelpers generalHelpers;

        private readonly ScenarioContext scenarioContext;
        private readonly IWebDriver instance;

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonStepDefinitions"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenario context information.</param>
        public CommonStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.instance = this.scenarioContext["Web_Driver"] as IWebDriver;
        }

        #endregion Constructor

        #region Stepdefinitions

        [When(@"I sign out of the account")]
        [Then(@"I sign out of my account")]
        [Then(@"I sign out of the user account")]
        [Then(@"I sign out of the account")]

        public void ThenISignOutOfMyAccount()
        {
            Assert.IsTrue(
                this.instance.PageSource.Contains("Successfully signed out"), "Failed to sign out of the account");
        }

        [Given(@"I click the (.*) link")]
        [When(@"I click the (.*) link")]
        public void GivenIClickTheLink(string link)
        {
            this.generalHelpers = new GeneralHelpers(this.scenarioContext);
            this.generalHelpers.ClickTheLink(link, this.instance);
        }

        [Given(@"I navigate to components page")]
        public void GivenINavigateToComponentsPage()
        {
            this.instance.Navigate().GoToUrl(ConstantUtils.BaseUrl + ConstantPageUtils.ComponentsPage);
        }

        [Then(@"the title of the page should be (.*)")]
        public void ThenTheTitleOfThePageShouldBe(string pagetitle)
        {
            Assert.AreEqual(
               pagetitle,
               this.instance.Title,
               "The page title '{0}' is incorrect",
               pagetitle);
        }


        #endregion Stepdefinitions
    }
}
