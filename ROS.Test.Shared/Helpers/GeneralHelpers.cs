namespace ROS.Test.Shared.Helpers
{
    using System.IO;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;
    using OpenQA.Selenium;
    using ROS.Test.Shared.Utilities;

    /// <summary>
    /// This class contains the general helper methods commonly used in all projects.
    /// </summary>
    public class GeneralHelpers
    {
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
            Commonlib.WaitUntilElementExists(elementLocator, instance);

            instance.FindElement(By.CssSelector(elementLocator)).Click();
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
            this.EnterInput("Email address field", DataUtils.GetResourceValue("ValidUsername"), instance);
            this.EnterInput("Password field", DataUtils.GetResourceValue("ValidPassword"), instance);
            this.ClickElement("Sign in button", instance);
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
            instance.FindElement(By.LinkText("Submit a single application")).Click();
            this.ClickElement("Continue button", instance);
        }

        #endregion Methods
    }
}