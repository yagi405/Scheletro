using System;
using System.Collections.Generic;

namespace Scheletro.ExcelHelper.NETStandard2_0.Tests.TestModels
{
    public class Customer
    {
        public int CustNo { get; set; }
        public string Company { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public double? TaxRate { get; set; }
        public string Contact { get; set; }
        public DateTime? LastInvoiceDate { get; set; }
        public List<Order> Orders { get; set; }
    }
}