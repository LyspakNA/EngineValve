using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineValveParameter
{
	public class EngineValveParameters
	{
		public EngineValveParameters(double lengthValve, double diameterStem,
			double widthGroove, double depthGroove, double distanceGroove,
			double diameterPlate, double thicknessPlate, double lengthChamfer,
			double radiusTransition)
		{
			var errors = Validate(lengthValve, diameterStem,
			widthGroove, depthGroove, distanceGroove,
			diameterPlate, thicknessPlate, lengthChamfer,
			radiusTransition);

			if (errors.Any())
			{
				throw new ArgumentException(GetErrorMessage(errors));
			}

			LengthValue = lengthValve;
			DiameterPlate = diameterPlate;
			WidthGroove = widthGroove;
			DepthGroove = depthGroove;
		}

		public double LengthValue { get; }
		public double DiameterStem { get; }
		public double WidthGroove { get; }
		public double DepthGroove { get; }
		public double DistanceGroove { get; }
		public double DiameterPlate { get; }
		public double ThicknessPlate { get; }
		public double LengthChamfer { get; }
		public double RadiusTransition { get; }

		private List<string> Validate(double lengthValve, double diameterStem,
			double widthGroove, double depthGroove, double distanceGroove,
			double diameterPlate, double thicknessPlate, double lengthChamfer,
			double radiusTransition)
		{
			var errors = new List<string>();

			const double minLengthValve = 50;
			const double maxLengthValve = 150;

			const double minDiameterStem = 5;
			const double maxDiameterStem = 15;

			const double minWidthGroove = 1;
			double maxWidthGroove = 0.1 * lengthValve;

			const double minDepthGroove = 0.5;
			double maxDepthGroove = 0.25 * diameterStem;

			const double minDistanceGroove = 5;
			double maxDistanceGroove = 0.25 * lengthValve;

			double minDiameterPlate = 2 * diameterStem;
			const double maxDiameterPlate = 70;

			const double minThincknessPlate = 5;
			const double maxThincknessPlate = 10;

			const double minLengthChamfer = 2;
			const double maxLengthChamfer = 10;

			const double minRadiusTransition = 5;
			double maxRadiusTransition = 0.75 * diameterPlate;

			ValidateValue(minLengthValve, maxLengthValve, lengthValve,
				"Длина клапана", errors);
			ValidateValue(minDiameterStem, maxDiameterStem, diameterStem,
				"Диаметр ножки клапана", errors);
			ValidateValue(minWidthGroove, maxWidthGroove, widthGroove,
				"Ширина проточки", errors);
			ValidateValue(minDepthGroove, maxDepthGroove, depthGroove,
				"Глубина проточки", errors);
			ValidateValue(minDistanceGroove, maxDistanceGroove, distanceGroove,
				"Расстояние до проточки", errors);
			ValidateValue(minDiameterPlate, maxDiameterPlate, diameterPlate,
				"Диаметр тарелки", errors);
			ValidateValue(minThincknessPlate, maxThincknessPlate, thicknessPlate,
				"Толщина тарелки", errors);
			ValidateValue(minLengthChamfer, maxLengthChamfer, lengthChamfer,
				"Длина фаски", errors);
			ValidateValue(minRadiusTransition, maxRadiusTransition, radiusTransition,
				"Радиус перехода", errors);

			return errors;
		}

		private void ValidateValue(double min, double max, double value, string name, List<string> error)
		{
			if (min > value || max < value)
			{
				error.Add($"{name} не входит в диапозон от {min} до {max}");
			}
		}

		private string GetErrorMessage(List<string> errors)
		{
			var result = "Проверьте правильность ввода данных:\n";
			result += string.Join(";\n", errors);
			result += '.';

			return result;
		}

	}
}
