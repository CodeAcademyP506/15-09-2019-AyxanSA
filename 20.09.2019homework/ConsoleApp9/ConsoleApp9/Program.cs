﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    delegate void myDelegate(User info);
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var result = 0;
                try
                {
                    result = MenuList();
                    switch (result)
                    {
                        case 1:
                            AddPerson();
                            break;
                        case 2:
                            SearchPerson();
                            break;
                        case 3:
                            Console.WriteLine("Please input name for delete");
                            string deleteName = Console.ReadLine();
                            DeletePerson(deleteName);
                            break;
                        default:
                            Console.WriteLine("Secim yalnishdir");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Bu xana bosh buraxila bilmez ve ya herf yazila bilmez");
                }            
            }
            


            
        }

        static int MenuList()
        {
            Console.WriteLine("1. Elave et ");
            Console.WriteLine("2. Listi goster ");
            Console.WriteLine("3. Sil ");
            Console.WriteLine("4. Deyis ");

            int res = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------");
            return res;
        }

        static void FileWriter(User i)
        {
            var result = i.FullInfo();
            File.AppendAllText("log.txt", result + Environment.NewLine);
        }

        static void AddPerson()
        {
            var users = new List<User>();
            Console.WriteLine("Ad: ");
            string name = Console.ReadLine();
            Console.WriteLine("Soyad: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Yas: ");
            int age = int.Parse(Console.ReadLine());
            var myDele = new myDelegate(FileWriter);
            users.Add(new User() { Name = name.ToUpper(), Surname = surname.ToUpper(), Age = age });

            foreach (var item in users)
            {
                myDele(item);
            }
        }

        static void SearchPerson()
        {

            using (var Data = new StreamReader("log.txt"))
            {

                var myUsers = new List<User>();
                var count = 0;
                while (true)
                {
                    var reader = Data.ReadLine();
                    if (reader == null) break;
                    var iData = reader.Split(' ');

                    myUsers.Add(new User() { Name = iData[0], Surname = iData[1], Age = int.Parse(iData[2]) });

                    Console.WriteLine(myUsers[count].FullInfo());
                    count++;
                }
                Console.WriteLine("Axtarix ucun secin");
                var search = Console.ReadLine();
                var c = myUsers.Where(m => m.Name.ToLower() == search.ToLower());

                foreach (var i in c)
                {
                    Console.WriteLine(i.FullInfo());
                }
            }

        }
        static void DeletePerson(string name)
        {
            var oldLines = File.ReadAllLines("log.txt");
            var word = name.ToUpper();
            var newLines = oldLines.Where(line => !line.Contains(word));
            File.WriteAllLines("log.txt", newLines);
            FileStream obj = new FileStream("log.txt", FileMode.Append);
            obj.Close();
        }
    }

    class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public string FullInfo()
        {
            return Name + " " + Surname + " " + Age;
        }
    }
}
