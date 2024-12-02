using TaskManagement.Utils.CustomMaths;

namespace TaskManagement.Test
{
    public class Tests
    {
        [TestCase(1, 1, 1)]
        public void MultiplyNumbers_ShouldReturn_double(double numberOne, double numberTwo, double expectedResult)
        {
            double result = MathBasic.MultiplyNumbers(numberOne, numberTwo);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 1, 2)]
        public void SumNumbers_ShouldReturn_double(double numberOne, double numberTwo, double expectedResult)
        {
            double result = MathBasic.SumNumbers(numberOne, numberTwo);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(2, 1, 2)]
        public void DivideNumbers_ShouldReturn_double(double numberOne, double numberTwo, double expectedResult)
        {
            double result = MathBasic.DivideNumbers(numberOne, numberTwo);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(2, 1, 1)]
        public void SubtractNumbers_shouldReturn_double(double numberOne, double numberTwo, double expectedResult)
        {
            double result = MathBasic.SubtractNumbers(numberOne, numberTwo);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(10, 5, 0)]
        public void RestOfDivision_shouldReturn_double(double numberOne, double numberTwo, double expectedResult)
        {
            double result = MathBasic.RestOfDivision(numberOne, numberTwo);

            Assert.AreEqual(expectedResult, result);
        }
    }
}