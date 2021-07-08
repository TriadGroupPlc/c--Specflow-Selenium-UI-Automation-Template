namespace ROS.Test.Shared.StepDefinitions
{
    using OpenQA.Selenium;
    using ROS.Test.Shared.Helpers;
    using ROS.Test.Shared.Utilities;
    using TechTalk.SpecFlow;

    /// <summary>
    /// This class contains commonly used step definitions for login steps in all specflow UI test projects.
    /// </summary>
    [Binding]
    public class LoginStepDefinition
    {
        #region Properties

        private readonly GeneralHelpers generalHelpers = new GeneralHelpers();

        private readonly ScenarioContext scenarioContext;

        private readonly IWebDriver instance;

        #endregion Properties

        #region Constructor

        public LoginStepDefinition(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.instance = this.scenarioContext["Web_Driver"] as IWebDriver;
        }

        #endregion Constructor

        #region Stepdefinitions

        [Given(@"I navigate to the ROS login page")]
        [Given(@"I am on the ROS login page")]
        public void GivenINavigateToTheROSLoginPage()
        {
            this.instance.Navigate().GoToUrl(ConstantUtils.BaseUrl);
            Commonlib.WaitForPageLoad(this.instance);
        }

        [Given(@"I navigate to the GOS login page")]
        public void GivenINavigateToTheGOSLoginPage()
        {
            this.instance.Navigate().GoToUrl(ConstantUtils.GosBaseUrl);
            Commonlib.WaitForPageLoad(this.instance);
        }

        [When(@"I enter my username (.*)")]
        public void WhenIEnterMyUsername(string username)
        {
            this.generalHelpers.EnterInput("Email address field", DataUtils.GetResourceValue(username), this.instance);
        }

        [When(@"I enter my password (.*)")]
        public void WhenIEnterMyPassword(string password)
        {
            this.generalHelpers.EnterInput("Password field", DataUtils.GetResourceValue(password), this.instance);
        }

        [When(@"I click Login buton")]
        public void WhenIClickLoginButon()
        {
            this.generalHelpers.ClickElement("Sign in button", this.instance);
        }

        [When(@"I click Showpassword buton")]
        public void WhenIClickShowpasswordButon()
        {
            this.generalHelpers.ClickElement("Show password button", this.instance);
        }

        [Given(@"I have logged in as a supplier")]
        public void GivenIHaveLoggedInAsASupplier()
        {
            this.generalHelpers.LoginAsSupplier(this.instance);
        }

        [Given(@"navigate to Fuel Information page")]
        public void GivenNavigateToFuelInformationPage()
        {
            this.generalHelpers.NavigateToFuelInformationPage(this.instance);
        }

        #endregion Stepdefinitions
    }
}