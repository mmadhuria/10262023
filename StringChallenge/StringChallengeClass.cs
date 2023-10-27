namespace StringChallenge
{
    public class StringChallengeClass
    {

        public const string NOT_POSSIBLE = "not possible";
        public const string PALINDROME = "palindrome";

        public static string StringChallenge(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 3)
                return NOT_POSSIBLE;

            if (IsPalindrome(str))
                return PALINDROME;

            for (var index1 = 0; index1 < str.Length; index1++)
            {
                var possiblePalindromeString = str.Remove(index1, 1);
                if (IsPalindrome(possiblePalindromeString))
                {
                    return str[index1].ToString();
                }
            }


            for (var index2 = 0; index2 < str.Length - 1; index2++)
            {
                for (var index3 = index2 + 1; index3 < str.Length; index3++)
                {
                    var possiblePalindromeString = str.Remove(index2, 1).Remove(index3 - 1, 1);
                    if (IsPalindrome(possiblePalindromeString))
                    {
                        return str[index2].ToString() + str[index3].ToString();
                    }
                }
            }

            return NOT_POSSIBLE;

        }

        public static bool IsPalindrome(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 3)
                return false;
            var leftPointer = 0;
            var rightPointer = str.Length - 1;

            while (leftPointer < rightPointer)
            {
                if (str[leftPointer] != str[rightPointer])
                    return false;
                leftPointer++;
                rightPointer--;
            }
            return true;
        }

        static void Main()
        {
            // keep this function call here
            Console.WriteLine(StringChallenge("abjckcba"));
        }

    }
}