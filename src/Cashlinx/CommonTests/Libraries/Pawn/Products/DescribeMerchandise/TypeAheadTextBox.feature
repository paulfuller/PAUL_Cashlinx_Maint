Feature: Type ahead text box
	In order to provide a better user experience
	As a user
	I want to be able to enter data quickly into a text field
	
Scenario Template: Typed three letters
	Given I have entered <first_two_chars>
	When I press <third_char>
	Then the typeahead options should include <typeahead_option>
Examples:
	| first_two_chars | third_char | typeahead_option |
	| ya              | m          | Yamaha           |

Scenario Template: Typed two letters
	Given I have entered <first_char>
	When I press <second_char>
	Then the typeahead options should be empty
Examples:
	| first_two_chars | third_char |
	| y               | a          |

Scenario Template: More than one answer
	Given I have entered <three_chars>
	Then there should be <n> typeahead options
Examples:
	| three_chars | n |
	| yam         | 1 |
	| glp         | 0 |

Scenario: Functionality capture
	Given a find box
	And a refactored find box
	When I enter every permutation of 3 characters
	Then the typeahead options should be the same
