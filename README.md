# Central Games Platform
---
The purpose of the Central Games Platform project is to develop a web based application intended for Windows 10 and Android that acts as a hybrid of both an e-commerce store to purchase and play video games, and an online casino, which is intended to be a one stop shop for gamers and casino players alike. 

Users will be able to purchase and play the latest and greatest blockbuster video game titles, or purchase casino passes to participate in casino games for a chance of winning real world currency. Video games can either be playable from the browser, or be installed to the supported device, depending on the game. Video games that are available on both platforms will be accessible to the user at no additional cost. The application will allow users to transfer winnings from casino games to a registered PayPal account. 

Impulsive gambling is a huge ethical issue, so a limit of 10 casino passes purchasable per day will be imposed on the user.

Central Games Platform is designed with a business to consumer/business to business approach. Users purchase games from the platform. Publishers of these games will receive the revenue the game has generated every month with a 15% cut. Casino games will be developed in-house. 100% of revenue from casino games goes straight to Central Games Platform. 


# Technologies and design patterns
---
* C#
* ASP.NET Core
* Microsoft Azure App Service Plan
* Microsoft Azure SQL Database
* Stripe Payment Gateway
* PayPal Payouts .NET SDK
* Bootstrap 4
* Entity Framework Core
* ASP.NET Core Identity API
* MVC Design Pattern
* Repository data access abstraction pattern

# How To Use
---
## Public Version
This project is hosted in the cloud using Microsoft Azure's App Service Plan and can be accessed through this URL:
https://centralgamesplatform.azurewebsites.net/

## Local Version
This project can also be ran locally on your machine:
1. Download and install .NET Core runtime version 3.1 or greater this can be done at [this link here](https://dotnet.microsoft.com/download).
2. Clone this repository into a directory of your choice.
3. Replace \<REDACTED\> on line 10 of appsettings.json with the connection string found in page __46__ of the __Technical Manual__ to connect to the database
4. Run the command `dotnet run` inside the directory that contains CentralGamesPlatform.csproj
5. The web app will be hosted on the localhost. The port may vary so be sure to see the output of the dotnet run command to see which port it is using
6. Paste the localhost and port number into a web browser of your choice. Example `https://localhost:50001`

Alternatively, the app can be ran through Visual Studio, providing ASP.NET Core is installed as a component.

Refer to page 20 of the technical manual for test card details for payments
Refer to page 27 of the technical manual for test email for payouts
