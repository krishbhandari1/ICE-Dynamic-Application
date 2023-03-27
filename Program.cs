using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using static Dynamic_Application_Krish.Globals;

namespace Dynamic_Application_Krish
{
    class Program
    {
        private static string userName, password;
        private static string accessToken;
        private static string firstName, lastName, streetAddress, city, state, zip;
        private static string docId;
        private static string loanId;
        static void Main(string[] args)
        {
            CompleteAuthentication();
            CreateLoan();
            AddDocument();

        }

        private static void CreateLoan()
        {
            Console.WriteLine("First Name:");
            firstName = Console.ReadLine();
            Console.WriteLine("Last Name:");
            lastName = Console.ReadLine();
            Console.WriteLine("Street Address:");
            streetAddress = Console.ReadLine();
            Console.WriteLine("City:");
            city = Console.ReadLine();
            Console.WriteLine("State:");
            state = Console.ReadLine();
            Console.WriteLine("Zip:");
            zip = Console.ReadLine();
            var task2 = CreateLoanTemplate(firstName, lastName, streetAddress, city, state, zip);
            Task.WaitAll(task2);
            loanId = GetLoanId();
            Console.WriteLine("\nThe loan ID (GUID) is: " + loanId);
            Console.ReadLine();
        }
        private static void CompleteAuthentication()
        {
            Console.WriteLine("User Name:");
            userName = Console.ReadLine();
            Console.WriteLine("Password:");
            password = Console.ReadLine();

            var task1 = SetAccessToken();

            Task.WaitAll(task1);
            accessToken = GetAccessToken();
            Console.WriteLine("The access token is: " + accessToken);
            Console.ReadLine();
        }
        private static void AddDocument()
        {
            var task3 = AddBankStatement();
            Task.WaitAll(task3);
            docId = GetDocId();
            Console.WriteLine("\nThe Document ID (GUID) is: " + docId);
            Console.ReadLine();
        }

    }
}



