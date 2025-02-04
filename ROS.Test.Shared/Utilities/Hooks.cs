namespace ROS.Test.Shared.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;
    using TechTalk.SpecFlow;

    [Binding]
     /// <summary>
    /// This class contains hooks for intialising and closing the driver before and after scenario and also taking screenshots for failed tests.
    /// </summary>

    public class Hooks : Driver
    {
        #region Properties

        private static string scenarioName;
        private static string featureName;
        private readonly ScenarioContext scenarioContext;
        private IWebDriver driver;

        #endregion Properties

        #region Constructor
        public Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        #endregion Constructor

        /// <summary>
        /// This method will initialise the webdriver before each scenario.
        /// </summary>
        [BeforeScenario]
        public void BeforeScenario()
        {
            this.driver = this.Initialize();
            this.scenarioContext["Web_Driver"] = this.driver;
        }

        #region Methods

        /// <summary>
        /// This method is to take a screenshot and save the screenshot in 'TestResults\\Screenshots` directory and close the browser after each scenario.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            var driver1 = this.scenarioContext["Web_Driver"] as IWebDriver;
            try
            {
                if (this.scenarioContext.TestError != null)
                {
                    Screenshot ss = driver1.TakeScreenshot();
                    Console.WriteLine("After screenshot" + ScenarioExecutionStatus.TestError.ToString());
                    string title = this.scenarioContext.ScenarioInfo.Title;
                    string runname = title + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");

                    ////the below code is to store the screenshot fies in Windows machine
                    string screenshotDirectory = Environment.CurrentDirectory.Replace("bin\\Debug\\netcoreapp3.1", "TestResults");

                    ////the below code is to store the screenshot fies in linux machine
                    ////string screenshotDirectory = Environment.CurrentDirectory.Replace("bin/Debug/netcoreapp3.1", "TestResults");
                    Directory.CreateDirectory(screenshotDirectory);

                    var screenshotfilename = screenshotDirectory + "\\" + runname + ".png";

                    ss.SaveAsFile(screenshotfilename);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (driver1 != null)
                {
                    Thread.Sleep(4000);
                    driver1.Quit();
                    driver1.Dispose();
                    driver1 = null;
                }
            }
        }

        #endregion Methods
    }
}
