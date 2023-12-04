using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

[Serializable]
public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public double Salary { get; set; }
}

class Program
{
    static void Main()
    {
        // Step 2: Binary Serialization and Deserialization
        Employee emp = GetUserInput();

        // Binary Serialization
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream("employee.bin", FileMode.Create))
        {
            binaryFormatter.Serialize(fileStream, emp);
        }

        // Binary Deserialization
        using (FileStream fileStream = new FileStream("employee.bin", FileMode.Open))
        {
            Employee deserializedEmp = (Employee)binaryFormatter.Deserialize(fileStream);
            Console.WriteLine("Binary Deserialization:");
            DisplayEmployeeDetails(deserializedEmp);
        }

        // Step 3: XML Serialization and Deserialization
        // XML Serialization
        using (TextWriter textWriter = new StreamWriter("employee.xml"))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Employee));
            xmlSerializer.Serialize(textWriter, emp);
        }

        // XML Deserialization
        using (TextReader textReader = new StreamReader("employee.xml"))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Employee));
            Employee deserializedXmlEmp = (Employee)xmlSerializer.Deserialize(textReader);
            Console.WriteLine("\nXML Deserialization:");
            DisplayEmployeeDetails(deserializedXmlEmp);
        }

        // Step 4: Test and Verification
        // Ensure that the deserialized objects match the original data
    }

    static Employee GetUserInput()
    {
        Employee emp = new Employee();

        Console.WriteLine("Enter Employee Details:");
        Console.Write("ID: ");
        emp.Id = Convert.ToInt32(Console.ReadLine());

        Console.Write("First Name: ");
        emp.FirstName = Console.ReadLine();

        Console.Write("Last Name: ");
        emp.LastName = Console.ReadLine();

        Console.Write("Salary: ");
        emp.Salary = Convert.ToDouble(Console.ReadLine());

        return emp;
    }

    static void DisplayEmployeeDetails(Employee emp)
    {
        Console.WriteLine($"ID: {emp.Id}");
        Console.WriteLine($"Name: {emp.FirstName} {emp.LastName}");
        Console.WriteLine($"Salary: {emp.Salary}");
        Console.WriteLine();
    }
}