using StringChallenge;

[TestClass]
public class StringChallengeClassTests
{
    [TestMethod]
    public void StringChallenge_NotPossible_EmptyString()
    {
        string result = StringChallengeClass.StringChallenge("");
        Assert.AreEqual(StringChallengeClass.NOT_POSSIBLE, result);
    }

    [TestMethod]
    public void StringChallenge_NotPossible_ShortString()
    {
        string result = StringChallengeClass.StringChallenge("a");
        Assert.AreEqual(StringChallengeClass.NOT_POSSIBLE, result);
    }

    [TestMethod]
    public void StringChallenge_Palindrome()
    {
        string result = StringChallengeClass.StringChallenge("racecar");
        Assert.AreEqual(StringChallengeClass.PALINDROME, result);
    }

    [TestMethod]
    public void StringChallenge_SingleCharacterPalindrome()
    {
        string result = StringChallengeClass.StringChallenge("abca");
        Assert.AreEqual("b", result);
    }

    [TestMethod]
    public void StringChallenge_DoubleCharacterPalindrome()
    {
        string result = StringChallengeClass.StringChallenge("abcda");
        Assert.AreEqual("bc", result);
    }

    [TestMethod]
    public void StringChallenge_LongStringPalindrome()
    {
        string result = StringChallengeClass.StringChallenge("abcdeedcba");
        Assert.AreEqual(StringChallengeClass.PALINDROME, result);
    }

    [TestMethod]
    public void StringChallenge_NoPossiblePalindrome()
    {
        string result = StringChallengeClass.StringChallenge("abcdefg");
        Assert.AreEqual(StringChallengeClass.NOT_POSSIBLE, result);
    }

    [TestMethod]
    public void IsPalindrome_Palindrome()
    {
        bool result = StringChallengeClass.IsPalindrome("racecar");
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsPalindrome_NotPalindrome()
    {
        bool result = StringChallengeClass.IsPalindrome("hello");
        Assert.IsFalse(result);
    }
    [TestMethod]
    public void StringChallenge_InputIsPalindrome_ReturnsPalindrome()
    {
        // Arrange
        string input = "racecar";

        // Act
        string result = StringChallengeClass.StringChallenge(input);

        // Assert
        Assert.AreEqual("palindrome", result);
    }

    [TestMethod]
    public void StringChallenge_CanRemoveOneCharacterToFormPalindrome_ReturnsRemovedCharacters()
    {
        // Arrange
        string input = "abjchba";

        // Act
        string result = StringChallengeClass.StringChallenge(input);

        // Assert
        Assert.AreEqual("jc", result);
    }

    [TestMethod]
    public void StringChallenge_CanRemoveTwoCharactersToFormPalindrome_ReturnsRemovedCharacters()
    {
        // Arrange
        string input = "abjckcba";

        // Act
        string result = StringChallengeClass.StringChallenge(input);

        // Assert
        Assert.AreEqual("j", result);
    }

    [TestMethod]
    public void StringChallenge_CannotFormPalindrome_ReturnsNotPossible()
    {
        // Arrange
        string input = "xyz";

        // Act
        string result = StringChallengeClass.StringChallenge(input);

        // Assert
        Assert.AreEqual("not possible", result);
    }

    [TestMethod]
    public void StringChallenge_EmptyInput_ReturnsNotPossible()
    {
        // Arrange
        string input = "";

        // Act
        string result = StringChallengeClass.StringChallenge(input);

        // Assert
        Assert.AreEqual("not possible", result);
    }

    [TestMethod]
    public void StringChallenge_InputWithLengthLessThan3_ReturnsNotPossible()
    {
        // Arrange
        string input = "a";

        // Act
        string result = StringChallengeClass.StringChallenge(input);

        // Assert
        Assert.AreEqual("not possible", result);
    }
}
