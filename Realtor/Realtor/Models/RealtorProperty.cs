using System;
using System.Collections.Generic;
using System.Text;

namespace Realtor.Models
{
    public class RealtorProperty
    {
        public string CompanyName { get; set; }
        public string AccountName { get; set; }
        public string Avatar { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ItemType { get; set; }
        public bool Negotiable { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FirstImage { get; set; }
        public string SecondImage { get; set; }
        public string ThirdImage { get; set; }
        public string FourthImage { get; set; }
        public bool IsFavorite { get; set; }

    }
}
