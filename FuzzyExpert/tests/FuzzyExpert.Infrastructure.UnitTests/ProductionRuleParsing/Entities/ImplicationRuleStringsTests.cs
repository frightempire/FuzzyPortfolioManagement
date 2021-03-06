﻿using System;
using FuzzyExpert.Infrastructure.ProductionRuleParsing.Entities;
using NUnit.Framework;

namespace FuzzyExpert.Infrastructure.UnitTests.ProductionRuleParsing.Entities
{
    [TestFixture]
    public class ImplicationRuleStringsTests
    {
        private ImplicationRuleStrings _implicationRuleStrings;
        private readonly string _ifStatement = "A=5|B=10";
        private readonly string _thenStatement = "C=100";

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void Constructor_ThrowsArgumentNullExceptionIfIfStatementIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ImplicationRuleStrings(null, _thenStatement));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullExceptionIfIfStatementIsEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ImplicationRuleStrings(string.Empty, _thenStatement));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullExceptionIfThenStatementIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ImplicationRuleStrings(_ifStatement, null));
        }

        [Test]
        public void Constructor_ThrowsArgumentNullExceptionIfThenStatementIsEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ImplicationRuleStrings(_ifStatement, string.Empty));
        }

        [Test]
        public void IfStatement_GetterReturnsValue()
        {
            // Arrange
            _implicationRuleStrings = new ImplicationRuleStrings(_ifStatement, _thenStatement);

            // Act
            string actualIfStatement = _implicationRuleStrings.IfStatement;

            // Assert
            Assert.AreEqual(_ifStatement, actualIfStatement);
        }

        [Test]
        public void ThenStatement_GetterReturnsValue()
        {
            // Arrange
            _implicationRuleStrings = new ImplicationRuleStrings(_ifStatement, _thenStatement);

            // Act
            string actualThenStatement = _implicationRuleStrings.ThenStatement;

            // Assert
            Assert.AreEqual(_thenStatement, actualThenStatement);
        }
    }
}