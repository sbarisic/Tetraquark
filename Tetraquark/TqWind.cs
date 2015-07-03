using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tq {
	public partial class TqWind : Form {

		public TqWind() {
			InitializeComponent();
		}

		private void TqWind_Load(object sender, EventArgs e) {
		}

		protected override void OnClosed(EventArgs e) {
			base.OnClosed(e);
			Program.Running = false;
		}
	}
}