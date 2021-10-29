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
				double lengthValve = double.Parse(textboxLengthValve.Text);
				double diameterStem = double.Parse(textboxDiameterStem.Text);
				double widthGroove = double.Parse(textboxWidthGroove.Text);
				double depthGroove = double.Parse(textboxDepthGroove.Text);
				double distanceGroove = double.Parse(textboxDistanceGroove.Text);
				double diameterPlate = double.Parse(textboxDiameterPlate.Text);
				double thicknessPlate = double.Parse(textboxThicknessPlate.Text);
				double lengthChamfer = double.Parse(textboxLengthChamfer.Text);
				double radiusTransition = double.Parse(textboxRadiusTransition.Text);

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
