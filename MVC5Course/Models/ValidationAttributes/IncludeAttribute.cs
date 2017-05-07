using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IncludeAttribute : DataTypeAttribute
    {
        private readonly string mustIncludeString;
        public IncludeAttribute(string mustIncludeString) : base(DataType.Text)
        {
            this.mustIncludeString = mustIncludeString;
            ErrorMessage = "必須包含 " + mustIncludeString;
        }
        
        public override bool IsValid(object value)
        {
            var str = (string) value;
            return str.Contains(mustIncludeString);
        }
    }
}