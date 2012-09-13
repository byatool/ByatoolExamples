using System;
using System.Collections.Generic;

namespace Byatool.Functional
{
    public class When<T>
    {
        #region Constructors

        private When(bool condition)
        {
            _condition = condition;
            _calls = new Dictionary<bool, Func<T>> { { false, null }, { true, null } };
        }

        private When(bool condition, Func<T> trueMethod, Func<T> falseMethod)
        {
            _calls = new Dictionary<bool, Func<T>>();
            _calls[false] = falseMethod;
            _calls[true] = trueMethod;

            _condition = condition;
        }
        #endregion

        #region Fields

        private readonly Dictionary<bool, Func<T>> _calls;
        private readonly Boolean _condition;

        #endregion

        #region Methods

        public T Else(Func<T> falseMethod)
        {
            GuardClause.IfNullThrowArgumentException(_calls[true], "");
            GuardClause.IfNullThrowArgumentException(falseMethod, "");

            return new When<T>(_condition, _calls[true], falseMethod).Execute();
        }

        public T OtherwiseThrow(Exception exception)
        {
            return new When<T>(_condition, _calls[true], () => { throw exception; }).Execute();
        }

        private T Execute()
        {
            return _calls[_condition]();
        }

        public When<T> Then(Func<T> trueMethod)
        {
            return new When<T>(_condition, trueMethod, _calls[false]);
        }

        public static When<T> True(bool condition)
        {
            return new When<T>(condition);
        }

        #endregion
    }
}