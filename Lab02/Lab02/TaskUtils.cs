using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Lab02
{
    /// <summary>
    /// TaskUtils static class for helper functions
    /// </summary>
    public static class TaskUtils
    {

        /// <summary>
        /// Creates Citizen class object using Tax object
        /// </summary>
        /// <param name="TaxList">Tax class object</param>
        /// <param name="citizenTaxList">CitizenTax object</param> 
        /// <returns>Citizen class object</returns>
        public static Citizen CreateCitizenData(Tax TaxList, CitizenTax citizenTaxList)
        {
            Citizen citizens = new Citizen();
            for (citizenTaxList.Begin(); citizenTaxList.Exist(); citizenTaxList.Next())
            {
                CitizenTaxData citizenTaxData = citizenTaxList.Get();
                for (TaxList.Begin(); TaxList.Exist(); TaxList.Next())
                {
                    TaxData taxData = TaxList.Get();
                    if(citizenTaxData.TaxCode == taxData.TaxCode)
                    {
                        citizens.AddMoney(citizenTaxData.LastName, citizenTaxData.FirstName, citizenTaxData.Address, (double)taxData.Price * citizenTaxData.TaxAmount);

                    }
                }
            }

            return citizens;
        }


    }
}