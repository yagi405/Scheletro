using System;
using System.Collections.Generic;
using Scheletro.ExcelHelper.Tests.TestModels;
using Xunit;

namespace Scheletro.ExcelHelper.Tests
{
    /// <summary>
    /// <see cref="ExcelReportCreator"/> のテストクラスです。
    /// </summary>
    public class ExcelReportCreatorTests
    {
        /// <summary>
        /// Excel レポートを作成することをテストします。
        /// </summary>
        [Fact]
        public void CreateReport_ShouldCreateReport()
        {
            var cust = new Customer
            {
                Company = "Sample Co., Ltd.",
                Addr1 = "1-1-2, Osiage",
                Addr2 = "#905",
                City = "Sumida-ku",
                State = "Tokyo",
                Country = "Japan",
                Zip = "131-0045",
                Phone = "03-1234-5678",
                Fax = "03-9876-5432",
                Contact = "Chris Tomas",
                Orders = new List<Order>
                {
                    new()
                    {
                        OrderNo = 1,
                        SaleDate = DateTime.Today,
                        ShipDate = DateTime.Today,
                        ShipToAddr1 = "foo",
                        ShipToAddr2 = "bar",
                        PaymentMethod = "Visa",
                        ItemsTotal = 480700,
                        TaxRate = 0,
                        AmountPaid = 306500
                    },
                    new()
                    {
                        OrderNo = 2,
                        SaleDate = DateTime.Today,
                        ShipDate = DateTime.Today,
                        ShipToAddr1 = "foo",
                        ShipToAddr2 = "bar",
                        PaymentMethod = "Visa",
                        ItemsTotal = 480700,
                        TaxRate = 0,
                        AmountPaid = 306500
                    },
                    new()
                    {
                        OrderNo = 3,
                        SaleDate = DateTime.Today,
                        ShipDate = DateTime.Today,
                        ShipToAddr1 = "foo",
                        ShipToAddr2 = "bar",
                        PaymentMethod = "Visa",
                        ItemsTotal = 480700,
                        TaxRate = 0,
                        AmountPaid = 306500
                    }
                }
            };

            using var creator = new ExcelReportCreator();
            creator.CreateReportFromTemplate(cust, @"Templates/tLists1_sort.xlsx", "report.xlsx");
        }
    }
}
