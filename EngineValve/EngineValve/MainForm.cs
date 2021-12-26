using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
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
			ExtraPanel(checkBoxNeckline);
			SetDefault();
		}
		
		/// <summary>
		/// Объект класса с параметрами
		/// </summary>
		private EngineValveParameter _parameters = new EngineValveParameter();
		/// <summary>
		/// Обработчик нажатия кнопки "Построить"
		/// </summary>
		private void BuildButton_Click(object sender, EventArgs e)
		{
			try
			{
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

				var engineValveBuilder = new EngineValveBuilder(_parameters);
				engineValveBuilder.BuildEngineValve();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

		}

		//TODO: Опустить в параметры.
		/// <summary>
		/// Устанавливает стандартные значения
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
				TextBox txt = (TextBox)sender;
				if (txt.Text.Contains("."))
				{
					e.Handled = true;
				}
				return;
			}

			if (!(char.IsDigit(e.KeyChar)))
			{
				if ((e.KeyChar != (char)Keys.Back))
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
				//TODO: Убрать названия объектов
				switch (textbox.Tag)
				{
					case "LengthValve":
					{
						labelValueWidth.Text = $"(от 1 до {value * 0.1} мм)";
						labelValueDistance.Text = $"(от 5 до {value * 0.25} мм)";
						break;
					}

					case "DiameterStem":
					{
						labelValueDepth.Text = $"(от 0.5 до {value * 0.25} мм)";
						labelValueDiameterPlate.Text = $"(от {value * 2} до 70 мм)";
						break;
					}

					case "DiameterPlate":
					{
						labelValueTransition.Text = $"(от 5 до {0.75 * value} мм)";
						labelNecklineDiam.Text = $"(до {value} мм)";
						break;
					}
					case "ThicknessPlate":
					{
						labelNecklaneDep.Text = $"(до {value * 4} мм)";
						break;
					}
				}
			}

		}
		/// <summary>
		/// Обработчик, блокирующий зависимые поля
		/// </summary>
		private void CheckText(TextBox textbox)
		{
			switch (textbox.Tag)
			{
				case "LengthValve":
				{
					if (textbox.Text == "")
					{
						textboxWidthGroove.Enabled = false;
						textboxDistanceGroove.Enabled = false;
						break;
					}

					textboxWidthGroove.Enabled = true;
					textboxDistanceGroove.Enabled = true;
					break;
				}

				case "DiameterStem":
				{
					if (textbox.Text == "")
					{
						textboxDepthGroove.Enabled = false;
						textboxDiameterPlate.Enabled = false;
						break;
					}

					textboxDepthGroove.Enabled = true;
					textboxDiameterPlate.Enabled = true;
					break;
				}

				case "DiameterPlate":
				{
					if (textbox.Text == "")
					{
						textboxRadiusTransition.Enabled = false;
						break;
					}

					textboxRadiusTransition.Enabled = true;
					break;
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
			ExtraPanel(checkbox);
			
		}

		/// <summary>
		/// Активирует доп.панель в зависимости от статуса чекбокса
		/// </summary>
		/// <param name="checkbox">Чекбокс для активации панели</param>
		private void ExtraPanel(CheckBox checkbox)
		{
			if (checkbox.Checked)
			{
				this.Height = 500;
				BuildButton.Location = new Point(300, 390);
				groupBox1.Visible = true;
			}
			else
			{
				this.Height = 440;
				BuildButton.Location = new Point(35, 320);
				groupBox1.Visible = false;
			}
		}
	}
}
