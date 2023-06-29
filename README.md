# FrontendBffProject

This project is a Single Page Application (SPA) built using React and CSS. It serves as the frontend for a cocktail-related application. The main functionalities of this project include:

- Login and Logout: Users can authenticate themselves to access the application and securely log out when done.
- Add Your Own Cocktail: Users have the ability to add their own cocktail recipes to the application.
- Search: Users can search for cocktails by name or ingredient.
- Quick Start: Users can quickly get started by selecting one of the suggested cocktails or one of the five liquors available on the page. Additionally, non-alcoholic options are also provided.
- Favorites: Users can store their favorite cocktails and view them in the favorites tab.

## Sipster IDP Project

The Sipster IDP project is responsible for handling the security aspects of the application. It utilizes IdentityServer 3 with Entity Framework Core for secure authentication and authorization. It also integrates with Facebook and Google for external login functionality.

## GraduationProject

The GraduationProject folder contains the backend API for the application. It serves as the bridge between the frontend and the database. The main features of this backend API include:

- Database: It has its own database to store user favorites and cocktail data.
- Integration with CocktailDBAPI: The backend API integrates with the CocktailDBAPI to fetch cocktail information, which is then stored in the local database.
- Data Mapper: A mapper is implemented to translate the information received from CocktailDBAPI to fit the schema of the local database.

## Unit Testing

Unit testing for this project is implemented using mocking in xUnit. This approach allows for isolated testing of individual components, ensuring their correctness and reliability.

Please note that this README provides a high-level overview of the project and its key components.
