using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab02
{
    public class CitizenTaxData
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public int TaxAmount { get; set; }
        public string Month { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lastName">last name of citizen</param>
        /// <param name="firstName">first name of citizen</param>
        /// <param name="address">address of the citizen</param>
        /// <param name="month">the month the tax was paid</param>
        /// <param name="taxCode">tax code</param>
        /// <param name="taxAmount">tax amount</param>
        public CitizenTaxData(string lastName, string firstName, string address, string month, string taxCode, int taxAmount)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            TaxCode = taxCode;
            TaxAmount = taxAmount;
            Month = month;
        }

        public override string ToString()
        {
            return $"{LastName,-20} {FirstName,-20}|{Address,-20}|{Month,-15}|{TaxCode,-20}|{TaxAmount,10}|";
        }
    }
}

