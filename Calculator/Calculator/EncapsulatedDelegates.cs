using System;
using System.Collections.Generic;
using Calculator.Exceptions;

internal class EncapsulatedDelegates
{
    Dictionary<Type, Delegate> _delegates = new Dictionary<Type, Delegate>();

    internal EncapsulatedDelegates(Type theType, Delegate theDelegate)
    {
        _delegates.Add(theType, theDelegate);
    }

    internal void AddDelegate(Type theType, Delegate theDelegate)
    {
        if (_delegates.ContainsKey(theType))
        {
            throw new AlreadyExistsOperationException();
        }

        _delegates.Add(theType, theDelegate);
    }

    internal Delegate GetDelegate(Type theType)
    {
        if (!_delegates.TryGetValue(theType, out Delegate theOperation))
        {
            throw new ParametersCountMismatchException();

            // если мы можем найти по ключу элемент справочника _operations из
            // класса CalculatorEngine, значит есть хотя бы один делегат по этому ключу.
            // значит, попытку найти делегат с другим количеством параметров  
            // можно расценивать как ParametersCountMismatchException()
        }

        return theOperation;
    }

    internal static Dictionary<int, Type> Accordance = new Dictionary<int, Type>()
    {
        { 1, typeof(Func<double, double>) },
        { 2, typeof(Func<double, double, double>) },
        { 3, typeof(Func<double, double, double, double>) },
    };
}