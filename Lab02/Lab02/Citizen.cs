using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Lab02
{
    /// <summary>
    /// Citizen class object
    /// </summary>
    public class Citizen
    {
        private Node head;
        private Node tail;
        private Node d;

        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        public Citizen()
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
        public CitizenData Get()
        { return d.Data; }

        /// <summary>
        /// Adds money to the specified citizen using his FirstName and Last name. 
        /// If the citizen does not exists, adds him to the LinkedList
        /// </summary>
        /// <param name="lastName">Last name of the citizen</param>
        /// <param name="firstName">FIrst name of the citizn</param>
        /// <param name="address">address of the citizen</param>
        /// <param name="taxSum">Tax sum to add to his TOTAL</param>
        public void AddMoney(string lastName, string firstName, string address, double taxSum)
        {
            // If Citizen exists, adds sum to his current balance
            for (Begin(); Exist(); Next())
            {
                CitizenData curr = Get();
                if (curr.LastName == lastName && curr.FirstName == firstName && curr.Address == address)
                {
                    curr.TaxSum += taxSum;
                    return;
                }
            }

            // If No citizen was found, adds the citizen to Linked List
            if (head == null)
            {
                CitizenData data = new CitizenData(lastName, firstName, address);
                head = new Node(data);
                head.Data.TaxSum = taxSum;
                tail = head;
            }
            else
            {
                CitizenData data = new CitizenData(lastName, firstName, address);
                tail.next = new Node(data);
                tail = tail.next;
                tail.Data.TaxSum = taxSum;
            }

        }

        /// <summary>
        /// Removes citiznens from linked list who payed belove average taxes
        /// </summary>
        public void RemoveUnderAverage()
        {
            if (head == null)
                return;

            Node prev = head;
            Node curr = head.next;
            double average = GetAverage();

            while(curr != null)
            {
                if(curr.Data.TaxSum < average)
                {
                    prev.next = curr.next;
                    curr = curr.next;
                }
                else
                {
                    curr = curr.next;
                    prev = prev.next;
                }
            }

            RemoveUnderAverageHead(average);
            ResetTail();
        }

        /// <summary>
        /// Checks if head/start of linked list is below average. If true removes
        /// </summary>
        /// <param name="average">Average tax sum of a citizen</param>
        private void RemoveUnderAverageHead(double average)
        {
            Node curr = head;
            while(curr.Data.TaxSum < average)
            {
                curr = curr.next;
            }
            head = curr;
        }

        /// <summary>
        /// Resets tail after removing elements
        /// </summary>
        private void ResetTail()
        {
            Node curr = head;
            if (curr == null)
            {
                tail = null;
                return;
            }

            while(curr.next != null)
            {
                curr = curr.next;
            }
            tail = curr;
        }

        /// <summary>
        /// Returns the total amount citizens payed for taxes
        /// </summary>
        /// <returns></returns>
        public double Sum()
        {
            Node curr = head;
            double sum = 0;
            while (curr != null)
            {
                sum += curr.Data.TaxSum;
                curr = curr.next;
            }
            return sum;
        }

        public double GetAverage()
        {
            Node curr = head;
            double sum = 0;
            int i = 0;
            while (curr != null)
            {
                sum += curr.Data.TaxSum;
                i++;
                curr = curr.next;
            }
            return (double)sum/i;
        }

        /// <summary>
        /// Removes citizens who did not pay taxes specified month
        /// </summary>
        /// <param name="taxCode"> Tax Code of the tax</param>
        /// <param name="month">Specified Month </param>
        /// <param name="data">CitizenTaxData to see what citizen payed what tax at the specified month</param>
        public void RemoveWhoDidNotPayTax(string taxCode, string month, CitizenTax data)
        {
            {
                if (head == null)
                    return;

                Node prev = head;
                Node curr = head.next;

                while (curr != null)
                {
                    // Checks if the citizen has payed Taxes in CitizenTaxData on specified Month
                    if (curr != null && data.CitizenPayed(taxCode, month, curr.Data.LastName, curr.Data.FirstName) == false)
                    {
                        prev.next = curr.next;
                        curr = curr.next;
                    }
                    else
                    {
                        curr = curr.next;
                        prev = prev.next;
                    }
                }

                RemoveWhoDidNotPayTaxHead(taxCode, month, data);
                ResetTail();
            }
        }

        /// <summary>
        /// Checks first/start/head element of the linked list if the tax was paid
        /// </summary>
        /// <param name="taxCode">Tax code of the specified tax</param>
        /// <param name="month">specified month to check</param>
        /// <param name="data">CitizenTaxData to check if the first element of the linked list payed for taxes</param>
        private void RemoveWhoDidNotPayTaxHead(string taxCode, string month, CitizenTax data)
        {
            Node curr = head;
            // Checks if the citizen has payed Taxes in CitizenTaxData on specified Month
            while (curr != null && data.CitizenPayed(taxCode, month, curr.Data.LastName, curr.Data.FirstName) == false)
            {
                curr = curr.next;
            }
            head = curr;
        }


        /// <summary>
        /// Sorts LinkedList A-Z using keys: address, last name, first name. Does data swap instead of pointers.
        /// </summary>
        public void Sort()
        {
            Node timer = head;
            while(timer != null)
            {
                Node curr = head;
                Node next = head.next;
                while(next != null)
                {
                    if (curr.Data.CompareTo(next.Data) > 0)
                    {
                        curr.SwapData(next);
                    }
                    curr = next;
                    next = next.next;
                }
                timer = timer.next;
            }
        }


        /// <summary>
        /// Node class to be used to save every citizen seperately 
        /// </summary>
        class Node
        {
            public CitizenData Data { get; set; }
            public Node next { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="data">CitizenData pointer</param>
            public Node(CitizenData data)
            {
                Data = data;
                next = null;
            }

            /// <summary>
            /// Swaps the DATA, keeps the pointers
            /// </summary>
            /// <param name="other">Other node to be swapped with</param>
            public void SwapData(Node other)
            {
                CitizenData temp = Data;
                Data = other.Data;
                other.Data = temp;
            }
        }
    }
}