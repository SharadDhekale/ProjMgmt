using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CalculatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Calculator : ICalculator
    {
        public int Addition(int firstNo, int SecondNo)
        {
            return firstNo + SecondNo;
        }

        public int Substraction(int firstNo, int SecondNo)
        {
            return firstNo - SecondNo;
        }

        public int Multiplication(int firstNo, int SecondNo)
        {
            return firstNo * SecondNo;
        }

        public decimal Division(int firstNo, int SecondNo)
        {
            if (SecondNo == 0 || firstNo == 0)
                return 0;
            return firstNo / SecondNo;
        }
    }
}
