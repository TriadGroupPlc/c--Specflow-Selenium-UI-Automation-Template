Feature: LoginTest
	As a supplie\Verifier\Admin user can I login to ROS system

@regressionTest @ignore
Scenario: Exisitng supplier login to the ROS system
	Given I navigate to the ROS login page
	When I enter my username <username>
	And I enter my password <password>
	And I click Showpassword buton
	And I click Login buton
	Then I should see the "Supplier Dashboard"

	Examples: 
|    username		| password		    |
| ValidUsername    | ValidPassword    |