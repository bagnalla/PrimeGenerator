using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Prime_Generator;

namespace PrimeTest
{
    [TestFixture]
    public class PrimeTest
    {
        [Test]
        public void GeneratePrimes()
        {
            var primeGenerator = new PrimeGenerator();

            Assert.AreEqual(2, primeGenerator.CurrentPrime);
            Assert.AreEqual(3, primeGenerator.NextPrime());
            Assert.AreEqual(5, primeGenerator.NextPrime());
            Assert.AreEqual(7, primeGenerator.NextPrime());
            Assert.AreEqual(11, primeGenerator.NextPrime());

            primeGenerator.Reset();

            Assert.AreEqual(2, primeGenerator.CurrentPrime);
            Assert.AreEqual(3, primeGenerator.NextPrime());
            Assert.AreEqual(5, primeGenerator.NextPrime());
            Assert.AreEqual(7, primeGenerator.NextPrime());
            Assert.AreEqual(11, primeGenerator.NextPrime());

            primeGenerator.Reset();

            for (var i = 0; i < 999; i++)
                primeGenerator.NextPrime();

            Assert.AreEqual(7919, primeGenerator.CurrentPrime);


            primeGenerator.Reset();

            foreach (var n in PrimeGenerator.FirstNPrimes(1000))
            {
                Assert.AreEqual(primeGenerator.CurrentPrime, n);
                primeGenerator.NextPrime();
            }

            //primeGenerator.Reset();
            //for (var i = 0; i < 999999; i++)
            //    primeGenerator.NextPrime();

            //Assert.AreEqual(primeGenerator.CurrentPrime, PrimeGenerator.FirstNPrimes(1000000).ElementAt(999999));
            //Assert.AreEqual(15485863, primeGenerator.CurrentPrime);
        }
    }
}
