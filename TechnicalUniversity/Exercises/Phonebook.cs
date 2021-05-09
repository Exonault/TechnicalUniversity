using System;

namespace Exercises
{
    public class Phonebook
    {
        private Contact[] _contacts;
        private int _contactsCount;

        public Phonebook()
        {
            _contacts = new Contact[100];
            _contactsCount = 0;
        }

        public void Run()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("a - Add;f- Find; q - Quit");

                switch (Console.ReadKey().KeyChar)
                {
                    case 'a':
                        Console.WriteLine("Enter a name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter a number");
                        string number = Console.ReadLine();
                        AddContact(name, number);
                        break;
                    case 'f':
                        Console.WriteLine("Enter name");
                        string nameToSearch = Console.ReadLine();
                        Console.WriteLine("Enter a number");
                        string numberToSearch = Console.ReadLine();
                        FindContact(nameToSearch, numberToSearch);
                        break;
                    case 'q':
                        return;
                }
            }
        }

        private void FindContact(string name, string number)
        {
            for (int i = 0; i < _contacts.Length; i++)
            {
                if (_contacts[i].Name == name && _contacts[i].Number == number)
                {
                    Console.WriteLine("Found contact");
                }
                else Console.WriteLine("Contact doesn't exists");
            }
        }

        private void FindContactBinary(string name, string number)
        {
            int left = 0;
            int right = _contacts.Length - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                int compare = String.Compare(_contacts[mid].Name, name);

                if (compare < 0)
                {
                    left = mid + 1;
                }
                else if (compare > 0)
                {
                    right = mid - 1;
                }
                else
                {
                    Console.WriteLine("Found contact");
                }
            }
        }

        private void InsertContact(string name, string number)
        {
            _contacts[_contactsCount] = new Contact(name, number);
            _contactsCount++;
        }

        private void AddContact(string name, string number)
        {
            Contact newContact = new Contact(name, number);
            int i;
            for (i = 0 ; i < _contactsCount; i++)
            {
                if (String.Compare(_contacts[i].Name, name) > 0)
                {
                    break;
                }
            }

            for (int k = _contactsCount - 1; k >= i; k++)
            {
                _contacts[k + 1] = _contacts[k];
            }

            _contacts[i] = newContact;
            _contactsCount++;
        }

        public struct Contact
        {
            public string Name;
            public string Number;

            public Contact(string name, string number)
            {
                this.Name = name;
                this.Number = number;
            }
        }
    }
}