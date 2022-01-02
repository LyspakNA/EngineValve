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
			TestName = "���������� - ���� ����� �������")]
		[TestCase(10, ParameterNames.DiameterStem,
			TestName = "���������� - ���� �������� �����")]
		[TestCase(1, ParameterNames.WidthGroove,
			TestName = "���������� - ���� ������ ��������")]
		[TestCase(1, ParameterNames.DepthGroove,
			TestName = "���������� - ���� ������� ��������")]
		[TestCase(7, ParameterNames.DistanceGroove,
			TestName = "���������� - ���� ���������� �� ��������")]
		[TestCase(35, ParameterNames.DiameterPlate,
			TestName = "���������� - ���� �������� �������")]
		[TestCase(5, ParameterNames.ThicknessPlate,
			TestName = "���������� - ���� ������� �������")]
		[TestCase(5, ParameterNames.LengthChamfer,
			TestName = "���������� - ���� ����� ������� �����")]
		[TestCase(10, ParameterNames.RadiusTransition,
			TestName = "���������� - ���� ������� �������� ��������")]
		[TestCase(4, ParameterNames.DiameterNeckline,
			TestName = "���������� - ���� �������� ������")]
		[TestCase(4, ParameterNames.DepthNeckline,
			TestName = "���������� - ���� ������� ��������")]
		public void TestCorrectParametersValveSet(double value, ParameterNames name)
		{
			_testEngineValveParameters = new EngineValveParameterCollection();
			var propertyInfo = typeof(EngineValveParameterCollection).GetProperty(name.ToString());
			propertyInfo.SetValue(_testEngineValveParameters, value);
			var actual = propertyInfo.GetValue(_testEngineValveParameters);

			Assert.AreEqual(actual, value);

		}

		[TestCase(25, ParameterNames.LengthValve,
			TestName = "���������� - ����� ������� ������ ������������")]
		[TestCase(2, ParameterNames.DiameterStem,
			TestName = "���������� - ������� ����� ������ ������������")]
		[TestCase(0.5, ParameterNames.WidthGroove,
			TestName = "���������� - ������ �������� ������ ������������ ")]
		[TestCase(0.01, ParameterNames.DepthGroove,
			TestName = "���������� - ������� �������� ������ ������������")]
		[TestCase(1, ParameterNames.DistanceGroove,
			TestName = "���������� - ���������� �� �������� ������ ������������")]
		[TestCase(5, ParameterNames.DiameterPlate,
			TestName = "���������� - ������� ������� ������ ������������")]
		[TestCase(0.001, ParameterNames.ThicknessPlate,
			TestName = "���������� - ������� ������� ������� ������������")]
		[TestCase(1, ParameterNames.LengthChamfer,
			TestName = "���������� - ����� ������� ����� ������ ������������")]
		[TestCase(2, ParameterNames.RadiusTransition,
			TestName = "���������� - ������ �������� �������� ������ ������������")]
		[TestCase(-1, ParameterNames.DiameterNeckline,
			TestName = "���������� - ������� ������ ������ ������������")]
		[TestCase(-1, ParameterNames.DepthNeckline,
			TestName = "���������� - ������� �������� ������ ������������")]

		[TestCase(200, ParameterNames.LengthValve,
			TestName = "���������� - ����� ������� ������ �������������")]
		[TestCase(35, ParameterNames.DiameterStem,
			TestName = "���������� - ������� ����� ������ �������������")]
		[TestCase(20, ParameterNames.WidthGroove,
			TestName = "���������� - ������ �������� ������ ������������� ")]
		[TestCase(20, ParameterNames.DepthGroove,
			TestName = "���������� - ������� �������� ������ �������������")]
		[TestCase(50, ParameterNames.DistanceGroove,
			TestName = "���������� - ���������� �� �������� ������ �������������")]
		[TestCase(100, ParameterNames.DiameterPlate,
			TestName = "���������� - ������� ������� ������ �������������")]
		[TestCase(50, ParameterNames.ThicknessPlate,
			TestName = "���������� - ������� ������� ������ �������������")]
		[TestCase(100, ParameterNames.LengthChamfer,
			TestName = "���������� - ����� ������� ����� ������ �������������")]
		[TestCase(100, ParameterNames.RadiusTransition,
			TestName = "���������� - ������ �������� �������� ������ �������������")]
		[TestCase(100, ParameterNames.DiameterNeckline,
			TestName = "���������� - ������� ������ ������ �������������")]
		[TestCase(100, ParameterNames.DepthNeckline,
			TestName = "���������� - ������� �������� ������ �������������")]
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