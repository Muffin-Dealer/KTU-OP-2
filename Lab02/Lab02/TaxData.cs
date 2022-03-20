using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab02
{
    /// <summary>
    /// TaxData object to be inherited by Tax object
    /// </summary>
    public class TaxData
    {
        public string TaxCode { get; set; }
        public string TaxName { get; set; }
        public double Price { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="taxCode"></param>
        /// <param name="taxName"></param>
        /// <param name="price"></param>
        public TaxData(string taxCode, string taxName, double price)
        {
            TaxCode = taxCode;
            TaxName = taxName;
            Price = price;
        }

        /// <summary>
        /// Returns Node in string format
        /// </summary>
        /// <returns>Node in string format</returns>
        public override string ToString()
        {
            return $"{TaxCode,-20}|{TaxName,-20}|{Price,10:f}|";
        }
    }
}