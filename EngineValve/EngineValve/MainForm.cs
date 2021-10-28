using System;
using System.Windows.Forms;
using System.Linq;
using EngineValveParameter;

namespace EngineValve
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void BuildButton_Click(object sender, EventArgs e)
		{
			try
			{
				double lengthValve = double.Parse(LengthValveTextBox.Text);
				double diameterStem = double.Parse(DiameterStemTextBox.Text);
				double widthGroove = double.Parse(WidthGrooveTextBox.Text);
				double depthGroove = double.Parse(DepthGrooveTextBox.Text);
				double distanceGroove = double.Parse(DistanceGrooveTextBox.Text);
				double diameterPlate = double.Parse(DiameterPlateTextBox.Text);
				double thicknessPlate = double.Parse(ThicknessPlateTextBox.Text);
				double lengthChamfer = double.Parse(LengthChamferTextBox.Text);
				double radiusTransition = double.Parse(RadiusTransitionTextBox.Text);

				var engineValveParameters = new EngineValveParameters(lengthValve, diameterStem,
			widthGroove, depthGroove, distanceGroove,
			diameterPlate, thicknessPlate, lengthChamfer,
			radiusTransition);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, 
					MessageBoxIcon.Error);
			}

		}


		private bool CheckValue (double value, double min, double max)
		{
			if (value < min || value > max)
			{
				return false;
			}
			return true;
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
	}
}
