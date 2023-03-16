using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Security.Claims;

namespace Rainbow_School
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassAndSection { get; set; }

        private const string FILE_PATH = @"C:\\assessment\rainbowschool.txt";
        public Teacher(int id, string name, string classAndSection)
        {
            this.Id = id;
            this.Name = name;
            this.ClassAndSection = classAndSection;
        }

        public static List<Teacher> LoadTeacherDetails()
        {
            List<Teacher> teachers = new List<Teacher>();
            StreamReader reader = new StreamReader(FILE_PATH);
            string line;
            string[] data;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                data = line.Split(',');
                teachers.Add(new Teacher(int.Parse(data[0]), data[1], data[2]));
            }
            reader.Close();
            return teachers;
        }
        public void SaveTeacherDetails(List<Teacher> teachers)
        {
            StreamWriter writer = new StreamWriter(FILE_PATH);
            foreach (Teacher teacher in teachers)
            {
                writer.WriteLine($"{teacher.Id},{teacher.Name},{teacher.ClassAndSection}");
            }
            writer.Close();
        }
        public Teacher GetTeacherDetails()
        {
            Console.Write("Enter Teacher ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Teacher Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Teacher Class and Section: ");
            string classAndSection = Console.ReadLine();
            return new Teacher(id, name, classAndSection);
        }
        public void AddTeacherDetails()
        {
            Teacher teacher = GetTeacherDetails();
            List<Teacher> teachers = LoadTeacherDetails();
            teachers.Add(teacher);
            SaveTeacherDetails(teachers);
            Console.WriteLine("Teacher data added to database successfully!");
        }
        public void UpdateTeacherDetails()
        {
            Console.Write("Enter the ID of the teacher to update: ");
            int id = int.Parse(Console.ReadLine());
            List<Teacher> teachers = LoadTeacherDetails();
            Teacher teacher = teachers.Find(t => t.Id == id);
            if (teacher != null)
            {
                Console.WriteLine("Update Teacher Details");
                teacher = GetTeacherDetails();
                SaveTeacherDetails(teachers);
                Console.WriteLine("Teacher  data updated successfully!");
            }
            else
            {
                Console.WriteLine("Teacher not found!");
            }
        }
        public void DeleteTeacherDetails()
        {
            Console.Write("Enter the ID of the teacher to delete: ");
            int id = int.Parse(Console.ReadLine());
            List<Teacher> teachers = LoadTeacherDetails();
            Teacher teacher = teachers.Find(t => t.Id == id);
            if (teacher != null)
            {
                teachers.Remove(teacher);
                SaveTeacherDetails(teachers);
                Console.WriteLine("Teacher data deleted successfully!");
            }
            else
            {
                Console.WriteLine("Teacher data not found!");
            }
        }
        public void RetrieveTeacherDetails()
        {
            Console.Write("Enter the ID of the teacher to retrieve: ");
            int id = int.Parse(Console.ReadLine());
            List<Teacher> teachers = LoadTeacherDetails();
            Teacher teacher = teachers.Find(t => t.Id == id);
            if (teacher != null)
            {
                Console.WriteLine($"ID: {teacher.Id}");
                Console.WriteLine($"Name: {teacher.Name}");
                Console.WriteLine($"Class and Section: {teacher.ClassAndSection}");
            }
            else
            {
                Console.WriteLine("Teacher data not found!");
            }
        }
    }
    class Program
    {

        public static void Main(string[] args)
        {
            Teacher teacher = new Teacher(10835, "fardeen", "5c");
            string choice;
            Console.WriteLine("Welcome to Rainbow School!");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Add Teacher Details");
                Console.WriteLine("2. Update Teacher Details");
                Console.WriteLine("3. Delete Teacher Details");
                Console.WriteLine("4. Retrieve Teacher Details");
                Console.Write("Please select an option: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        teacher.AddTeacherDetails();
                        break;
                    case "2":
                        teacher.UpdateTeacherDetails();
                        break;
                    case "3":
                        teacher.DeleteTeacherDetails();
                        break;
                    case "4":
                        teacher.RetrieveTeacherDetails();
                        break;
                    default:
                        Console.WriteLine("Invalid option Please try again....");
                        break;
                }
            }
        }
    }

}

