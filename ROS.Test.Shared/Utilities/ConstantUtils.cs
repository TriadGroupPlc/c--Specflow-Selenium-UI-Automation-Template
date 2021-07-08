namespace ROS.Test.Shared.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConstantUtils
    {
        /// <summary>
        /// Base url of the site to be passed from jenkins job.
        /// </summary>
        ////public static string BaseUrl = Environment.GetEnvironmentVariable("BASEURL");
        public const string BaseUrl = "https://ros-integration.triad.co.uk";
        public const string GosBaseUrl = "https://gos-staging.triad.co.uk";

        // this will set the wait time that can used across all the tests
        public const int WAITFORPAGELOAD = 60;
        public const int WAITFORELEMENT = 40;
        public const int IMPLICITWAIT = 30;

        ////public static string BrowserType = Environment.GetEnvironmentVariable("BROWSERTYPE");
        public static readonly string BrowserType = "Chrome";
    }
}
