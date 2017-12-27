using System;

namespace FractionMultiplication
{


    class Program
    {
        static void Main(string[] args)
        {
            bool isSameSign = false;
            Console.WriteLine("Enter the input numbers ");
            Console.WriteLine("Enter first number is number1: ");
            double firstInputNumber = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter second number is number2: ");
            double secondInputNumber = Convert.ToDouble(Console.ReadLine());
            if ((firstInputNumber < 0 && secondInputNumber < 0) || (firstInputNumber > 0 && secondInputNumber > 0))
            {
                isSameSign = true;
            }
            else
            {
                isSameSign = false;
            }
            firstInputNumber = Math.Abs(firstInputNumber);
            secondInputNumber = Math.Abs(secondInputNumber);
            int realPartFirstNumber = (int)firstInputNumber;
            int realPartSecondNumber = (int)secondInputNumber;
            float fractionalPartFirstNumber = (float)firstInputNumber - realPartFirstNumber;
            float fractionalPartSecondNumber = (float)secondInputNumber - realPartSecondNumber;
            FloatToBinaryConversion floatToBinaryObject = new FloatToBinaryConversion();
            string binaryOfFirstNumber = floatToBinaryObject.ConversionRealPartToBinary(realPartFirstNumber) + "." + floatToBinaryObject.ConversionFractionalPartToBinary(fractionalPartFirstNumber, (float)firstInputNumber);
            string binaryOfSecondNumber = floatToBinaryObject.ConversionRealPartToBinary(realPartSecondNumber) + "." + floatToBinaryObject.ConversionFractionalPartToBinary(fractionalPartSecondNumber, (float)secondInputNumber);
            MultiplicationOfTwoNumbers multiplicationObject = new MultiplicationOfTwoNumbers();
            string reverseBinaryOfSecondNumber = null;
            string reverseBinaryOfFirstNumber = null;
            for (int i = binaryOfFirstNumber.Length - 1; i >= 0; i--)
            {
                reverseBinaryOfFirstNumber = reverseBinaryOfFirstNumber + binaryOfFirstNumber[i];
            }
            for (int i = binaryOfSecondNumber.Length - 1; i >= 0; i--)
            {
                reverseBinaryOfSecondNumber = reverseBinaryOfSecondNumber + binaryOfSecondNumber[i];
            }
            string tempResult = multiplicationObject.Multiplication(reverseBinaryOfFirstNumber, reverseBinaryOfSecondNumber);
            string finalResult = null;
            for (int i = tempResult.Length - 1; i >= 0; i--)
            {
                finalResult = finalResult + tempResult[i];
            }
            int pre_point = multiplicationObject.PrecisionOfPoint(reverseBinaryOfFirstNumber, reverseBinaryOfSecondNumber);
            BinaryToFloatConversion binaryToFloatObject = new BinaryToFloatConversion();
            float resultantNumber = binaryToFloatObject.ConversionToFloat(finalResult, pre_point);
            Console.WriteLine(" The product of the two numbers are: ");
            if (isSameSign != true)
            {
                resultantNumber = -resultantNumber;
            }
            Console.WriteLine(resultantNumber);
            Console.ReadKey();
        }
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

class MultiplicationOfTwoNumbers
{
    public string Multiplication(string firstNumber, string secondNumber)
    {
        string tempResult = null;
        string result = null;
        int precision1 = 0;
        for (int i = 0; i < firstNumber.Length; i++)
        {
            if (firstNumber[i] == '.')
            {
                precision1 = i;
            }
            else
            {
                for (int j = 0; j < secondNumber.Length; j++)
                {
                    if (firstNumber[i] != secondNumber[j] && secondNumber[j] != '.')
                    {
                        tempResult = tempResult + "0";
                    }
                    else if ((firstNumber[i] == secondNumber[j]) && firstNumber[i] == '1')
                    {
                        tempResult = tempResult + "1";
                    }
                    else if ((firstNumber[i] == secondNumber[j]) && firstNumber[i] == '0')
                    {
                        tempResult = tempResult + "0";
                    }
                }
                if (precision1 < 1)
                    result = Addition(result, tempResult, (i));
                else if (precision1 >= 1)
                    result = Addition(result, tempResult, (i - 1));
                tempResult = null;
            }

        }

        return result;
    }
    public int PrecisionOfPoint(string firstNumber, string secondNumber)
    {
        int totalPrecision = 0;
        int precision1 = 0;
        int precision2 = 0;
        for (int i = 0; i < firstNumber.Length; i++)
        {
            if (firstNumber[i] == '.')
            {
                precision1 = i;
            }
            else
            {
                for (int j = 0; j < secondNumber.Length; j++)
                {
                    if (secondNumber[j] == '.')
                    {
                        precision2 = j;
                    }
                }
            }
        }
        totalPrecision = precision1 + precision2;
        return totalPrecision;
    }
    public string Addition(string str1, string str2, int left)
    {
        string result = null;
        int i = 0;
        int j = 0;
        int carry = 0;
        while (left > 0 && str1 != null)
        {
            result = result + str1[i];
            i++;
            left--;
        }
        if (str1 != null)
        {
            while (i < str1.Length)
            {
                if (str1[i] == '1' && str2[j] == '1' && carry == 0)
                {
                    result = result + "0";
                    carry = 1;
                    i++;
                    j++;
                }
                else if (str1[i] == '1' && str2[j] == '1' && carry == 1)
                {
                    result = result + "1";
                    carry = 1;
                    i++;
                    j++;
                }
                else if (str1[i] != str2[j] && carry == 1)
                {
                    result = result + "0";
                    carry = 1;
                    i++;
                    j++;
                }
                else if (str1[i] != str2[j] && carry == 0)
                {
                    result = result + "1";
                    carry = 0;
                    i++;
                    j++;
                }
                else if (str1[i] == '0' && str2[j] == '0')
                {
                    result = result + Convert.ToString(carry);
                    carry = 0;
                    i++;
                    j++;
                }
            }
        }
        while (j < str2.Length)
        {
            if (str2[j] == '1' && carry == 1)
            {
                result = result + "0";
                carry = 1;
                j++;
            }
            else if (str2[j] == '1' && carry == 0)
            {
                result = result + "1";
                carry = 0;
                j++;
            }
            else if (str2[j] == '0')
            {
                result = result + Convert.ToString(carry);
                carry = 0;
                j++;
            }
        }
        if (carry == 1)
            result = result + Convert.ToString(carry);
        return result;
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

