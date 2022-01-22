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

		//TODO:кодировка
		[TestCase(50, ParameterNames.LengthValve,
			TestName = "Ïîçèòèâíûé - ââîä äëèíû êëàïàíà")]
		[TestCase(10, ParameterNames.DiameterStem,
			TestName = "Ïîçèòèâíûé - ââîä äèàìåòðà íîæêè")]
		[TestCase(1, ParameterNames.WidthGroove,
			TestName = "Ïîçèòèâíûé - ââîä øèðèíû ïðîòî÷êè")]
		[TestCase(1, ParameterNames.DepthGroove,
			TestName = "Ïîçèòèâíûé - ââîä ãëóáèíû ïðîòî÷êè")]
		[TestCase(7, ParameterNames.DistanceGroove,
			TestName = "Ïîçèòèâíûé - ââîä ðàññòîÿíèÿ äî ïðîòî÷êè")]
		[TestCase(35, ParameterNames.DiameterPlate,
			TestName = "Ïîçèòèâíûé - ââîä äèàìåòðà òàðåëêè")]
		[TestCase(5, ParameterNames.ThicknessPlate,
			TestName = "Ïîçèòèâíûé - ââîä òîëùèíû òàðåëêè")]
		[TestCase(5, ParameterNames.LengthChamfer,
			TestName = "Ïîçèòèâíûé - ââîä äëèíû ðàáî÷åé ôàñêè")]
		[TestCase(10, ParameterNames.RadiusTransition,
			TestName = "Ïîçèòèâíûé - ââîä ðàäèóñà ïëàâíîãî ïåðåõîäà")]
		[TestCase(4, ParameterNames.DiameterNeckline,
			TestName = "Ïîçèòèâíûé - ââîä äèàìåòðà âûðåçà")]
		[TestCase(4, ParameterNames.DepthNeckline,
			TestName = "Ïîçèòèâíûé - ââîä ãëóáèíû ïðîòî÷êè")]
		public void TestCorrectParametersValveSet(double value, ParameterNames name)
		{
			_testEngineValveParameters = new EngineValveParameterCollection();
			var propertyInfo = typeof(EngineValveParameterCollection).GetProperty(name.ToString());
			propertyInfo.SetValue(_testEngineValveParameters, value);
			var actual = propertyInfo.GetValue(_testEngineValveParameters);

			Assert.AreEqual(actual, value);

		}

		[TestCase(25, ParameterNames.LengthValve,
			TestName = "Ïîçèòèâíûé - äëèíà êëàïàíà ìåíüøå ìèíèìàëüíîãî")]
		[TestCase(2, ParameterNames.DiameterStem,
			TestName = "Ïîçèòèâíûé - äèàìåòð íîæêè ìåíüøå ìèíèìàëüíîãî")]
		[TestCase(0.5, ParameterNames.WidthGroove,
			TestName = "Ïîçèòèâíûé - øèðèíà ïðîòî÷êè ìåíüøå ìèíèìàëüíîãî ")]
		[TestCase(0.01, ParameterNames.DepthGroove,
			TestName = "Ïîçèòèâíûé - ãëóáèíà ïðîòî÷êè ìåíüøå ìèíèìàëüíîãî")]
		[TestCase(1, ParameterNames.DistanceGroove,
			TestName = "Ïîçèòèâíûé - ðàññòîÿíèå äî ïðîòî÷êè ìåíüøå ìèíèìàëüíîãî")]
		[TestCase(5, ParameterNames.DiameterPlate,
			TestName = "Ïîçèòèâíûé - äèàìåòð òàðåëêè ìåíüøå ìèíèìàëüíîãî")]
		[TestCase(0.001, ParameterNames.ThicknessPlate,
			TestName = "Ïîçèòèâíûé - òîëùèíà òàðåëêè ìåíüüøå ìèíèìàëüíîãî")]
		[TestCase(1, ParameterNames.LengthChamfer,
			TestName = "Ïîçèòèâíûé - äëèíà ðàáî÷åé ôàñêè ìåíüøå ìèíèìàëüíîãî")]
		[TestCase(2, ParameterNames.RadiusTransition,
			TestName = "Ïîçèòèâíûé - ðàäèóñ ïëàâíîãî ïåðåõîäà ìåíüøå ìèíèìàëüíîãî")]
		[TestCase(-1, ParameterNames.DiameterNeckline,
			TestName = "Ïîçèòèâíûé - äèàìåòð âûðåçà ìåíüøå ìèíèìàëüíîãî")]
		[TestCase(-1, ParameterNames.DepthNeckline,
			TestName = "Ïîçèòèâíûé - ãëóáèíà ïðîòî÷êè ìåíüùå ìèíèìàëüíîãî")]

		[TestCase(200, ParameterNames.LengthValve,
			TestName = "Ïîçèòèâíûé - äëèíà êëàïàíà áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(35, ParameterNames.DiameterStem,
			TestName = "Ïîçèòèâíûé - äèàìåòð íîæêè áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(20, ParameterNames.WidthGroove,
			TestName = "Ïîçèòèâíûé - øèðèíà ïðîòî÷êè áîëüøå ìàêñèìàëüíîãî ")]
		[TestCase(20, ParameterNames.DepthGroove,
			TestName = "Ïîçèòèâíûé - ãëóáèíà ïðîòî÷êè áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(50, ParameterNames.DistanceGroove,
			TestName = "Ïîçèòèâíûé - ðàññòîÿíèå äî ïðîòî÷êè áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(100, ParameterNames.DiameterPlate,
			TestName = "Ïîçèòèâíûé - äèàìåòð òàðåëêè áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(50, ParameterNames.ThicknessPlate,
			TestName = "Ïîçèòèâíûé - òîëùèíà òàðåëêè áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(100, ParameterNames.LengthChamfer,
			TestName = "Ïîçèòèâíûé - äëèíà ðàáî÷åé ôàñêè áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(100, ParameterNames.RadiusTransition,
			TestName = "Ïîçèòèâíûé - ðàäèóñ ïëàâíîãî ïåðåõîäà áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(100, ParameterNames.DiameterNeckline,
			TestName = "Ïîçèòèâíûé - äèàìåòð âûðåçà áîëüøå ìàêñèìàëüíîãî")]
		[TestCase(100, ParameterNames.DepthNeckline,
			TestName = "Ïîçèòèâíûé - ãëóáèíà ïðîòî÷êè áîëüøå ìàêñèìàëüíîãî")]
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