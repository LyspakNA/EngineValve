using System;
using System.Globalization;
using System.Windows.Forms;
using EngineValveParameter;
using EngineValveBuild;

namespace EngineValve
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			SetDefault();
		}

		private KompasConnector _kompas = new KompasConnector();
		private EngineValveParameters _parameters = new EngineValveParameters();

		private void BuildButton_Click(object sender, EventArgs e)
		{
			try
			{
				double lengthValve = double.Parse(textboxLengthValve.Text,
					CultureInfo.InvariantCulture);
				double diameterStem = double.Parse(textboxDiameterStem.Text,
					CultureInfo.InvariantCulture);
				double widthGroove = double.Parse(textboxWidthGroove.Text, 
					CultureInfo.InvariantCulture);
				double depthGroove = double.Parse(textboxDepthGroove.Text, 
					CultureInfo.InvariantCulture);
				double distanceGroove = double.Parse(textboxDistanceGroove.Text, 
					CultureInfo.InvariantCulture);
				double diameterPlate = double.Parse(textboxDiameterPlate.Text, 
					CultureInfo.InvariantCulture);
				double thicknessPlate = double.Parse(textboxThicknessPlate.Text, 
					CultureInfo.InvariantCulture);
				double lengthChamfer = double.Parse(textboxLengthChamfer.Text,
					CultureInfo.InvariantCulture);
				double radiusTransition = double.Parse(textboxRadiusTransition.Text, 
					CultureInfo.InvariantCulture);

				_parameters = new EngineValveParameters(lengthValve, diameterStem,
			widthGroove, depthGroove, distanceGroove,
			diameterPlate, thicknessPlate, lengthChamfer,
			radiusTransition);

				_kompas.Start();
				var document3D = _kompas.CreateDocument3D();
				var engineValveBuilder = new EngineValveBuilder(document3D, _parameters);
				engineValveBuilder.BuildEngineValve();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, 
					MessageBoxIcon.Error);
			}

		}

		//TODO: Опустить в параметры.
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
		}

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
						break;
					}
				}
			}

		}

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
	}
}
