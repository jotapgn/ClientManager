using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClientManager
{
    class Program
    {
        [System.Serializable]
        struct Client
        {
            public string name;
            public string email;
            public string cpf;
        }
        static List<Client> clients = new List<Client>();
        enum Menu { ListCLients = 1, AddClient, DeleteClient, Exit };
        static void Main(string[] args)
        {
            LoadClient();
            bool chooseExit = false;
            while (!chooseExit)
            {
                Console.WriteLine("=== Welcome to the Client Manager ===");
                Console.WriteLine("Select one of the options below: ");
                Console.WriteLine("1-List Clients\n2-Register Client\n3-Remove Client\n4-Exit");
                int option = int.Parse(Console.ReadLine());
                
                Menu options = (Menu)option;
                switch (options)
                {
                    case Menu.ListCLients:
                        ListCLients();
                        break;
                    case Menu.AddClient:
                        AddClient();
                        break;
                    case Menu.DeleteClient:
                        DeleteClient();
                        break;
                    case Menu.Exit:
                        chooseExit = true;
                        break;
                }
                Console.Clear();
            }
        }
            
        static void AddClient()
        {
            
            Client client = new Client();
            Console.WriteLine("Add Client: ");
            Console.WriteLine("Name: ");
            client.name = Console.ReadLine();
            Console.WriteLine("Email: ");
            client.email = Console.ReadLine();
            Console.WriteLine("CPF: ");
            client.cpf = Console.ReadLine();

            clients.Add(client);
            SaveClient();

            Console.WriteLine("Register Completed, press ENTER to return to the Menu");
            Console.ReadLine();
        }
        static void ListCLients()
        {
            if(clients.Count > 0)
            {
                Console.WriteLine("List of Clients");
                int i = 0;
                foreach (Client client in clients)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Name: {client.name}");
                    Console.WriteLine($"Email: {client.email}");
                    Console.WriteLine($"CPF: {client.cpf}");
                    Console.WriteLine("===========================");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("List of Clients");
                Console.WriteLine("No registered client");
            }
            Console.WriteLine("Press ENTER to return to the Menu");
            Console.ReadLine();
        }
        static void DeleteClient()
        {
            ListCLients();
            Console.WriteLine("Enter the client ID  you want remove");
            int idNumeber = int.Parse(Console.ReadLine());
            if(idNumeber >= 0 && idNumeber < clients.Count){
                    clients.RemoveAt(idNumeber);
                    SaveClient();
            }else{
                Console.WriteLine("Invalid ID, try again");
                Console.ReadLine();
            }
        }
        static void SaveClient()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();
            enconder.Serialize(stream, clients);
            stream.Close();
        }
        static void LoadClient()
        {           
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            try{
               
                BinaryFormatter enconder = new BinaryFormatter();
                clients = (List<Client>)enconder.Deserialize(stream);
                if(clients == null){
                    clients = new List<Client>();
                }
                
            
            }
            catch(Exception e){
                clients = new List<Client>();
            }
         
            stream.Close();
        }
            
    }
}
