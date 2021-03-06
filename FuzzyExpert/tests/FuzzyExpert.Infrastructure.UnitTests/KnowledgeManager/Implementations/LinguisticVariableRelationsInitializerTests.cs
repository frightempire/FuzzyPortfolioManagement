﻿using System.Collections.Generic;
using FuzzyExpert.Application.Entities;
using FuzzyExpert.Base.UnitTests;
using FuzzyExpert.Core.Entities;
using FuzzyExpert.Core.Enums;
using FuzzyExpert.Infrastructure.KnowledgeManager.Implementations;
using NUnit.Framework;

namespace FuzzyExpert.Infrastructure.UnitTests.KnowledgeManager.Implementations
{
    [TestFixture]
    public class LinguisticVariableRelationsInitializerTests
    {
        private LinguisticVariableRelationsInitializer _relationsInitializer;

        [SetUp]
        public void SetUp()
        {
            _relationsInitializer = new LinguisticVariableRelationsInitializer();
        }

        [Test]
        public void FormRelations_ReturnsCorrectRelations()
        {
            // Arrange
            Dictionary<int, ImplicationRule> implicationRules = PrepareImplicationRules();
            Dictionary<int, LinguisticVariable> linguisticVariables = PrepateLinguisticVariables();

            List<LinguisticVariableRelations> expectedRelations = new List<LinguisticVariableRelations>
            {
                new LinguisticVariableRelations(1, new List<string> {"Temperature > HOT"}),
                new LinguisticVariableRelations(2, new List<string> {"Pressure = HIGH"}),
                new LinguisticVariableRelations(3, new List<string> {"Volume >= BIG"}),
                new LinguisticVariableRelations(4, new List<string> {"Color = RED"}),
                new LinguisticVariableRelations(5, new List<string> {"Danger = HIGH"}),
                new LinguisticVariableRelations(6, new List<string> {"Evacuate = TRUE"})
            };

            // Act
            List<LinguisticVariableRelations> actualRelations = _relationsInitializer.FormRelations(implicationRules, linguisticVariables);

            // Assert
            Assert.AreEqual(expectedRelations.Count, actualRelations.Count);
            for (var i = 0; i < actualRelations.Count; i++)
            {
                Assert.IsTrue(ObjectComparer.LinguisticVariableRelationsAreEqual(expectedRelations[i], actualRelations[i]));
            }
        }

        private Dictionary<int, ImplicationRule> PrepareImplicationRules()
        {
            ImplicationRule firstImplicationRule = new ImplicationRule(
                new List<StatementCombination>
                {
                    new StatementCombination(new List<UnaryStatement>
                    {
                        new UnaryStatement("Temperature", ComparisonOperation.Greater, "HOT")
                    })
                },
                new StatementCombination(new List<UnaryStatement>
                {
                    new UnaryStatement("Pressure", ComparisonOperation.Equal, "HIGH")
                }));
            ImplicationRule secondImplicationRule = new ImplicationRule(
                new List<StatementCombination>
                {
                    new StatementCombination(new List<UnaryStatement>
                    {
                        new UnaryStatement("Volume", ComparisonOperation.GreaterOrEqual, "BIG"),
                        new UnaryStatement("Color", ComparisonOperation.Equal, "RED")
                    })
                },
                new StatementCombination(new List<UnaryStatement>
                {
                    new UnaryStatement("Danger", ComparisonOperation.Equal, "HIGH")
                }));
            ImplicationRule thirdImplicationRule = new ImplicationRule(
                new List<StatementCombination>
                {
                    new StatementCombination(new List<UnaryStatement>
                    {
                        new UnaryStatement("Pressure", ComparisonOperation.Equal, "HIGH"),
                        new UnaryStatement("Danger", ComparisonOperation.Equal, "HIGH")
                    })
                },
                new StatementCombination(new List<UnaryStatement>
                {
                    new UnaryStatement("Evacuate", ComparisonOperation.Equal, "TRUE")
                }));

            return new Dictionary<int, ImplicationRule>
            {
                {1, firstImplicationRule},
                {2, secondImplicationRule},
                {3, thirdImplicationRule}
            };

        }

        private Dictionary<int, LinguisticVariable> PrepateLinguisticVariables()
        {
            MembershipFunctionList firstMembershipFunctionList = new MembershipFunctionList
            {
                new TrapezoidalMembershipFunction("Cold", 0, 20, 20, 30),
                new TrapezoidalMembershipFunction("Hot", 50, 60, 60, 80)
            };
            LinguisticVariable firstLinguisticVariable = new LinguisticVariable("Temperature", firstMembershipFunctionList, isInitialData: true);

            MembershipFunctionList secondMembershipFunctionList = new MembershipFunctionList
            {
                new TrapezoidalMembershipFunction("Low", 20, 50, 50, 60),
                new TrapezoidalMembershipFunction("High", 80, 100, 100, 150)
            };
            LinguisticVariable secondLinguisticVariable = new LinguisticVariable("Pressure", secondMembershipFunctionList, isInitialData: false);

            MembershipFunctionList thirdMembershipFunctionList = new MembershipFunctionList
            {
                new TrapezoidalMembershipFunction("Small", 100, 200, 200, 600),
                new TrapezoidalMembershipFunction("Big", 800, 1000, 1000, 1500)
            };
            LinguisticVariable thirdLinguisticVariable = new LinguisticVariable("Volume", thirdMembershipFunctionList, isInitialData: true);

            MembershipFunctionList fourthMembershipFunctionList = new MembershipFunctionList
            {
                new TrapezoidalMembershipFunction("Blue", 5, 10, 10, 20),
                new TrapezoidalMembershipFunction("Red", 50, 60, 60, 80)
            };
            LinguisticVariable fourthLinguisticVariable = new LinguisticVariable("Color", fourthMembershipFunctionList, isInitialData: true);

            MembershipFunctionList fifthMembershipFunctionList = new MembershipFunctionList
            {
                new TrapezoidalMembershipFunction("Low", 5, 10, 10, 20),
                new TrapezoidalMembershipFunction("High", 50, 60, 60, 80)
            };
            LinguisticVariable fifthLinguisticVariable = new LinguisticVariable("Danger", fifthMembershipFunctionList, isInitialData: false);

            MembershipFunctionList sixthMembershipFunctionList = new MembershipFunctionList
            {
                new TrapezoidalMembershipFunction("True", 5, 10, 10, 20),
                new TrapezoidalMembershipFunction("False", 50, 60, 60, 80)
            };
            LinguisticVariable sixthLinguisticVariable = new LinguisticVariable("Evacuate", sixthMembershipFunctionList, isInitialData: false);

            return new Dictionary<int, LinguisticVariable>
            {
                {1, firstLinguisticVariable},
                {2, secondLinguisticVariable},
                {3, thirdLinguisticVariable},
                {4, fourthLinguisticVariable},
                {5, fifthLinguisticVariable},
                {6, sixthLinguisticVariable}
            };
        }
    }
}