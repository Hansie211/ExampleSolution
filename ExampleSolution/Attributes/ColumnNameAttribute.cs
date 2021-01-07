using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleSolution.Attributes
{
    public class ColumnNameAttribute : Attribute
    {
        public string Value { get; }

        public ColumnNameAttribute( string value )
        {
            Value = value;
        }
    }
}
