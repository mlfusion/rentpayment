using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Common.Helper
{
    public class Generator
    {
        public static string Password()
        {
            var url =
                "http://passwordsgenerator.net/calc.php?Length=6&Symbols=0&Lowercase=1&Uppercase=1&Numbers=1&Nosimilar=0&Last=503";

            using (System.Net.WebClient objWebClient = new WebClient())
            {
                var data = objWebClient.DownloadString(url);

                data = data.Substring(0, 6);

                return data;
            }
            return null;
        }

        public static string AccessCode()
        {
            using (Entities.RentEntities context = new RentEntities())
            {
                var length = 6;
                var r = "";

                const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                while (0 < length--)
                    res.Append(valid[rnd.Next(valid.Length)]);
                r = res.ToString();

                while (context.UsersAccesses.Any(x => x.AccessCode == r))
                {
                    length = 6;
                    r = "";
                    res.Clear();

                    while (0 < length--)
                        res.Append(valid[rnd.Next(valid.Length)]);

                    r = res.ToString();
                }

                return r;
            }

            return null;
        }
    }
}
