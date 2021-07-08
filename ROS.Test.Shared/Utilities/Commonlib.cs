namespace ROS.Test.Shared.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using ROS.Test.Shared.Helpers;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// This class contains the methods commonly used across all project.
    /// </summary>
    public static class Commonlib
    {
        #region Methods

        /// <summary>
        /// This method will be used to wait for page load with the value provided from the constant utils.
        /// </summary>
        /// <param name="instance">The instance of the driver to interact.</param>
        public static void WaitForPageLoad(IWebDriver instance)
        {
            try
            {
                instance.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(ConstantUtils.WAITFORPAGELOAD);
            }
            catch (Exception e)
            {
                Assert.Fail("Page not Loadedmessage : {0} ", e.Message);
            }
        }

        /// <summary>
        /// This will search for the element until a timeout is reached.
        /// </summary>
        /// <param name="elementLocator">The element loctaor to identify the element.</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        public static void WaitUntilElementExists(string elementLocator, IWebDriver instance)
        {
            new WebDriverWait(instance, TimeSpan.FromSeconds(ConstantUtils.WAITFORELEMENT))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(elementLocator)));
        }

        /// <summary>
        /// This will wait for the element to be clickable until a timeout is reached.
        /// </summary>
        /// <param name="elementLocator">The element loctaor to identify the element.</param>
        /// <param name="instance">The instance of the driver to interact.</param>
        public static void WaitUntilElementIsClickable(string elementLocator, IWebDriver instance)
        {
            new WebDriverWait(instance, TimeSpan.FromSeconds(ConstantUtils.WAITFORELEMENT))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(elementLocator)));
        }

        #endregion Methods
    }
}
