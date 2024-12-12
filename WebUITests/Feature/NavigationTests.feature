Feature: Navigation and Page Interaction
  As a user of the website
  I want to navigate to different sections of the site
  So that I can view the desired content

  Scenario: Verify About Page Navigation
    Given the user is on the homepage
    When the user clicks on the About link
    Then the URL should be "https://en.ehu.lt/about/"
    And the page header should be "About"
