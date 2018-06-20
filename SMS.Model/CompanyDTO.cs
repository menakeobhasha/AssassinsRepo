using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class CompanyDTO
    {
        
        public int CompanyId { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public string Telephone { get; set; }
        
        public string Email { get; set; }
        
        public string Description { get; set; }
        
        public int Type { get; set; }
        
        public int NumberOfShares { get; set; }
        
        public decimal SharePrice { get; set; }
        
        public int Status { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string CreatedUser { get; set; }
        
        public string CreatedMachine { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
        
        public string ModifiedUser { get; set; }
        
        public string ModifiedMachine { get; set; }
        
        public DateTime ModifiedDateTime { get; set; }


    }
}
