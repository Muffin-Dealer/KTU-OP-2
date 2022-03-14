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
        private int count;
        public int Count { get { return count; } }

        public Tax()
        {
            head = null;
            tail = null;
            count = 0;
        }
        /// <summary>
        /// Adds Node to the tail of the LinkedList
        /// </summary>
        /// <param name="taxCode">Code of the tax</param>
        /// <param name="name"> name of the company</param>
        /// <param name="price">price of a single use</param>
        public void Add(string taxCode, string name, double price)
        {
            if (head == null)
            {
                head = new Node(taxCode, name, price);
                tail = head;
                count++;
            }
            else
            {
                tail.next = new Node(taxCode, name, price);
                tail = tail.next;
                count++;

            }

        }
                
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
                if (curr.TaxCode == taxCode)
                    return curr.Price;
                curr = curr.next;
            }
            return 0;
        }

        /// <summary>
        /// Returns string format of Tax Node 
        /// </summary>
        /// <param name="index">index of the node in Tax Linked List</param>
        /// <returns>string format of the specified Node</returns>
        public string ToString(int index)
        {
            int i = 0;
            for(Node curr = head; curr != null; curr = curr.next)
            {
                if (i == index)
                    return curr.ToString();
                i++;
            }
            return "";
        }

        /// <summary>
        /// Returns Tax Node in TableRow format
        /// </summary>
        /// <param name="index">Index of the  Linked List node</param>
        /// <returns>Returns Node with specified index in TableRow format</returns>
        public TableRow GetRow(int index)
        {
            int i = 0;
            TableRow row = new TableRow();
            for (Node curr = head; curr != null; curr = curr.next)
            {
                if (i == index)
                {
                    row.Cells.Add(TaskUtils.CreateCell(curr.TaxCode));
                    row.Cells.Add(TaskUtils.CreateCell(curr.TaxName));
                    row.Cells.Add(TaskUtils.CreateCell(curr.Price.ToString()));
                    return row;
                }
                i++;
            }
            return row;
        }

        /// <summary>
        /// Tax Node
        /// </summary>
        internal class Node
        {
            public string TaxCode { get; set; }
            public string TaxName { get; set; }
            public double Price { get; set; }
            public Node next;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="taxCode">Code of the tax</param>
            /// <param name="name">name of the compnay</param>
            /// <param name="price">price of a single use</param>
            public Node (string taxCode, string name, double price)
            {
                TaxCode = taxCode;
                TaxName = name;
                Price = price;
                next = null;    
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
}