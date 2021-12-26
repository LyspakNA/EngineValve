using System.Collections.Generic;

namespace EngineValveParameters
{
	/// <summary>
	/// Параметры клапана.
	/// </summary>
	public class EngineValveParameter
	{
		/// <summary>
		/// Длина клапана
		/// </summary>
		private Parameter<double> _lengthValve;
		/// <summary>
		/// Диаметр ножки
		/// </summary>
		private Parameter<double> _diameterStem;
		/// <summary>
		/// Ширина проточки под сухарь
		/// </summary>
		private Parameter<double> _widthGroove;
		/// <summary>
		/// Глубина проточки под сухарь
		/// </summary>
		private Parameter<double> _depthGroove;
		/// <summary>
		/// Расстояние до проточки
		/// </summary>
		private Parameter<double> _distanceGroove;
		/// <summary>
		/// Диаметр тарелки клапана
		/// </summary>
		private Parameter<double> _diameterPlate;
		/// <summary>
		/// Толщина тарелки клапана
		/// </summary>
		private Parameter<double> _thicknessPlate;
		/// <summary>
		/// Длина рабочей фаски
		/// </summary>
		private Parameter<double> _lengthChamfer;
		/// <summary>
		/// радиус плавного перехода
		/// </summary>
		private Parameter<double> _radiusTransition;
		/// <summary>
		/// Диаметр выреза в тарелке
		/// </summary>
		private Parameter<double> _diameterNeckline;
		/// <summary>
		/// Глубина выреза
		/// </summary>
		private Parameter<double> _depthNeckline;


		/// <summary>
		/// Конструктор для стандартных параметров.
		/// </summary>
		public EngineValveParameter()
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
				_lengthValve = new Parameter<double>("Length Value",
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
				_diameterStem = new Parameter<double>("Diameter Stem",
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
				_widthGroove = new Parameter<double>("Width Groove",
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
				_depthGroove = new Parameter<double>("Depth Groove",
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
				_distanceGroove = new Parameter<double>("Distance Groove",
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
				_diameterPlate = new Parameter<double>("Diameter Plate",
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
				_thicknessPlate = new Parameter<double>("Thickness Plate",
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
				_lengthChamfer = new Parameter<double>("Length Chamfer",
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
				_radiusTransition =
					new Parameter<double>("Radius Transition",
						maxRadiusTransition, minRadiusTransition, value);
			}
		}

		public double DiameterNeckline
		{
			get => _diameterNeckline.Value;
			set
			{
				double maxDiameterNeckline = DiameterPlate;
				_diameterNeckline =
					new Parameter<double>("Diameter Neckline",
						maxDiameterNeckline, 0, value);
			}
		}

		public double DepthNeckline
		{
			get => _depthNeckline.Value;
			set
			{
				double maxDepthNeckline = ThicknessPlate * 4;
				_depthNeckline =
					new Parameter<double>("Depth Neckline", maxDepthNeckline, 0, value);
			}
		}

	}
}
