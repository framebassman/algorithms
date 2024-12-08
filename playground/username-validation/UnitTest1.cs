namespace username_validation;

public class Username
{
    public static bool Validate(string username)
    {
        if (username.Length < 4) {
            return false;
        }

        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "0123456789";
        char underscore = '_';

        username = username.ToLower();

        bool isStartedWithLetter = false;
        foreach (char symbol in alphabet) {
            if (username[0] == symbol) {
                isStartedWithLetter = true;
                break;
            }
        }
        if (!isStartedWithLetter) {
            return false;
        }

        if (username[username.Length - 1] == underscore) {
            return false;
        }

        for (int i = 0; i < alphabet.Length; i++) {
            username = username.Replace(alphabet.Substring(i, 1), string.Empty);
        }

        for (int i = 0; i < numbers.Length; i++) {
            username = username.Replace(numbers.Substring(i, 1), string.Empty);
        }

        foreach (char symbol in username) {
            if (symbol == underscore) {
                return username.Length == 1;
            }
        }

        return username.Length == 0;
    }
}

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Xunit.Assert.True(Username.Validate("Mike_Standish"));
        Xunit.Assert.False(Username.Validate("Mike Standish"));
        Xunit.Assert.False(Username.Validate("abc"));
        Xunit.Assert.False(Username.Validate("1bc"));
        Xunit.Assert.False(Username.Validate("abc_"));
        Xunit.Assert.False(Username.Validate("ab__cd"));
    }
}
