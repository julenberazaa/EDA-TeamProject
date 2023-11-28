using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace StudentsAndSubjects
{
    public class Tests
    {
        public static bool RunBasicTests()
        {
            SchoolManager schoolManager= new SchoolManager();

            Console.WriteLine("Adding test students/grades...");
            schoolManager.AddStudent(new Student() { Name = "Student-1", Address = "Address-1", Email = "student1@ehu.eus", Id = "1111111a" });
            schoolManager.AddStudent(new Student() { Name = "Student-2", Address = "Address-2", Email = "student2@ehu.eus", Id = "2222222b" });
            schoolManager.AddStudent(new Student() { Name = "Student-3", Address = "Address-3", Email = "student3@ehu.eus", Id = "3333333c" });
            schoolManager.AddGrade(new StudentGrade() { Date = DateTime.Now, Grade = 6.2, StudentId = "1111111a", Subject = "Subject-1" });
            schoolManager.AddGrade(new StudentGrade() { Date = DateTime.Now, Grade = 5.8, StudentId = "1111111a", Subject = "Subject-2" });
            schoolManager.AddGrade(new StudentGrade() { Date = DateTime.Now, Grade = 6.2, StudentId = "2222222b", Subject = "Subject-2" });
            schoolManager.AddGrade(new StudentGrade() { Date = DateTime.Now, Grade = 6, StudentId = "2222222b", Subject = "Subject-2" });
            Console.Write("Checking AverageGradeByStudentName(\"Student-1\")...");
            if (schoolManager.AverageGradeByStudentName("Student-1") != 6)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking AverageGradeByStudentName(\"Student-2\")...");
            if (schoolManager.AverageGradeByStudentName("Student-2") != 6.1)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking AverageGradeByStudentName(\"Student-3\") [has no grades, should return 0]...");
            if (schoolManager.AverageGradeByStudentName("Student-3") != 0)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking AverageGradeByStudentName(\"Student-4\") [non-existing student, should return 0]...");
            if (schoolManager.AverageGradeByStudentName("Student-4") != 0)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");

            Console.Write("Checking AverageGradeBySubject(\"Subject-1\")...");
            if (schoolManager.AverageGradeBySubject("Subject-1") != 6.2)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking AverageGradeBySubject(\"Subject-2\")...");
            if (schoolManager.AverageGradeBySubject("Subject-2") != 6)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.Write("Checking AverageGradeBySubject(\"Subject-3\") [non-existing subject, should return 0]...");
            if (schoolManager.AverageGradeBySubject("Subject-3") != 0)
            {
                Console.WriteLine("Error");
                return false;
            }
            Console.WriteLine("OK");
            Console.WriteLine("Basic tests passed. The method seems to work correctly...");
            return true;
        }

        public static SpeedMeasure MeasureSpeed()
        {
            Console.WriteLine("Preparing test data...");
            SchoolManager schoolManager = new SchoolManager();

            int numStudents = 500;
            int numGradesPerStudent = 500;
            int numSubjects = 100;
            int[] array = new int[numStudents];
            Random random = new Random();
            for (int i = 0; i < numStudents; i++)
            {
                array[i] = random.Next(100000, 999999);

                schoolManager.AddStudent(new Student() { Name = $"Student-{array[i]}", Id = $"{array[i]}X" });
                for (int j = 0; j < numGradesPerStudent; j++)
                {
                    schoolManager.AddGrade(new StudentGrade() 
                    {
                        Date = DateTime.Now,
                        Grade = random.NextDouble() *10,
                        StudentId = $"{array[i]}X",
                        Subject = $"Subject-{i % numSubjects}"
                    });
                }
            }

            Console.WriteLine($"Running AverageGradeByStudentName/AverageGradeBySubject with {numStudents} students and {numGradesPerStudent} grades per student...");
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < numStudents; i++)
            {
                double avg = schoolManager.AverageGradeByStudentName($"Student-{array[i]}");
                if (avg == 0)
                    return new SpeedMeasure() { Success = false };
            }
            for (int i = 0; i < numSubjects; i++)
            {
                double avg = schoolManager.AverageGradeBySubject($"Subject-{i % numSubjects}");
                if (avg == 0)
                    return new SpeedMeasure() { Success = false };
            }

            return new SpeedMeasure() { Success = true, Time = stopwatch.Elapsed.TotalSeconds };
        }
    }
}
