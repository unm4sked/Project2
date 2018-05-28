namespace Project2.Models
{
    public class UserModel
    {
        public string Login {get; set;}
        public string Password {get; set;}
        public string Reg {get; set;}
        public string Description {get; set;}
        public int MinLength {get; set;}
        public int MaxLength {get; set;}
        public bool chMinLength {get; set;}
        public bool chMaxLength {get; set;}
        public int MinUppercase {get; set;}
        public bool chUppercase {get; set;}
        public int MinLowercase {get; set;}
        public bool chLowercase {get; set;}
        public int MinSpecialSigns {get; set;}
        public bool chSpecialSigns {get; set;}
        public bool chDigits {get; set;}
        public int MinDigits {get; set;}
    }
}