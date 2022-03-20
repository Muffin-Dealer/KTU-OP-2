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
    public class CitizenTax
    {
        private Node head;
        private Node tail;
        private Node d;

        /// <summary>
        /// Constructor
        /// </summary>
        public CitizenTax()
        {
            head = null;
            tail = null;
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
        public void Add(CitizenTaxData data)
        {
            if (head == null)
            {
                head = new Node(data);
                tail = head;
            
            }
            else
            {
                tail.next = new Node(data);
                tail = tail.next;
            }
            
        }

        /** Address of the head of the list is assigned */
        public void Begin()
        { d = head; }
        /** Interface variable gets address of the next entry*/
        public void Next()
        { d = d.next; }
        /** Return true, if list is empty*/
        public bool Exist()
        { return d != null; }
        //-----------------------------------------------
        /** Return data according to the interface address*/
        public CitizenTaxData Get()
        { return d.Data; }

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
            while (curr != null)
            {
                if (curr.Data.LastName == lastName && curr.Data.FirstName == firstName && curr.Data.Month == month && curr.Data.TaxCode == taxCode)
                    return true; // The Person paid for the month

                curr = curr.next;
            }
            return false;
        }

        /// <summary>
        /// Node class object for CitizenTaxData
        /// </summary>
        class Node
        { 
            public Node next;
            public CitizenTaxData Data { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="data">Pointer to CitizenTaxData object</param>
            public Node(CitizenTaxData data)
            {
                Data = data;
                next = null;
            }
        }
    }
}