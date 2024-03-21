using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace NorthwindCustomerList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> cities = ReadCitiesFromXml("cities.xml");

            Console.WriteLine("Available cities:");
            foreach (string city in cities)
            {
                Console.WriteLine(city);
            }

            Console.Write("Enter the name of a city: ");
            string inputCity = Console.ReadLine();

            List<string> customers = ReadCustomersFromXml("customers.xml", inputCity);

            Console.WriteLine($"There are {customers.Count} customers in {inputCity}:");
            foreach (string customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        static List<string> ReadCitiesFromXml(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            return doc.XPathSelectElements("cities/city")
                .Select(city => city.Value)
                .ToList();
        }

        static List<string> ReadCustomersFromXml(string filePath, string city)
        {
            XDocument doc = XDocument.Load(filePath);

            return doc.XPathSelectElements($"customers/customer[city/text()='{city}']")
                .Select(c => c.Element("name").Value)
                .ToList();
        }
    }
}
