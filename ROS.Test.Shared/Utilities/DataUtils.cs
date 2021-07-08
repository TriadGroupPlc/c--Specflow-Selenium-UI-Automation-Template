namespace ROS.Test.Shared.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Resources;
    using System.Text;

    public static class DataUtils
    {
        public static string GetResourceValue(string name)
        {
            ResourceManager rm = new ResourceManager("ROS.Test.Shared.TestData.TestResource", Assembly.GetExecutingAssembly());

            return rm.GetString(name);
        }
    }
}
