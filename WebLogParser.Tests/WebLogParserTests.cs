using System.Net;
using WebLogParser;

[TestClass]
public class WebLogParserTests
{
    [TestMethod]
    public void ExtractUniqueIds_ParsesIdsCorrectly()
    {
        // Arrange
        string logContent = "Sample log ?sharelinkId-123 text ?sharelinkId-456 within the log.";

        // Act
        List<string> uniqueIds = WebLogParserClass.ExtractUniqueIds(logContent);

        // Assert
        CollectionAssert.AreEqual(new List<string> { "123", "456" }, uniqueIds);
    }

    [TestMethod]
    public void ExtractUniqueIds_NoMatchingIds_ReturnsEmptyList()
    {
        // Arrange
        string logContent = "No sharelinkId in this log.";

        // Act
        List<string> uniqueIds = WebLogParserClass.ExtractUniqueIds(logContent);

        // Assert
        CollectionAssert.AreEqual(new List<string>(), uniqueIds);
    }

    [TestMethod]
    public void PrintUniqueIds_PrintsUniqueIdsCorrectly()
    {
        // Arrange
        List<string> uniqueIds = new() { "123", "456", "123" };

        // Act
        string printedOutput = CaptureConsoleOutput(() => WebLogParserClass.PrintUniqueIds(uniqueIds));

        // Assert
        StringAssert.Contains(printedOutput, "123:2");
        StringAssert.Contains(printedOutput, "456");
    }

    private string CaptureConsoleOutput(Action action)
    {
        using (var consoleOutput = new StringWriter())
        {
            Console.SetOut(consoleOutput);
            action.Invoke();
            return consoleOutput.ToString();
        }
    }
}

public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly string _content;
    private readonly HttpStatusCode _statusCode;

    public MockHttpMessageHandler(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        _content = content;
        _statusCode = statusCode;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent(_content)
        };
        await Task.Delay(1000, cancellationToken);
        return response;
    }
}
