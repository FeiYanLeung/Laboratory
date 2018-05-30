namespace Laboratory.NetCore.Web.Models
{
    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public bool Verification()
        {
            return !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Password);
        }
    }
}
