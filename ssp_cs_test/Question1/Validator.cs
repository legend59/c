using System;

namespace Question1
{
	class Validator
	{
		public bool CheckIdPsw(string id, string psw)
        {
            bool bRet = false;

            string[] lines = System.IO.File.ReadAllLines(@"..\\CLIENT\\INSPECTOR.TXT");
            foreach (string line in lines)
            {
                String fileId = line.Substring(0, 8);
                String filePsw = line.Substring(9);

                if (id.Equals(fileId))
                {
                    String encPsw = CardUtility.passwordEncryption_SHA256(psw);
                    if (encPsw.Equals(filePsw))
                    {
                        bRet = true;
                        break;
                    }
                }
            }

            return bRet;
        }
    }
}