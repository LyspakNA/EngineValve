using EngineValveParameters;
using NUnit.Framework;

namespace EngineValve.Tests
{
	[TestFixture]
	public class EngineValveParameterTest
	{
		private EngineValveParameterCollection _testEngineValveParameters;
		
		[TestCase(50, ParameterNames.LengthValve,
			TestName = "Позитивный - корректное значение длины клапана")]
		[TestCase(10, ParameterNames.DiameterStem,
			TestName = "Позитивный - корректное значение диаметра ножки")]
		[TestCase(1, ParameterNames.WidthGroove,
			TestName = "Позитивный - корректное значение ширины проточки")]
		[TestCase(1, ParameterNames.DepthGroove,
			TestName = "Позитивный - корректное значение глубины проточки")]
		[TestCase(7, ParameterNames.DistanceGroove,
			TestName = "Позитивный - корректное значение расстояния до проточки")]
		[TestCase(35, ParameterNames.DiameterPlate,
			TestName = "Позитивный - корректное значение диаметра тарелки")]
		[TestCase(5, ParameterNames.ThicknessPlate,
			TestName = "Позитивный - корректное значение толщины тарелки")]
		[TestCase(5, ParameterNames.LengthChamfer,
			TestName = "Позитивный - корректное значение длины фаски")]
		[TestCase(10, ParameterNames.RadiusTransition,
			TestName = "Позитивный - корректное значение радиуса перехода")]
		[TestCase(4, ParameterNames.DiameterNeckline,
			TestName = "Позитивный - корректное значение диаметра выреза")]
		[TestCase(4, ParameterNames.DepthNeckline,
			TestName = "Позитивный - корректное значение глубины выреза")]
		public void TestCorrectParametersValveSet(double value, ParameterNames name)
		{
			_testEngineValveParameters = new EngineValveParameterCollection();
			var propertyInfo = typeof(EngineValveParameterCollection).GetProperty(name.ToString());
			propertyInfo.SetValue(_testEngineValveParameters, value);
			var actual = propertyInfo.GetValue(_testEngineValveParameters);

			Assert.AreEqual(actual, value);

		}

		[TestCase(25, ParameterNames.LengthValve,
			TestName = "Позитивный - длина клапана меньше допустимой")]
		[TestCase(2, ParameterNames.DiameterStem,
			TestName = "Позитивный - диаметр ножки меньше допустимого")]
		[TestCase(0.5, ParameterNames.WidthGroove,
			TestName = "Позитивный - ширина проточки меньше допустимой")]
		[TestCase(0.01, ParameterNames.DepthGroove,
			TestName = "Позитивный - глубина проточки меньше допустимой")]
		[TestCase(1, ParameterNames.DistanceGroove,
			TestName = "Позитивный - расстояние до проточки меньше допустимой")]
		[TestCase(5, ParameterNames.DiameterPlate,
			TestName = "Позитивный - диаметр тарелки меньше допустимого")]
		[TestCase(0.001, ParameterNames.ThicknessPlate,
			TestName = "Позитивный - толщина тарелки меньше допустимой")]
		[TestCase(1, ParameterNames.LengthChamfer,
			TestName = "Позитивный - длина фаски меньше допустимой")]
		[TestCase(2, ParameterNames.RadiusTransition,
			TestName = "Позитивный - радиус перехода меньше допустимого")]
		[TestCase(-1, ParameterNames.DiameterNeckline,
			TestName = "Позитивный - диаметр выреза меньше допустимого")]
		[TestCase(-1, ParameterNames.DepthNeckline,
			TestName = "Позитивный - глубина выреза меньше допустимой")]

		[TestCase(200, ParameterNames.LengthValve,
			TestName = "Позитивный - длина клапана больше допустимого")]
		[TestCase(35, ParameterNames.DiameterStem,
			TestName = "Позитивный - диаметр ножки больше допустимого")]
		[TestCase(20, ParameterNames.WidthGroove,
			TestName = "Позитивный - ширина проточки больше допустимой")]
		[TestCase(20, ParameterNames.DepthGroove,
			TestName = "Позитивный - глубина проточки больше допустимой")]
		[TestCase(50, ParameterNames.DistanceGroove,
			TestName = "Позитивный - расстояние до проточки больше допустимой")]
		[TestCase(100, ParameterNames.DiameterPlate,
			TestName = "Позитивный - диаметр тарелки больше допустимого")]
		[TestCase(50, ParameterNames.ThicknessPlate,
			TestName = "Позитивный - толщина тарелки больше допустимой")]
		[TestCase(100, ParameterNames.LengthChamfer,
			TestName = "Позитивный - длина фаски больше допустимой")]
		[TestCase(100, ParameterNames.RadiusTransition,
			TestName = "Позитивный - радиус перехода больше допустимого")]
		[TestCase(100, ParameterNames.DiameterNeckline,
			TestName = "Позитивный - диаметр выреза больше допустимого")]
		[TestCase(100, ParameterNames.DepthNeckline,
			TestName = "Позитивный - глубина выреза больше допустимой")]
		public void TestIncorrectParametersValveSet(double value, ParameterNames name)
		{
			_testEngineValveParameters = new EngineValveParameterCollection();
			var propertyInfo = typeof(EngineValveParameterCollection).GetProperty(name.ToString());
			propertyInfo.SetValue(_testEngineValveParameters, value);
			var actual = propertyInfo.GetValue(_testEngineValveParameters);

			Assert.AreNotEqual(actual, value);
		}

		[TestCase(true,
			TestName = "Позитивный - изменение флага выреза на положительный")]
		[TestCase(false,
			TestName = "Позитивный - изменение флага выреза на отрицательный")]
		public void TestCreateNecklineFlagSet(bool value)
		{
			_testEngineValveParameters = new EngineValveParameterCollection();
			_testEngineValveParameters.CreateNeckline = value;
			var actual = _testEngineValveParameters.CreateNeckline;

			Assert.AreEqual(actual, value);
		}
	}
}