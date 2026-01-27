# 056 Unhappy Path Tests
## Lecture

[![# Database Tests](https://img.youtube.com/vi/Fgjhb54oQFA/0.jpg)](https://www.youtube.com/watch?v=Fgjhb54oQFA)

## Instructions

In this lesson you will be expanding upon the tests you wrote in `HomeEnergyApi.Tests/Lesson55Tests/Model/UserRepository.Tests.cs`. You should NOT change any test files inside of the `HomeEnergyApi.Tests/GradingTests`, these are used to grade your assignment.

- In `HomeEnergyApi.Tests/Lesson56Tests/Model/UserRepository.Tests.cs`
    - Update the class declaration to implement `IAsyncLifetime` on `UserRepositoryTest`
    - Add two new private properties below the existing `_testUser` property:
        - `repository` of type `UserRepository`
        - `context` of type `HomeDbContext`
    - Create a new public async method returning a `Task` named `InitializeAsync()`
        - In the method body, create a new `MockDb` and call `CreateDbContext()` on it, assigning the result to `context`
        - Create a new `UserRepository` passing in `context`, and assign the result to `repository`
    - Create a new public async method returning a `Task` named `DisposeAsync()`
        - In the method body, await the result of calling `DisposeAsync()` on `context`
    - Remove the unnecessary lines for creating the `HomeDbContext` and `UserRepository` from the five methods you created in the previous lesson, since you should now have the class level variables `repository` and `context`
    - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldNotSaveUser_WhenInvalidUserProvided()`
        - Set `_testUser.Username` to `null`
        - Create a variable named `exception` and assign it the result of calling `Assert.Throws<DbUpdateException>()` with a lambda that calls `repository.Save(_testUser)`
        - Assert that `exception.Message` contains the string "Required properties '{'Username'}' are missing for the instance of entity type 'User'."
    - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldNotFindUserByUsername_WhenUserDoesNotExists()`
        - Create a variable named `saveUser` and assign it the result of calling `repository.Save(_testUser)`
        - Create a variable named `foundUser` and assign it the result of calling `repository.FindByUsername("badName")`
        - Assert that `foundUser` is Null
    - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldReturnNull_WhenRemovingUserWithAnIdThatDoesNotExist()`
        - Create a variable named `foundUser` and assign it the result of calling `repository.RemoveById(1)`
        - Assert that `foundUser` is Null
    - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldNotUpdateUser_WhenUserIsNull()`
        - Use `Assert.Throws<NullReferenceException>()` with a lambda that calls `repository.Update(1, null)`
    - Create a new public async method returning a `Task` and with the `[Fact]` attribute named `ShouldCountUser_WhenNoUsersExists()`
        - Create a variable named `count` and assign it the result of calling `repository.Count()`
        - Assert that `count` is equal to 0

- In `HomeEnergyApi/Models/UserRepository.cs`
    - In the `RemoveById()` method, wrap the lines removing the `User` and saving changes in an if block, so the `User` is only removed and the changes are only saved if the `User` is not null.

## Additional Information

- Some Models may have changed for this lesson from the last, as always all code in the lesson repository is available to view
- Along with `using` statements being added, any packages needed for the assignment have been pre-installed for you, however in the future you may need to add these yourself

## Building toward CSTA Standards:

## Resources
- https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
