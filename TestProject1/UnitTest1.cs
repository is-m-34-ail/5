using NUnit.Framework;
using System;

namespace WpfApp1.Tests
{
    [TestFixture]
    public class IntegralCalculatorTests
    {
        private readonly Func<double, double> _testFunction = x => x * x;

        [Test]
        public void TrapezoidalCalculator_LinearFunction_ReturnsExactResult()
        {
            // Arrange
            var calculator = new TrapezoidalIntegralCalculator(x => 2 * x);
            double a = 0, b = 1;
            int n = 100;
            double expected = 1.0;

            // Act
            double result = calculator.Calculate(a, b, n);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void SimpsonCalculator_QuadraticFunction_ReturnsExactResult()
        {
            // Arrange
            var calculator = new SimpsonIntegralCalculator(x => x * x);
            double a = 0, b = 1;
            int n = 100;
            double expected = 1.0 / 3.0;

            // Act
            double result = calculator.Calculate(a, b, n);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void SimpsonCalculator_OddNumberOfIntervals_AutomaticallyMakesEven()
        {
            // Arrange
            var calculator = new SimpsonIntegralCalculator(_testFunction);
            double a = 0, b = 1;
            int oddN = 101;

            // Act
            double result = calculator.Calculate(a, b, oddN);

            // Assert
            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void BothCalculators_SameInput_ReturnSimilarResults()
        {
            // Arrange
            var trapezCalculator = new TrapezoidalIntegralCalculator(_testFunction);
            var simpsonCalculator = new SimpsonIntegralCalculator(_testFunction);
            double a = 0, b = 2;
            int n = 1000;
            double expected = 8.0 / 3.0;

            // Act
            double trapResult = trapezCalculator.Calculate(a, b, n);
            double simpResult = simpsonCalculator.Calculate(a, b, n);

            // Assert
            double tolerance = 0.001;
            Assert.That(trapResult, Is.EqualTo(expected).Within(tolerance));
            Assert.That(simpResult, Is.EqualTo(expected).Within(tolerance));
        }

        [Test]
        public void Calculators_ZeroInterval_ReturnsZero()
        {
            // Arrange
            var trapezCalculator = new TrapezoidalIntegralCalculator(_testFunction);
            var simpsonCalculator = new SimpsonIntegralCalculator(_testFunction);
            double a = 1, b = 1;
            int n = 100;
            double expected = 0;

            // Act
            double trapResult = trapezCalculator.Calculate(a, b, n);
            double simpResult = simpsonCalculator.Calculate(a, b, n);

            // Assert
            Assert.That(trapResult, Is.EqualTo(expected));
            Assert.That(simpResult, Is.EqualTo(expected));
        }

        [Test]
        public void Calculators_ReverseInterval_ReturnsNegativeResult()
        {
            // Arrange
            var trapezCalculator = new TrapezoidalIntegralCalculator(_testFunction);
            var simpsonCalculator = new SimpsonIntegralCalculator(_testFunction);
            double a = 2, b = 0;
            int n = 100;
            double expected = -8.0 / 3.0;

            // Act
            double trapResult = trapezCalculator.Calculate(a, b, n);
            double simpResult = simpsonCalculator.Calculate(a, b, n);

            // Assert
            double tolerance = 0.001;
            Assert.That(trapResult, Is.EqualTo(expected).Within(tolerance));
            Assert.That(simpResult, Is.EqualTo(expected).Within(tolerance));
        }

        [Test]
        public void TrapezoidalCalculator_LargeNumberOfIntervals_ImprovesAccuracy()
        {
            // Arrange
            var calculator = new TrapezoidalIntegralCalculator(x => Math.Sin(x));
            double a = 0, b = Math.PI;
            int smallN = 10;
            int largeN = 1000;
            double expected = 2.0; // ∫sin(x) dx from 0 to π = 2

            // Act
            double smallResult = calculator.Calculate(a, b, smallN);
            double largeResult = calculator.Calculate(a, b, largeN);

            // Assert
            double smallError = Math.Abs(smallResult - expected);
            double largeError = Math.Abs(largeResult - expected);
            Assert.That(largeError, Is.LessThan(smallError));
        }

        [Test]
        public void SimpsonCalculator_WithComplexFunction_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = new SimpsonIntegralCalculator(x => Math.Exp(x));
            double a = 0, b = 1;
            int n = 100;
            double expected = Math.Exp(1) - 1; // ∫e^x dx from 0 to 1 = e - 1

            // Act
            double result = calculator.Calculate(a, b, n);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }
    }
}


