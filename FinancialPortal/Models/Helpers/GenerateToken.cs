using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace FinancialPortal.Models.Helpers
{
    public class GenerateToken
    {
        public string GenerateHHToken()
        {
            string finalHHToken = "";
            int[] length = { 7, 4, 4, 4, 13 };
            for(int i = 0; i < length.Length; i++)
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                if(i == length.Length -1)
                {
                    finalHHToken += TokenGenerator(length[i], random);
                }
                else
                {
                    finalHHToken += (TokenGenerator(length[i], random) + "-");
                }
                Thread.Sleep(millisecondsTimeout: 20);
            }
            return finalHHToken;
        }

        public string TokenGenerator(int length, Random random)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}