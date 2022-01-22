using System;
using System.Collections.Generic;

namespace EngineValveParameters
{
	/// <summary>
	/// Параметры клапана.
	/// </summary>
	public class EngineValveParameterCollection
	{
		/// <summary>
		/// Длина клапана
		/// </summary>
		private static Parameter<double> _lengthValve;
		/// <summary>
		/// Диаметр ножки
		/// </summary>
		private static Parameter<double> _diameterStem;
		/// <summary>
		/// Ширина проточки под сухарь
		/// </summary>
		private static Parameter<double> _widthGroove;
		/// <summary>
		/// Глубина проточки под сухарь
		/// </summary>
		private static Parameter<double> _depthGroove;
		/// <summary>
		/// Расстояние до проточки
		/// </summary>
		private static Parameter<double> _distanceGroove;
		/// <summary>
		/// Диаметр тарелки клапана
		/// </summary>
		private static Parameter<double> _diameterPlate;
		/// <summary>
		/// Толщина тарелки клапана
		/// </summary>
		private static Parameter<double> _thicknessPlate;
		/// <summary>
		/// Длина рабочей фаски
		/// </summary>
		private static Parameter<double> _lengthChamfer;
		/// <summary>
		/// радиус плавного перехода
		/// </summary>
		private static Parameter<double> _radiusTransition;
		/// <summary>
		/// Диаметр выреза в тарелке
		/// </summary>
		private static Parameter<double> _diameterNeckline;
		/// <summary>
		/// Глубина выреза
		/// </summary>
		private static Parameter<double> _depthNeckline;
		/// <summary>
		/// Поле определяющее будет ли построен вырез
		/// </summary>
		private bool _createNeckline;

		/// <summary>
		/// Словарь, сопоставляющий
		/// названия параметров и их поля
		/// </summary>
		private Dictionary<ParameterNames, Parameter<double>> _parametersDictionary =
			new Dictionary<ParameterNames, Parameter<double>>()
			{
				{ParameterNames.LengthValve, _lengthValve},
				{ParameterNames.DiameterStem, _diameterStem},
				{ParameterNames.WidthGroove, _widthGroove},
				{ParameterNames.DepthGroove, _depthGroove},
				{ParameterNames.DistanceGroove, _distanceGroove},
				{ParameterNames.DiameterPlate, _diameterPlate},
				{ParameterNames.ThicknessPlate, _thicknessPlate},
				{ParameterNames.LengthChamfer, _lengthChamfer},
				{ParameterNames.RadiusTransition, _radiusTransition},
				{ParameterNames.DiameterNeckline, _diameterNeckline},
				{ParameterNames.DepthNeckline, _depthNeckline}
			};
		
		//TODO: RSDN
		/// <summary>
		/// Свойство, обрабатывающее словарь ошибок
		/// </summary>
		public Dictionary<ParameterNames, string> ErrorsDictionary { get; } = new Dictionary<ParameterNames, string>();

		/// <summary>
		/// Конструктор для стандартных параметров.
		/// </summary>
		public EngineValveParameterCollection()
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
			DiameterNeckline = 0;
			DepthNeckline = 0;
			CreateNeckline = false;
		}
		
