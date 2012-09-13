using System;
using System.Collections.Generic;
using System.Linq;

namespace Byatool.Functional
{
    public class Match<T, TReturn>
    {
        #region Constructors

        public Match(T testAgainst)
            : this(testAgainst, new Dictionary<T, Func<TReturn>>(), null)
        {
        }

        private Match(T testAgainst, IDictionary<T, Func<TReturn>> methodsToRun, Func<TReturn> theDefault)
        {
            TestAgainst = testAgainst;
            MethodsToRun = methodsToRun;
            TheDefault = theDefault;
        }

        #endregion

        #region Methods

        public TReturn Default(TReturn theDefault)
        {
            return new Match<T, TReturn>(TestAgainst, MethodsToRun, () => theDefault).Go();
        }

        public TReturn Go()
        {
            var foundPair = MethodsToRun.Where(item => item.Key.Equals(TestAgainst));

            return
                When<TReturn>
                    .True(foundPair.Any())
                    .Then(() => foundPair.First().Value())
                    .Else(() => TheDefault());

        }

        public Match<T, TReturn> When(T matchAgainst, Func<TReturn> theReturn)
        {
            var newList = MethodsToRun.ToDictionary(x => x.Key, y => y.Value);

            newList.Add(matchAgainst, theReturn);
            return new Match<T, TReturn>(TestAgainst, newList, TheDefault);
        }

        #endregion

        #region Properties

        private IDictionary<T, Func<TReturn>> MethodsToRun { get; set; }
        private T TestAgainst { get; set; }
        public Func<TReturn> TheDefault { get; set; }

        #endregion
    }
}