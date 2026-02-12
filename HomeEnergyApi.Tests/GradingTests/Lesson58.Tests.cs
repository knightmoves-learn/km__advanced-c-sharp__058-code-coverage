using System.Reflection;
using HomeEnergyApi.Models;

public class LessonTests
{
    private readonly string[] _requiredTestMethods =
    {
        "ShouldCreateHome_WhenGivenValidHome",
        "ShouldNotAllow_WhenUserIsNotAdmin",
        "ShouldNotCreateHome_WhenHomeStreetAddressDoesNotContainDigit",
        "ShouldNotCreateHome_WhenHomeStreetAddressIsTooLong"
    };

    [Fact]
    public void JwtHelperClassExists()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var jwtHelperClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "JwtHelper");

        Assert.NotNull(jwtHelperClass);
    }

    [Fact]
    public void JwtHelperHasGenerateTokenAsyncMethod()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var jwtHelperClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "JwtHelper");

        Assert.NotNull(jwtHelperClass);

        var method = jwtHelperClass.GetMethod("GenerateTokenAsync", BindingFlags.Public | BindingFlags.Static);
        Assert.NotNull(method);
        Assert.Equal(typeof(Task<string>), method.ReturnType);
    }

    [Fact]
    public void TokenResponseClassExists()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var tokenResponseClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "TokenResponse");

        Assert.NotNull(tokenResponseClass);

        var tokenProperty = tokenResponseClass.GetProperty("Token");
        Assert.NotNull(tokenProperty);
        Assert.Equal(typeof(string), tokenProperty.PropertyType);
    }

    [Fact]
    public void UserAcceptanceTestClassExists()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var userAcceptanceTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "UserAcceptanceTest");

        Assert.NotNull(userAcceptanceTestClass);
    }

    [Fact]
    public void UserAcceptanceTestImplementsIAsyncLifetime()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var userAcceptanceTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "UserAcceptanceTest");

        Assert.NotNull(userAcceptanceTestClass);

        var implementsIAsyncLifetime = userAcceptanceTestClass.GetInterfaces()
            .Any(i => i.Name == "IAsyncLifetime");
        
        Assert.True(implementsIAsyncLifetime, "UserAcceptanceTest should implement IAsyncLifetime");
    }

    [Fact]
    public void UserAcceptanceTestHasInitializeAsyncMethod()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var userAcceptanceTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "UserAcceptanceTest");

        Assert.NotNull(userAcceptanceTestClass);

        var initializeMethod = userAcceptanceTestClass.GetMethod("InitializeAsync");
        Assert.NotNull(initializeMethod);
        Assert.Equal(typeof(Task), initializeMethod.ReturnType);
    }

    [Fact]
    public void UserAcceptanceTestHasDisposeAsyncMethod()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var userAcceptanceTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "UserAcceptanceTest");

        Assert.NotNull(userAcceptanceTestClass);

        var disposeMethod = userAcceptanceTestClass.GetMethod("DisposeAsync");
        Assert.NotNull(disposeMethod);
        Assert.Equal(typeof(Task), disposeMethod.ReturnType);
    }

    [Fact]
    public void HomeAdminControllerTestExists()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var homeAdminTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "HomeAdminControllerTest");

        Assert.NotNull(homeAdminTestClass);
    }

    [Fact]
    public void HomeAdminControllerTestExtendsUserAcceptanceTest()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var homeAdminTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "HomeAdminControllerTest");

        Assert.NotNull(homeAdminTestClass);

        var baseType = homeAdminTestClass.BaseType;
        Assert.NotNull(baseType);
        Assert.Equal("UserAcceptanceTest", baseType.Name);
    }

    [Fact]
    public void HomeAdminControllerTestHasAllRequiredMethods()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var homeAdminTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "HomeAdminControllerTest");

        Assert.NotNull(homeAdminTestClass);

        foreach (var requiredMethodName in _requiredTestMethods)
        {
            var testMethod = homeAdminTestClass.GetMethod(requiredMethodName);
            Assert.True(testMethod != null, $"Method {requiredMethodName} not found in HomeAdminControllerTest class");
        }
    }

    [Fact]
    public void HomeAdminControllerTestMethodsHaveFactAttribute()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var homeAdminTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "HomeAdminControllerTest");

        Assert.NotNull(homeAdminTestClass);

        foreach (var requiredMethodName in _requiredTestMethods)
        {
            var testMethod = homeAdminTestClass.GetMethod(requiredMethodName);
            Assert.NotNull(testMethod);

            var factAttribute = testMethod.GetCustomAttribute<FactAttribute>();
            Assert.True(factAttribute != null, $"Method {requiredMethodName} should have [Fact] attribute");
        }
    }

    [Fact]
    public void HomeAdminControllerTestMethodsReturnTask()
    {
        var testAssembly = Assembly.GetExecutingAssembly();
        var homeAdminTestClass = testAssembly.GetTypes()
            .FirstOrDefault(t => t.Name == "HomeAdminControllerTest");

        Assert.NotNull(homeAdminTestClass);

        foreach (var requiredMethodName in _requiredTestMethods)
        {
            var testMethod = homeAdminTestClass.GetMethod(requiredMethodName);
            Assert.NotNull(testMethod);
            Assert.Equal(typeof(Task), testMethod.ReturnType);
        }
    }
}
