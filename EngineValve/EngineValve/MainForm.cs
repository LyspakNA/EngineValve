using System;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using System.Linq;
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
		private EngineValveParameters _parameters;

		private void BuildButton_Click(object sender, EventArgs e)
		{
			try
			{
				double lengthValve = double.Parse(textboxLengthValve.Text);
				double diameterStem = double.Parse(textboxDiameterStem.Text);
				double widthGroove = double.Parse(textboxWidthGroove.Text);
				double depthGroove = double.Parse(textboxDepthGroove.Text);
				double distanceGroove = double.Parse(textboxDistanceGroove.Text);
				double diameterPlate = double.Parse(textboxDiameterPlate.Text);
				double thicknessPlate = double.Parse(textboxThicknessPlate.Text);
				double lengthChamfer = double.Parse(textboxLengthChamfer.Text);
				double radiusTransition = double.Parse(textboxRadiusTransition.Text);

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

		private void SetDefault()
		{
			textboxLengthValve.Text = "100";
			textboxDiameterStem.Text = "8";
			textboxWidthGroove.Text = "2";
			textboxDepthGroove.Text = "2";
			textboxDistanceGroove.Text = "10";
			textboxDiameterPlate.Text = "50";
			textboxThicknessPlate.Text = "2";
			textboxLengthChamfer.Text = "3";
			textboxRadiusTransition.Text = "20";
		}

		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '.') || (e.KeyChar == ','))
			{
				TextBox txt = (TextBox)sender;
				if (txt.Text.Contains(".") || txt.Text.Contains(","))
				{
					e.Handled = true;
				}
				return;
			}

			if (!(Char.IsDigit(e.KeyChar)))
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
			if (double.TryParse(textbox.Text, out double value))
			{
				switch (textbox.Name)
				{
					case "textboxLengthValve":
					{
						labelWidthGroove2.Text = $"(от 1 до {value * 0.1} мм)";
						labelDistanceGroove2.Text = $"(от 5 до {value * 0.25} мм)";
						break;
					}

					case "textboxDiameterStem":
					{
						labelDepthGroove2.Text = $"(от 0.5 до {value * 0.25} мм)";
						labelDiameterPlate2.Text = $"(от {value * 2} до 70 мм)";
						break;
					}

					case "textboxDiameterPlate":
					{
						labelThicknessPlate2.Text = $"(от 1 до {0.75 * value} мм)";
						labelRadiusTransition2.Text = labelThicknessPlate2.Text;
						break;
					}
				}
			}

		}
	}
}
