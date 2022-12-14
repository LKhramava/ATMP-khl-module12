Feature: SpecFlowMailRuTests

Testing the mai.ru page

Background:
	Given I open mail.ru page


@smoke
Scenario: Login to mail.ru page simple
	#Given I open mail.ru page
	When I login with 'lizakhramova' and '070461040485' to mail.ru page
	Then My account 'lizakhramova' page is opened

@smoke
Scenario Outline: Login to mail.ru page
	#Given I open mail.ru page
	When I login with '<Login>' and '<Password>' to mail.ru page
	Then My account '<Login>' page is opened

Examples: 
| Login        | Password     |
| lizakhramova | 070461040485 |

@smoke
@DataSource:UserData.xlsx @DataSet:user_data
Scenario: Login to mail.ru page with data
	#Given I open mail.ru page
	When I login with '<Login>' and '<Password>' to mail.ru page
	Then My account '<Login>' page is opened


@smoke
Scenario Outline: Save letter in draft
	#Given I open mail.ru page
	When I login with '<Login>' and '<Password>' to mail.ru page
	And I save email with address '<LetterEmail>', subject '<LetterSubject>' and body '<LetterBody>' in draft
	Then I check the saved email in draft 

Examples: 
| Login        | Password     | LetterEmail          | LetterSubject | LetterBody                                               |
| lizakhramova | 070461040485 | lizakhramova@mail.ru | TestAT        | Hello! My name is Liza! How are you? See you later. Bye. |



