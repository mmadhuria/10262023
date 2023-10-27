using JsonCleanup;
using Newtonsoft.Json.Linq;

[TestClass]
public class JsonCleaningTests
{
    [TestMethod]
    public void CleanJsonObject_RemovesNATagInString()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""name"": ""N/A"" }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        Assert.IsNull(jsonObject["name"]);
    }

    [TestMethod]
    public void CleanJsonObject_RemovesHyphenInString()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""value"": ""-"" }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        Assert.IsNull(jsonObject["value"]);
    }

    [TestMethod]
    public void CleanJsonObject_RemovesEmptyString()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""description"": """" }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        Assert.IsNull(jsonObject["description"]);
    }

    [TestMethod]
    public void CleanJsonObject_KeepsValidString()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""name"": ""Daniel"" }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        Assert.IsNotNull(jsonObject["name"]);
        Assert.AreEqual("Daniel", jsonObject["name"]?.Value<string>());
    }

    [TestMethod]
    public void CleanJsonObject_RemovesNATagInArray()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""values"": [1, 2, ""N/A"", 4] }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        var valuesArray = jsonObject["values"]?.Value<JArray>();
        CollectionAssert.AreEqual(new JArray(1, 2, 4), valuesArray);
    }

    [TestMethod]
    public void CleanJsonObject_RemovesHyphenInArray()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""values"": [1, 2, ""-"", 4] }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        var valuesArray = jsonObject["values"].Value<JArray>();
        CollectionAssert.AreEqual(new JArray(1, 2, 4), valuesArray);
    }

    [TestMethod]
    public void CleanJsonObject_RemovesEmptyStringInArray()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""values"": [1, 2, """", 4] }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        var valuesArray = jsonObject["values"]?.Value<JArray>();
        CollectionAssert.AreEqual(new JArray(1, 2, 4), valuesArray);
    }

    [TestMethod]
    public void CleanJsonObject_KeepsValidStringInArray()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""values"": [1, 2, ""Alice"", 4] }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        var valuesArray = jsonObject["values"]?.Value<JArray>();
        CollectionAssert.AreEqual(new JArray(1, 2, "Alice", 4), valuesArray);
    }

    [TestMethod]
    public void CleanJsonObject_RemovesInvalidObjectProperty()
    {
        // Arrange
        var jsonObject = JObject.Parse(@"{ ""name"": { ""first"": ""Alice"", ""middle"": ""-"" } }");

        // Act
        JsonCleaner.CleanJsonObject(jsonObject);

        // Assert
        Assert.IsNotNull(jsonObject["name"]);
        Assert.IsNull(jsonObject["name"]?["middle"]);
        Assert.IsNotNull(jsonObject["name"]?["first"]);
    }
}
