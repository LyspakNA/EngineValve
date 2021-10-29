using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineValveParameter
{
	public class EngineValveParameters
	{
		private double _lengthValve;
		private double _diameterStem;
		private double _widthGroove;
		private double _depthGroove;
		private double _distanceGroove;
		private double _diameterPlate;
		private double _thicknessPlate;
		private double _lengthChamfer;
		private double _radiusTransition;

		public EngineValveParameters(double lengthValve, double diameterStem,
			double widthGroove, double depthGroove, double distanceGroove,
			double diameterPlate, double thicknessPlate, double lengthChamfer,
			double radiusTransition)
		{
			LengthValve = lengthValve;
			DiameterStem = diameterStem;
			WidthGroove = widthGroove;
			DepthGroove = depthGroove;
			DistanceGroove = distanceGroove;
			DiameterPlate = diameterPlate;
			ThicknessPlate = thicknessPlate;
			LengthChamfer = lengthChamfer;
			RadiusTransition = radiusTransition;

		}

		public double LengthValve
		{
			get => _lengthValve;
			private set
			{
				const double minLengthValve = 50;
				const double maxLengthValve = 150;
				if (value < minLengthValve || value > maxLengthValve)
				{
					throw new ArgumentException(GetErrorMessage(minLengthValve,
						maxLengthValve, value, "Длина клапана"));
				}
				_lengthValve = value;
			}
		}
			public double DiameterStem
		{
			get => _diameterStem;
			private set
			{
				const double minDiameterStem = 5;
				const double maxDiameterStem = 15;
				if (value < minDiameterStem || value > maxDiameterStem)
				{
					throw new ArgumentException(GetErrorMessage(minDiameterStem,
						maxDiameterStem, value, "Диаметр ножки"));
				}
				_diameterStem = value;
			}
		}
		public double WidthGroove
		{
			get => _widthGroove;
			private set
			{
				const double minWidthGroove = 1;
				double maxWidthGroove = 0.1 * LengthValve;
				if (value < minWidthGroove || value > maxWidthGroove)
				{
					throw new ArgumentException(GetErrorMessage(minWidthGroove,
						maxWidthGroove, value, "Ширина проточки"));
				}
				_widthGroove = value;
			}
		}
		public double DepthGroove
		{
			get => _depthGroove;
			private set
			{
				const double minDepthGroove = 0.5;
				double maxDepthGroove = 0.25 * DiameterStem;
				if (minDepthGroove>value || maxDepthGroove<value)
				{
					throw new ArgumentException(GetErrorMessage(minDepthGroove,
						maxDepthGroove, value, "Глубина проточки"));
				}
				_depthGroove = value;
			}
		}
		public double DistanceGroove
		{
			get => _distanceGroove;
			private set
			{
				const double minDistanceGroove = 5;
				double maxDistanceGroove = 0.25 * LengthValve;
				if (minDistanceGroove > value || maxDistanceGroove < value)
				{
					throw new ArgumentException(GetErrorMessage(minDistanceGroove,
						maxDistanceGroove, value, "Расстояние о проточки"));
				}
				_distanceGroove = value;
			}
		}
		public double DiameterPlate
		{
			get => _diameterPlate;
			private set 
			{
				double minDiameterPlate = 2 * DiameterStem;
				const double maxDiameterPlate = 70;
				if (minDiameterPlate > value || maxDiameterPlate < value) 
				{
					throw new ArgumentException(GetErrorMessage(minDiameterPlate,
						maxDiameterPlate, value, "Диаметр тарелки"));
				}
				_diameterPlate = value;
			}
		}
		public double ThicknessPlate
		{
			get => _thicknessPlate;
			private set
			{
				const double minThincknessPlate = 5;
				const double maxThincknessPlate = 10;
				if(minThincknessPlate> value || maxThincknessPlate<value)
				{
					throw new ArgumentException(GetErrorMessage(minThincknessPlate,
						maxThincknessPlate, value, "Толщина тарелки"));
				}
				_thicknessPlate = value;
			}
		}
		public double LengthChamfer
		{
			get => _lengthChamfer;
			private set
			{
				const double minLengthChamfer = 2;
				const double maxLengthChamfer = 10;
				if(minLengthChamfer>value || maxLengthChamfer<value)
				{
					throw new ArgumentException(GetErrorMessage(minLengthChamfer,
						maxLengthChamfer, value, "Длина фаски"));
				}
				_lengthChamfer = value;
			}
		}
		public double RadiusTransition
		{
			get => _radiusTransition;
			private set
			{
				const double minRadiusTransition = 5;
				double maxRadiusTransition = 0.75 * DiameterPlate;
				if (minRadiusTransition>value || maxRadiusTransition<value)
				{
					throw new ArgumentException(GetErrorMessage(minRadiusTransition,
						maxRadiusTransition, value, "Радиус перехода"));
				}
				_radiusTransition = value;
			}
		}

			private string GetErrorMessage(double min, double max, double value, string name)
		{
			var result = "Проверьте правильность ввода данных:\n";
			result += $"{name} = {value} не входит в диапозон от {min} до {max}";
			return result;
		}

	}
}
