﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Lab02
{
    /// <summary>
    /// Static InOutUtils helper class for Input/Output with files
    /// </summary>
    public static class InOutUtils
    {
        /// <summary>
        /// Reads Tax Data from txt to Tax class object+
        /// </summary>
        /// <param name="fileLoc">Location of the data in .txt format</param>
        /// <returns>Tax class object</returns>
        public static Tax ReadTaxData(string fileLoc)
        {
            Tax TaxData = new Tax();
            string[] lines = File.ReadAllLines(fileLoc);
            foreach (string line in lines)
            {
                string[] elements = line.Split(';');
                TaxData.Add(elements[0], elements[1], double.Parse(elements[2]));
            }
            return TaxData;
        }

        /// <summary>
        /// Creates CitizenTaxData from .txt file
        /// </summary>
        /// <param name="fileLoc">Location of .txt file</param>
        /// <returns>CitizenTaxData class object</returns>
        public static CitizenTaxData ReadCitizenTaxData(string fileLoc)
        {
            CitizenTaxData data = new CitizenTaxData();
            string[] lines = File.ReadAllLines(fileLoc);
            foreach (string line in lines)
            {
                string[] elements = line.Split(';');
                data.Add(elements[0], elements[1], elements[2], elements[3], elements[4], int.Parse(elements[5]));
            }
            return data;
        }

        /// <summary>
        /// Appends a header to a file
        /// </summary>
        /// <param name="fileLoc">Name/location of the file</param>
        /// <param name="header">text to be appended</param>
        public static void WriteHeader(string fileLoc, string header)
        {
            using (StreamWriter writer = new StreamWriter(fileLoc, append: true))
            {
                writer.WriteLine(header);
                writer.WriteLine();               
            }
        }

        /// <summary>
        /// Creates a new or wipes a file
        /// </summary>
        /// <param name="fileLoc">Location of the file</param>
        public static void CreateFile(string fileLoc)
        {
            using (FileStream fs = new FileStream(fileLoc, FileMode.Create))
                new StreamWriter(fs, encoding: System.Text.Encoding.UTF8).Close();
        }

        /// <summary>
        /// Appends CitizenTaxData to a file
        /// </summary>
        /// <param name="fileLoc">Location/name of the file</param>
        /// <param name="data">data to append to the .txt file</param>
        /// <param name="header">Header text of the data file</param>
        public static void WriteCitizenTaxData(string fileLoc, CitizenTaxData data, string header)
        {
            using (StreamWriter writer = new StreamWriter(fileLoc, append:true))
            {
                writer.WriteLine(header);
                writer.WriteLine();
                writer.WriteLine($"{"LastName",-20} {"FirstName",-20}|{"Address",-20}|{"Month",-15}|{"TaxCode",-20}|{"TaxAmount",10}|");
                for (int i = 0; i < data.Count; i++)
                {
                    writer.WriteLine(data.ToString(i));
                }
                writer.WriteLine();
            }
        }

        /// <summary>
        /// appends Citizen class object data to text file
        /// </summary>
        /// <param name="fileLoc">location/name of the file</param>
        /// <param name="data">data to append to the file</param>
        /// <param name="header">Header of the file</param>
        public static void WriteCitizenData(string fileLoc, Citizen data, string header)
        {
            using (StreamWriter writer = new StreamWriter(fileLoc, append: true))
            {
                writer.WriteLine(header);
                writer.WriteLine();
                writer.WriteLine($"{"LastName",-20} {"FirstName",-20}|{"Address",-20}|{"TaxSum",-10}|");
                for (int i = 0; i < data.Count; i++)
                {
                    writer.WriteLine(data.ToString(i));
                }
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Appends Tax data to a .txt file
        /// </summary>
        /// <param name="fileLoc">Location/name of the file</param>
        /// <param name="data">data to append to the .txt file</param>
        /// <param name="header">header to be added to the file</param>
        public static void WriteTaxData(string fileLoc, Tax data, string header)
        {
            using (StreamWriter writer = new StreamWriter(fileLoc, append: true))
            {
                writer.WriteLine(header);
                writer.WriteLine();
                writer.WriteLine($"{"TaxCode",-20}|{"TaxName",-20}|{"Price",10:2f}|");
                for (int i = 0; i < data.Count; i++)
                {
                    writer.WriteLine(data.ToString(i));
                }
                writer.WriteLine();
            }
        }
    }
}