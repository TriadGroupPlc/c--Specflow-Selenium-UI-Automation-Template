Feature: Verify The Footer Links
	As a user of the ROS website
	I should see the links in the footer are pointing to respective pages
	So I can easily navigate to different pages to get help


	@RegressionTest
  Scenario Outline: Verify the links in the footer
    Given I am on the ROS login page
	When I click the <footerLink> link
	Then the title of the page should be <page>

	Examples: 
	  | footerLink              | page                                                                       |
	  | Help                    | Renewable Transport Fuel Obligation - GOV.UK                                  |
	  | Cookies                 | Cookies - GOV.UK - Renewable Transport Fuels Obligation                 |
	  | Contact                 | Renewable Transport Fuel Obligation - GOV.UK                                  |
	  | Privacy policy          | Privacy policy - GOV.UK - Renewable Transport Fuels Obligation          |
	  | Accessibility statement | Accessibility statement - GOV.UK - Renewable Transport Fuels Obligation      |