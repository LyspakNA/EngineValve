using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineValveParameters
{
	/// <summary>
	/// Перечисление с именами параметров
	/// </summary>
	public enum ParameterNames
	{
		/// <summary>
		///	Длина клапана
		/// </summary>
		LengthValve,

		/// <summary>
		/// Диаметр клапана
		/// </summary>
		DiameterStem,

		/// <summary>
		/// Ширина проточки
		/// </summary>
		WidthGroove,

		/// <summary>
		/// Глубина проточки
		/// </summary>
		DepthGroove,

		/// <summary>
		/// Расстояние до проточки
		/// </summary>
		DistanceGroove,

		/// <summary>
		/// Диаметр тарелки
		/// </summary>
		DiameterPlate,
		
		/// <summary>
		/// Толщина тарелки
		/// </summary>
		ThicknessPlate,

		/// <summary>
		/// Длина рабочей фаски
		/// </summary>
		LengthChamfer,

		/// <summary>
		/// Радиус плавного перехода
		/// </summary>
		RadiusTransition,

		/// <summary>
		/// Диаметр выреза в тарелке
		/// </summary>
		DiameterNeckline,

		/// <summary>
		/// Глубина выреза в тарелке
		/// </summary>
		DepthNeckline,
	}
}
