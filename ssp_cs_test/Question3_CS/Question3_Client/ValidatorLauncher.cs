using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Question3_Client
{
    class ValidatorLauncher
    {
        static void Main(string[] args)
        {
            Validator validator = new Validator();
            string strId, strPsw, str_busid, str_cardinfo;

            while (true)
            {
                string[] words = Console.ReadLine().Split(' ');

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

            // Inspection
            while (true)
            {
                str_busid = Console.ReadLine();  // busid 

                if (str_busid.Equals("LOGOUT"))
                    break;
                else if (str_busid.Length < 7 || !str_busid.Substring(0, 4).Equals("BUS_"))
                {
                    Console.WriteLine("Wrong Bus ID");
                    continue;
                }

                // Get Start Time
                string strTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                // Card Validation
                while (true)
                {
                    str_cardinfo = Console.ReadLine();
                    if (str_cardinfo.Equals("DONE"))
                    {
                        break;
                    }
                    else if (str_cardinfo.Length < 30)
                    {
                        Console.WriteLine("Wrong Card Info");
                        continue;
                    }
                    validator.InspectCard(strTime, strId, str_busid, str_cardinfo);
                }
            }

            // Send Files to Server
            //validator.SendToServer(strId);
            validator.SendFiles(strId);
        }
    }
}
