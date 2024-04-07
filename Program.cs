namespace linqMethod
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double Tuition { get; set; }
    }
    public class StudentClubs
    {
        public int StudentID { get; set; }
        public string ClubName { get; set; }
    }
    public class StudentGPA
    {
        public int StudentID { get; set; }
        public double GPA { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 5, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 7, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
            };
            // Student GPA Collection
            IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
            // Club collection
            IList<StudentClubs> studentClubList = new List<StudentClubs>() {
                new StudentClubs() {StudentID=1, ClubName="Photography" },
                new StudentClubs() {StudentID=1, ClubName="Game" },
                new StudentClubs() {StudentID=2, ClubName="Game" },
                new StudentClubs() {StudentID=5, ClubName="Photography" },
                new StudentClubs() {StudentID=6, ClubName="Game" },
                new StudentClubs() {StudentID=7, ClubName="Photography" },
                new StudentClubs() {StudentID=3, ClubName="PTK" },
            };

            var groupByGPA = studentGPAList.GroupBy(g => g.GPA);
            Console.WriteLine("Student ID grouping by GPA");
            foreach(var gpa in groupByGPA)
            {
                Console.WriteLine($"GPA: {gpa.Key}");
                foreach(StudentGPA id in gpa)
                {
                    Console.WriteLine($"Student ID: {id.StudentID}");
                }
            }

            var sortByClub = studentClubList.OrderBy(s => s.ClubName)
                .GroupBy(g => g.ClubName);
            Console.WriteLine("\nStudent ID sorting by Club and grouping by Club");
            foreach(var club in sortByClub)
            {
                Console.WriteLine($"Club Name: {club.Key}");
                foreach(StudentClubs id in club)
                {
                    Console.WriteLine($"Student ID: {id.StudentID}");
                }
            }

            var numOfStudent = studentGPAList.Where(n => n.GPA > 2.5)
                .Where(n => n.GPA < 4.0)
                .Count();
            Console.WriteLine($"\nNumber of students with a GPA between 2.5 and 4.0: {numOfStudent}");

            var aveOfTuition = studentList.Average(t => t.Tuition);
            Console.WriteLine($"\nAverage all student's tuition: {String.Format("{0:C}", aveOfTuition)}");

            var maxTuition = studentList.Max(m => m.Tuition);
            Console.WriteLine("\nThe student paying the most tuition");
            foreach(Student s in studentList)
            {
                if (s.Tuition == maxTuition)
                {
                    Console.WriteLine($"Name: {s.StudentName}\tMajor: {s.Major}\tTuition: {String.Format("{0:C}", s.Tuition)}");
                }
            }

            var studentlistAndGPA = studentList.Join(studentGPAList,
                std => std.StudentID,
                gpa => gpa.StudentID,
                (std, gpa) => new
                {
                    name = std.StudentName,
                    major = std.Major,
                    gpa = gpa.GPA
                });
            Console.WriteLine("\nJoin the student list and student GPA list");
            foreach(var s in studentlistAndGPA)
            {
                Console.WriteLine($"Name: {s.name}\t\tMajor: {s.major}\tGPA: {s.gpa}");
            }

            var studentlistAndClub = studentList.Join(studentClubList,
                std => std.StudentID,
                club => club.StudentID,
                (std, club) => new
                {
                    name = std.StudentName,
                    club = club.ClubName
                });
            Console.WriteLine("\nJoin the student list and student club list\nGame Club Member");
            foreach(var sc in studentlistAndClub)
            {
                if(sc.club == "Game")
                    Console.WriteLine($"Name: {sc.name}");
            }
        }
    }
}
