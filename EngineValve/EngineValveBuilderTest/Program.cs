using System;
using System.Diagnostics;
using System.IO;
using EngineValveParameters;
using EngineValveBuild;
using Microsoft.VisualBasic.Devices;

namespace EngineValveBuilderTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var parameters = new EngineValveParameterCollection();
			var builder = new EngineValveBuilder(parameters);
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var streamWriter = new StreamWriter($"logapiService.txt", true);
			Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
			var count = 0;
			while (true)
			{
				builder.BuildEngineValve();
				var computerInfo = new ComputerInfo();
			}
		}
	}
}
