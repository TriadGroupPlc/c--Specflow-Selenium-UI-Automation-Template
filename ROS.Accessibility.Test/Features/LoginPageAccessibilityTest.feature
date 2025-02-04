Feature: Login Pages Accessibility Test
	In order to avoid Accessibility violations in Login page
	As a QA
	I want to check the accessibility violations


	 @AccessibilityTest
  Scenario: Check Accessibility violations in Login page
	  Given I have logged in as "Administrator" user using "Company One"
	When I validate the page for accessibility violations
    Then I should see 0 violations 



	


