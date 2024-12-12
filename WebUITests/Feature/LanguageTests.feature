Feature: Language Switch
  As a user of the website
  I want to switch between different languages
  So that I can view the content in my preferred language

  Scenario: Verify Language Change Functionality
    Given the user navigates to the base URL
    When the user switches to the Lithuanian version
    Then the page URL should be updated to the Lithuanian version
    And the page content should reflect the Lithuanian language