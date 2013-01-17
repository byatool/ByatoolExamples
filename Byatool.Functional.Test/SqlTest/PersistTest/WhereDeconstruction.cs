using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Byatool.Functional.ToSql.Persist;
using Byatool.Functional.ToSql.Persist.Section;
using FluentAssertions;

namespace Byatool.Functional.Test.SqlTest.PersistTest
{
    public class WhereDeconstruction
    {
        #region Fields

        protected const BindingFlags BindingFlagsToSeeAll =
                BindingFlags.Static | BindingFlags.FlattenHierarchy |
                BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public;

        #endregion

        #region Constructors

        #endregion

        #region Methods


        public static IList<string> RetrieveTheWhereItemUniqueNames(Where whereToCheck)
        {
            return
                whereToCheck.GetType().GetProperty("WhereItems", BindingFlagsToSeeAll)
                            .GetValue(whereToCheck, BindingFlagsToSeeAll, null, null, null)
                            .As<IList<WhereItem>>()
                            .Select(x => x.UniqueKey)
                            .ToList();
        }



        #endregion

        #region Properties

        #endregion
    }
}