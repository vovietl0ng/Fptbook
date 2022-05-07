using System.ComponentModel.DataAnnotations;

namespace Fptbook.ViewModel.Login
{
    public class RegisterStoreRequest
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        [Display(Name = "Store Name")]
        public string Name { get; set; }
        public string Slogan { get; set; }

    }
}
