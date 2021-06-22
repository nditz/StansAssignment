Warehouse System 
==============


**Document history:**

| Date | Description 
-|-
| 22 June 2021 | First version of this document

**Table of Content**

<!-- TOC -->autoauto- [Introduction](#introduction)auto    -[Improvements](#imprpvements)autoauto<!-- /TOC -->



# Introduction

A quickly put together API mean to demonstrate DDD and clean architecture approach and creating a service that adds, retrieves and aggregates warehouse products and inventory data.
The goal is to show how different components come together using `strategy pattern`, `dependancy injection` and implementing a few test cases 

Swagger has been integrated with the project and once run should have all endpoints working and well demonstarted   



# Improvements
As mentioned the project was hastly put together though below are areas i feel would be of focus was there more time to work on it 

	1) Logging and Error handling - This can be achieved by implementing a standard logging library to be able to capture and give proper feedback to frontend 
	2) Event Handlers such as Email handler that would be used to send emails across the system and notify users of events 
	3) More Unit Test coverage specific to each main projects i.e infrastructure and web api endpoints 
	4) Integration with database - right now the logic is made such that it persists data on memory and aggregate from there since no specification was given about 
		any specific database 
	5) Use of some Mappers and mediators to achieve full clean architecture functionality and loosly coupled objects 




