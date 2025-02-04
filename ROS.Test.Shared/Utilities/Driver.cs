namespace ROS.Test.Shared.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.Extensions;

    public class Driver
    {
        #region Properties

       public IWebDriver Instance { get; set; }

       public static string BaseDirPath = System.AppDomain.CurrentDomain.BaseDirectory;
       public static string FilePath = BaseDirPath.Replace("netcoreapp3.1", "DownloadedCsv");

       public static string BrowserType
        {
            get { return ConstantUtils.BrowserType; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialise "Chrome driver".
        /// Set implicit wait to 10 seconds.
        /// Maximise the browser window.
        /// </summary>
        /// <returns>The webdriver.</returns>
       public IWebDriver Initialize()
        {
            {
                var browserType = BrowserType.ToLower();
                switch (browserType)
                {
                    case "firefox":
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        FirefoxProfile firefoxprofile = new FirefoxProfile();
                        firefoxprofile.SetPreference("browser.download.dir", FilePath);
                        firefoxprofile.SetPreference("browser.download.folderList", 2);
                        firefoxprofile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "txt csv text/csv");

                        FirefoxOptions firefoxoptions = new FirefoxOptions() { Profile = firefoxprofile };
                        firefoxoptions.AddArgument("--headless");
                        firefoxoptions.AddArgument("--disable-dev-shm-usage");
                        firefoxoptions.AddArgument("--no-sandbox");

                        this.Instance = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), firefoxoptions, TimeSpan.FromMinutes(3));
                        break;
                    case "internetexplorer":
                    case "ie":
                        InternetExplorerOptions ieoptions = new InternetExplorerOptions
                        {
                            EnableNativeEvents = false,
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            EnsureCleanSession = true,
                            EnablePersistentHover = false,
                        };
                       // ieoptions.AddAdditionalCapability("disable-popup-blocking", true);
                        ieoptions.PageLoadStrategy = PageLoadStrategy.Eager;
                        ieoptions.IgnoreZoomLevel = true;
                        this.Instance = new InternetExplorerDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ieoptions);
                        break;
                    default:
                        ChromeOptions chromeoptions = new ChromeOptions();
                       //// chromeoptions.AddArgument("headless");
                        chromeoptions.AddArgument("--disable-dev-shm-usage");
                        ////chromeoptions.AddArgument("--no-sandbox");
                        chromeoptions.AddUserProfilePreference("download.default_directory", FilePath);
                        ////chromeoptions.AddAdditionalCapability("browserVersion", "90");
                        ////chromeoptions.AddAdditionalCapability("platformName", "Windows 10");
                        ////chromeoptions.UseSpecCompliantProtocol = false;
                        ////chromeoptions.SetLoggingPreference(LogType.Browser, LogLevel.All);

                       ////var remoteUrl = "http://localhost:4444/wd/hub";
                       //// = new RemoteWebDriver(new Uri(remoteUrl), chromeoptions);
                      ////Instance.Navigate().GoToUrl("https://gos-integration.triad.co.uk");

                        this.Instance = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeoptions, TimeSpan.FromMinutes(3));
                        this.Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                        this.Instance.Manage().Window.Maximize();
                        this.Instance.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
                        Screenshot ss = this.Instance.TakeScreenshot();

                        break;
                }
            }

            return this.Instance;
            ////Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ConstantUtils.WAITFORELEMENT);
            ////Instance.Manage().Window.Maximize();
            ////Instance.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(ConstantUtils.WAITFORPAGELOAD);
        }

         /// <summary>
        /// Close the opened browser window'.
        /// </summary>.
       public void Close()
        {
            this.Instance.Dispose();
        }

        #endregion Methods
    }
}