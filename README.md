# Financial Chatroom
Chatroom API with stock quotes from an external API.

# Features
- MULTI Chatroom Support.
- Create Chatrooms.
- List Chatroom list.
- Register User.
- Login User.
- Secure Chat that allows clients to send messages to the server and the other way round.
- A decoupled bot that will call an API using the stock_code as a parameter.             
- Frontend login form.
- Frontend chatroom window. Ability to switch among chatrooms.
- List the last 50 messages from a particular chatroom.
- Incremental database Migrations.
- Error Handling Middleware.

# Technologies

- .Net Code App (2.2).
- Postgresql database with Entity Framework.
- JWT Authentication and Authorization in ASP.NET Core Web API.
- SignalR hub.
- xUnit


# Instructions

1) Pull repository locally and run the API in Visual Studio or deploy all necessary files in IIS.

2) Configure connection string and Token options in AppSettings.{env}.json. 
 "DefaultConnection": "Host=localhost;Port=5432;Username=<user>;Password=<password>;Database=<database>;"

3) Install Postgress where the DB will be deployed. If the database will run in a local machine, run "cmd.exe" on ~/JobsityFinancialChat.Domain and run the following command "dotnet ef database update". 
It will run all necessary migration files to create and put the database up-to-date. (note 1)

4) Once the WebApi is deployed, go to the Frontend folder and run the UI. Click on "init.html" and enter your user credentials**. If the WebAPI is running in a local environment, a new user must be created with:

```POST /api/Accounts/register HTTP/1.1
Host: localhost:57092
Content-Type: application/json
cache-control: no-cache
Postman-Token: af0dcf4c-91ce-4f10-822a-76a19282831d
{
  "firstName": <firstName>,
  "firstName": "string",
  "email": "user_2@jobsity.com",
  "password": <password> // Password must have at least 8 characters, an Uppercase letter, a symbol and a number. (i.e."A$2WEe_1")
}     
```

5) After the user is authenticated, the UI saves the user token which is retrieved from the login endpoint. This process is automatic.

6) The user will see a list of available chatrooms, where he/she can join in (note 3). After the user selects a chatroom, the API retrieves the last 50 messages sorted by created date.

7) The user can post messages as commands into the chatroom with the following format `/stock=stock_code`. The message will be a stock quote using the following format: “APPL.US quote is $93.42 per share.

8) All users that belong to the chatroom should see all the messages, but the command messages are not stored in the DB.

 (note 1) There's a remote server DB that can be used. Please send an email to ddonari@gmail.com to get access.

 (note 2) There are preexisting users in the remote database. Please send an email to ddonari@gmail.com to get access.

 (note 3) The API provides an endpoint that can be used to create new chat rooms:

```POST /api/Chatrooms/ HTTP/1.1
Host: localhost:57092
Content-Type: application/json
Authorization: Bearer <<BEARER TOKEN>>
User-Agent: PostmanRuntime/7.18.0
Accept: */*
Cache-Control: no-cache
Postman-Token: 5b635b87-e21a-4952-82c3-018c6132145f,4a555128-d21b-43c9-b39c-57688fff7a30
Host: localhost:57092
Accept-Encoding: gzip, deflate
Content-Length: 31
Cookie: .AspNetCore.Identity.Application=<<APPLICATION TOKEN>>
Connection: keep-alive
cache-control: no-cache

{
    "Name": "chatroom name"
}```
    
