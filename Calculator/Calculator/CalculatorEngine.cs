namespace Calculator
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using System.Reflection;

    public class CalculatorEngine : ICalculatorEngine
    {

        private Dictionary<string, EncapsulatedDelegates> _operations
            = new Dictionary<string, EncapsulatedDelegates>();

        public double PerformOperation(Operation operation)
        {
            var operationSign = operation.Sign;

            if(!_operations.TryGetValue(operationSign, out EncapsulatedDelegates aOperation))
            {
                throw new NotFoundOperationException();
            }

            if (!EncapsulatedDelegates.Accordance.TryGetValue(
                operation.Parameters.Length, out Type aType))
            {
                throw new ParametersCountMismatchException();
            }

            var theOperation = aOperation.GetDelegate(aType);

            //switch (operation.Parameters.Length)
            //{
            //    case 1:
            //        return (double)theOperation.DynamicInvoke(operation.Parameters[0]);
            //    case 2:
            //        return (double)theOperation.DynamicInvoke(operation.Parameters[0], operation.Parameters[1]);
            //    case 3:
            //        return (double)theOperation.DynamicInvoke(operation.Parameters[0], operation.Parameters[1], operation.Parameters[2]);
            //}

            // не работает
            //switch (aType)
            //{
            //    case typeof(Func<double, double, double, double>).GetType():
            //        return (double)theOperation.DynamicInvoke(operation.Parameters[0]);
            //}
            try
            {
                if (aType == typeof(Func<double, double, double, double>))
                {
                    return (double)theOperation.DynamicInvoke(operation.Parameters[0], operation.Parameters[1], operation.Parameters[2]);
                }
                else if (aType == typeof(Func<double, double, double>))
                {
                    return (double)theOperation.DynamicInvoke(operation.Parameters[0], operation.Parameters[1]);
                }
                else
                {
                    return (double)theOperation.DynamicInvoke(operation.Parameters[0]);
                }
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        public void DefineOperation(string sign, Func<double, double, double, double> body) => DefineAnyOperation(sign, body);

        public void DefineOperation(string sign, Func<double, double, double> body) => DefineAnyOperation(sign, body);

        public void DefineOperation(string sign, Func<double, double> body) => DefineAnyOperation(sign, body);

        private void DefineAnyOperation(string sign, Delegate body)
        {
            if (!_operations.ContainsKey(sign))
            {
                _operations.Add(sign, new EncapsulatedDelegates(body.GetType(), body));
                return;
            }
            else
            {
                _operations[sign].AddDelegate(body.GetType(), body);
            }
        }
    }
}



// Сейчас наш калькулятор знает три операции. 
// Необходимо добавить возможность “обучения” калькулятора новым операциям. 
// Пример обучения есть в классе Program.cs  
// Очевидно, что от Switch-а придется избавиться. 
// Достойной альтернативой Switch-у может быть, например, 
// словарь в котором ключём будет строка (знак операции), 
// а значением будет делегат или лямбда-выражение.
//
//  Переработайте метод PerformOperation()
//
// предлагаемая реализация с помощью cловаря (словарей):
// ищем знак операции в словарях 
// если находим, выполняем найденную лямбду с помощью параметров,
//   передаваемых в operation
//
// Если что-то пойдет не так, не забудьте сгенерировать 
// соответствующее исключение из папки Exceptions 
//
// Обратите внимание на юнит-тесты для этого класса

// реализуйте методы DefineOperation().
// метод должен добавить новую операцию в калькулятор
//
// предлагаемая реализация с помощью cловаря (словарей):
//  - проверка на существование операции
//  - добавление новой операции в словарь
// Если что-то пойдет не так, не забудьте сгенерировать 
// соответствующее исключение из папки Exceptions 
//
// Обратите внимание на юнит-тесты для этого класса