		/// <summary>
		/// Свойство обрабатывающее поле длины клапана
		/// </summary>
		public double LengthValve
		{
			get => _lengthValve.Value;
			set
			{
				const double minLengthValve = 50;
				const double maxLengthValve = 150;
				_lengthValve = SetValue(ParameterNames.LengthValve,
					maxLengthValve, minLengthValve, value);
			}
		}
		/// <summary>
		/// Свойство обрабатывающее поле диаметра ножки
		/// </summary>
			public double DiameterStem
		{
			get => _diameterStem.Value;
			set
			{
				const double minDiameterStem = 5;
				const double maxDiameterStem = 15;
				_diameterStem = SetValue(ParameterNames.DiameterStem,
					maxDiameterStem, minDiameterStem, value);
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле ширины проточки
		/// </summary>
		public double WidthGroove
		{
			get => _widthGroove.Value;
			set
			{
				const double minWidthGroove = 1;
				double maxWidthGroove = 0.1 * LengthValve;
				_widthGroove = SetValue(ParameterNames.WidthGroove,
					maxWidthGroove, minWidthGroove, value);
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле глубины проточки
		/// </summary>
		public double DepthGroove
		{
			get => _depthGroove.Value;
			set
			{
				const double minDepthGroove = 0.5;
				double maxDepthGroove = 0.25 * DiameterStem;
				_depthGroove = SetValue(ParameterNames.DepthGroove,
					maxDepthGroove, minDepthGroove, value);
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле расстояния до проточки
		/// </summary>
		public double DistanceGroove
		{
			get => _distanceGroove.Value;
			set
			{
				const double minDistanceGroove = 5;
				double maxDistanceGroove = 0.25 * LengthValve;
				_distanceGroove = SetValue(ParameterNames.DistanceGroove,
					maxDistanceGroove, minDistanceGroove, value);
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле диаметра тарелки
		/// </summary>
		public double DiameterPlate
		{
			get => _diameterPlate.Value;
			set 
			{
				double minDiameterPlate = 2 * DiameterStem;
				const double maxDiameterPlate = 70;
				_diameterPlate = SetValue(ParameterNames.DiameterPlate,
					maxDiameterPlate, minDiameterPlate, value);
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле толщины тарелки
		/// </summary>
		public double ThicknessPlate
		{
			get => _thicknessPlate.Value;
			set
			{
				const double minThicknessPlate = 1;
				const double maxThicknessPlate = 10;
				_thicknessPlate = SetValue(ParameterNames.ThicknessPlate,
					maxThicknessPlate, minThicknessPlate, value);
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле длины рабочей фаски
		/// </summary>
		public double LengthChamfer
		{
			get => _lengthChamfer.Value;
			set
			{
				const double minLengthChamfer = 2;
				const double maxLengthChamfer = 10;
				_lengthChamfer = SetValue(ParameterNames.LengthChamfer,
					maxLengthChamfer, minLengthChamfer, value);
			}
		}
		/// <summary>
		/// Свойство обрабатыващее поле диаметра плавного перехода
		/// </summary>
		public double RadiusTransition
		{
			get => _radiusTransition.Value;
			set
			{
				const double minRadiusTransition = 5;
				double maxRadiusTransition = 0.75 * DiameterPlate;
				_radiusTransition = SetValue(ParameterNames.RadiusTransition,
						maxRadiusTransition, minRadiusTransition, value);
			}
		}
		/// <summary>
		/// Диаметр выреза в тарелке
		/// </summary>
		public double DiameterNeckline
		{
			get => _diameterNeckline.Value;
			set
			{
				double maxDiameterNeckline = DiameterPlate;
				_diameterNeckline = SetValue(ParameterNames.DiameterNeckline,
						maxDiameterNeckline, 0, value);
			}
		}

		/// <summary>
		/// Глубина выреза в тарелке
		/// </summary>
		public double DepthNeckline
		{
			get => _depthNeckline.Value;
			set
			{
				double maxDepthNeckline = ThicknessPlate * 4;
				_depthNeckline = SetValue(ParameterNames.DepthNeckline,
					maxDepthNeckline, 0, value);
			}
		}
		/// <summary>
		/// Устанавливает, будет ли построен вырез в тарелке.
		/// </summary>
		public bool CreateNeckline
		{
			get => _createNeckline;
			set => _createNeckline = value;
		}

		/// <summary>
		/// Приватный метод инициализирующий
		/// поля параметров
		/// </summary>
		/// <param name="name">Название параметра</param>
		/// <param name="max">Максимальное значение</param>
		/// <param name="min">Минимальное значение</param>
		/// <param name="value">Устанавливаемое значение</param>
		/// <returns>Экземпляр параметров</returns>
		private Parameter<double> SetValue(ParameterNames name, double max, double min, double value)
		{
			try
			{
				return _parametersDictionary[name] = 
					new Parameter<double>(name, max, min, value);
			}
			catch (Exception ex)
			{
				ErrorsDictionary.Add(name, ex.Message);
			}

			return _parametersDictionary[name];
		}
    }
}
