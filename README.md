# 058 Code Coverage
## Lecture

[![# Code Coverage(Part 1)](https://img.youtube.com/vi/fv_UtpUIb5Y/0.jpg)](https://www.youtube.com/watch?v=fv_UtpUIb5Y)
[![# Code Coverage(Part 2)](https://img.youtube.com/vi/fv_UtpUIb5Y/0.jpg)](https://www.youtube.com/watch?v=fv_UtpUIb5Y)

## Instructions

In this lesson you will be expanding upon the tests you wrote in `HomeEnergyApi.Tests/Lesson55Tests/Model/UserRepository.Tests.cs`. 
**Important:** Do not modify any test files inside `HomeEnergyApi.Tests/GradingTests/` as these are used to grade your assignment.

- To run the commands from the lecture for this assignment
    - Run `dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=lcov -p:CoverletOutput=coverage.info ./HomeEnergyApi.Tests` at the root of this project
    - Run `dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=lcov -p:CoverletOutput=coverage.info ./HomeEnergyApi.Tests` at the root of this project
- The lines `/CoverageReport` and `**/**/coverage.info` have been added to your .gitignore prior to starting this assignment

-In `HomeEnergyApi.Tests/Lesson57Tests/Helpers/ConfigHelper.cs`

## Additional Information

- Some Models may have changed for this lesson from the last, as always all code in the lesson repository is available to view
- Along with `using` statements being added, any packages needed for the assignment have been pre-installed for you, however in the future you may need to add these yourself

## Building toward CSTA Standards:

## Resources
- https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
