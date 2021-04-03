# Portfolio Project #1: Task Tracker

## Description

Task Tracker is a task tracking web application similar to Trello or Azure Devops, but is much simpler and has a section for showing statistics related to the tasks.

>Disclaimer: The purpose of these projects is to showcase my knowledge of technologies, libraries and concepts in a simple application, it is expected to be a small-scaled application, the focus should be on the use cases of said technologies, libraries and concepts.

## Technologies and Libraries

The following technologies/libraries/concepts were used:

Server:

* Language - C#
* Framework - [`ASP.NET Core 3.1 (Web API)`](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1)
* CORS
* Database - [`MS SQL`](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
* ORM - [`EntityFramework Core`](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)
* Authentication - [`JWT`](https://jwt.io/)
* Logging - [`NLog`](https://www.nuget.org/packages/NLog/)
* Real-Time Communication - [`SignalR`](https://www.nuget.org/packages/Microsoft.AspNetCore.SignalR/)
* Validation and Errors - Custom Implementation
* Mapper - Custom Implementation
* CQRS - Custom Implementation

Client:

* SPA Framework/Library - [`Vue`](https://vuejs.org/) and [`React`](https://reactjs.org/) (Hooks)
* Charts - [`c3.js`](https://c3js.org/)
* HttpClient - [`axios`](https://www.npmjs.com/package/axios)
* UI Library - [`Vuetify`](https://vuetifyjs.com/en/) (Vue) and [`Material-UI`](https://material-ui.com/) (React)
* State Management - Custom Implementation using Vue object (Vue) and [`RxJS`](https://rxjs-dev.firebaseapp.com/guide/overview) (React)
* Real-Time Communication - [`SignalR client`](https://www.npmjs.com/package/@microsoft/signalr)
* Routing - [`vue-router`](https://router.vuejs.org/) and [`react-router`](https://reactrouter.com/)


## Running the Project

You need to install the following:

* [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1)
* [Node and NPM](https://nodejs.org/en/download/)

Clone Repo:
```
> git clone https://github.com/itsdaniellucas/task-tracker

or using GitHub CLI
> gh repo clone itsdaniellucas/task-tracker
```

Run Server:

Before anything, you need to edit `ConnectionStrings.DefaultConnection` segment of `appsettings.json` file in `task-tracker\src\server-dotnet\TaskTracker` and change it to your computer name or SQL instance, then you can run the following command:

```
> cd task-tracker\src\server-dotnet\TaskTracker
> dotnet run
```

Run Client:
```
> cd task-tracker\src\client-vue
> npm install
> npm run serve

or React client
> cd task-tracker\src\client-react
> npm install
> npm run start
```

Default Users:
|   Username    |   Password    |   Description                                                 |
|---------------|---------------|---------------------------------------------------------------|
|   admin       |   admin       |   Create and modify tasks, create sprints, view statistics    |
|   user        |   user        |   Create and modify tasks                                     |
|   (anonymous) |   (anonymous) |   View tasks                                                  |

## License

MIT