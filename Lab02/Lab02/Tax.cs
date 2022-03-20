using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Lab02
{
    public class Tax
    {
        private Node head;
        private Node tail;
        private Node d;
        public Tax()
        {
            head = null;
            tail = null;
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
        public TaxData Get()
        { return d.Data; }

        /// <summary>
        /// Returns the price of the tax of a single use
        /// </summary>
        /// <param name="taxCode">Code to identify the type of tax</param>
        /// <returns>Double, price of a single use tax item</returns>
        public double GetPrice(string taxCode)
        {
            Node curr = head;
            while (curr != null)
            {
                if (curr.Data.TaxCode == taxCode)
                    return curr.Data.Price;
                curr = curr.next;
            }
            return 0;
        }

        /// <summary>
        /// Adds Node to the tail of the LinkedList
        /// </summary>
        /// <param name="taxCode">Code of the tax</param>
        /// <param name="name"> name of the company</param>
        /// <param name="price">price of a single use</param>
        public void Add(TaxData data)
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
        
        /// <summary>
        /// Tax Node
        /// </summary>
        class Node
        {
            public Node next;
            public TaxData Data { get; set; }
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="data">TaxData pointer</param>
            public Node(TaxData data)
            {
                Data = data;
                next = null;
            }

        }
    }
}