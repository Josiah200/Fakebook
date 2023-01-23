# Fakebook
Fakebook is a Facebook clone created in C# with ASP.NET Core. Users can create and customize their  own profiles, create posts, search for and add friends, and send instant messages to one another. All of this information is stored in a SQLite database.

The site is live at [**fakebook.josiahmatheson.com**](https://fakebook.josiahmatheson.com).
## Details:

<sup>**Tech used:** C#, ASP.NET Core, SQLite, JavaScript, JQuery, Bootstrap, CSS, HTML</sup>

### Architecture:
Fakebook uses Clean/Onion architecture, separating the app into 3 layers:

* **Core** contains application business logic, and has no dependencies. This decoupling allows for changes to the details of the other layers without worrying about it breaking the core of the app.
* **Infrastructure** handles our database access. Entity Framework allows us to build a database here using the logic outlined in the Core project. This layer also defines repositories, which act as endpoints for the Core project to access data. This is done using dependency injection, so that the Core project does not need to depend on Infrastructure or any of the technology that it uses to implement the database.
* **Web** is the presentation layer, responsible for both the API endpoints and the front-end. This is done using a standard Model-View-Controller setup. This is all done with a standard Model-View-Controller pattern.

### Database:
The project is configured to use a SQLite database for production, and an in-memory database for development. The database is also seeded using the Bogus library, with 100 users upon creation, each with thier own randomly generated profiles, posts, and comments.

### Instant Messaging:
The app's instant messaging component is built using [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr). This creates a websocket connection between users and the server.

### Front-End:
The front-end is mostly served with razor templating. For dynamically loaded content, I used JQuery to make AJAX http requests. The styling is done with JavaScript, Bootstrap, and CSS.

### Hosting:
The [website](https://fakebook.josiahmatheson.com) is hosted on a Ubuntu v20.04 VM running [NGINX](https://www.nginx.com/)

### What I learned:
This project was a targeted effort to learn back-end ASP.NET. I put a lot of effort into learning and implementing Clean Architecture, while keeping the app readable and maintainable during its long development.

The front-end was intentionally done without a framework so I could focus on the back-end implementation. This was a mistake, and resulted in the front-end being difficult to maintain when it inevitably had to keep up with the growing size of the app. Regardless, this allowed--or rather forced--me to become very comfortable with JQuery and Razor.

This project involved countless hours of studying and experimentation, which taught me more than the sum of the finalized product's parts. This left me very confident in my ability to create large-scale ASP.NET web apps, including those with more complex technologies to learn and problems to solve.
