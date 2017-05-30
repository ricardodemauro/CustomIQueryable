using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToTerraServerProvider
{
    internal class LocationFinder : ExpressionVisitor
    {
        private Expression expression;
        private List<string> locations;

        public LocationFinder(Expression exp)
        {
            this.expression = exp;
        }

        public List<string> Locations
        {
            get
            {
                if (locations == null)
                {
                    locations = new List<string>();
                    this.Visit(this.expression);
                }
                return this.locations;
            }
        }

        protected override Expression VisitBinary(BinaryExpression be)
        {
            if (be.NodeType == ExpressionType.Equal)
            {
                if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(Place), "Name"))
                {
                    locations.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression(be, typeof(Place), "Name"));
                    return be;
                }
                else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(Place), "State"))
                {
                    locations.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression(be, typeof(Place), "State"));
                    return be;
                }
                else
                    return base.VisitBinary(be);
            }
            else
                return base.VisitBinary(be);
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(String) && m.Method.Name == "StartsWith")
            {
                if (ExpressionTreeHelpers.IsSpecificMemberExpression(m.Object, typeof(Place), "Name") ||
                ExpressionTreeHelpers.IsSpecificMemberExpression(m.Object, typeof(Place), "State"))
                {
                    locations.Add(ExpressionTreeHelpers.GetValueFromExpression(m.Arguments[0]));
                    return m;
                }
            }

            return base.VisitMethodCall(m);
        }
    }
}
