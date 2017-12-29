using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloatAddition
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the input numbers ");
            Console.WriteLine("Enter first number is number1: ");
            double firstInputNumber = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter second number is number2: ");
            double secondInputNumber = Convert.ToDouble(Console.ReadLine());
            int detectSameSign = 0;
            if (firstInputNumber > 0 && secondInputNumber > 0)
            {
                detectSameSign = 1;
            }
            else if (firstInputNumber < 0 && secondInputNumber < 0)
            {
                detectSameSign = 2;
            }
            else
            {
                detectSameSign = 3;
            }
            firstInputNumber = Math.Abs(firstInputNumber);
            secondInputNumber = Math.Abs(secondInputNumber);
            int realPartFirstNumber = (int)firstInputNumber;
            int realPartSecondNumber = (int)secondInputNumber;
            float fractionalPartFirstNumber = (float)firstInputNumber - realPartFirstNumber;
            float fractionalPartSecondNumber = (float)secondInputNumber - realPartSecondNumber;
            FloatToBinaryConversion floatToBinaryObject = new FloatToBinaryConversion();
            string binaryOfFirstRealNumber = floatToBinaryObject.ConversionRealPartToBinary(realPartFirstNumber);
            string binaryOfSecondRealNumber = floatToBinaryObject.ConversionRealPartToBinary(realPartSecondNumber);
            string binaryOfFirstFractionalNumber = floatToBinaryObject.ConversionFractionalPartToBinary(fractionalPartFirstNumber, (float)firstInputNumber);
            string binaryOfSecondFractionalNumber = floatToBinaryObject.ConversionFractionalPartToBinary(fractionalPartSecondNumber, (float)secondInputNumber);
            if (binaryOfFirstRealNumber.Length > binaryOfSecondRealNumber.Length)
            {
                binaryOfSecondRealNumber = binaryOfSecondRealNumber.PadLeft(binaryOfFirstRealNumber.Length, '0');
            }
            else
            {
                binaryOfFirstRealNumber = binaryOfFirstRealNumber.PadLeft(binaryOfSecondRealNumber.Length, '0');
            }
            if (binaryOfFirstFractionalNumber.Length > binaryOfSecondFractionalNumber.Length)
            {
                binaryOfSecondFractionalNumber = binaryOfSecondFractionalNumber.PadRight(binaryOfFirstFractionalNumber.Length, '0');
            }
            else
            {
                binaryOfFirstFractionalNumber = binaryOfFirstFractionalNumber.PadRight(binaryOfSecondFractionalNumber.Length, '0');
            }
            int precision = binaryOfFirstFractionalNumber.Length;
            string binaryOfFirstNumber = binaryOfFirstRealNumber + binaryOfFirstFractionalNumber;
            string binaryOfSecondNumber = binaryOfSecondRealNumber + binaryOfSecondFractionalNumber;
            if (detectSameSign == 1 || detectSameSign == 2)
            {
                AdditionOfTwoNumbers additionObject = new AdditionOfTwoNumbers();
                string finalBinary = additionObject.Addition(binaryOfFirstNumber, binaryOfSecondNumber);
                BinaryToFloatConversion binaryToFloatObject = new BinaryToFloatConversion();
                float convertedFinalBinary = binaryToFloatObject.ConversionToFloat(finalBinary, precision);
                if (detectSameSign == 1)
                {
                    Console.WriteLine(convertedFinalBinary);
                }
                else
                {
                    Console.WriteLine("-" + convertedFinalBinary);
                }
            }
            Console.ReadKey();
        }
    }

    class FloatToBinaryConversion
    {
        public string ConversionRealPartToBinary(int number)
        {
            string realBinary = null;
            string realBinary1 = null;
            while (number > 0)
            {
                int binaryDigits = number % 2;
                realBinary = realBinary + Convert.ToString(binaryDigits);
                number = number / 2;
            }
            for (int i = realBinary.Length - 1; i >= 0; i--)
            {
                realBinary1 = realBinary1 + realBinary[i];
            }
            return realBinary1;
        }
        public string ConversionFractionalPartToBinary(float number, float realNumber)
        {
            string fractionalBinary = null;
            int integerPartOfNumber = (int)realNumber;
            if (integerPartOfNumber - realNumber == 0)
            {
                fractionalBinary = "0";
                return fractionalBinary;
            }
            while (number != (int)number)
            {
                int duplicateNumber;
                number = number * 2;
                duplicateNumber = (int)number;
                fractionalBinary = fractionalBinary + Convert.ToString(duplicateNumber);
                number = number - duplicateNumber;
            }
            return fractionalBinary;
        }
    }

    class AdditionOfTwoNumbers
    {
        public string Addition(string firstNumber, string secondNumber)
        {
            string resultantNumber = null;
            int digitSum = 0;
            int sizeOfFirstNumber = firstNumber.Length - 1;
            int sizeOfSecondNumber = secondNumber.Length - 1;
            while (sizeOfFirstNumber >= 0 || sizeOfSecondNumber >= 0 || digitSum == 1)
            {
                digitSum += ((sizeOfFirstNumber >= 0) ? firstNumber[sizeOfFirstNumber] - '0' : 0);
                digitSum += ((sizeOfSecondNumber >= 0) ? secondNumber[sizeOfSecondNumber] - '0' : 0);
                resultantNumber = Convert.ToString(digitSum % 2) + resultantNumber;
                digitSum /= 2;
                sizeOfFirstNumber--;
                sizeOfSecondNumber--;
            }
            return resultantNumber;
        }
    }
    class BinaryToFloatConversion
    {
        public float ConversionToFloat(string binaryNumber, int precisionPoint)
        {
            float resultantNumber = 0;
            int power = (binaryNumber.Length - precisionPoint) - 1;
            for (int i = 0; i < binaryNumber.Length; i++)
            {
                int number = 0;
                if (binaryNumber[i] == '1')
                    number = 1;
                else
                    number = 0;
                resultantNumber = resultantNumber + (number * (float)Math.Pow(2, power));
                power = power - 1;
            }
            return resultantNumber;
        }
    }
}


