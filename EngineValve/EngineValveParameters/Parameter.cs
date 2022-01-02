using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineValveParameters
{
	/// <summary>
	/// Шаблонный класс параметров
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Parameter<T> where T : struct
	{
		/// <summary>
		/// Минимальное значение параметра
		/// </summary>
		private T _min;

		/// <summary>
		/// Максимальное значение параметра
		/// </summary>
		private T _max;

		/// <summary>
		/// Присваемое значение парамтра
		/// </summary>
		private T _value;

		/// <summary>
		/// Название параметра для исключений
		/// </summary>
		private ParameterNames _name;

		/// <summary>
		/// Устанавливает или возвращает имя
		/// </summary>
		public ParameterNames Name
		{
			get => _name;
			set => _name = value;
		}

		/// <summary>
		/// Устанавливает или возвращает минимальное значение
		/// </summary>
		public T Min
		{
			get => _min;
			set => _min = value;
		}

		/// <summary>
		/// Устанавливает или возвращает максимальное значение
		/// </summary>
		public T Max
		{
			get => _max;
			set
			{
				var comparerResult =
					Comparer<T>.Default.Compare(value, _min);
				if (comparerResult <= 0)
				{
					throw new Exception("Maximum should be more" +
					                    " or equal minimum");
				}

				_max = value;
			}
		}

		/// <summary>
		/// Устанавливает или возвращает присваемое значение
		/// </summary>
		public T Value
		{
			get => _value;
			set
			{
				var comparerResultMin =
					Comparer<T>.Default.Compare(_min, value);

				if (comparerResultMin > 0)
				{
					throw new Exception($"{Name} should be "
					                    + $"more or equal to {Min}");
				}

				var comparerResultMax =
					Comparer<T>.Default.Compare(value, _max);

				if (comparerResultMax > 0)
				{
					throw new Exception($"{Name} should be "
					                    + $"less or equal to {Max}");
				}

				_value = value;
			}
		}

		/// <summary>
		/// Конструктор шаблона параметров 
		/// </summary>
		/// <param name="name">Название параметра</param>
		/// <param name="max">Максимальное значение</param>
		/// <param name="min">Минимальное значение</param>
		/// <param name="value">Присваемое значение</param>
		public Parameter(ParameterNames name, T max, T min, T value)
		{
			Name = name;
			Min = min;
			Max = max;
			Value = value;
		}
	}
}
