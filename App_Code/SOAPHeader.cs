namespace EZGoal
{
    public class SOAPHeader : System.Web.Services.Protocols.SoapHeader
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public SOAPHeader() { }

        public SOAPHeader(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public bool IsAuthenticated()
        {
            return (this.Username == "admin" && this.Password == "admin");
        }
    }
}
