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
        private int count;
        public int Count { get { return count; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public Citizen()
        {
            head = null;
            tail = null;
            count = 0;
        }
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
            for (Node curr = head; curr != null; curr = curr.next)
            {
                if (curr.LastName == lastName && curr.FirstName == firstName)
                {
                    curr.TaxSum += taxSum;
                    return;
                }
            }

            // If No citizen was found, adds the citizen to Linked List
            count++;
            if (head == null)
            {
                head = new Node(lastName, firstName, address);
                head.TaxSum = taxSum;
                tail = head;
            }
            else
            {
                tail.next = new Node(lastName, firstName, address);
                tail = tail.next;
                tail.TaxSum = taxSum;
            }

        }

        /// <summary>
        /// Removes citiznens from linked list who payed belove average taxes
        /// </summary>
        public void RemoveUnderAverage()
        {
            if (count == 0)
                return;

            Node prev = head;
            Node curr = head.next;
            double average = GetAverage();

            while(curr != null)
            {
                if(curr.TaxSum < average)
                {
                    prev.next = curr.next;
                    curr = curr.next;
                    count--;
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
            while(curr.TaxSum < average)
            {
                curr = curr.next;
                count--;
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
                count = 0;
                return;
            }

            while(curr.next != null)
            {
                curr = curr.next;
            }
            tail = curr;
        }

        /// <summary>
        /// Removes citizens who did not pay taxes specified month
        /// </summary>
        /// <param name="taxCode"> Tax Code of the tax</param>
        /// <param name="month">Specified Month </param>
        /// <param name="data">CitizenTaxData to see what citizen payed what tax at the specified month</param>
        public void RemoveWhoDidNotPayTax(string taxCode, string month, CitizenTaxData data)
        {
            {
                if (count == 0)
                    return;

                Node prev = head;
                Node curr = head.next;

                while (curr != null)
                {
                    // Checks if the citizen has payed Taxes in CitizenTaxData on specified Month
                    if (curr != null && data.CitizenPayed(taxCode, month, curr.LastName, curr.FirstName) == false)
                    {
                        prev.next = curr.next;
                        curr = curr.next;
                        count--;
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
        private void RemoveWhoDidNotPayTaxHead(string taxCode, string month, CitizenTaxData data)
        {
            Node curr = head;
            // Checks if the citizen has payed Taxes in CitizenTaxData on specified Month
            while (curr != null && data.CitizenPayed(taxCode, month, curr.LastName, curr.FirstName) == false)
            {
                curr = curr.next;
                count--;
            }
            head = curr;
        }

            public double GetAverage()
        {  
            return count > 0 ? (double)Sum() / count : 0;
        }

        /// <summary>
        /// Sorts LinkedList A-Z using keys: address, last name, first name. Does data swap instead of pointers.
        /// </summary>
        public void Sort()
        {
            if (count > 1)
            {
                for (int i = 0; i < count; i++)
                {
                    Node curr = head;
                    Node next = head.next;
                    for (int j = 0; j < count - 1 - i; j++)
                    {
                        if (curr.CompareTo(next) > 0)
                        {
                            curr.SwapData(next);
                        }
                        curr = next;
                        next = next.next;
                    }
                }
            }
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
                sum += curr.TaxSum;
                curr = curr.next;
            }
            return sum;
        }

        /// <summary>
        /// Returns citizen in string format
        /// </summary>
        /// <param name="index"> specified citizen</param>
        /// <returns>string format of the citizen for .txt output</returns>
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
        /// Returns cictizen in TableRow format for the specified citizen
        /// </summary>
        /// <param name="index">Index of the citizen</param>
        /// <returns>TableRow format of the specified citizen</returns>
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
                    row.Cells.Add(TaskUtils.CreateCell(curr.TaxSum.ToString()));
                    return row;
                }
                i++;
            }
            return row;
        }

        /// <summary>
        /// Node class to be used to save every citizen seperately 
        /// </summary>
        internal class Node
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }

            public double TaxSum { get; set; }

            public Node next;
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="lastName">Last name of the citizen</param>
            /// <param name="firstName">First Name of the citizen</param>
            /// <param name="address">Address of the citizen</param>
            public Node(string lastName, string firstName, string address)
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
            /// Swaps the DATA, keeps the pointers
            /// </summary>
            /// <param name="other">Other node to be swapped with</param>
            public void SwapData(Node other)
            {
                string lastName = LastName;
                string firstName = FirstName;
                string address = Address;
                double taxSum = TaxSum;
                LastName = other.LastName;
                FirstName = other.FirstName;
                Address = other.Address;
                TaxSum = other.TaxSum;
                other.LastName = lastName;
                other.FirstName = firstName;
                other.Address = address;
                other.TaxSum = taxSum;
            }

            /// <summary>
            /// Compares to other Node of citizen type
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public int CompareTo(Node other)
            {
                int comparison = other.Address.CompareTo(Address);
                if (comparison == 0 )
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
}