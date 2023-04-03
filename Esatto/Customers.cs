using System;
using System.Text.RegularExpressions;

namespace Esatto
{
    class Customers
    {
        private List<Dictionary<string, string>> _customers = new List<Dictionary<string, string>>();

        // This function shows options which you could choose
        public void Start()
        {
            while (true)
            {
                ShowOptions();
                int number = Convert.ToInt16(Console.ReadLine());
                switch (number)
                {
                    case 1: AddClient(); break;
                    case 2: EditClient(); break;
                    case 3: DeleteClient(); break;
                    case 4: ListClient(); break;
                    default: break;
                }
                Console.WriteLine("Do you want to continue?[y/n]");
                string answer = Console.ReadLine();
                if (answer == "n")
                {
                    break;
                }
            }
        }
        private void ShowOptions() 
        {
            Console.WriteLine("What do you want to do? (input must be int)");
            Console.WriteLine("1. Add user");
            Console.WriteLine("2. Edit user");
            Console.WriteLine("3. Delete user");
            Console.WriteLine("4. Display user");
        }

        private void AddClient()
        {
            Dictionary<string, string> client = new Dictionary<string, string>();
            
            Console.WriteLine("Add user");
            string firstName = EnterString("First name");
            string lastName = EnterString("Last name");
            string VATID = CheckVATID("VAT ID");
            string address = EnterString("Address");
            DateTime date = DateTime.Now;

            client.Add("First name", firstName);
            client.Add("Last name", lastName);
            client.Add("VAT ID", VATID);
            client.Add("Address", address);
            client.Add("Date", date.ToString());
            
            _customers.Add(client);
            
        }

        private void EditClient()
        {
            Console.WriteLine("Edit user");
            ListClient();

            Console.WriteLine("Which client do you want to change?");
            string editFirstName = EnterString("Enter first name");
            string editLastName = EnterString("Enter last name");
            string toChange = EnterString("Which properties do you want to change?");
            string value;

            if (toChange == "VAT ID") { value = CheckVATID("Value you want to insert"); }
            else { value = EnterString("Value you want to insert"); }
            DateTime date = DateTime.Now;

            foreach (Dictionary<string, string> client in _customers)
            {
                if (client["First name"] == editFirstName && client["Last name"] == editLastName)
                {
                    client["Date"] = date.ToString() + " modified";
                    client[toChange] = value;
                    break;
                }
            }
        }
        private void DeleteClient()
        {
            Console.WriteLine("Delete user");
            ListClient();

            string deleteFirstName = EnterString("First name");
            string deleteLastName = EnterString("Last name");

            foreach (Dictionary<string, string> client in _customers)
            {
                if (client["First name"] == deleteFirstName && client["Last name"] == deleteLastName)
                {
                    _customers.Remove(client);
                    break;
                }
            }
        }
        private void ListClient()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("------------------------------------------");
            foreach (Dictionary<string, string> client in _customers)
            {
                foreach (var name in client)
                {
                    Console.WriteLine(name.Key.PadRight(15) + ": " + name.Value);
                }
                Console.WriteLine("------------------------------------------");
            }
            Console.WriteLine("==========================================");
        }

        private string EnterString(string message)
        {
            Console.WriteLine($"{message}");
            return Console.ReadLine();
        }

        private string CheckVATID(string message)
        {
            // Regex formula to check if VAT ID is valid. 
            string formula = @"[A-Z]{2}[0-9]{8,11}$";
            Regex re = new Regex(formula);
            string toCheck;
            do
            {
                toCheck = EnterString(message);
                if (!re.IsMatch(toCheck))
                {
                    Console.WriteLine("Wrong VAT ID " +
                        "\n you need insert VAT ID similar to this 'PL12345678'");
                }
            } while (!re.IsMatch(toCheck));
            return toCheck;
        }
    }
}
