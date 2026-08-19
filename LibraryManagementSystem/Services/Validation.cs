using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public class Validation
    {
        /// <summary>
        /// Reads a non-empty string input from the user, ensuring that it only contains letters and spaces.
        /// </summary>
        /// <param name="Massage"></param>
        /// <returns></returns>
        public static string ReadNonEmptyString(string Massage)
        {
            string? input;
            string pattern = @"^[\p{L}\s]+$";
            while (true)
            {
                Console.Write(Massage);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, pattern))
                {
                    return input;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input cannot be empty or contain invalid characters. Please try again.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Reads a valid year input from the user, ensuring that it falls within the range of 1300 to 2027.
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static int ReadValidYear(string Message)
        {
            const int MinYear = 1300;
            const int MaxYear = 2027;
            int year;
            string? input;

            while(true)
            {
                Console.Write(Message);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out year) || year < MinYear || year > MaxYear)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Please enter a valid year (1300-2027).");
                    Console.ResetColor();
                    continue;
                }
                return year;
            }
        }

        /// <summary>
        /// Reads a valid email input from the user, ensuring that it matches a standard email format.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReadValidEmail(string message)
        {

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, emailPattern, RegexOptions.IgnoreCase))
                {
                    return input;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Invalid email format. Please enter a valid email (e.g. name@example.com).");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Reads a positive integer input from the user, ensuring that it is greater than zero.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int ReadPositiveInteger(string message)
        {
            int number;
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out number) && number > 0)
                {
                    return number;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Please enter a positive integer.");
                Console.ResetColor();
            }
        }
    }
}
