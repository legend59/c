using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    class ValidatorLauncher
    {
        static void Main(string[] args)
        {
            Validator validator = new Validator();
            string strId, strPsw;

            while (true)
            {
                string [] words = Console.ReadLine().Split(' ');

                strId = words[0];
                strPsw = words[1]; 

                if (validator.CheckIdPsw(strId, strPsw))
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
    }
}
