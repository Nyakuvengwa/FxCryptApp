# FxCryptApp
This project is a demonstration of to get the Bid Price of BTC/USD from different sources.

## Running the application.
Prerequisites:
- Visual Studio 2022 Community Edition
- Git 
- Docker Desktop
- .Net 6 

To run this demo, first create a folder, cd into it, and then git clone the project.

```
git clone https://github.com/Nyakuvengwa/FxCryptApp.git
```

Then run these commands to start the required containers:
```
docker-compose up
```


Then you double click on FxCryptApp.sln to open the solution in Visual Studio.

In Visual Studio, Use the IIS Express to run the application and bring up the SwaggerUI.
## Details 
A few key points about this application:

### docker-compose.yml file

Note that there are two images - one for the [PostgreSQL](https://hub.docker.com/_/postgres) and another for [pgAdmin 4](https://hub.docker.com/r/dpage/pgadmin4/). The images are pre-configured with default users and passwords. If you have 

#### NB: Local PostgreSQL
if you have PostgreSQL preinstalled in on your local you can stop the service temporarily so that you can run the solution to test the application.
