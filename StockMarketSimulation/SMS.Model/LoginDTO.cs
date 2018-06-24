using System;

namespace SMS.Model
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public int LoginAttempts { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedMachine { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedUser { get; set; }
        public string ModifiedMachine { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
