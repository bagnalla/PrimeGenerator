using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prime_Generator
{
    public class PrimeGenerator
    {
        public ulong CurrentPrime { get; private set; }
        private readonly List<ulong> _primes = new List<ulong>(); 

        public PrimeGenerator() { CurrentPrime = 2; }

        public ulong NextPrime()
        {
            // if current is 2, start loop at 3
            // else start at current + 2
            for (var n = (CurrentPrime == 2 ? 3 : CurrentPrime + 2); ; n += 2)
            {
                if (IsPrime(n, _primes))
                //if (IsPrime(n))
                {
                    _primes.Add(n);
                    return (CurrentPrime = n);
                }
            }
        }

        public void Reset()
        {
            CurrentPrime = 2;
            _primes.Clear();
            _primes.Add(2);
        }

        public void SetPrimeListCapacity(int n)
        {
            _primes.Capacity = n;
        }

        static bool IsPrime(ulong n)
        {
            if (n != 2 && n % 2 == 0)
                return false;

            for (ulong i = 3; i * i <= n; i += 2)
                if (n % i == 0)
                    return false;
            return true;
        }

        private static bool IsPrime(ulong n, List<ulong> primes)
        {
            //if (n != 2 && n % 2 == 0)
            //    return false;

            for (var i = 0; primes[i] * primes[i] <= n; i++)
            {
                if (n%primes[i] == 0)
                    return false;
            }

            //return primes.TakeWhile(t => t * t <= n).All(t => n % t != 0);

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
                if (!IsPrime(i))
                    continue;

                yield return i;
                if (++count == n)
                    yield break;
            }
        }
    }
}
