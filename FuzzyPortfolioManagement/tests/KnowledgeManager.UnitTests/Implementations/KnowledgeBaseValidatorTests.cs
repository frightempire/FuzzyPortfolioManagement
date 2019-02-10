﻿using System;
using System.Collections.Generic;
using KnowledgeManager.Implementations;
using KnowledgeManager.Interfaces;
using LinguisticVariableParser.Entities;
using MembershipFunctionParser.Entities;
using MembershipFunctionParser.Implementations;
using NUnit.Framework;
using ProductionRuleParser.Entities;
using ProductionRuleParser.Enums;
using Rhino.Mocks;

namespace KnowledgeManager.UnitTests.Implementations
{
    [TestFixture]
    public class KnowledgeBaseValidatorTests
    {
        private IImplicationRuleManager _implicationRuleManagerMock;
        private ILinguisticVariableManager _linguisticVariableManagerMock;
        private KnowledgeBaseValidator _knowledgeBaseValidator;

        [SetUp]
        public void SetUp()
        {
            _implicationRuleManagerMock = MockRepository.GenerateMock<IImplicationRuleManager>();
            _linguisticVariableManagerMock = MockRepository.GenerateMock<ILinguisticVariableManager>();
            _knowledgeBaseValidator = new KnowledgeBaseValidator(_implicationRuleManagerMock, _linguisticVariableManagerMock);
        }

        [Test]
        public void ValidateLinguisticVariablesNames_ThrowsArgumentExceptionIfOneOfVariablesInImplicationRulesIsNotKnownToKnowledgeBase()
        {
            // Arrange
            List<ImplicationRule> implicationRules = PrepareImplicationRules();
            _implicationRuleManagerMock.Expect(i => i.ImplicationRules).Return(implicationRules);
            List<LinguisticVariable> linguisticVariables = PrepareLinguisticVariables();
            _linguisticVariableManagerMock.Expect(l => l.LinguisticVariables).Return(linguisticVariables);
            string exceptionMessage = "Knowledge base: one of linguistic variables in implication rule is unknown to linguistic variable base";

            // Act & Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                _knowledgeBaseValidator.ValidateLinguisticVariablesNames();
            });
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        private List<LinguisticVariable> PrepareLinguisticVariables()
        {
            // Water variable
            MembershipFunctionList firstMembershipFunctionList = new MembershipFunctionList
            {
                new TrapezoidalMembershipFunction("Cold", 0, 20, 20, 30),
                new TrapezoidalMembershipFunction("Hot", 50, 60, 60, 80)
            };
            LinguisticVariable firstLinguisticVariable =
                new LinguisticVariable("Water", firstMembershipFunctionList, isInitialData: true);

            // Pressure vatiable
            MembershipFunctionList secondsMembershipFunctionList = new MembershipFunctionList
            {
                new TrapezoidalMembershipFunction("Low", 20, 50, 50, 60),
                new TrapezoidalMembershipFunction("Medium", 60, 65, 65, 80),
                new TrapezoidalMembershipFunction("High", 80, 100, 100, 150)
            };
            LinguisticVariable secondLinguisticVariable =
                new LinguisticVariable("Pressure", secondsMembershipFunctionList, isInitialData: false);

            List<LinguisticVariable> linguisticVariables = new List<LinguisticVariable>
            {
                firstLinguisticVariable,
                secondLinguisticVariable
            };
            return linguisticVariables;
        }

        private List<ImplicationRule> PrepareImplicationRules()
        {
            // IF(Water IS Cold) THEN (Pressure IS Low)
            ImplicationRule firstImplicationRule = new ImplicationRule(
                new List<StatementCombination>
                {
                    new StatementCombination(new List<UnaryStatement>
                    {
                        new UnaryStatement("Water", ComparisonOperation.Equal, "Cold")
                    })
                },
                new StatementCombination(new List<UnaryStatement>
                {
                    new UnaryStatement("Pressure", ComparisonOperation.Equal, "Low")
                }));

            // IF(Water IS Hot AND Air IS Cold) THEN (Pressure IS Medium)
            ImplicationRule secondImplicationRule = new ImplicationRule(
                new List<StatementCombination>
                {
                    new StatementCombination(new List<UnaryStatement>
                    {
                        new UnaryStatement("Water", ComparisonOperation.Equal, "Hot"),
                        new UnaryStatement("Air", ComparisonOperation.Equal, "Cold")
                    })
                },
                new StatementCombination(new List<UnaryStatement>
                {
                    new UnaryStatement("Pressure", ComparisonOperation.Equal, "Medium")
                }));

            List<ImplicationRule> implicationRules = new List<ImplicationRule>
            {
                firstImplicationRule,
                secondImplicationRule
            };
            return implicationRules;
        }
    }
}