﻿using System.Collections.Generic;
using FuzzyExpert.Base.UnitTests;
using FuzzyExpert.Infrastructure.MembershipFunctionParsing.Entities;
using NUnit.Framework;

namespace FuzzyExpert.Infrastructure.UnitTests.MembershipFunctionParsing.Implementations
{
    [TestFixture]
    public class MembershipFunctionParserTests
    {
        private FuzzyExpert.Infrastructure.MembershipFunctionParsing.Implementations.MembershipFunctionParser _membershipFunctionParser;

        [SetUp]
        public void SetUp()
        {
            _membershipFunctionParser = new FuzzyExpert.Infrastructure.MembershipFunctionParsing.Implementations.MembershipFunctionParser();
        }

        [Test]
        public void ParseMembershipFunctions_ReturnsCorrectListOfMembershipFunctionStrings()
        {
            // Arrange
            string membershipFunctionPart = "Cold:Trapezoidal:(0,20,20,30)|Warm:Trapezoidal:(40,40,50,50)|Hot:Triangular:(50,60,70)";
            List<MembershipFunctionStrings> expectedMembershipFunctionStrings = new List<MembershipFunctionStrings>
            {
                new MembershipFunctionStrings("Cold", "Trapezoidal", new List<double> {0, 20, 20, 30}),
                new MembershipFunctionStrings("Warm", "Trapezoidal", new List<double> {40, 40, 50, 50}),
                new MembershipFunctionStrings("Hot", "Triangular", new List<double> {50, 60, 70})
            };

            // Act
            List<MembershipFunctionStrings> actualMembershipFunctionStrings = 
                _membershipFunctionParser.ParseMembershipFunctions(membershipFunctionPart);

            // Assert
            Assert.AreEqual(expectedMembershipFunctionStrings.Count, actualMembershipFunctionStrings.Count);
            for (int i = 0; i < expectedMembershipFunctionStrings.Count; i++)
            {
                Assert.IsTrue(ObjectComparer.MembershipFunctionStringsAreEqual(expectedMembershipFunctionStrings[i], actualMembershipFunctionStrings[i]));
            }
        }
    }
}