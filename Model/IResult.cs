namespace PersonalBook.API.Model
{
    public class ErrorMessage
    {
        public const string UsernameExist = "Username is not available";
        public const string ShortPassword = "Password too short. Minimum length should be 6";
        public const string PhoneNumberUsed = "This phone number is already used.";
        public const string EmailAddressUsed = "This email address is already used.";
        public const string UserNotExist = "User not exist.";
        public const string InvalidPassword = "Invalid password";
    }

    public class SignUpResult
    {
        public int Successed { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public class LoginResult
    {
        public int Successed { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }

    public class LoginStatus
    {
        public int IsLoggedIn { get; set; }
        public string? Username { get; set; }
        public string? Role { get; set; }
    }

    public class Convertion
    {
        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }
    }
}
