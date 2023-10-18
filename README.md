# Xunit.TestLogger

## Before this NuGet package
If you make [integration tests](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0) with [xUnit](https://xunit.net/), your logging is gone. You can just see if the test succeeds or fails but you cannot see why as you don't have the logging to help you understanding what happened during the runtime of the test, which makes it hard to solve it.

## Now
The is situation is different now! The problem is solved. Alle you need to do is stop using this way of creating a `WebApplicationFactory`:

```csharp
public NoLoggingTest()
{
    _factory = new WebApplicationFactory<Program>();
}
```

and start using this way

```csharp
public ImprovedLoggingTest(ITestOutputHelper output)
{
    _factory = new TestLoggerWebApplicationFactory<Program>(output);
}
```
Here is how it looks like:

![Alt text](ScreenForLogging.png)


