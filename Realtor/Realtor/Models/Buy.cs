using System;
using System.Collections.Generic;
using System.Text;

namespace Realtor.Models
{
    public class Buy
    {
        public string AccountName { get; set; }
        public string AccountProfilePicture { get; set; }
        public string ItemName { get; set; }
        public string ImagePicture { get; set; } // to be changed later
        public decimal ItemPrice { get; set; }
        public List<string> ItemImages { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string Location { get; set; }
        public bool Negotiable { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
