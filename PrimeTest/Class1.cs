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
            PrimeGenerator primeGenerator = new PrimeGenerator();

            Assert.AreEqual(2, primeGenerator.NextPrime());
            Assert.AreEqual(3, primeGenerator.NextPrime());
            Assert.AreEqual(5, primeGenerator.NextPrime());
            Assert.AreEqual(7, primeGenerator.NextPrime());
            Assert.AreEqual(11, primeGenerator.NextPrime());

            primeGenerator.Reset();

            Assert.AreEqual(2, primeGenerator.NextPrime());
            Assert.AreEqual(3, primeGenerator.NextPrime());
            Assert.AreEqual(5, primeGenerator.NextPrime());
            Assert.AreEqual(7, primeGenerator.NextPrime());
            Assert.AreEqual(11, primeGenerator.NextPrime());

            primeGenerator.Reset();

            for (int i = 0; i < 999; i++)
                primeGenerator.NextPrime();

            Assert.AreEqual(7919, primeGenerator.NextPrime());
        }
    }
}
