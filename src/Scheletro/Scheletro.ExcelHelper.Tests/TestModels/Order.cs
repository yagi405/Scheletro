using System;

namespace Scheletro.ExcelHelper.Tests.TestModels
{
    public class Order
    {
        public double OrderNo { get; set; } 
        public double CustNo { get; set; } 
        public DateTime? SaleDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public int EmpNo { get; set; }
        public string ShipToContact { get; set; }
        public string ShipToAddr1 { get; set; }
        public string ShipToAddr2 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZip { get; set; }
        public string ShipToCountry { get; set; }
        public string ShipToPhone { get; set; }
        public string ShipVia { get; set; }
        public string Po { get; set; }
        public string Terms { get; set; }
        public string PaymentMethod { get; set; }
        public double? ItemsTotal { get; set; }
        public double? TaxRate { get; set; }
        public double? Freight { get; set; }
        public double? AmountPaid { get; set; }
    }
}
