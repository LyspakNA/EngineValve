using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EngineValveBuild;
using EngineValveParameters;

namespace EngineValve
{
	/// <summary>
	/// Класс хранящий и обрабатывающий пользовательский интерфейс плагина
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Коструктор главной формы с необходимыми инициализациями
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			TextBoxDictionary = new Dictionary<ParameterNames, TextBox>()
			{
				{ParameterNames.LengthValve, textboxLengthValve},
				{ParameterNames.DiameterStem, textboxDiameterStem},
				{ParameterNames.WidthGroove, textboxWidthGroove},
				{ParameterNames.DepthGroove, textboxDepthGroove},
				{ParameterNames.DistanceGroove, textboxDistanceGroove},
				{ParameterNames.DiameterPlate, textboxDiameterPlate},
				{ParameterNames.ThicknessPlate, textboxThicknessPlate},
				{ParameterNames.LengthChamfer, textboxLengthChamfer},
				{ParameterNames.RadiusTransition, textboxRadiusTransition},
				{ParameterNames.DiameterNeckline, textBoxDiameterNeckline},
				{ParameterNames.DepthNeckline, textBoxDepthNeckline}
			};
			ExtraPanelEnabler(checkBoxNeckline);
			SetDefault();
		}

		/// <summary>
		/// Объект класса с параметрами
		/// </summary>
		private EngineValveParameterCollection _parameters = new EngineValveParameterCollection();

		/// <summary>
		/// Словарь пар текстбокс - имя парвметра
		/// </summary>
		public Dictionary<ParameterNames, TextBox> TextBoxDictionary { get; }

		/// <summary>
		/// Обработчик нажатия кнопки "Построить".
		/// Запускает проверку введённых значений.
		/// Проверяет наличие записей в словаре ошибок
		/// и выводит их в сообщении.
		/// Запускает построение детали
		/// </summary>
		private void BuildButton_Click(object sender, EventArgs e)
		{
			try
			{
				_parameters.ErrorsDictionary.Clear();

				_parameters.LengthValve = double.Parse(textboxLengthValve.Text,
					CultureInfo.InvariantCulture);
				_parameters.DiameterStem = double.Parse(textboxDiameterStem.Text,
					CultureInfo.InvariantCulture);
				_parameters.WidthGroove = double.Parse(textboxWidthGroove.Text,
					CultureInfo.InvariantCulture);
				_parameters.DepthGroove = double.Parse(textboxDepthGroove.Text,
					CultureInfo.InvariantCulture);
				_parameters.DistanceGroove = double.Parse(textboxDistanceGroove.Text,
					CultureInfo.InvariantCulture);
				_parameters.DiameterPlate = double.Parse(textboxDiameterPlate.Text,
					CultureInfo.InvariantCulture);
				_parameters.ThicknessPlate = double.Parse(textboxThicknessPlate.Text,
					CultureInfo.InvariantCulture);
				_parameters.LengthChamfer = double.Parse(textboxLengthChamfer.Text,
					CultureInfo.InvariantCulture);
				_parameters.RadiusTransition = double.Parse(textboxRadiusTransition.Text,
					CultureInfo.InvariantCulture);
				_parameters.DiameterNeckline = double.Parse(textBoxDiameterNeckline.Text,
					CultureInfo.InvariantCulture);
				_parameters.DepthNeckline = double.Parse(textBoxDepthNeckline.Text,
					CultureInfo.InvariantCulture);

				if (_parameters.ErrorsDictionary.Any())
				{
					string message = null;
					foreach (var param in
						_parameters.ErrorsDictionary.Keys)
					{
						message +=
							_parameters.ErrorsDictionary[param]
							+ "\n";
					}
					throw new Exception(message);
				}

				var engineValveBuilder = new EngineValveBuilder(_parameters);
				engineValveBuilder.BuildEngineValve();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

		}
		
		/// <summary>
		/// Записывает стандартные значения в текстбоксы
		/// </summary>
		private void SetDefault()
		{
			textboxLengthValve.Text =
				_parameters.LengthValve.ToString(CultureInfo.InvariantCulture);
			textboxDiameterStem.Text =
				_parameters.DiameterStem.ToString(CultureInfo.InvariantCulture);
			textboxWidthGroove.Text =
				_parameters.WidthGroove.ToString(CultureInfo.InvariantCulture);
			textboxDepthGroove.Text =
				_parameters.DepthGroove.ToString(CultureInfo.InvariantCulture);
			textboxDistanceGroove.Text =
				_parameters.DistanceGroove.ToString(CultureInfo.InvariantCulture);
			textboxDiameterPlate.Text =
				_parameters.DiameterPlate.ToString(CultureInfo.InvariantCulture);
			textboxThicknessPlate.Text =
				_parameters.ThicknessPlate.ToString(CultureInfo.InvariantCulture);
			textboxLengthChamfer.Text =
				_parameters.LengthChamfer.ToString(CultureInfo.InvariantCulture);
			textboxRadiusTransition.Text =
				_parameters.RadiusTransition.ToString(CultureInfo.InvariantCulture);
			textBoxDiameterNeckline.Text =
				_parameters.DiameterNeckline.ToString(CultureInfo.InvariantCulture);
			textBoxDepthNeckline.Text =
				_parameters.DepthNeckline.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Обработчик ввода 
		/// </summary>

		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '.') || (e.KeyChar == ','))
			{
				e.KeyChar = '.';
				TextBox txt = (TextBox) sender;
				if (txt.Text.Contains("."))
				{
					e.Handled = true;
				}

				return;
			}

			if (!(char.IsDigit(e.KeyChar)))
			{
				if ((e.KeyChar != (char) Keys.Back))
				{
					e.Handled = true;
				}
			}
		}

		/// <summary>
		/// Обработчик, устанавливающий значения диапазонов
		/// </summary>
		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			var textbox = (TextBox) sender;
			CheckText(textbox);
			if (double.TryParse(textbox.Text, NumberStyles.Float,
				CultureInfo.InvariantCulture, out double value))
            {
				if(textbox == TextBoxDictionary[ParameterNames.LengthValve])
				{
						labelValueWidth.Text = $"(от 1 до {value * 0.1} мм)";
						labelValueDistance.Text = $"(от 5 до {value * 0.25} мм)";
                }
				else if (textbox == TextBoxDictionary[ParameterNames.DiameterStem])
				{
						labelValueDepth.Text = $"(от 0.5 до {value * 0.25} мм)";
						labelValueDiameterPlate.Text = $"(от {value * 2} до 70 мм)";
				}

				else if(textbox == TextBoxDictionary[ParameterNames.DiameterPlate])
				{
						labelValueTransition.Text = $"(от 5 до {0.75 * value} мм)";
						labelNecklineDiam.Text = $"(до {value} мм)";
				}
				else if(textbox == TextBoxDictionary[ParameterNames.ThicknessPlate])
				{
						labelNecklaneDep.Text = $"(до {value * 4} мм)";
				}
			}

		}

		/// <summary>
		/// Обработчик, блокирующий зависимые поля
		/// </summary>
		private void CheckText(TextBox textbox)
		{
			if(textbox == TextBoxDictionary[ParameterNames.LengthValve])
            {
				//TODO:
                textboxWidthGroove.Enabled = textboxDistanceGroove.Enabled = textbox.Text != "";
				
                if (textbox.Text == "")
				{
					textboxWidthGroove.Enabled = false;
					textboxDistanceGroove.Enabled = false;
				}

				else
				{
					textboxWidthGroove.Enabled = true;
					textboxDistanceGroove.Enabled = true;
				}
			}
			else if (textbox == TextBoxDictionary[ParameterNames.DiameterStem])
			{
				if (textbox.Text == "")
				{
					textboxDepthGroove.Enabled = false;
					textboxDiameterPlate.Enabled = false;
				}
				else
				{
					textboxDepthGroove.Enabled = true;
					textboxDiameterPlate.Enabled = true;
				}
			}

			else if (textbox == TextBoxDictionary[ParameterNames.DiameterPlate])
			{
				//TODO:
					if (textbox.Text == "")
					{
						textboxRadiusTransition.Enabled = false;
					}
					else
					{
						textboxRadiusTransition.Enabled = true;
					}
			}
		}

		/// <summary>
		/// Обработчик смены статуса CheckBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxNeckline_CheckedChanged(object sender, EventArgs e)
		{
			var checkbox = (CheckBox) sender;
			ExtraPanelEnabler(checkbox);

		}

		/// <summary>
		/// Активирует доп.панель в зависимости от статуса чекбокса
		/// </summary>
		/// <param name="checkbox">Чекбокс для активации панели</param>
		private void ExtraPanelEnabler(CheckBox checkbox)
		{
			if (checkbox.Checked)
			{
				this.Height = 500;
				BuildButton.Location = new Point(300, 390);
				groupBox1.Visible = true;
				_parameters.CreateNeckline = true;
			}
			else
			{
				this.Height = 440;
				BuildButton.Location = new Point(35, 320);
				groupBox1.Visible = false;
				_parameters.CreateNeckline = false;
			}
		}

	}
}
