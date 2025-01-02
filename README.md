# StorySpoil-App-Testing

Story Spoil is an app for creating story spoilers. Users can create, edit, delete and search for story spoilers. Also they can edit and manage their accounts.


<p align="center">
  <img 
    alt="Software University Logo"
    src="https://vizia.sofia.bg/wp-content/uploads/2018/11/software-university-logo.png"
    width="300"
  >
</p>

> _🧪 Web .NET Core application to be tested, provided by Software University 'SoftUni' - Bulgaria_

## Test cases
Written test cases by me for this application can be found on [this link](https://docs.google.com/spreadsheets/d/1EQ8GlasIktTlla4jjLqdzei-pgCaJ4JP/edit?usp=drive_link&ouid=101865710122533479047&rtpof=true&sd=true). 

Features:

  - Create Story Spoilers: Create, edit & delete spoilers.
  -  Search functionality: Search spoilers by keywords for easy access.
  -  Edit and manage: Update spoiler details seamlessly.
  -  User Management: Update and manage personal information.

## Run tests
First, you need to have Visual Studio installed, then run the following commands in the terminal:

#install needed dependencies

dotnet restore

#build the project

dotnet build

#run tests

dotnet test

## Testing Types performed

✅ UI Testing
Validated user interface elements and interactions for:

  - User Registration & Login
  - Spoiler Creation & Manipulation
  - Search functionality
  - User Management

✅ API Testing
Tested backend functionality and API endpoints for:

  - User Authentication
  - Spoiler Creation & Manipulation



## Bug Fixes and Improvements
## 1. Improved Quality Assurance

Tests have ensured robust software by detecting and preventing critical issues:

🛠 Critical Bug Fixes:

  - User cannot restore their password.

  - User cannot edit their profile.
   
  - Search Functionality does not work.


🛠 Reliability Enhancements: Validated features under negative conditions.

Bugs Found:

  - User can register with invalid email.

  - Repeat password accept more characters than required.

  - No message is displayed when user seach for non-existing story.

## 2. Documentation Design Enhancements

Ensured the documentation reflects accurate project requirements:

  - Missing requirements for Edit Task page.
