using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;

namespace ConsoleApp3.DPL
{
    public class StudentDBStorage
    {
        private readonly StudentContext _context;
        public StudentDBStorage(StudentContext context)
        {
            _context = context;
        }
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }
        public List<Group> GetGroups()
        {
            return _context.Groups.ToList();
        }
        public void RemoveStudent(int studentId)
        {
            var studentToRemove = _context.Students.FirstOrDefault(p => p.StudentId == studentId);
            if (studentToRemove != null)
            {
                _context.Students.Remove(studentToRemove);
                _context.SaveChanges();
            }
        }
        public void EditStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddStudents(StudentDBStorage storage)
        {
            Console.WriteLine("Добавление студента:");
            Console.WriteLine("Выберите номер группы:");
            List<Group> groups = storage.GetGroups();
            for (int i = 0; i < groups.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {groups[i].GroupName}");
            }

            int groupIndex;
            if (int.TryParse(Console.ReadLine(), out groupIndex) && groupIndex >= 1 && groupIndex <= groups.Count)
            {
                Group selectedGroup = groups[groupIndex - 1];

                Console.WriteLine("Введите имя студента:");
                string studentName = Console.ReadLine();
                Console.WriteLine("Введите фамилию студента:");
                string studentSurname = Console.ReadLine();

                Student newStudent = new Student() { StudentName = studentName, StudentSurname = studentSurname, Group = selectedGroup };
                storage.AddStudent(newStudent);

                Console.WriteLine("Студент успешно добавлен.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
        }

        private static void RemoveStudents(StudentDBStorage storage)
        {
            Console.WriteLine("Удаление студента:");
            Console.WriteLine("Выберите номер группы:");

            List<Group> groups = storage.GetGroups();
            for (int i = 0; i < groups.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {groups[i].GroupName}");
            }

            int groupIndex;
            if (int.TryParse(Console.ReadLine(), out groupIndex) && groupIndex >= 1 && groupIndex <= groups.Count)
            {
                Group selectedGroup = groups[groupIndex - 1];
                List<Student> students = selectedGroup.Students;

                if (students.Count == 0)
                {
                    Console.WriteLine(" ! Список студентов пуст ! ");
                    return;
                }
                Console.WriteLine("Выберите номер студента для удаления:");

                for (int i = 0; i < students.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {students[i].StudentName} {students[i].StudentSurname}");
                }

                int studentIndex;
                if (int.TryParse(Console.ReadLine(), out studentIndex) && studentIndex >= 1 && studentIndex <= students.Count)
                {
                    Student selectedStudent = students[studentIndex - 1];
                    storage.RemoveStudent(selectedStudent.StudentId);

                    Console.WriteLine("Студент успешно удален.");
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
        }

        public void mainMenu()
        {
            StudentDBStorage studentDBStorage = new StudentDBStorage(new StudentContext());

            Group g1 = new Group { GroupName = "ФИИТ" };
            Group g2 = new Group { GroupName = "МОАИС" };
            Group g3 = new Group { GroupName = "ПМИ" };

            Student s1 = new Student() { StudentName = "И1", StudentSurname = "Ф1", Age = 18, Group = g1 };
            Student s2 = new Student() { StudentName = "И2", StudentSurname = "Ф2", Age = 17, Group = g2 };
            Student s3 = new Student() { StudentName = "И3", StudentSurname = "Ф3", Age = 17, Group = g3 };

            studentDBStorage.AddStudent(s1);
            studentDBStorage.AddStudent(s2);
            studentDBStorage.AddStudent(s3);

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Списки студентов по группам:");
                List<Group> groups = studentDBStorage.GetGroups();
                foreach (Group group in groups)
                {
                    Console.WriteLine($"{group.GroupName} ({group.Students.Count} студентов)");

                    List<Student> sortedStudents = group.Students;
                    sortedStudents.Sort((s1, s2) => s1.StudentSurname.CompareTo(s2.StudentSurname));

                    foreach (Student student in sortedStudents)
                    {
                        Console.WriteLine($"- {student.StudentName} {student.StudentSurname}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Добавить студента");
                Console.WriteLine("2 - Удалить студента");
                Console.WriteLine("0 - Выйти из приложения");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        studentDBStorage.AddStudents(studentDBStorage);
                        break;
                    case "2":
                        RemoveStudents(studentDBStorage);
                        break;
                    case "0":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}