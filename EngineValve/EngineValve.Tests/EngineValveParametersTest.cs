using System;
using System.Runtime.InteropServices;
using EngineValveParameters;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EngineValve.Tests
{
	[TestFixture]
	public class EngineValveParameterTest
	{
		private EngineValveParameterCollection _testEngineValveParameters;

		[TestCase(50, ParameterNames.LengthValve,
			TestName = "Позитивный - ввод длины клапана")]
		[TestCase(10, ParameterNames.DiameterStem,
			TestName = "Позитивный - ввод диаметра ножки")]
		[TestCase(1, ParameterNames.WidthGroove,
			TestName = "Позитивный - ввод ширины проточки")]
		[TestCase(1, ParameterNames.DepthGroove,
			TestName = "Позитивный - ввод глубины проточки")]
		[TestCase(7, ParameterNames.DistanceGroove,
			TestName = "Позитивный - ввод расстояния до проточки")]
		[TestCase(35, ParameterNames.DiameterPlate,
			TestName = "Позитивный - ввод диаметра тарелки")]
		[TestCase(5, ParameterNames.ThicknessPlate,
			TestName = "Позитивный - ввод толщины тарелки")]
		[TestCase(5, ParameterNames.LengthChamfer,
			TestName = "Позитивный - ввод длины рабочей фаски")]
		[TestCase(10, ParameterNames.RadiusTransition,
			TestName = "Позитивный - ввод радиуса плавного перехода")]
		[TestCase(4, ParameterNames.DiameterNeckline,
			TestName = "Позитивный - ввод диаметра выреза")]
		[TestCase(4, ParameterNames.DepthNeckline,
			TestName = "Позитивный - ввод глубины проточки")]
		public void TestCorrectParametersValveSet(double value, ParameterNames name)
		{
			_testEngineValveParameters = new EngineValveParameterCollection();
			var propertyInfo = typeof(EngineValveParameterCollection).GetProperty(name.ToString());
			propertyInfo.SetValue(_testEngineValveParameters, value);
			var actual = propertyInfo.GetValue(_testEngineValveParameters);

			Assert.AreEqual(actual, value);

		}

		[TestCase(25, ParameterNames.LengthValve,
			TestName = "Позитивный - длина клапана меньше минимального")]
		[TestCase(2, ParameterNames.DiameterStem,
			TestName = "Позитивный - диаметр ножки меньше минимального")]
		[TestCase(0.5, ParameterNames.WidthGroove,
			TestName = "Позитивный - ширина проточки меньше минимального ")]
		[TestCase(0.01, ParameterNames.DepthGroove,
			TestName = "Позитивный - глубина проточки меньше минимального")]
		[TestCase(1, ParameterNames.DistanceGroove,
			TestName = "Позитивный - расстояние до проточки меньше минимального")]
		[TestCase(5, ParameterNames.DiameterPlate,
			TestName = "Позитивный - диаметр тарелки меньше минимального")]
		[TestCase(0.001, ParameterNames.ThicknessPlate,
			TestName = "Позитивный - толщина тарелки менььше минимального")]
		[TestCase(1, ParameterNames.LengthChamfer,
			TestName = "Позитивный - длина рабочей фаски меньше минимального")]
		[TestCase(2, ParameterNames.RadiusTransition,
			TestName = "Позитивный - радиус плавного перехода меньше минимального")]
		[TestCase(-1, ParameterNames.DiameterNeckline,
			TestName = "Позитивный - диаметр выреза меньше минимального")]
		[TestCase(-1, ParameterNames.DepthNeckline,
			TestName = "Позитивный - глубина проточки меньще минимального")]

		[TestCase(200, ParameterNames.LengthValve,
			TestName = "Позитивный - длина клапана больше максимального")]
		[TestCase(35, ParameterNames.DiameterStem,
			TestName = "Позитивный - диаметр ножки больше максимального")]
		[TestCase(20, ParameterNames.WidthGroove,
			TestName = "Позитивный - ширина проточки больше максимального ")]
		[TestCase(20, ParameterNames.DepthGroove,
			TestName = "Позитивный - глубина проточки больше максимального")]
		[TestCase(50, ParameterNames.DistanceGroove,
			TestName = "Позитивный - расстояние до проточки больше максимального")]
		[TestCase(100, ParameterNames.DiameterPlate,
			TestName = "Позитивный - диаметр тарелки больше максимального")]
		[TestCase(50, ParameterNames.ThicknessPlate,
			TestName = "Позитивный - толщина тарелки больше максимального")]
		[TestCase(100, ParameterNames.LengthChamfer,
			TestName = "Позитивный - длина рабочей фаски больше максимального")]
		[TestCase(100, ParameterNames.RadiusTransition,
			TestName = "Позитивный - радиус плавного перехода больше максимального")]
		[TestCase(100, ParameterNames.DiameterNeckline,
			TestName = "Позитивный - диаметр выреза больше максимального")]
		[TestCase(100, ParameterNames.DepthNeckline,
			TestName = "Позитивный - глубина проточки больше максимального")]
		public void TestIncorrectParametersValveSet(double value, ParameterNames name)
		{
			_testEngineValveParameters = new EngineValveParameterCollection();
			var propertyInfo = typeof(EngineValveParameterCollection).GetProperty(name.ToString());
			propertyInfo.SetValue(_testEngineValveParameters, value);
			var actual = propertyInfo.GetValue(_testEngineValveParameters);

			Assert.AreNotEqual(actual, value);

		}

	}
}