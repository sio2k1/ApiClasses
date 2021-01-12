using System;
using System.Collections.Generic;
using System.Text;

namespace Workers.core
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ImportableAttribute : System.Attribute
    {
        private string name;
        public double version;
        public ImportableAttribute()
        {

        }
    }
}
