using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab02
{
    /// <summary>
    /// CitizenData class object to be used by class Citizen
    /// </summary>
    public class CitizenData
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public double TaxSum { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lastName">Last name of the citizen</param>
        /// <param name="firstName">First Name of the citizen</param>
        /// <param name="address">Address of the citizen</param>
        public CitizenData(string lastName, string firstName, string address)
        {
            LastName = lastName;
            FirstName = firstName;
            Address = address;
            TaxSum = 0;
        }


        /// <summary>
        /// To String override
        /// </summary>
        /// <returns>stringg format of the citizen</returns>
        public override string ToString()
        {
            return $"{LastName,-20} {FirstName,-20}|{Address,-20}|{TaxSum,10:f}|";
        }

        /// <summary>
        /// Compares to other Node of citizen type
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(CitizenData other)
        {
            int comparison = other.Address.CompareTo(Address);
            if (comparison == 0)
            {
                comparison = other.LastName.CompareTo(LastName);
                if (comparison == 0)
                {
                    comparison = other.FirstName.CompareTo(FirstName);
                }
            }

            return comparison;
        }
    }
}