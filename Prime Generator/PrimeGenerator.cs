using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prime_Generator
{
    public class PrimeGenerator
    {
        ulong currentPrime = 0;

        public ulong NextPrime()
        {
            if (currentPrime == 0)
                return (currentPrime = 2);

            // if current is 2, start loop at 3
            // else start at current + 2
            for (ulong n = (currentPrime == 2 ? 3 : currentPrime + 2); ; n += 2)
            {
                if (isPrime(n))
                {
                    return (currentPrime = n);
                }
            }
        }

        public void Reset()
        {
            currentPrime = 0;
        }

        static bool isPrime(ulong n)
        {
            if (n != 2 && n % 2 == 0)
                return false;

            for (ulong i = 3; i * i <= n; i += 2)
                if (n % i == 0)
                    return false;
            return true;
        }
    }
}
