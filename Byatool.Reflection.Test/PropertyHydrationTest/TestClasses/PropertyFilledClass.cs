using System;
using System.Collections.Generic;

namespace Byatool.Reflection.Test.PropertyHydrationTest.TestClasses
{
    public class PropertyFilledClass
    {
        public bool BoolProperty { set; get; }
        public DateTime DateTimeProperty { get; set; }
        public DateTime? DateTimeNullableProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public int IntProperty { get; set; }
        public long LongProperty { get; set; }
        public short ShortProperty { get; set; }
        public string StringProperty { get; set; }

        public bool HasNoSetter { get { return 1 == 1; } }

        public IList<ChildClass> Children { get; set; }

        public ChildClass ChildClass { get; set; }
    }
}