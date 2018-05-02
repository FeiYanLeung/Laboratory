using Laboratory.NetCore.Tests.Models;
using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace Laboratory.NetCore.Tests
{
    [Trait("class", "patient_should")]
    public class PatientShould : IDisposable
    {
        private readonly ITestOutputHelper output;
        public PatientShould(ITestOutputHelper output)
        {
            this.output = output;

            this.output.WriteLine(".ctor");
        }

        /// <summary>
        /// Assert.Boolean
        /// </summary>
        [Fact]
        [Trait("category", "assert.boolean")]
        public void HaveHeartBeatWhenNew()
        {
            this.output.WriteLine("executing HaveHeartBeatWhenNew()");

            var patient = new Patient();
            Assert.True(patient.IsNew);
        }

        /// <summary>
        /// Assert.Equal/ignoreCase
        /// </summary>
        [Fact]
        public void CalculateFullName()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            Assert.Equal("Nick Carter", p.FullName, ignoreCase: true);
        }

        /// <summary>
        /// Assert.StartsWith
        /// </summary>
        [Fact]
        public void CalculateFullNameStartsWithFirstName()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            Assert.StartsWith("Nick", p.FullName);
        }

        /// <summary>
        /// Assert.EndsWith
        /// </summary>
        [Fact]
        public void CalculateFullNameEndsWithFirstName()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            Assert.EndsWith("Carter", p.FullName);
        }

        /// <summary>
        /// Assert.Contains
        /// </summary>
        [Fact]
        public void CalculateFullNameSubstring()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };
            Assert.Contains("ck Ca", p.FullName);
        }

        /// <summary>
        /// Assert.Matches
        /// </summary>
        [Fact]
        public void CalculcateFullNameWithTitleCase()
        {
            var p = new Patient
            {
                FirstName = "Nick",
                LastName = "Carter"
            };

            Debug.Write(p.FullName);

            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", p.FullName);
        }

        /// <summary>
        /// <![CDATA[Assert.Equal<T>]]>
        /// </summary>
        [Fact]
        public void BloodSugarStartWithDefaultValue()
        {
            var p = new Patient();
            Assert.Equal(5.0, p.BloodSugar);
        }

        /// <summary>
        /// Assert.InRange
        /// </summary>
        [Fact]
        public void BloodSugarIncreaseAfterDinner()
        {
            var p = new Patient();

            var haveDinner = new Random().Next(0, 2) == 1;

            if (haveDinner)
            {
                p.HaveDinner();
            }

            // Assert.InRange<float>(p.BloodSugar, 5, 6);
            Assert.InRange(p.BloodSugar, 5, 6);
        }

        public void Dispose()
        {
            this.output.WriteLine("dispose");
        }
    }
}
