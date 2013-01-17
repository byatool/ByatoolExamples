using Byatool.Functional.ToSql.Persist;
using Byatool.Functional.ToSql.Persist.Section;
using FluentAssertions;
using NUnit.Framework;

namespace Byatool.Functional.Test.SqlTest.PersistTest.SectionTest
{
    public class WhenCreatingAnAlso
    {

        #region Fields

        #endregion

        #region Support Methods

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void TheAndIsPreserved()
        {
            const string name = "First";
            Also.And(name.IsEqualTo(1)).Should().Match((WhereItem x) => x.Name == name && ((int)x.Value) == 1 && x.WhereType == WhereType.And);
        }

        [Test]
        public void TheOrIsPreserved()
        {
            const string name = "First";
            Also.Or(name.IsEqualTo(1)).Should().Match((WhereItem x) => x.Name == name && ((int)x.Value) == 1 && x.WhereType == WhereType.Or);
        }

        #endregion
    }
}