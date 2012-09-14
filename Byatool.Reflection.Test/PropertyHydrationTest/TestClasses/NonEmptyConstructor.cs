namespace Byatool.Reflection.Test.PropertyHydrationTest.TestClasses
{
    public class NonEmptyConstructor
    {
        public string Test { get; set; }
        public int Test2 { get; set; }
        public ChildClass ChildClass { get; set; }

        public NonEmptyConstructor(string test, int test2, ChildClass childClass)
        {
            Test = test;
            Test2 = test2;
            ChildClass = childClass;
        }
    }
}