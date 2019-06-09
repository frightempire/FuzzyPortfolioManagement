﻿using MembershipFunctionParser.Entities;

namespace MembershipFunctionParser.UnitTests.TestEntities
{
    public class StubMembershipFunction: MembershipFunction
    {
        public StubMembershipFunction(string linguisticVariableName) : base(linguisticVariableName) { }

        public override double MembershipDegree(double value) => 0;
    }
}