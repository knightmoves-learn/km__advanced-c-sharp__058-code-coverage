# 058 Code Coverage

## Lecture

[![# Code Coverage(Part 1)](https://img.youtube.com/vi/fv_UtpUIb5Y/0.jpg)](https://www.youtube.com/watch?v=fv_UtpUIb5Y)
[![# Code Coverage(Part 2)](https://img.youtube.com/vi/X-m4R4Va-nQ/0.jpg)](https://www.youtube.com/watch?v=X-m4R4Va-nQ)

## Instructions

In this lesson you will be expanding upon the tests you wrote in `HomeEnergyApi.Tests/Lesson55Tests/Model/UserRepository.Tests.cs`.
**Important:** Do not modify any test files inside `HomeEnergyApi.Tests/GradingTests/` as these are used to grade your assignment.

- To run the commands from the lecture for this assignment
  - Run `dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=lcov -p:CoverletOutput=coverage.info ./HomeEnergyApi.Tests` at the root of this project
  - Run `dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=lcov -p:CoverletOutput=coverage.info ./HomeEnergyApi.Tests` at the root of this project
  - Run `dotnet tool install -g dotnet-reportgenerator-globaltool` if you have not already, before...
    - Running `reportgenerator -reports:HomeEnergyApi.Tests/coverage.info -targetdir:CoverageReport -reporttypes:Html` at the root of this project
- The lines `/CoverageReport` and `**/**/coverage.info` have been added to your .gitignore prior to starting this assignment

- In `HomeEnergyApi.Tests/Lesson58Tests/Helpers/JwtHelper.cs`
  - Create a new public class `JwtHelper`
  - Create a new public static async method returning a `Task<string>` named `GenerateTokenAsync()` that takes parameters `client` of type `HttpClient` and `role` of type `string`
    - Create a variable named `guid` and assign it the result of calling `NewGuid()` on `Guid` and converting it to a string
    - Create a variable named `userDto` of type `UserDtoV1` and initialize it with the following properties:
      - `Username` set to `"TestUser"` concatenated with `guid`
      - `Password` set to `"TestPassword"` concatenated with `guid`
      - `Role` set to `role`
      - `HomeStreetAddress` set to `"123 Main St"`
    - Create a variable named `initialSetupResponse` and assign it the result of awaiting `PostAsJsonAsync()` on `client`, passing in `"/v1/authentication/register"` and `userDto`
    - Assert that `initialSetupResponse.StatusCode` is equal to `HttpStatusCode.OK`
    - Create a variable named `response` and assign it the result of awaiting `PostAsJsonAsync()` on `client`, passing in `"/v1/authentication/token"` and `userDto`
    - Create a variable named `content` and assign it the result of awaiting `ReadFromJsonAsync<TokenResponse>()` on `response.Content`
    - Assert that `response.StatusCode` is equal to `HttpStatusCode.OK`
    - Return `content?.Token`
  - Create a new class `TokenResponse`
    - Add a public property `Token` of type `string`

- In `HomeEnergyApi.Tests/Lesson58Tests/Helpers/UserAcceptanceTest.cs`
  - Create a new public class `UserAcceptanceTest` implementing `IClassFixture<WebApplicationFactory<Program>>` and `IAsyncLifetime`
  - Add a protected readonly property `_client` of type `HttpClient`
  - Add a private readonly property `_role` of type `string`
  - Create a constructor that takes parameters `factory` of type `WebApplicationFactory<Program>` and `role` of type `string`
    - In the constructor body, assign the result of calling `CreateClient()` on `factory` to `_client`
    - Call `Add()` on `_client.DefaultRequestHeaders`, passing in `"X-API-Key"` and the result of calling `ConfigHelper.LookupSecret("ApiKey")`
    - Assign `role` to `_role`
  - Create a new public async method returning a `Task` named `InitializeAsync()`
    - Create a variable named `token` and assign it the result of awaiting `JwtHelper.GenerateTokenAsync()`, passing in `_client` and `_role`
    - Assign a new `AuthenticationHeaderValue` with `"Bearer"` and `token` to `_client.DefaultRequestHeaders.Authorization`
  - Create a new public method returning a `Task` named `DisposeAsync()`
    - In the method body, return `Task.CompletedTask`

- In `HomeEnergyApi.Tests/Lesson58Tests/Controllers/HomeAdminController.Tests.cs`
  - Create a new public class `HomeAdminControllerTest` extending `UserAcceptanceTest`
  - Add a private readonly property `_homeDto` of type `HomeDto`
  - Create a constructor that takes a parameter `factory` of type `WebApplicationFactory<Program>` and calls the base constructor passing in `factory` and `"Admin"`
    - In the constructor body, create a variable named `result` of type `PaginatedResult<Home>` and initialize it
    - Assign a new `List<Home>()` to `result.Items`
    - Assign a new `HomeDto` to `_homeDto` with the following properties:
      - `OwnerLastName` set to `"Testy"`
      - `StreetAddress` set to `"49 Test St"`
      - `City` set to `"Test City"`
      - `MonthlyElectricUsage` set to `1234`
  - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldCreateHome_WhenGivenValidHome()`
    - Create a variable named `response` and assign it the result of awaiting `PostAsJsonAsync()` on `_client`, passing in `"/admin/Homes"` and `_homeDto`
    - Assert that `response.StatusCode` is equal to `HttpStatusCode.Created`
    - Create a variable named `createdHome` and assign it the result of awaiting `ReadFromJsonAsync<Home>()` on `response.Content`
    - Assert that `createdHome` is not Null
    - Assert that `createdHome.Id` is greater than 0
    - Assert that `createdHome.OwnerLastName` is equal to `_homeDto.OwnerLastName`
    - Assert that `createdHome.StreetAddress` is equal to `_homeDto.StreetAddress`
    - Assert that `createdHome.City` is equal to `_homeDto.City`
    - Assert that `createdHome.HomeUsageData.MonthlyElectricUsage` is equal to `_homeDto.MonthlyElectricUsage`
  - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldNotAllow_WhenUserIsNotAdmin()`
    - Create a variable named `token` and assign it the result of awaiting `JwtHelper.GenerateTokenAsync()`, passing in `_client` and `"NotAdmin"`
    - Assign a new `AuthenticationHeaderValue` with `"Bearer"` and `token` to `_client.DefaultRequestHeaders.Authorization`
    - Create a variable named `response` and assign it the result of awaiting `PostAsJsonAsync()` on `_client`, passing in `"/admin/Homes"` and `_homeDto`
    - Assert that `response.StatusCode` is equal to `HttpStatusCode.Forbidden`
  - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldNotCreateHome_WhenHomeStreetAddressDoesNotContainDigit()`
    - Assign `"NoNumber St"` to `_homeDto.StreetAddress`
    - Create a variable named `response` and assign it the result of awaiting `PostAsJsonAsync()` on `_client`, passing in `"/admin/Homes"` and `_homeDto`
    - Assert that `response.StatusCode` is equal to `HttpStatusCode.BadRequest`
  - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldNotCreateHome_WhenHomeStreetAddressIsTooLong()`
    - Assign a new string of 65 `'A'` characters to `_homeDto.StreetAddress`
    - Create a variable named `response` and assign it the result of awaiting `PostAsJsonAsync()` on `_client`, passing in `"/admin/Homes"` and `_homeDto`
    - Assert that `response.StatusCode` is equal to `HttpStatusCode.BadRequest`

## Additional Information

- Some Models may have changed for this lesson from the last, as always all code in the lesson repository is available to view
- Along with `using` statements being added, any packages needed for the assignment have been pre-installed for you, however in the future you may need to add these yourself

## Building toward CSTA Standards:

- Develop and use a series of test cases to verify that a program performs according to its design specifications (3B-AP-21) https://www.csteachers.org/page/standards

## Resources

- https://en.wikipedia.org/wiki/Unit_testing
- https://martinfowler.com/bliki/TestPyramid.html
- https://xunit.net/
- https://github.com/coverlet-coverage/coverlet

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
