namespace DSMServerMani.Models
{
    public class LoginRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? App_Version { get; set; }
        public string? Device_Name { get; set; }
        public string? Device_ID { get; set; }
        public string? Device_Version { get; set; }
        public string? App_Token { get; set; }
        public string? LoginType { get; set; }

    }
}
