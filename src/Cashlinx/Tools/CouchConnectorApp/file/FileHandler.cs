using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using file;

namespace CouchConsoleApp.file
{
    class FileHandler
    {
        public static string mainPath = null;
        public static string userInfoFilePath = null;

        public static void init()
        {
          string appPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
          DirectoryInfo df = new DirectoryInfo(appPath + @"\sys\");
          if(!df.Exists)
          {
              DirectoryInfo di = Directory.CreateDirectory(appPath + @"\sys\" );
              //Console.WriteLine("Directory created path"+di.FullName);
              mainPath=di.FullName;
          }else
          {
              Console.WriteLine(df.FullName);
              mainPath = df.FullName;
          }

          userInfoFilePath = mainPath + "userInfo.ccon";
        }

        public static string createPDFDir()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DirectoryInfo df = new DirectoryInfo(appPath + @"\docs\");
            if (!df.Exists)
            {
                DirectoryInfo di = Directory.CreateDirectory(appPath + @"\docs\");
                //Console.WriteLine("Directory created path"+di.FullName);
                mainPath = di.FullName;
            }
            else
            {
                Console.WriteLine(df.FullName);
                mainPath = df.FullName;
            }
            return mainPath;
            //userInfoFilePath = mainPath + "userInfo.ccon";
        }


        public static bool register(string user,string pwd)
        {
            

            if(!File.Exists(userInfoFilePath))
            {
               File.Create(userInfoFilePath).Close();
            }

             System.IO.StreamWriter file = new System.IO.StreamWriter(userInfoFilePath);
             StringBuilder build=new StringBuilder();
              build.Append("\n");
              build.Append("username=" + SimpleHash.CreateSaltedPassword("jak", user));
              build.Append("\n");
              build.Append("password=" + SimpleHash.CreateSaltedPassword("jak", pwd));
              file.WriteLine(build.ToString());
              file.Close();
            return true;
        }

        public static int login(string user, string pwd)
        {
            init();
            if (!File.Exists(userInfoFilePath))
            {
                return - 1;
            }

            System.IO.StreamReader sr = new System.IO.StreamReader(userInfoFilePath);
            string line = null;
            string userName1 = null;
            string pwd1 = null;
            while ((line = sr.ReadLine()) != null)
            {
               if(line.IndexOf("username")!=-1)
               {
                   Console.WriteLine(line.LastIndexOf("="));
                   Console.WriteLine(line.Length);

                   userName1 = line.Substring(line.LastIndexOf("="), (line.Length - line.LastIndexOf("=")));
                   Console.WriteLine(userName1);

               }
               else if (line.IndexOf("password") != -1)
               {
                   pwd1 = line.Substring(line.LastIndexOf("="), (line.Length - line.LastIndexOf("=")));
                   Console.WriteLine(pwd1);
                  // pwd1 = line.Substring(line.IndexOf("=")+1, line.Length);
               }
            }
            if (string.IsNullOrEmpty(userName1) || string.IsNullOrEmpty(pwd1))
            {
                return -2;
            }
            bool uNameB=SimpleHash.ComparePasswords("jak", user, userName1);
            bool uPwdB = SimpleHash.ComparePasswords("jak", pwd, pwd1);

            if (!uNameB || !uPwdB)
                return -3;

          return 0;
        }


    }

    
}
