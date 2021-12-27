using System;
using EngineValveParameters;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EngineValve.Tests
{
	[TestFixture]
	public class ValveTest
	{

		[TestCase(30, 8, 2, 2,
		10, 50, 2, 3, 20,
			TestName = "Длина клапана меньше минимального (50)")]
		[TestCase(100, 2, 2, 2,
		10, 50, 2, 3, 20,
			TestName = "Диаметр ножки клапана меньше минимального (5)")]
		[TestCase(100, 8, 0.5, 2,
		10, 50, 2, 3, 20,
			TestName = "Ширина паза под сухарь меньше минимального (1)")]
		[TestCase(100, 8, 2, 0.1,
		10, 50, 2, 3, 20,
			TestName = "Глубина паза под сухарь меньше минимального (0.5)")]
		[TestCase(100, 8, 2, 2,
		2, 50, 2, 3, 20,
			TestName = "Расстояние до паза под сухарь меньше минимального (50)")]
		[TestCase(100, 8, 2, 2,
		10, 15, 2, 3, 20,
			TestName = "Диаметр тарелки меньше минимального (2 диаметра ножки)")]
		[TestCase(100, 8, 2, 2,
		10, 50, 0.5, 3, 20,
			TestName = "Толщина тарелки меньше минимального (1)")]
		[TestCase(100, 8, 2, 2,
		10, 50, 2, 1, 20,
			TestName = "Длина фаски меньше минимального (2)")]
		[TestCase(100, 8, 2, 2,
		10, 50, 2, 3, 2,
			TestName = "Радиус перехода меньше минимального (5)")]

		[TestCase(200, 8, 2, 2,
		10, 50, 2, 3, 20,
			TestName = "Длина клапана больше максимального (150)")]
		[TestCase(100, 20, 2, 2,
		10, 50, 2, 3, 20,
			TestName = "Диаметр ножки больше максимального (15)")]
		[TestCase(100, 8, 20, 2,
		10, 50, 2, 3, 20,
			TestName = "Ширина паза больше максимального (1/10 Длины клапана)")]
		[TestCase(100, 8, 2, 20,
		10, 50, 2, 3, 20,
			TestName = "Глубина паза больше максимального (1/4 диаметра ножки)")]
		[TestCase(100, 8, 2, 2,
		100, 50, 2, 3, 20,
			TestName = "Расстояние до паза больше максимального (1/4 длины клапана)")]
		[TestCase(100, 8, 2, 2, 
		10, 80, 2, 3, 20,
			TestName = "Диаметр тарелки больше максимального (70)")]
		[TestCase(100, 8, 2, 2, 
		10, 50, 70, 3, 20,
			TestName = "Толщина тарелки больше максимального (3/4 диаметра тарелки)")]
		[TestCase(100, 8, 2, 2, 
		10, 50, 2, 20, 20,
			TestName = "Длина фаски больше максимального (10)")]
		[TestCase(100, 8, 2, 2, 
		10, 50, 2, 3, 70,
			TestName = "Радиус перехода больше максимального (3/4 диаметра тарелки)")]
        public void EngineValveParameterTest_ArgumentException(double lengthValve,
			double diameterStem, double widthGroove, double depthGroove, double distanceGroove,
			double diameterPlate, double thicknessPlate, double lengthChamfer,
			double radiusTransition)
        {
	        EngineValveParameter testParameter = new EngineValveParameter();
			Assert.Throws<Exception>(()=>
			{
				testParameter.LengthValve = lengthValve;
				testParameter.DiameterStem = diameterStem;
				testParameter.WidthGroove = widthGroove;
				testParameter.DepthGroove = depthGroove;
				testParameter.DistanceGroove = distanceGroove;
				testParameter.DiameterPlate = diameterPlate;
				testParameter.ThicknessPlate = thicknessPlate;
				testParameter.LengthChamfer = lengthChamfer;
				testParameter.RadiusTransition = radiusTransition;
			});
		}

		[TestCase(100, 8, 2, 2, 10, 50, 2, 3, 20,
			TestName = "Нормальные параметры")]
        public void EngineValveParameterTest_Correct(double lengthValve,
			double diameterStem, double widthGroove, double depthGroove, double distanceGroove,
			double diameterPlate, double thicknessPlate, double lengthChamfer,
			double radiusTransition)
		{
			var testParameter = new EngineValveParameter();

			testParameter.LengthValve = lengthValve;
			testParameter.DiameterStem = diameterStem;
			testParameter.WidthGroove = widthGroove;
			testParameter.DepthGroove = depthGroove;
			testParameter.DistanceGroove = distanceGroove;
			testParameter.DiameterPlate = diameterPlate;
			testParameter.ThicknessPlate = thicknessPlate;
			testParameter.LengthChamfer = lengthChamfer;
			testParameter.RadiusTransition = radiusTransition;

			Assert.AreEqual(lengthValve, testParameter.LengthValve);
			Assert.AreEqual(diameterStem, testParameter.DiameterStem);
			Assert.AreEqual(widthGroove, testParameter.WidthGroove);
			Assert.AreEqual(depthGroove, testParameter.DepthGroove);
			Assert.AreEqual(distanceGroove, testParameter.DistanceGroove);
			Assert.AreEqual(diameterPlate, testParameter.DiameterPlate);
			Assert.AreEqual(thicknessPlate, testParameter.ThicknessPlate);
			Assert.AreEqual(lengthChamfer, testParameter.LengthChamfer);
			Assert.AreEqual(radiusTransition, testParameter.RadiusTransition);
		}

	}
}