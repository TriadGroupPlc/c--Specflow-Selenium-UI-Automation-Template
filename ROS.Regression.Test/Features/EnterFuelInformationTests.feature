Feature: EnterFuelInformationTests
	As a Supplier can I enter the fule information

@regressionTest @ignore
Scenario: Can I enter the fuel information details and click submit
	Given I have logged in as a supplier
	And navigate to Fuel Information page
	And I select the "Fuel type" as <fueltype>
	And I select the "Country of origin" as <origin>
	And I select the "Feed stock" as <feedstock>
	And I enter the "Volume" as <volume>
	And I enter the "Carbon intensity" as <carbon intensity>
	When I click Submit
	Then I should be see the "Sustainability information" page

		Examples: 
| fueltype	| origin | feedstock | volume |carbon intensity |
|Bio Petrol |Norway |Corn oil    |900     |30               |