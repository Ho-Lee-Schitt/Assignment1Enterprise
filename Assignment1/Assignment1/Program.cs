using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {
        public static void printTableLine(int length) // Helper function. Prints a table line of given length
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }// End printTableLine

        public static IQueryable<Lecturer> findLecturer(UniModelContainer dbc, string lecturerGivenEmail = "") // Helper function to find lecturer by ID or email.
        {
            string ID;
            int lecturerID;
            if (String.IsNullOrEmpty(lecturerGivenEmail))
            {
                Console.Write("Please enter the Lecturer's Number: ");
                ID = Console.ReadLine();
                if (int.TryParse(ID, out lecturerID))
                {
                    var query = from l in dbc.Lecturers
                                where (l.LecturerStaffNumber == lecturerID)
                                select l;
                    return query;
                }
                else
                {
                    Console.WriteLine("Invalid Lecturer Number");
                    var blankQuery = from l in dbc.Lecturers
                                     where (l.LecturerStaffNumber == -1)
                                     select l;
                    return (blankQuery);
                }// End IF
            }
            else
            {
                var query = from l in dbc.Lecturers
                            where (l.LecturerEmail == lecturerGivenEmail)
                            select l;
                return query;
            }// End IF
        }// End findLecturer

        // Print all Lecturers
        static void printLecturers(UniModelContainer dbc)
        {
            int tableLength = 70;

            printTableLine(tableLength);
            Console.WriteLine(String.Format("|{0,-10}|{1,-10}|{2,-30}|{3,-15}|", "Forename", "Surname", "Email", "Contact Number"));
            printTableLine(tableLength);
            var lecturers = from l in dbc.Lecturers
                            orderby l.LecturerSurname
                            select l;
            if (lecturers.Any()) // Check if anything was found
            {
                foreach (var lecturer in lecturers)
                {
                    Console.WriteLine(String.Format("|{0,-10}|{1,-10}|{2,-30}|{3,-15}|", lecturer.LecturerForename, lecturer.LecturerSurname, lecturer.LecturerEmail, lecturer.LecturerContactNumber.ToString("0## #### ####")));
                } // End FOREACH
                printTableLine(tableLength);
            }// End IF
            Console.WriteLine();
        }// End printLecturers

        // Given Lecturer number look up lecturer
        static void lecturerLookup(UniModelContainer dbc)
        {
            int tableLength = 33;
            var query = findLecturer(dbc);
            if (query.Any()) // Check if anything was found
            {
                printTableLine(tableLength);
                Console.WriteLine(String.Format("|{0,-20}|{1,-10}|", (query.FirstOrDefault().LecturerForename + " " + query.FirstOrDefault().LecturerSurname), "Modules"));
                printTableLine(tableLength);
                var query1 = from m in dbc.Modules
                             where (m.LecturerLecturerStaffNumber == query.FirstOrDefault().LecturerStaffNumber)
                             select m;
                if (query1.Any()) // Check if anything was found
                {
                    foreach (var module in query1)
                    {
                        Console.WriteLine(String.Format("|{0,-31}|", (module.ModuleName)));
                    }// End FOREACH
                } // End IF
                printTableLine(tableLength);
            }
            else
            {
                Console.WriteLine("No Lecturer with that Number");
            }// End IF
            Console.WriteLine();
        }// End lecturerLookup

        // Given course code loopup course
        static void courseLookup(UniModelContainer dbc)
        {
            string courseCode;
            int int_courseCode;
            int tableLength = 58;
            Console.Write("Please enter the Course Code: ");
            courseCode = Console.ReadLine();
            if (int.TryParse(courseCode, out int_courseCode))
            {
                var query = from c in dbc.Courses
                            where (c.CourseCode == int_courseCode)
                            select c;
                if (query.Any()) // Check if anything was found
                {
                    printTableLine(tableLength);
                    Console.WriteLine(String.Format("|{0,-15}|{1,-40}|", "Course Code", "Course Name"));
                    printTableLine(tableLength);
                    Console.WriteLine(String.Format("|{0,-15}|{1,-40}|", query.FirstOrDefault().CourseCode, query.FirstOrDefault().CourseName));
                    printTableLine(tableLength);

                    Console.WriteLine();
                    printTableLine(tableLength);
                    Console.WriteLine(String.Format("|{0,-15}|{1,-40}|", "Module Code", "Module Name"));
                    printTableLine(tableLength);

                    var query1 = from cm in dbc.CourseModules
                                 where (cm.CourseCourseId == query.FirstOrDefault().CourseId)
                                 select cm;
                    if (query1.Any())
                    {
                        foreach (var courseModule in query1)
                        {
                            var query2 = from m in dbc.Modules
                                         where (m.ModuleId == courseModule.ModuleModuleId)
                                         select m;
                            if (query2.Any())
                            {
                                foreach (var module in query2)
                                {
                                    Console.WriteLine(String.Format("|{0,-15}|{1,-40}|", module.ModuleCode, module.ModuleName));
                                }// End FOREACH
                                printTableLine(tableLength);
                            }// End IF
                        }// End FOREACH
                    }// End IF
                }
                else
                {
                    Console.WriteLine("No Course with that Code");
                }// End IF
            }// End IF
            Console.WriteLine();
        }// End courseLookup

        // Given module code loopup module
        static void moduleLookup(UniModelContainer dbc)
        {
            string moduleCode;
            int tableLength = 58;
            Console.Write("Please enter the Module Code: ");
            moduleCode = Console.ReadLine();

            var query = from m in dbc.Modules
                        where (m.ModuleCode == moduleCode)
                        select m;
            if (query.Any()) // Check if anything was found
            {
                printTableLine(tableLength);
                Console.WriteLine(String.Format("|{0,-15}|{1,-40}|", "Module Code", "Module Name"));
                printTableLine(tableLength);
                Console.WriteLine(String.Format("|{0,-15}|{1,-40}|", query.FirstOrDefault().ModuleCode, query.FirstOrDefault().ModuleName));
                printTableLine(tableLength);

                Console.WriteLine();
                printTableLine(tableLength);
                Console.WriteLine(String.Format("|{0,-15}|{1,-40}|", "Course Code", "Course Name"));
                printTableLine(tableLength);

                var query1 = from cm in dbc.CourseModules
                             where (cm.ModuleModuleId == query.FirstOrDefault().ModuleId)
                             select cm;
                if (query1.Any())
                {
                    foreach (var courseModule in query1)
                    {
                        var query2 = from c in dbc.Courses
                                     where (c.CourseId == courseModule.CourseCourseId)
                                     select c;
                        if (query2.Any()) // Check if anything was found
                        {
                            foreach (var course in query2)
                            {
                                Console.WriteLine(String.Format("|{0,-15}|{1,-40}|", course.CourseCode, course.CourseName));
                            }// End FOREACH
                            printTableLine(tableLength);
                        }// End IF
                    }// End FOREACH
                }// End IF
            }
            else
            {
                Console.WriteLine("No Module with that Code");
            }// End IF
            Console.WriteLine();
        }// End moduleLookup

        // Given student number loopup student
        static void studentLookup(UniModelContainer dbc)
        {
            int averageGrade = 0;
            string studentNumber;
            int tableLength = 17;
            Console.Write("Please enter the Student Number: ");
            studentNumber = Console.ReadLine();

            var query = from s in dbc.Students
                        where (s.StudentNumber == studentNumber)
                        select s;
            if (query.Any()) // Check if anything was found
            {
                printTableLine(tableLength);
                Console.WriteLine(String.Format("|{0,-15}|", "Student Name"));
                printTableLine(tableLength);
                Console.WriteLine(String.Format("|{0,-15}|", (query.FirstOrDefault().StudentForename + " " + query.FirstOrDefault().StudentSurname)));
                printTableLine(tableLength);

                tableLength = 74;

                Console.WriteLine();
                printTableLine(tableLength);
                Console.WriteLine(String.Format("|{0,-15}|{1,-40}|{2,-15}|", "Module Code", "Module Name", "Student Grade"));
                printTableLine(tableLength);

                var query1 = from smg in dbc.StudentModuleGrades
                             where (smg.StudentStudentId == query.FirstOrDefault().StudentId)
                             select smg;

                if (query1.Any()) // Check if anything was found
                {
                    foreach (var studentModuleGrade in query1)
                    {
                        var query2 = from m in dbc.Modules
                                     where (m.ModuleId == studentModuleGrade.ModuleModuleId)
                                     select m;
                        if (query2.Any()) // Check if anything was found
                        {
                            Console.WriteLine(String.Format("|{0,-15}|{1,-40}|{2,-15}|", query2.FirstOrDefault().ModuleCode, query2.FirstOrDefault().ModuleName, studentModuleGrade.StudentGrade));
                        }// End IF
                        averageGrade += studentModuleGrade.StudentGrade;
                    }// End FOREACH
                    printTableLine(tableLength);
                    Console.WriteLine(String.Format("|{0,-56}|{1,-15}|", "Overall Average", (averageGrade / query1.Count())));
                    printTableLine(tableLength);
                }// End IF
            }// End IF
            Console.WriteLine();
        }// End studentLookup

        // Replace lecturer with new lecturer
        static void replaceLecturer(UniModelContainer dbc)
        {
            string lecturerName;
            string[] lecturerSplit;
            string lecturerEmail;
            string lecturerPhone;
            long lecturerPhoneNumber;
            string lecturerRoom;

            // Get new lecturer details
            Console.WriteLine("Sorry to hear about the depature for they shall be missed. To the leaver I say So long and thanks for all the fish! And to our new staff member I'll need a few details.");
            Console.Write("Your full name please: ");
            lecturerName = Console.ReadLine();
            Console.WriteLine();
            if (!String.IsNullOrEmpty(lecturerName))
            {
                lecturerSplit = lecturerName.Split(' ');
                if (lecturerSplit.Count() != 2)
                {
                    Console.WriteLine("Error: Unfortunatly I can only accept names with a Forename and Surname. Sorry.");
                    Console.WriteLine();
                    return;
                }// End IF
            }
            else
            {
                Console.WriteLine("Error: I've never met anyone without a name. And not having one is a problem for me. Would you mind at least making one up?");
                Console.WriteLine();
                return;
            }// End IF
            Console.Write("Your email please: ");
            lecturerEmail = Console.ReadLine();
            Console.WriteLine();

            if (String.IsNullOrEmpty(lecturerEmail))
            {
                Console.WriteLine("Error: I'll need a vaild email.");
                Console.WriteLine();
                return;
            }// End IF

            Console.Write("Your Contact Number please: ");
            lecturerPhone = Console.ReadLine();
            Console.WriteLine();

            if (String.IsNullOrEmpty(lecturerPhone))
            {
                Console.WriteLine("Error: I'll need a vaild Phone Number.");
                Console.WriteLine();
                return;
            }// End IF

            if (!long.TryParse(lecturerPhone, out lecturerPhoneNumber))
            {
                Console.WriteLine("Error: Invalid Phone Number.");
                Console.WriteLine();
                return;
            }// End IF

            Console.Write("Your Room Number please: ");
            lecturerRoom = Console.ReadLine();
            Console.WriteLine();

            if (String.IsNullOrEmpty(lecturerRoom))
            {
                Console.WriteLine("Error: I'll need a vaild Room Number.");
                Console.WriteLine();
                return;
            }// End IF

            // We want to confirm the removal of the old lecturer before adding a new lecturer to the table. So we preform a look up and check with the user to be safe.
            Console.WriteLine("Ok your new Lecturer will be added. Now what is the Staff Number of the Lecturer that is leaving? ");
            var oldLecturer = findLecturer(dbc);
            Console.WriteLine("This is the Lecturer you wish to remove:");

            int tableLength = 70;

            printTableLine(tableLength);
            Console.WriteLine(String.Format("|{0,-10}|{1,-10}|{2,-30}|{3,-15}|", "Forename", "Surname", "Email", "Contact Number"));
            printTableLine(tableLength);

            if (oldLecturer.Any())
            {
                foreach (var l in oldLecturer)
                {
                    Console.WriteLine(String.Format("|{0,-10}|{1,-10}|{2,-30}|{3,-15}|", l.LecturerForename, l.LecturerSurname, l.LecturerEmail, l.LecturerContactNumber.ToString("0## #### ####")));
                }// End FOREACH
                printTableLine(tableLength);
            }// End IF

            Console.Write("Are you sure you wish to remove them? (Y/N) ");
            switch (Console.ReadLine()[0])
            {
                case 'y':
                case 'Y':
                    break;
                case 'n':
                case 'N':
                    Console.WriteLine("You have cancelled the deletion of your old lecturer. Your new lecturer will not be added.");
                    return;
                default:
                    Console.WriteLine("Invalid character entered. Your new lecturer will not be added.");
                    return;
            }// End SWITCH

            // With the old lecturer ready to be removed we add our new lecturer and look them up to get their just assigned staff number. We'll need this to update the modules.
            var lecturer = new Lecturer { LecturerForename = lecturerSplit[0], LecturerSurname = lecturerSplit[1], LecturerEmail = lecturerEmail, LecturerRoom = lecturerRoom, LecturerContactNumber = lecturerPhoneNumber };
            dbc.Lecturers.Add(lecturer);
            try
            {
                dbc.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }// End TRTCATCH
            Console.WriteLine("Your new Lecturer has been added to the database.");

            // Looking up new lecturer to get their id.
            var newLecturer = findLecturer(dbc, lecturerEmail);

            foreach (var l in oldLecturer)
            {
                var modules = from m in dbc.Modules
                             where (m.LecturerLecturerStaffNumber == l.LecturerStaffNumber)
                             select m;
                foreach (var module in modules)
                {
                    foreach (var lecturerNumber in newLecturer) {
                        module.LecturerLecturerStaffNumber = lecturerNumber.LecturerStaffNumber;
                    }// End FOREACH
                }// End FOREACH
                dbc.Lecturers.Remove(l);
            }// End FOREACH
            try
            {
                dbc.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }// End TRTCATCH
            Console.WriteLine("Your database has been updated with the changes.");
            Console.WriteLine();
        }// End replaceLecturer

        static void Main(string[] args)
        {
            using (var db = new UniModelContainer())
            {
                string option;
                int selection = 0;
                Console.WriteLine("****************************");
                Console.WriteLine("University Management System");
                Console.WriteLine("****************************");

                while (selection != 7)
                {

                    Console.WriteLine("Please select an option:");

                    Console.WriteLine("(1) - List the lecturers.");
                    Console.WriteLine("(2) - Given a Lecturer’s Staff Number display the lecturers name and a list of all the modules taught by that specified lecturer.");
                    Console.WriteLine("(3) - Given a Course Code list the Module Code and Module Name of all the modules linked with that specified course.");
                    Console.WriteLine("(4) - Given a Module Code display the Course code plus name of all the courses that include the specified module.");
                    Console.WriteLine("(5) - Given a Student number, display the student name and list the Module code, module name and result for all modules taken by that Student.");
                    Console.WriteLine("(6) - Replace current Lecturer with new Hire.");
                    Console.WriteLine("(7) - LOGOUT");

                    Console.Write("Your selection: ");
                    option = Console.ReadLine();
                    Console.WriteLine();

                    // Handle User input

                    if (int.TryParse(option, out selection))
                    {
                        switch (selection)
                        {
                            case 1:
                                printLecturers(db);
                                break;
                            case 2:
                                lecturerLookup(db);
                                break;
                            case 3:
                                courseLookup(db);
                                break;
                            case 4:
                                moduleLookup(db);
                                break;
                            case 5:
                                studentLookup(db);
                                break;
                            case 6:
                                replaceLecturer(db);
                                break;
                            case 7:
                                Console.WriteLine("Goodbye!");
                                break;
                            default:
                                Console.WriteLine("Invalid Selection. Please re-enter your selection");
                                Console.WriteLine();
                                break;
                        }// End Swtich
                    }
                    else
                    {
                        Console.WriteLine("Invalid Selection. Please re-enter your selection");
                        Console.WriteLine();
                    }// End If
                }// End While
            }// End Using Database
        }// End Main
    }// End Program
}// End namespace
