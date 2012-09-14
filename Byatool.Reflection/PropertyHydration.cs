using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Byatool.Functional;
using Byatool.Shared;

namespace Byatool.Reflection
{
    public class PropertyHydration
    {
        #region Fields

        private const BindingFlags BindingFlagsForInfoSearch = BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        private readonly IDictionary<Type, Func<dynamic>> _typeToValue;

        #endregion

        #region Constructors

        public PropertyHydration()
        {
            _typeToValue = new Dictionary<Type, Func<dynamic>>
                               {
                                   {typeof (bool), () => RandomTool.CreateABoolean() },
                                   {typeof (DateTime), () => DateTime.Now },
                                   {typeof (DateTime?), () => DateTime.Now },
                                   {typeof (decimal), () => RandomTool.CreateADecimal() },
                                   {typeof (long), () => RandomTool.CreateAnInt64() },
                                   {typeof (int), () => RandomTool.CreateAnInt32() },
                                   {typeof (short), () => RandomTool.CreateAnInt16() },
                                   {typeof (string), () => RandomTool.CreateAString() },
                               };
        }

        #endregion

        #region Support Methods

        private Func<ParameterInfo, dynamic> ADefaultValueForAnyValueTypes()
        {
            return item =>
                   _typeToValue.ContainsKey(item.ParameterType)
                       ? GetARandomValueForTheValueType(item.ParameterType)
                       : CreateAnObjectIfItIsNotGeneric(item.ParameterType);
        }

        private object CreateAnInstanceUsingTheConstructorWithTheMostParameters(Type info, ConstructorInfo[] constructors)
        {
            var fullList =
                constructors
                    .First(WhereTheConstructorHasTheMostParameters(constructors))
                    .GetParameters()
                    .Select(ADefaultValueForAnyValueTypes())
                    .ToArray();

            return Activator.CreateInstance(info, fullList);
        }

        private object CreateAnObjectIfItIsNotGeneric(Type info)
        {
            return When<object>
                .True(!info.IsGenericType)
                .Then(() =>
                          {
                              var constructors = info.GetConstructors();

                              var createdChild =
                                  When<object>
                                      .True(ThatThereAreConstructorsWithParameters(constructors))
                                      .Then(() => CreateAnInstanceUsingTheConstructorWithTheMostParameters(info, constructors))
                                      .Else(() => CreateTheInstanceUsingTheDefaultConstructor(info));

                              return createdChild;
                          })
                .Else(() => null);
        }

        private object CreateTheInstanceUsingTheDefaultConstructor(Type info)
        {
            return CreateAFilled(Activator.CreateInstance(info));
        }

        private dynamic GetARandomValueForTheValueType(Type check)
        {
            return _typeToValue[check]();
        }

        private bool ThatThereAreConstructorsWithParameters(IEnumerable<ConstructorInfo> constructors)
        {
            return constructors.Any(item => item.GetParameters().Any());
        }

        private Func<ConstructorInfo, bool> WhereTheConstructorHasTheMostParameters(IEnumerable<ConstructorInfo> constructors)
        {
            return item => item.GetParameters().Count() == (constructors.Max(innerItem => innerItem.GetParameters().Count()));
        }

        #endregion

        #region Methods

        public T CreateAFilled<T>(T classToReturn) where T : class
        {
            var propertyInfo = classToReturn.GetType().GetProperties(BindingFlagsForInfoSearch);

            foreach (var info in propertyInfo)
            {
                var foundPropertyInfo = classToReturn.GetType().GetProperty(info.Name, BindingFlagsForInfoSearch);

                var createdValue =
                    When<dynamic>
                        .True(_typeToValue.ContainsKey(info.PropertyType))
                        .Then(() => GetARandomValueForTheValueType(info.PropertyType))
                        .Else(() => CreateAnObjectIfItIsNotGeneric(info.PropertyType));

                if (foundPropertyInfo.CanWrite)
                {
                    foundPropertyInfo.SetValue(classToReturn, createdValue, null);
                }
            }

            return classToReturn;
        }

        public T CreateAFilled<T>() where T : class
        {
            return (T)CreateAnObjectIfItIsNotGeneric(typeof(T));
        }

        #endregion
    }
}