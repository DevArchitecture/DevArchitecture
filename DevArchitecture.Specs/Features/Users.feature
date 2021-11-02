Feature: Users
	Users Api Feature



@GetUsers
Scenario: Get all users
	Given add "getall" to base url
	When call the api for all users
	Then the all users response must contain
		| FullName     | Email               | Gender |
		| System Admin | admin@adminmail.com | 0      |

@GetUserByID
Scenario: Get user by userId
	Given add "getbyid" to base url
	And use userId "1"
	When call the api for user
	Then the user response must contain
		| Name         | Email               | Gender |
		| System Admin | admin@adminmail.com | 0      |

@AddUser
Scenario: Add a user to Api
	Given use json
	"""
	{"userId":0,"citizenId":0,"fullName":"TestSpec","email":"TestSpec@deneme.c","mobilePhones":"123","status":true,"birthDate":"2021-10-13T07:38:16.261Z","gender":0,"recordDate":"2021-10-13T07:38:16.261Z","address":"string","notes":"string","updateContactDate":"2021-10-13T07:38:16.261Z","password":"string"}
	"""
	When post the json to url
	Then the result should be "Added"

@PutUser
Scenario: Update a user to Api
	Given use json
	"""
	{"UserId":2,"Email":"1","FullName":"1","MobilePhones":"1","Address":"1","Notes":"1"}
	"""
	When put the json to url
	Then the result should be "Updated"

@DeleteUser
Scenario: Delete a user to Api
	Given use userId "{"userId": 2}"
	When delete user
	Then the result should be "Deleted"