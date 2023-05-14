# Swisscom SminGate Coding Challenge Backend

## Background
A public JSON REST-API is available that returns a list of target assets from our postman mock server on the endpoint ```/targetAsset```. For the purposes of this task, this API will be referred to as the "TargetAsset-API".

The objective of the task is to create a new JSON API, referred to as the "Demo-API", which will provide its own endpoint ```/targetAsset```. You can use the existing project "Demo-API" for this purpose.

## Task
Create a new endpoint ```/targetAsset``` in the Demo-API. This should call the TargetAsset-API (GET on /targetAsset), then add the fields "isStartable" and "parentTargetAssetCount" to the received target assets, and return the enriched target assets as JSON to the Demo-API caller.

- The ```isStartable``` field should be ```true```&nbsp;if, according to the current date (local system time of the Demo-API), it is the third day of the month and the ```status``` field of the target asset is ```Running```. Otherwise, ```isStartable``` should be ```false```.

- The ```parentTargetAssetCount``` field should contain the number of parent target assets that can be determined by the ```parentId``` field of each target asset. For example, if a target asset named "TestTargetAsset" has a ```parentId``` of ```5```, the target asset with ID 5 has a ```parentId``` of ```1```, and the target asset with ID ```1``` has a ```parentId``` of ```null```, then the ```parentTargetAssetCount``` field of "TestTargetAsset" should be ```3```. The code for determining this should be written by yourself and should not come from a library.

Think about edge cases and possible sources of errors and try to handle them cleanly. The business logic should be covered with unit tests wherever necessary. We should be able to see how you generally test business logic with UnitTests and which cases you consider important/worthy of testing.

We have a strong emphasis on the quality of our codebase, and as a team, we demand that you uphold this standard by employing both clean code principles and C# best practices in your code. These practices are highly valued in our organization and are critical to building maintainable, efficient, and scalable software solutions.

> **Important:** To ensure your privacy, please **download** the Github repository and submit your work in your own Github repository. Once finished, kindly send us the link to the repository for review.

## TargetAsset-API
REST Endpoint:&nbsp;[https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/targetAsset]

No Authentication is needed to the TargetAsset-API.


## The solution 

Choosing the Clean Architecture for C# project I know it is little over engineering for this task but where is the fun :D
so the Desgin consists of four layers Domain , infrastructure , Application , Demo-Api each layer is seprated project

	- Domain : contain the modals class for the solution
	- Infrastructure : Repos , database in this case it has repo that make the request to get the data 
	- Application : here where the bussinse logic live , has service that make the isStartable , and count parent 
	- Demo-API : is the layer that outsiders talk to it has the controller with the API fucntion that return the target- Assets 

For testing Infrastructure and Application has it's own test project 
for Domain no need as it is only class definations , for Demo-API will be only calling the Application layer so no need to test it alone and more likly will be in the end to end testing 

So the Solution 

	- Domain : Class for the target Asset
	- Infrastructure : Service for DateTime to isolate this logic and to be easily replaced , TargetAssetRepo to make async request to get the data 
	- Application: service for muniplation of the returned data from the repo to add the two new properties 
	- Demo- API : One controller for the API to get the target Assets 

Thanks you for reach this part 

Made by Nour Ahmed Abbas 
