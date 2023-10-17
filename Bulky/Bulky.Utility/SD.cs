using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company"; //They will have 30 days to make the payment after the order has been placed, A company user can be registered  by an admin user
        public const string Role_Admin = "Admin"; //perform all CRUD operations on product and other content management
        public const string Role_Employee = "Employee"; //Role to modify shipping of a product
    }
}
