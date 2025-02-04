Feature: LoginTest
	As a supplie\Verifier\Admin user can I login to ROS system

@regressionTest

Scenario Outline: Exisitng admin login to the ROS system
	Given I have logged in as "<userType>" user using "Company One"
	Then the title of the page should be <page>

	Examples: 
	| userType      | page                                                                    |
	| Administrator | Administrator dashboard - GOV.UK - Renewable Transport Fuels Obligation |
	| supplier      | Supplier dashboard - GOV.UK - Renewable Transport Fuels Obligation      |
	| Verifier      | Verifier dashboard - GOV.UK - Renewable Transport Fuels Obligation      |


