using System;
using System.Collections.Generic;
using System.Text;

namespace LagosArch.Models
{
    public class Payment
    {
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public decimal Amount { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; } = "NGN";
        public string CustomDescription { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerLastname { get; set; }
        public string CustomLogo { get; set; } = "laap.png";
        public string CustomTitle { get; set; }
        public string PayButtonText { get; set; } = "Donate";
    }
}
