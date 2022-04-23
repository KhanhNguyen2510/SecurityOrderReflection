using System;
using System.Text;
using System.Threading;

namespace SOR.Application
{
    public static class AutoGenerate
    {
        private static Random random = new Random();
        public static StringBuilder RandomString(int X)
        {
            var random = new Random();
            var rString = new StringBuilder();

            for (int i = 1; i <= X; i++)
            {
                int rNumber = random.Next(0, 3);
                var r = ((char)(random.Next(1, 26) + 64)).ToString();

                switch (rNumber)
                {
                    case 0:
                        rString.Append(r.ToUpper());
                        break;
                    case 1:
                        rString.Append(r.ToLower());
                        break;
                    case 2:
                        rString.Append(random.Next(0, 9));
                        break;
                }
            }
            return rString;
        }

      
    }
}
