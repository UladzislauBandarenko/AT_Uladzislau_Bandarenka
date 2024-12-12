Feature: Search Functionality
  As a user
  I want to search for content
  So that I can find information on the site

  Scenario: Verify About Page Navigation
    Given the user is on the homepage for navigation testing
    When the user clicks on the search button
    And enters the search term "study programs"
    Then the URL should contain "?s=study+programs"
    And search results should be displayed
