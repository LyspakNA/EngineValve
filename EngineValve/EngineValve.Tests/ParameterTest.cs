using System;
using EngineValveParameters;
using NUnit.Framework;

namespace EngineValve.Tests
{
	[TestFixture]
	public class ParameterTests
	{
		/// <summary>
		/// Объект шаблонного класса для тестов
		/// </summary>
		private Parameter<double> _testParameter
			= new Parameter<double>(ParameterNames.DistanceGroove, 100, 0, 50);

		[TestCase(-1, Description = "Значение максимума меньше минимума")]
		[Test(Description = "Негативный тест на сеттер максимума")]
		public void TestParameterSet_MaxIncorrect(double wrongMax)
		{
			Assert.Throws<Exception>(() => _testParameter.Max = wrongMax,
				"Возникает, если максимальное значение меньше минимального");
		}

		[TestCase(-1, Description = "Значение меньше допустимого")]
		[TestCase(101, Description = "Значение больше допустимого")]
		[Test(Description = "Негативный тест на сеттер параметра")]
		public void TestParameterSet_ValueIncorrect(double wrongValue)
		{
			Assert.Throws<Exception>(() => _testParameter.Value = wrongValue,
				"Возникает, если высота крепления больше 100 или меньше 0");
		}
	}
}
