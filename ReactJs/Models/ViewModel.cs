using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReactJs.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }

    public class RestaurantInfo
    {

        public string Name { get; set; }
        public Address Location { get; set; }
        public string Summary { get; set; }
        public int Rating { get; set; }
    }

}