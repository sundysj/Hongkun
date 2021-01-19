using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RandomCodeHelper
    {
        public static String CreateRandomCode(int codeCount)
        {
            try
            {
                string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
                //里面的字符你可以自bai己加啦
                string[] allCharArray = allChar.Split(',');
                string randomCode = "";
                int temp = -1;
                Random random = new Random();
                for (int i = 0; i < codeCount; i++)
                {
                    if (temp != -1)
                    {
                        random = new Random(i * temp * (int)DateTime.Now.Ticks);
                    }
                    int t = random.Next(35);
                    if (temp == t)
                    {
                        return CreateRandomCode(codeCount);
                    }
                    temp = t;
                    randomCode += allCharArray[temp];
                }
                return randomCode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
