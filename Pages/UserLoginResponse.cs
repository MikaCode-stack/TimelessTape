namespace TimelessTapes.Pages
{
    public class UserLoginResponse
    {
        public int userId { get; set; }
        public string accessType { get; set; } // "Admin" or "Customer"
    }
}
