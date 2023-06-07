using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Entities
{
    public class Address
    {
        [Key]
        public int AddressId { get; private set; }
        
        public string City { get; private set; }

        public string Street { get; private set; }

        public string PostalCode { get; private set; }
        
        private Address(string city, string street, string postalCode)
        {
            City= city;
            Street= street;
            PostalCode= postalCode;
        }

        /// <summary>
        /// Create new instance of Address
        /// </summary>
        /// <param name="name"></param>
        public static Address New(string city, string street, string postalCode) => new Address(city, street, postalCode);
        
       
    }
}
