using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgebraLibrary.UnitTesting
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void HandlesTrignometryAndDecimalTogether()
        {
            //Arrange
            string exp = "-(3+5.56-.56+cos(90))";
            double expectedResult = -8;
            double actualResult;
            //Act
            ExpressionEvaluator expression = new ExpressionEvaluator();
            actualResult = expression.Evaluate(exp);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void HandlesNegativeSign()
        {
            //Arrange
            string exp = "-4+2";
            double expectedResult = -2;
            double actualResult;
            //Act
            ExpressionEvaluator expression = new ExpressionEvaluator();
            actualResult = expression.Evaluate(exp);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void NestedTrignometry()
        {
            //Arrange
            string exp = "sin(Cos(tan(45)))";
            double expectedResult = 0.01745;
            double actualResult;
            //Act
            ExpressionEvaluator expression = new ExpressionEvaluator();
            actualResult = expression.Evaluate(exp);
            //Assert
            Assert.AreEqual(expectedResult, actualResult, expectedResult);
        }
        [TestMethod]
        public void ExponentialExpression()
        {
            //Arrange
            string exp = "4+5/(3^2)+sin(90)+log(10)";
            double expectedResult = 6.555;
            double actualResult;
            //Act
            ExpressionEvaluator expression = new ExpressionEvaluator();
            actualResult = expression.Evaluate(exp);
            //Assert
            Assert.AreEqual(expectedResult, actualResult, expectedResult);
        }
        [TestMethod]
        public void Log10AndLogE()
        {
            //Arrange
            string exp = "log(10)+ln(10)";
            double expectedResult = 3.30258;
            double actualResult;
            //Act
            ExpressionEvaluator expression = new ExpressionEvaluator();
            actualResult = expression.Evaluate(exp);
            //Assert
            Assert.AreEqual(expectedResult, actualResult,expectedResult);
        }
        [TestMethod]
        public void PercentageTesting()
        {
            //Arrange
            string exp = "sin(30%100)";
            double expectedResult = 0.5;
            double actualResult;
            //Act
            ExpressionEvaluator expression = new ExpressionEvaluator();
            actualResult = expression.Evaluate(exp);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
