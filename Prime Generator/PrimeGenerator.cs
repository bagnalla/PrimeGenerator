using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prime_Generator
{
    public class PrimeGenerator
    {
        public ulong CurrentPrime { get; private set; }

        public PrimeGenerator() { CurrentPrime = 2; }

        public ulong NextPrime()
        {
            // if current is 2, start loop at 3
            // else start at current + 2
            for (ulong n = (CurrentPrime == 2 ? 3 : CurrentPrime + 2); ; n += 2)
            {
                if (isPrime(n))
                {
                    return (CurrentPrime = n);
                }
            }
        }

        public void Reset()
        {
            CurrentPrime = 2;
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

        public static IEnumerable<ulong> FirstNPrimes(ulong n)
        {
            if (n == 0)
                yield break;

            yield return 2;

            if (n == 1)
                yield break;

            ulong count = 1;

            for (ulong i = 3; ; i += 2)
            {
                if (isPrime(i))
                {
                    yield return i;
                    if (++count == n)
                        yield break;
                }
            }
        }
    }
}
