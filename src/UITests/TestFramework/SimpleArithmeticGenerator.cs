using System;
using org.mariuszgromada.math.mxparser;

namespace UITests.TestFramework
{
    class SimpleArithmeticGenerator
    {
        /**
         * operators - [addition, subtraction, division, multiplication]
         */
        public static string GenerateExpression(int numOfOperands, bool[] operators)
        {
            string expression = "";
            Random rand = new Random(587);
            int operatorIndex = -1;

            for (int i = 0; i < numOfOperands; i++)
            {

                int num = rand.Next(1, 1000000);
                expression += num.ToString();

                bool validOperator = false;
                while (!validOperator)
                {
                    Random rand2 = new Random();
                    operatorIndex = rand2.Next(4);
                    if (operators[operatorIndex]) // Operator Flag is true
                    {
                        validOperator = true;
                    }
                }

                expression += SelectOperator(operatorIndex);
            }

            expression += rand.Next(1, 1000000).ToString(); // Last Value
            return expression;
        }

        private static string SelectOperator(int value)
        {
            string operand = "";
            switch (value)
            {
                case 0:
                    operand += "+";
                    break;
                case 1:
                    operand += "-";
                    break;
                case 2:
                    operand += ".0/";
                    break;
                case 3:
                    operand += "*";
                    break;
                default:
                    return "";
            }
            return operand;
        }

        public static string ArithmeticEvaluator(string expression)
        {
            Expression el = new Expression(expression);
            return el.calculate().ToString();

        }
    }
}
