using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
#nullable disable

namespace DapperDemo.Models
{
    // install-package Dapper.Contrib

    [Table("Customer")]
    public partial class Customer
    {
        [ExplicitKey]
        public int CustomerId { get; set; } = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
