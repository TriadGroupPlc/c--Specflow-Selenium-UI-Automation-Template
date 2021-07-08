namespace ROS.Accessibility.Test.StepDefinitions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using ROS.Test.Shared.Utilities;
    using Selenium.Axe;
    using TechTalk.SpecFlow;

    [Binding]
    public class AccessibilityStepDefinitions
    {
        #region Properties

        private AxeResult AxeResult { get; set; }

        private readonly ScenarioContext scenarioContext;
        private readonly IWebDriver instance;

        #endregion Properties

        #region Constructor

        public AccessibilityStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.instance = this.scenarioContext["Web_Driver"] as IWebDriver;
        }

        #region Stepdefinitions

        /// <summary>
        /// Run aXe analysis against the page for accessibility violations.
        /// </summary>
        [When(@"I validate the page for accessibility violations")]
        public void WhenIValidateThePageForAccessibilityViolations()
        {
            var builder = new AxeBuilder(this.instance);
            this.AxeResult = builder.Analyze();
        }

        /// <summary>
        /// Verify the number of accessibility violations in the page.
        /// </summary>
        /// <param name="expNumberOfViolations">exptected number of violations.</param>
        [Then(@"I should see (.*) violations")]
        public void ThenIShouldSeeViolations(int expNumberOfViolations)
        {
            int violationCount = this.AxeResult.Violations.Length + this.AxeResult.Incomplete.Length;

            //// The below code is used to ignore the ARIA type violations
            ////if (this.GetViolationCountExcludingAriaCurrent() == 0)
            ////{
            ////    //violationCount = 0;
            ////}

            Assert.IsTrue(violationCount == expNumberOfViolations, this.ExtractResult(this.AxeResult.Violations, this.AxeResult.Incomplete).ToString());
        }

        #endregion Stepdefinitions

        #region Private Methods

        /// <summary>
        /// Extracts the result items from the results object.
        /// </summary>
        /// <param name="violations">The violations from the Axe tool.</param>
        /// <returns>Details of all accessibility violations on the page.</returns>
        private StringBuilder ExtractResult(AxeResultItem[] violations, AxeResultItem[] incomplete)
        {
            StringBuilder allViolations = new StringBuilder();
            int srlNumber = 1;

            if (violations.Length > 0)
            {
                allViolations.Append("\n\n" + "Found " + violations.Length + " accessibility violations:");
                allViolations.Append("\n Test page url: " + this.AxeResult.Url + "\n");


                foreach (var item in violations)
                {
                    allViolations.Append("\n" + srlNumber + ") ");
                    allViolations.Append("Violation type: " + item.Help.ToString() + ": " + item.HelpUrl.ToString() + "\n");
                    allViolations.Append("      " + item.Description.ToString() + "\n");
                    allViolations.Append("      Violation impact: " + item.Impact.ToString() + "\n");

                    foreach (var tagItem in item.Tags)
                    {
                        allViolations.Append("      Tags: " + tagItem.ToString() + "\n");
                    }

                    int nodeNumber = 1;

                    foreach (var nodeItem in item.Nodes)
                    {
                        allViolations.Append("      " + nodeNumber + ") ");
                        allViolations.Append("      Impact: " + nodeItem.Impact.ToString() + "\n");
                        allViolations.Append("      Html: " + nodeItem.Html.ToString() + "\n");

                        foreach (var tarItem in nodeItem.Target)
                        {
                            allViolations.Append("      Target: " + tarItem.ToString() + "\n");
                        }

                        allViolations.Append("      Fix the following issues: \n");

                        foreach (var anyItem in nodeItem.Any)
                        {
                            var msg = anyItem.Message;
                            allViolations.Append("          " + msg + "\n");
                        }

                        allViolations.Append("\n");
                        nodeNumber++;
                    }

                    srlNumber++;
                }
            }

            if (incomplete.Length > 0)
            {
                allViolations.Append("\n\n" + "Found " + incomplete.Length + " accessibility incomplete:");
                allViolations.Append("\n Test page url: " + this.AxeResult.Url + "\n");


                foreach (var item in incomplete)
                {
                    allViolations.Append("\n" + srlNumber + ") ");
                    allViolations.Append("Violation type: " + item.Help.ToString() + ": " + item.HelpUrl.ToString() + "\n");
                    allViolations.Append("      " + item.Description.ToString() + "\n");
                    allViolations.Append("      Violation impact: " + item.Impact.ToString() + "\n");

                    foreach (var tagItem in item.Tags)
                    {
                        allViolations.Append("      Tags: " + tagItem.ToString() + "\n");
                    }

                    int nodeNumber = 1;

                    foreach (var nodeItem in item.Nodes)
                    {
                        allViolations.Append("      " + nodeNumber + ") ");
                        allViolations.Append("      Impact: " + nodeItem.Impact.ToString() + "\n");
                        allViolations.Append("      Html: " + nodeItem.Html.ToString() + "\n");

                        foreach (var tarItem in nodeItem.Target)
                        {
                            allViolations.Append("      Target: " + tarItem.ToString() + "\n");
                        }

                        allViolations.Append("      Fix the following issues: \n");

                        foreach (var anyItem in nodeItem.Any)
                        {
                            var msg = anyItem.Message;
                            allViolations.Append("          " + msg + "\n");
                        }

                        allViolations.Append("\n");
                        nodeNumber++;
                    }

                    srlNumber++;
                }
            }

            return allViolations;
        }

        /// <summary>
        /// Counts the number of violations excluding Aria-Current.
        /// </summary>
        /// <returns>Violation Count.</returns>
        private int GetViolationCountExcludingAriaCurrent()
        {
            int violationCount = 0;

            foreach (var item in this.AxeResult.Violations)
            {
                foreach (var nodeItem in item.Nodes)
                {
                    foreach (var anyItem in nodeItem.All)
                    {
                        var errMsg = anyItem.Message;
                        if (!(errMsg.Contains("aria-current") ||
                            errMsg.Contains("aria-expanded") ||
                            errMsg.Contains("aria-describedby")))
                        {
                            violationCount++;
                        }
                    }
                    }
            }

            return violationCount;
        }

        #endregion Private Methods
    }

#endregion Stepdefinitions
}