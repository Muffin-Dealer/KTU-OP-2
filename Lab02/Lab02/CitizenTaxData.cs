using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Lab02
{
    /// <summary>
    /// Citizen class object meant to store name and how much the individual payed for taxe
    /// </summary>
    public class CitizenTaxData
    {
        private Node head;
        private Node tail;
        private int count;
        public int Count { get { return count; } }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public CitizenTaxData()
        {
            head = null;
            tail = null;
            count = 0;
        }
        /// <summary>
        /// Adds element to Linked List
        /// </summary>
        /// <param name="lastName">Last Name</param>
        /// <param name="firstName">First Name</param>
        /// <param name="address">Address</param>
        /// <param name="month">Month</param>
        /// <param name="taxCode">Tax Code</param>
        /// <param name="taxAmount">Tax Amount</param>
        public void Add(string lastName, string firstName, string address, string month, string taxCode, int taxAmount)
        {
            count++;
            if (head == null)
            {
                head = new Node(lastName, firstName, address, month, taxCode, taxAmount);
                tail = head;
            
            }
            else
            {
                tail.next = new Node(lastName, firstName, address, month, taxCode, taxAmount);
                tail = tail.next;
            }
            
        }
        /// <summary>
        /// Creates Citizen class object using Tax object
        /// </summary>
        /// <param name="TaxInfo">Tax class object</param>
        /// <returns>Citizen class object</returns>
        public Citizen CreateCitizenData(Tax TaxInfo)
        {
            Citizen citizens = new Citizen();
            for (Node curr = head; curr != null; curr = curr.next)
            {
                citizens.AddMoney(curr.LastName, curr.FirstName, curr.Address, (double)TaxInfo.GetPrice(curr.TaxCode) * curr.TaxAmount);
            }
            return citizens;
        }

        /// <summary>
        /// Returns string format using index
        /// </summary>
        /// <param name="index">index of CitizenTaxData Node element</param>
        /// <returns>string format of CitizenTaxData</returns>
        public string ToString(int index)
        {
            int i = 0;
            for (Node curr = head; curr != null; curr = curr.next)
            {
                if (i == index)
                    return curr.ToString();
                i++;
            }
            return "";
        }

        /// <summary>
        /// Returns CitizenTaxData specified element in TableRow format
        /// </summary>
        /// <param name="index">index of the Node element</param>
        /// <returns>TableRow of the specified Node</returns>
        public TableRow GetRow(int index)
        {
            int i = 0;
            TableRow row = new TableRow();
            for (Node curr = head; curr != null; curr = curr.next)
            {
                if (i == index)
                {
                    row.Cells.Add(TaskUtils.CreateCell(curr.LastName));
                    row.Cells.Add(TaskUtils.CreateCell(curr.FirstName));
                    row.Cells.Add(TaskUtils.CreateCell(curr.Address));
                    row.Cells.Add(TaskUtils.CreateCell(curr.Month));
                    row.Cells.Add(TaskUtils.CreateCell(curr.TaxCode));
                    row.Cells.Add(TaskUtils.CreateCell(curr.TaxAmount.ToString()));
                    return row;
                }
                i++;
            }
            return row;
        }

        /// <summary>
        /// Checks of the specified citizen has payed
        /// </summary>
        /// <param name="taxCode">Tax Code of the Tax Company</param>
        /// <param name="month">Month</param>
        /// <param name="lastName">Last name of the citizen</param>
        /// <param name="firstName"> First Name of the citizen</param>
        /// <returns>true if citizen has payed for specified tax on specified month, false if the citizen did not</returns>
        public bool CitizenPayed(string taxCode, string month, string lastName, string firstName)
        {
            Node curr = head;
            while(curr != null)
            {
                if (curr.LastName == lastName && curr.FirstName == firstName && curr.Month == month && curr.TaxCode == taxCode)
                    return true; // The Person paid for the month

                curr = curr.next;
            }
            return false;
        }

        /// <summary>
        /// Node class object for CitizenTaxData
        /// </summary>
        internal class Node
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string TaxCode { get; set; }
            public int TaxAmount { get; set; }
            public string Month { get; set; }

            public Node next;
            /// <summary>
            /// Node of CitizenTaxData LinkedList
            /// </summary>
            /// <param name="lastName">Last Name</param>
            /// <param name="firstName">First Name</param>
            /// <param name="address">Address of the specified Citizen</param>
            /// <param name="month">Time of Month when the specified tax was payed</param>
            /// <param name="taxCode">TaxCode of the tax</param>
            /// <param name="taxAmount">how much tax units did the citizen use</param>
            public Node(string lastName, string firstName, string address, string month, string taxCode, int taxAmount)
            {
                LastName = lastName;
                FirstName = firstName;
                Address = address;
                Month = month;
                TaxCode = taxCode;
                TaxAmount = taxAmount;
            }

            /// <summary>
            /// ToString format of the node
            /// </summary>
            /// <returns>string format of CitizenTaxData Node</returns>
            public override string ToString()
            {
                return $"{LastName,-20} {FirstName,-20}|{Address, -20}|{Month, -15}|{TaxCode, -20}|{TaxAmount,10}|";
            }
        }
    }
}