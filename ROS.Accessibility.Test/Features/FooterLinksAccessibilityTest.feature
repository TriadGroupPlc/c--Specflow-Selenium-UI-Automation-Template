Feature: Verify the footer pages for any violations
	

@AccessibilityTest
Scenario: Verify the footer pages for any accessibility violations
    Given I have logged in as "Administrator" user using "Company One"
	And I click the <footerLink> link
    When I validate the page for accessibility violations
    Then I should see 0 violations

	Examples: 
	  |footerLink              | 
	  | Cookies                |	 
	  | Privacy policy         | 
	  | Accessibility statement|


	