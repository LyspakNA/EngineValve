using System;
using NUnit.Framework;
using EngineValveParameter;
using NUnit.Framework.Internal;

namespace EngineValve.Tests
{
	[TestFixture]
	public class ValveTest
	{

		[TestCase(30, 8, 2, 2, 10, 50, 2, 3, 20,
			TestName = "����� ������� ������ ������������ (50)")]
		[TestCase(100, 2, 2, 2, 10, 50, 2, 3, 20,
			TestName = "������� ����� ������� ������ ������������ (5)")]
		[TestCase(100, 8, 0.5, 2, 10, 50, 2, 3, 20,
			TestName = "������ ���� ��� ������ ������ ������������ (1)")]
		[TestCase(100, 8, 2, 0.1, 10, 50, 2, 3, 20,
			TestName = "������� ���� ��� ������ ������ ������������ (0.5)")]
		[TestCase(100, 8, 2, 2, 2, 50, 2, 3, 20,
			TestName = "���������� �� ���� ��� ������ ������ ������������ (50)")]
		[TestCase(100, 8, 2, 2, 10, 15, 2, 3, 20,
			TestName = "������� ������� ������ ������������ (2 �������� �����)")]
		[TestCase(100, 8, 2, 2, 10, 50, 0.5, 3, 20,
			TestName = "������� ������� ������ ������������ (1)")]
		[TestCase(100, 8, 2, 2, 10, 50, 2, 1, 20,
			TestName = "����� ����� ������ ������������ (2)")]
		[TestCase(100, 8, 2, 2, 10, 50, 2, 3, 2,
			TestName = "������ �������� ������ ������������ (5)")]

		[TestCase(200, 8, 2, 2, 10, 50, 2, 3, 20,
			TestName = "����� ������� ������ ������������� (150)")]
		[TestCase(100, 20, 2, 2, 10, 50, 2, 3, 20,
			TestName = "������� ����� ������ ������������� (15)")]
		[TestCase(100, 8, 20, 2, 10, 50, 2, 3, 20,
			TestName = "������ ���� ������ ������������� (1/10 ����� �������)")]
		[TestCase(100, 8, 2, 20, 10, 50, 2, 3, 20,
			TestName = "������� ���� ������ ������������� (1/4 �������� �����)")]
		[TestCase(100, 8, 2, 2, 100, 50, 2, 3, 20,
			TestName = "���������� �� ���� ������ ������������� (1/4 ����� �������)")]
		[TestCase(100, 8, 2, 2, 10, 80, 2, 3, 20,
			TestName = "������� ������� ������ ������������� (70)")]
		[TestCase(100, 8, 2, 2, 10, 50, 70, 3, 20,
			TestName = "������� ������� ������ ������������� (3/4 �������� �������)")]
		[TestCase(100, 8, 2, 2, 10, 50, 2, 20, 20,
			TestName = "����� ����� ������ ������������� (10)")]
		[TestCase(100, 8, 2, 2, 10, 50, 2, 3, 70,
			TestName = "������ �������� ������ ������������� (3/4 �������� �������)")]
        public void EngineValveParameterTest_ArgumentException(double lengthValve,
			double diameterStem, double widthGroove, double depthGroove, double distanceGroove,
			double diameterPlate, double thicknessPlate, double lengthChamfer,
			double radiusTransition)
		{
			Assert.Throws<ArgumentException>(()=>new EngineValveParameters(lengthValve,
				diameterStem, widthGroove,  depthGroove,  distanceGroove,
				 diameterPlate,  thicknessPlate,  lengthChamfer, radiusTransition));
		}

		[TestCase(100, 8, 2, 2, 10, 50, 2, 3, 20,
			TestName = "���������� ���������")]
        public void EngineValveParameterTest_Correct(double lengthValve,
			double diameterStem, double widthGroove, double depthGroove, double distanceGroove,
			double diameterPlate, double thicknessPlate, double lengthChamfer,
			double radiusTransition)
		{
			var testParameters = new EngineValveParameters(lengthValve,
				diameterStem, widthGroove, depthGroove, distanceGroove,
				diameterPlate, thicknessPlate, lengthChamfer, radiusTransition);

			Assert.AreEqual(lengthValve, testParameters.LengthValve);
			Assert.AreEqual(diameterStem, testParameters.DiameterStem);
			Assert.AreEqual(widthGroove, testParameters.WidthGroove);
			Assert.AreEqual(depthGroove, testParameters.DepthGroove);
			Assert.AreEqual(distanceGroove, testParameters.DistanceGroove);
			Assert.AreEqual(diameterPlate, testParameters.DiameterPlate);
			Assert.AreEqual(thicknessPlate, testParameters.ThicknessPlate);
			Assert.AreEqual(lengthChamfer, testParameters.LengthChamfer);
			Assert.AreEqual(radiusTransition, testParameters.RadiusTransition);
		}

	}
}