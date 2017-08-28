# Crossvertise_calender
Summary:
This is my implementation for crossvertise programming excercies
In this task I'm performing read operations for calender enteries in db
by getting each month events and also we can explore any event details

Prereq:
1. .Net Framework 4.6.2
2. MS Sql server

How to run:
1. Launch source\Crossvertise.Calender.sln
2. you may need to change CalenderDbEntities connection string if MS Sql server installed on another machine (currently it's using local)
3. Open Nuget Package Manager Console
	a. Make sure "Crossvertise.Calender.WebApi" is your startup project in the solution
	b. choose "Crossvertise.Calender.DAL.EF" for "Default Project"
	c. execute command: "Update-Database" (this is to create database from code first and seed database)
	d. you may need to add enteries in db or by Seed Method in: Crossvertise.Calender.DAL.EF\Migrations\Configuration.cs
4. Debug new instance from Backend\Crossvertise.Calender.WebApi (Web api service for backend)
5. Debug new instance from Frontend\Crossvertise.Calender.WebApplication (web application frontend)
6. from upper navigation bar choose "Calender"
7. Database has only one event entry. so, please choose "Aug" month to get events 

Comments:
1. In this project I was focusing on:
	a. Architecure and Design
	b. separation of concerns ( that's why you will see a lot of projects in the solution, 6 projects for backend, 1 project for frontend and 2 for testing
								also you will find me using Management extensibility framework for injecting backend layers beside using Unity IoC container)
2. I found myself out of time that's why I used Razor and it's not the best option for frontend
3. to accomplish the required UI you will find me repeating myself in code for Calender controller and hitting backend twice in a single frontend request
   but this is to make application like single page I've more time I would prefer using Angular 4 to develop frontend
4. I chose to do unit testing for Business logic layer and if I have more time I would develop unit testing for WebApi as well,
   that's why you will find "Crossvertise.Calender.Tests.Common" so as not repeat my logic again for WebApi unit testing

   
If you have any questions in advance, don’t hesitate to contact me at: karim.mansour91@gmail.com
and finally, I'm looking forward our next step ;)
