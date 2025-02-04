Feature: Verify The Footer Links
	As a user of the ROS website
	I should see the links in the footer are pointing to respective pages
	So I can easily navigate to different pages to get help


	@RegressionTest
  Scenario Outline: Verify the links in the footer
    Given I have logged in as "Administrator" user using "Company One"
	When I click the <footerLink> link
	Then the title of the page should be <page>

Examples: 
	  | footerLink              | page                                                                    |
	  | Help                    | Renewable Transport Fuel Obligation (RTFO) scheme - GOV.UK              |
	  | Cookies                 | Cookies - GOV.UK - Renewable Transport Fuels Obligation                 |
	  | Contact                 | Renewable Transport Fuel Obligation (RTFO) scheme - GOV.UK              |
	  | Privacy policy          | Privacy policy - GOV.UK - Renewable Transport Fuels Obligation          |
	  | Accessibility statement | Accessibility statement - GOV.UK - Renewable Transport Fuels Obligation |