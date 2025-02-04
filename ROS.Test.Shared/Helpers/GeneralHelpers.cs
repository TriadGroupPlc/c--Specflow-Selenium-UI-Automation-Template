namespace ROS.Test.Shared.Helpers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.BiDi.Communication;
    using OpenQA.Selenium.Support.UI;
    using ROS.Test.Shared.Utilities;
    using TechTalk.SpecFlow;

    /// <summary>
    /// This class contains the general helper methods commonly used in all projects.
    /// </summary>
    public class GeneralHelpers
    {

        #region Properties

        private readonly ScenarioContext scenarioContext;
        private readonly IWebDriver instance;


        #endregion Properties

        #region Constructor

        public GeneralHelpers(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.instance = this.scenarioContext["Web_Driver"] as IWebDriver;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets css selectors (locators) from Locators.json.
        /// </summary>
        /// <param name="key">inoput parameter to get the element locator from locators.json.</param>
        /// <returns>return a srring value.</returns>
        public static string GetLocator(string key)
        {
            // Open the json file
            var stream = File.OpenText("PageObjects/Locators.json");

            // Read the file
            string json = stream.ReadToEnd();
            JObject obj = JObject.Parse(json);

            return (string)obj[key];
        }

        /// <summary>
        /// Identify the element using the elementCssSelector and Enter the input in the field using sendkeys.
        /// </summary>
        /// <param name="elementCssSelector">The locator stirng to identify the element.</param>
        /// <param name="input">The string value to input.</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        public void EnterInput(string elementCssSelector, string input, IWebDriver instance)
        {
            string elementLocator = GeneralHelpers.GetLocator(elementCssSelector);
            Commonlib.WaitUntilElementExists(elementLocator, instance);

            instance.FindElement(By.CssSelector(elementLocator)).Clear();
            instance.FindElement(By.CssSelector(elementLocator)).SendKeys(input);
        }

        /// <summary>
        /// Identify the element using the elementCssSelector and click the element.
        /// </summary>
        /// <param name="elementCssSelector">The locator stirng to identify the element.</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        public void ClickElement(string elementCssSelector, IWebDriver instance)
        {
            string elementLocator = GeneralHelpers.GetLocator(elementCssSelector);
            Commonlib.WaitUntilElementExists(elementLocator, instance);

            instance.FindElement(By.CssSelector(elementLocator)).Click();
            Commonlib.WaitForPageLoad(instance);
        }

        /// <summary>
        /// Identify the element using the elementCssSelector and click the element.
        /// </summary>
        /// <param name="elementLocator">The locator stirng to identify the element.</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        public void ClickTheLink(string elementLocator, IWebDriver instance)
        {
            Commonlib.WaitUntilLinkTextElementExists(elementLocator, instance);
            instance.FindElement(By.LinkText(elementLocator)).Click();
            Commonlib.WaitForPageLoad(instance);
        }

        public void ClickXpathElement(string elementLink, IWebDriver instance)
        {
            string elementLocator = GeneralHelpers.GetLocator(elementLink);
            if (!elementLocator.StartsWith("//"))
            {
                this.ClickElement(elementLink, instance);
                return;
            }

            Commonlib.WaitUntilXpathElementExists(elementLocator, instance);

            instance.FindElement(By.XPath(elementLocator)).Click();
            Commonlib.WaitForPageLoad(instance);
        }


        /// <summary>
        /// Identify the element using the elementCssSelector and return the text value from the element.
        /// </summary>
        /// <param name="elementCssSelector">The locator stirng to identify the element.</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        /// <returns>The text of the element.</returns>
        public string GetTextFromElement(string elementCssSelector, IWebDriver instance)
        {
            string elementLocator = GeneralHelpers.GetLocator(elementCssSelector);
            Commonlib.WaitUntilElementExists(elementLocator, instance);

            return instance.FindElement(By.CssSelector(elementLocator)).Text;
        }

        /// <summary>
        /// Steps to login using the supplier account.
        ///  </summary>
        /// <param name="userType">Admin/Supplier/Verifier.....</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        /// <param name="company">Company One/Company Two/Company 24/etc.</param>
        public void LoginAsaUserType(string userType, IWebDriver instance, string company)
        {
            this.NavigateToLoginPage(instance);
            Thread.Sleep(4000);
            Console.WriteLine("Navigated to url");
            this.SelectaUserTypeInAuthenticationPage(userType, instance, company, null);

            //// Below is the code to login as a Suplier when IAP is not enabled (UAT/TEST envts). Comment out the above one lines before enabling the below:
            // instance.FindElement(By.Id("identifierId")).SendKeys("laurageoffrey9@gmail.com");
            // instance.FindElement(By.Id("identifierNext")).Click();
            // Commonlib.WaitForPageLoad(instance);
            // instance.FindElement(By.Name("password")).SendKeys("Ros1234!");
            // instance.FindElement(By.Id("passwordNext")).Click();
            // Commonlib.WaitForPageLoad(instance);
            // Thread.Sleep(5000);
        }

        /// <summary>
        /// Click on the dropdown list and select an item.
        /// </summary>
        /// <param name="elementCssSelector">The locator stirng to identify the element.</param>
        /// <param name="item">The item to be selected.</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        public void SelectElementFromDropdown(string elementCssSelector, string item, IWebDriver instance)
        {
            string elementLocator = GeneralHelpers.GetLocator(elementCssSelector);
            string elementItemLocator = GeneralHelpers.GetLocator(elementCssSelector + " " + "options");
            Commonlib.WaitUntilElementExists(elementLocator, instance);

            var dropdown = instance.FindElement(By.CssSelector(elementLocator));
            dropdown.SendKeys(item);
            ////var items = dropdown.FindElements(By.XPath("(//*[contains(@id,'ac-apply-fuelType__option')]"));

            dropdown.FindElements(By.XPath(elementItemLocator)).Single(t => t.Text.Trim().Contains(item)).Click();
        }

        /// <summary>
        /// Steps to login using the supplier account.
        ///  </summary>
        /// <param name="instance">The instance of the driver to interact.</param>
        public void LoginAsSupplier(IWebDriver instance)
        {
            this.NavigateToLoginPage(instance);
            //this.EnterInput("Email address field", DataUtils.GetResourceValue("company"), instance);
            //this.EnterInput("Password field", DataUtils.GetResourceValue("userType"), instance);

            this.SelectaUserTypeInAuthenticationPage(DataUtils.GetResourceValue("userType"), instance, DataUtils.GetResourceValue("OrgName"), null);
        }

        /// <summary>
        /// Steps to navigate to the Login page.
        /// </summary>
        /// <param name="instance">The instance of the driver to interact.</param>
        public void NavigateToLoginPage(IWebDriver instance)
        {
            instance.Navigate().GoToUrl(ConstantUtils.BaseUrl + ConstantPageUtils.LoginPage);

            Commonlib.WaitForPageLoad(instance);
        }

        /// <summary>
        /// Steps to navigate to the Fuel information page.
        /// </summary>
        /// <param name="instance">The instance of the driver to interact.</param>
        public void NavigateToFuelInformationPage(IWebDriver instance)
        {
            instance.FindElement(By.LinkText("Volumes")).Click();
            instance.FindElement(By.LinkText("Report RTFO dutiable volume")).Click();
   
        }

        /// <summary>
        /// Steps to select a user type from the Authentication page. Do pass the userType variable same as the one in Locators.json file.
        ///  </summary>
        /// <param name="userType">Admin/Supplier/Verifier.....</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        /// <param name="company">Parse the company name of you want other than COMPANY ONE.</param>
        /// <param name="userID">1 or 2.</param>
        public void SelectaUserTypeInAuthenticationPage(string userType, IWebDriver instance, string company, string userID)
        {
            instance.Navigate().GoToUrl(ConstantUtils.BaseUrl + "/account/test");
            Commonlib.WaitForPageLoad(instance);

            if (company == null || company == "null")
            {
                Console.WriteLine(this.scenarioContext["OrganisationFullName"]);
                this.SelectElementFromDropdown("Organisation", this.scenarioContext["OrganisationFullName"].ToString(), instance);
            }
            else
            {
                this.SelectElementFromDropdown("Organisation", company, instance);
                Console.WriteLine("selected org");
            }

            if (userID == null || userID == "null")
            {
                // Keep blank to use the default user 2 (supplier@example.com), or change to user 1 (serviceaccount@triad.co.uk) depending on what you're doing.
            }
            else
            {
                this.EnterInput("User", userID, instance);
            }

            this.ClickXpathElement(userType, instance);
            Console.WriteLine("before clicking sign in button");
            this.ClickElement("Sign in button", instance);
            Console.WriteLine("clicked sign in button");
            Commonlib.WaitForPageLoad(instance);
        }

        #endregion Methods
    }
}