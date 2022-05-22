using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    class ValidatorLauncher
    {
        static void Main(string[] args)
        {
            //prepareInspectorAll();
            Validator validator = new Validator();

            while (true)
            {
                string line = Console.ReadLine();

                //Console.WriteLine("ID : {0}", string.IsNullOrEmpty(line));
                if (string.IsNullOrEmpty(line))
                {
                    Console.WriteLine("EOF");

                    break;
                }

                string[] words = line.Split(' ');
                string id = words[0];
                string password = words[1];
                //Console.WriteLine(QCreate(qname, int.Parse(words[2])));
                Console.WriteLine("ID : {0}, Password : {1}", id, password);

                //string enc_password = CardUtility.passwordEncryption_SHA256(password);
                //Console.WriteLine("enc_password : {0}", enc_password);
                if (validator.CheckIdPsw(id, password))
                {
                    Console.WriteLine("LOGIN SUCCESS");
                    break;
                }
                else
                {
                    Console.WriteLine("LOGIN FAIL");
                }
            }
        }

        static Dictionary<string, string> inspector_all;
        static void prepareInspectorAll(string filename = "..\\CLIENT\\INSPECTOR.TXT")
        {
            Console.WriteLine("filename : {0}", filename);

            string line;
            StreamReader file = new StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(' ');
                string id = words[0];
                string password = words[1];

                Console.WriteLine("ID : {0}, Password(enc) : {1}", id, password);
            }
        }
    }
}
