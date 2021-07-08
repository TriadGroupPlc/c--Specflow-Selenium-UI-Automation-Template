Feature: Verify the footer pages for any violations
	

@AccessibilityTest
Scenario: Verify the footer pages for any accessibility violations
    Given I am on the ROS login page
	And I click the <footerLink> link
    When I validate the page for accessibility violations
    Then I should see 0 violations

	Examples: 
	  |footerLink              | 
	  | Cookies                |	 
	  | Privacy policy         | 
	  | Accessibility statement|

@AccessibilityTest
Scenario: Verify the components pages for any accessibility violations
    Given I am on the ROS login page
	And I navigate to components page	
    When I validate the page for accessibility violations
    Then I should see 0 violations
	