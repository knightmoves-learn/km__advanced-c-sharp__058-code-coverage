# 054 Async Testing
## Lecture

[![# Async Testing (Part 1)](https://img.youtube.com/vi/6XL7YSYtQos/0.jpg)](https://www.youtube.com/watch?v=6XL7YSYtQos)
[![# Async Testing (Part 2)](https://img.youtube.com/vi/XKtsefeE2IQ/0.jpg)](https://www.youtube.com/watch?v=XKtsefeE2IQ)

## Instructions

In this lesson you will be testing the method contained in `HomeEnergyApi/Services/RateLimitingService.cs` where a new method `IsWeekend()` has been added. You will need to create test stubs to test this method, in addition to adding tests to `HomeEnergyApi.Tests/Lesson54Tests/RateLimitingService.Tests.cs`. You should NOT change any test files inside of the `HomeEnergyApi.Tests/GradingTests`, these are used to grade your assignment.

- In `HomeEnergyApi.Tests/Stubs/StubLogger.cs`
    - Create a new public property on `StubLogger<T>` `LoggedDebugMessages` of type `List<string>` and initialize it as an empty list of strings
    - In the method `Log<TState>` add a second if block that checks if the logLevel is `LogLevel.Debug` and if so add the formatted message to `LoggeddDebugMessages`

- In `HomeEnergyApi.Tests/Stubs/StubRateLimitingService.cs`
    - Create a new public class `StubRateLimitingService` implementing `RateLimitingService`
    - Add a private readonly property `_isRequestAllowed` of type `bool`
    - Create a constructor that takes a parameter `isRequestAllowed` of type `bool` and calls the base constructor passing in `new DateTimeWrapper()`
        - In the constructor body assign `isRequestAllowed` to `_isRequestAllowed`
    - Override the method `IsRequestAllowed` that takes a parameter `clientKey` of type `string` and returns a `bool`
        - In the method body return `_isRequestAllowed`

- In `HomeEnergyApi.Tests/Lesson54Tests/RateLimitingMiddlewareTest.cs`
    - Add the following private property names / types / values assigned in constructor
        - _stubLogger / `StubLogger<RateLimitingMiddleware>` / `new StubLogger<RateLimiting Middleware>`
        - _expectedHttpContext / `DefaultHttpContext` / `_expectedHttpContext.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1")` and `_expectedHttpContext.Response.Body = new MemoryStream()`
        - _underRateLimitService / `StubRateLimitingService` / `new StubRateLimitingService(true)`
        - _overRateLimitService / `StubRateLimitingService` / `new StubRateLimitingService(false)`
        - _actualContext / `HttpContext?` 
            - This should be initialized with a value of `null` and does not need to be initialized in the constructor
        - _stubRequestDelegate / `RequestDelegate` / `async (HttpContext context) => _actualContext = context`
    - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldCallNextMiddleware_WhenRateLimitNotExceeded`
        - Create a variable to hold your middleware, by creating a new `RateLimitingMiddleware` and passing `_stubRequestDelegate`, `_underRateLimitService`, and `_stubLogger` to its constructor.
        - Await the result of calling `InvokeAsync()` on your newly created `RateLimitingMiddleware`, passing in `_expectedHttpContext`
        - Call `Seek()` on `_expectedHttpContext.Response.Body` passing in `0` and `SeekOrigin.Begin`
        - Create a variable to hold the response body by awaiting the result of calling `ReadToEndAsync()` on a new `StreamReader` passing in `_expectedHttpContext.Response.Body` and `Encoding.UTF8`
        - Assert that the response body is equal to an empty string
        - Assert that `_expectedHttpContext` is the Same as `_actualContext`
        - Assert that `_expectedHttpContext.Response.StatusCode` is equal to `StatusCodes.Status200OK`
        - Assert that `_stubLogger.LoggedDebugMessages.Count` is equal to 2
        - Assert that `_stubLogger.LoggedDebugMessages[0]` is equal to "Starting middleware"
        - Assert that `_stubLogger.LoggedDebugMessages[1]` is equal to "Calling next middleware"
    - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldReturn429_WhenRateLimitExceeded`
        - Create a variable to hold your middleware, by creating a new `RateLimitingMiddleware` and passing `_stubRequestDelegate`, `_overRateLimitService`, and `_stubLogger` to its constructor.
        - Await the result of calling `InvokeAsync()` on your newly created `RateLimitingMiddleware`, passing in `_expectedHttpContext`
        - Call `Seek()` on `_expectedHttpContext.Response.Body` passing in `0` and `SeekOrigin.Begin`
        - Create a variable to hold the response body by awaiting the result of calling `ReadToEndAsync()` on a new `StreamReader` passing in `_expectedHttpContext.Response.Body` and `Encoding.UTF8`
        - Assert that the response body is equal to "Slow down! Too many requests."
        - Assert that `_actualContext` is Null
        - Assert that `_expectedHttpContext.Response.StatusCode` is equal to `StatusCodes.Status429TooManyRequests`
        - Assert that `_stubLogger.LoggedDebugMessages` is a Single item
        - Assert that `_stubLogger.LoggedDebugMessages[0]` is equal to "Starting middleware"

## Additional Information

- Some Models may have changed for this lesson from the last, as always all code in the lesson repository is available to view
- Along with `using` statements being added, any packages needed for the assignment have been pre-installed for you, however in the future you may need to add these yourself

## Building toward CSTA Standards:

## Resources
- https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
