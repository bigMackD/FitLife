# FitLife

FitLife is an Angular Web Application with a backend created in .NET CORE 3.
Main purpose while creating this app was just to have fun and mess with some new technologies/design patterns.

#  Main business features
  - Creating invidual users accounts
  - Managing users diet and workout plans
  - Cool charts and daily/monthly summaries

# Tech features

  - API Swagger documentation
  - Designed in CQRS pattern
  - Command/queries validation created using FluentValidation
  - Unit tests written with NUnit
  - MS Sql Database with Entity Framework Core as ORM
  - Angular guards, interceptors, resolvers and dataSources

# Libraries
- [FluentValidation.AspNetCore](https://www.nuget.org/packages/FluentValidation.AspNetCore/) for requests validation
- [NUnit](https://www.nuget.org/packages/NUnit/3.13.1) for unit testing
- [Moq](https://www.nuget.org/packages/Moq/4.15.2) for mocking objects
- [EntityFrameworkCore.InMemory](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory/3.1.6) for mocking DB
- [EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/3.0.0) as ORM
- [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore/3.0.0) as identity provider
- [Swashbuckle](https://www.nuget.org/packages/Swashbuckle.AspNetCore/5.6.3) for API documentation
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Swashbuckle.AspNetCore/5.6.3) for JSON Web Token Authentication
- [ngx-charts](https://www.npmjs.com/package/@swimlane/ngx-charts) for Angular charts


### Screenshots
Right now FitLife isn't deployed anywhere buy enjoy some screenshots :)

#### Login Page
![Alt text](screenshots/1.png?raw=true "Login Page")

#### Home Screen
![Alt text](screenshots/2.png?raw=true "Home Screen")

#### Meals list
![Alt text](screenshots/3.png?raw=true "Meals list")

#### Product Details
![Alt text](screenshots/4.png?raw=true "Product Details")

#### Meal Details
![Alt text](screenshots/5.png?raw=true "Meal Details")

#### Dashboard
![Alt text](screenshots/6.png?raw=true "Dashboard")

#### Users list
![Alt text](screenshots/7.png?raw=true "Users list")




### Todos

 - Write services to communicate to using RabbitMQ (introduce microservices)
 - Add docker support
 - Add workouts tracking feature
 - Monthly summaries of meals/workouts
 - Dynamically register all interfaces

If you like this project, consider giving it a :star: 

License
----
MIT



