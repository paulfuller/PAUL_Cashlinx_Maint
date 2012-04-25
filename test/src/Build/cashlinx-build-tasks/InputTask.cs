using System;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.Collections.Generic;

namespace Cashlinx.Build.Tasks
{
    [TaskName("input")]
    public class InputTask : Task
    {
        [TaskAttribute("forcelowercase")]
        public bool ForceLowerCase { get; set; }

        [TaskAttribute("forceuppercase")]
        public bool ForceUpperCase { get; set; }

        [TaskAttribute("passwordmode")]
        public bool PasswordMode { get; set; }

        [TaskAttribute("prompt")]
        public string Prompt { get; set; }

        [TaskAttribute("property")]
        public string Property { get; set; }

        [TaskAttribute("validentries")]
        public string ValidEntries { get; set; }

        protected override void ExecuteTask()
        {
            string result = null;

            do
            {
                result = PromptUser();
            }
            while (EntryIsInvalid(result));

            Properties[Property] = result;
        }

        private bool EntryIsInvalid(string result)
        {
            List<string> validEntries;
            if (string.IsNullOrEmpty(ValidEntries))
            {
                validEntries = new List<string>();
            }
            else
            {
                validEntries = new List<string>(ValidEntries.Split(new char[] { ',' }));
            }

            if (validEntries.Count == 0)
            {
                return false;
            }

            return !validEntries.Contains(result);
        }

        private string PromptUser()
        {
            Console.Write(Prompt + ": ");
            string result = string.Empty;
            if (!PasswordMode && ForceLowerCase)
            {
                result = ReadLine().ToLower();
            }
            else if (!PasswordMode && ForceUpperCase)
            {
                result = ReadLine().ToUpper();
            }
            else
            {
                result = ReadLine();
            }

            return result;
        }

        private string ReadLine()
        {
            if (!PasswordMode)
            {
                return Console.ReadLine();
            }
            else
            {
                Stack<string> passbits = new Stack<string>();
                //keep reading
                for (ConsoleKeyInfo cki = Console.ReadKey(true); cki.Key != ConsoleKey.Enter; cki = Console.ReadKey(true))
                {
                    if (cki.Key == ConsoleKey.Backspace && passbits.Count > 0)
                    {
                        //rollback the cursor and write a space so it looks backspaced to the user
                        Console.SetCursorPosition(Math.Max(0, Console.CursorLeft - 1), Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Math.Max(0, Console.CursorLeft - 1), Console.CursorTop);
                        passbits.Pop();
                    }
                    else if (cki.KeyChar >= ' ' && cki.KeyChar <= '~')
                    {
                        Console.Write("*");
                        passbits.Push(cki.KeyChar.ToString());
                    }
                }
                string[] pass = passbits.ToArray();
                Array.Reverse(pass);
                Console.WriteLine();
                return string.Join(string.Empty, pass);
            }
        }
    }
}
