using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineValveParameter
{
	/// <summary>
	/// Параметры клапана.
	/// </summary>
	public class EngineValveParameters
	{
		/// <summary>
		/// Длина клапана
		/// </summary>
		private double _lengthValve;
		/// <summary>
		/// Диаметр ножки
		/// </summary>
		private double _diameterStem;
		/// <summary>
		/// Ширина проточки под сухарь
		/// </summary>
		private double _widthGroove;
		/// <summary>
		/// Глубина проточки под сухарь
		/// </summary>
		private double _depthGroove;
		/// <summary>
		/// Расстояние до проточки
		/// </summary>
		private double _distanceGroove;
		/// <summary>
		/// Диаметр тарелки клапана
		/// </summary>
		private double _diameterPlate;
		/// <summary>
		/// Толщина тарелки клапана
		/// </summary>
		private double _thicknessPlate;
		/// <summary>
		/// Длина рабочей фаски
		/// </summary>
		private double _lengthChamfer;
		/// <summary>
		/// радиус плавного перехода
		/// </summary>
		private double _radiusTransition;

		/// <summary>
		/// Конструктор для пользовательских значений
		/// </summary>
		/// <param name="lengthValve">Длина клапана</param>
		/// <param name="diameterStem">Диаметр ножки</param>
		/// <param name="widthGroove">Ширина проточки</param>
		/// <param name="depthGroove">Глубина проточки</param>
		/// <param name="distanceGroove">Расстояние до проточки</param>
		/// <param name="diameterPlate">Диаметр тарелки</param>
		/// <param name="thicknessPlate">Толщина тарелки</param>
		/// <param name="lengthChamfer">Длина рабочей фаски</param>
		/// <param name="radiusTransition">Радиус плавного перехода</param>
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
			if (ErrorList.Any())
			{
				throw new ArgumentException(GetErrorMessage(ErrorList));
			}
		}
		/// <summary>
		/// Конструктор для стандартных параметров.
		/// </summary>
		public EngineValveParameters()
		{
			LengthValve = 100;
			DiameterStem = 8;
			WidthGroove = 2;
			DepthGroove = 2;
			DistanceGroove = 10;
			DiameterPlate = 50;
			ThicknessPlate = 2;
			LengthChamfer = 3;
			RadiusTransition = 20;
		}
		/// <summary>
		/// Лист ошибок
		/// </summary>
		private List<string> ErrorList { get; } = new List<string>();
		/// <summary>
		/// Свойство обрабатывающее поле длины клапана
		/// </summary>
		public double LengthValve
		{
			get => _lengthValve;
			private set
			{
				const double minLengthValve = 50;
				const double maxLengthValve = 150;
				if (value < minLengthValve || value > maxLengthValve)
				{
					ErrorList.Add($"Длина клапана: {value} не входит" +
					              $" в диапозон от {minLengthValve} до {maxLengthValve}");
				}
				else
				{
					_lengthValve = value;
				}
			}
		}
		/// <summary>
		/// Свойство обрабатывающее поле диаметра ножки
		/// </summary>
			public double DiameterStem
		{
			get => _diameterStem;
			private set
			{
				const double minDiameterStem = 5;
				const double maxDiameterStem = 15;
				if (value < minDiameterStem || value > maxDiameterStem)
				{
					ErrorList.Add($"Диаметр ножки: {value} не входит" +
					              $" в диапозон от {minDiameterStem} до {maxDiameterStem}");
				}
				else
				{
					_diameterStem = value;
				}
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле ширины проточки
		/// </summary>
		public double WidthGroove
		{
			get => _widthGroove;
			private set
			{
				const double minWidthGroove = 1;
				double maxWidthGroove = 0.1 * LengthValve;
				if (minWidthGroove < maxWidthGroove)
				{
					if (value < minWidthGroove || value > maxWidthGroove)
					{
						ErrorList.Add($"Ширина проточки: {value} не входит" +
						              $" в диапозон от {minWidthGroove} до {maxWidthGroove}");
					}
					else
					{
						_widthGroove = value;
					}
				}
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле глубины проточки
		/// </summary>
		public double DepthGroove
		{
			get => _depthGroove;
			private set
			{
				const double minDepthGroove = 0.5;
				double maxDepthGroove = 0.25 * DiameterStem;
				if (minDepthGroove < maxDepthGroove)
				{
					if (minDepthGroove > value || maxDepthGroove < value)
					{
						ErrorList.Add($"Глубина проточки: {value} не входит" +
						              $" в диапозон от {minDepthGroove} до {maxDepthGroove}");
					}
					else
					{
						_depthGroove = value;
					}
				}
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле расстояния до проточки
		/// </summary>
		public double DistanceGroove
		{
			get => _distanceGroove;
			private set
			{
				const double minDistanceGroove = 5;
				double maxDistanceGroove = 0.25 * LengthValve;
				if (minDistanceGroove < maxDistanceGroove)
				{
					if (minDistanceGroove > value || maxDistanceGroove < value)
					{
						ErrorList.Add($"Расстояние до проточки: {value} не входит" +
						              $" в диапозон от {minDistanceGroove} до {maxDistanceGroove}");
					}
					else
					{
						_distanceGroove = value;
					}
				}
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле диаметра тарелки
		/// </summary>
		public double DiameterPlate
		{
			get => _diameterPlate;
			private set 
			{
				double minDiameterPlate = 2 * DiameterStem;
				const double maxDiameterPlate = 70;
				if (minDiameterPlate < maxDiameterPlate)
				{
					if (minDiameterPlate > value || maxDiameterPlate < value)
					{
						ErrorList.Add($"Диаметр тарелки: {value} не входит" +
						              $" в диапозон от {minDiameterPlate} до {maxDiameterPlate}");
					}
					else
					{
						_diameterPlate = value;
					}
				}
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле толщины тарелки
		/// </summary>
		public double ThicknessPlate
		{
			get => _thicknessPlate;
			private set
			{
				const double minThicknessPlate = 1;
				const double maxThicknessPlate = 10;
				if(minThicknessPlate> value || maxThicknessPlate<value)
				{
					ErrorList.Add($"Толщина тарелки: {value} не входит" +
					              $" в диапозон от {minThicknessPlate} до {maxThicknessPlate}");
				}
				else
				{
					_thicknessPlate = value;
				}
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле длины рабочей фаски
		/// </summary>
		public double LengthChamfer
		{
			get => _lengthChamfer;
			private set
			{
				const double minLengthChamfer = 2;
				const double maxLengthChamfer = 10;
				if(minLengthChamfer>value || maxLengthChamfer<value)
				{
					ErrorList.Add($"Длина фаски: {value} не входит" +
					              $" в диапозон от {minLengthChamfer} до {maxLengthChamfer}");
				}
				else
				{
					_lengthChamfer = value;
				}
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле диаметра плавного перехода
		/// </summary>
		public double RadiusTransition
		{
			get => _radiusTransition;
			private set
			{
				const double minRadiusTransition = 5;
				double maxRadiusTransition = 0.75 * DiameterPlate;
				if (minRadiusTransition < maxRadiusTransition)
				{
					if (minRadiusTransition > value || maxRadiusTransition < value)
					{
						ErrorList.Add($"Радиус перехода: {value} не входит" +
						              $" в диапозон от {minRadiusTransition} до {maxRadiusTransition}");
					}
					else
					{
						_radiusTransition = value;
					}
				}
			}
		}
		
			private string GetErrorMessage(List<string> errorMessages)
			{
				var result = "Проверьте правильность ввода данных:\n\n";

				result += string.Join(";\n\n", errorMessages);
				result += '.';

				return result;
			}

	}
}
