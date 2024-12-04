

Design Patterns & Design Principles:

1. Singleton (WebDriverManager)
Pattern: Singleton

Implementation:

The Singleton pattern is used to manage instances of IWebDriver using ThreadLocal.
Each thread (test) has its own unique IWebDriver instance, avoiding conflicts during parallel execution.
The GetDriver method returns the current driver instance for the thread, and the QuitDriver method ensures proper cleanup of the driver.

Advantages:

Unique driver instance for each test.
Simplified management of the driver's lifecycle.

2. Builder (PageBuilder)
Pattern: Builder

Implementation:

The Builder pattern is used to create page (Page Object) instances, providing a centralized approach to initialization.
PageBuilder accepts an IWebDriver instance and creates page objects through methods like BuildHomePage.

Advantages:

Structured initialization of pages.
Easy addition of new pages without changes to existing tests.

3. Page Object (HomePage)
Pattern: Page Object

Implementation:

Each page is represented by a separate class that encapsulates the logic for interacting with its elements.
HomePage contains methods to perform actions on the page, such as Open, Search, and SwitchLanguageToLithuanian.

Advantages:

Test readability: Tests focus on actions rather than locator details.
Reusability of page logic: Methods can be called from multiple tests.


These patterns together ensure:

Test isolation during parallel execution.
Ease of maintenance through structured code.
Test readability, focusing on scenarios rather than implementation details.



WebUITests Task:
Comparison of NUnit and xUnit

Task Description
Tests for Web UI Automation were implemented using NUnit and xUnit. The following features were implemented:

Parallel test execution.
Setup/TearDown.
Data Provider.
Test Categories.
Test Execution Results


![image](https://github.com/user-attachments/assets/fa0b95b4-3715-482f-a5e8-0b96ca68e041)


![image](https://github.com/user-attachments/assets/4c295900-e71d-447d-b82a-7f342424f40b)


Framework	Passed	Failed	Skipped	Total	Duration
NUnit	3	0	0	3	10.2 seconds
xUnit	3	0	0	3	9.9 seconds
Conclusions

Overall Test Execution:
Both frameworks (NUnit and xUnit) executed all 3 tests successfully with no failures or skipped tests. The tests were reliable and stable in both environments.

Execution Time:
xUnit completed the tests slightly faster, with a total duration of 9.9 seconds. NUnit took 10.2 seconds, which is a 0.3-second difference. The time difference is minimal but still indicates a slight performance advantage for xUnit.

Parallel Execution:
Both frameworks support parallel test execution, and both performed well in this aspect. However, NUnit provides more out-of-the-box features for parallel test execution, making it easier to configure in some cases.

Ease of Use:
I found that NUnit was easier to work with for configuring parallelism, Setup/TearDown methods, and data provider functionality. While xUnit is also user-friendly, NUnit gave me a bit more flexibility and simplicity when setting up these features.

Final Thoughts:
Both frameworks performed well with minimal differences in execution time. xUnit was slightly faster, and I found it to be more convenient for my specific needs, especially when working with simpler test setups.
