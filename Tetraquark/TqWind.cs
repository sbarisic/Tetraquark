using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetraquark {
	public partial class TqWind : Form {

		public TqWind() {
			InitializeComponent();
		}

		private void TqWind_Load(object sender, EventArgs e) {
			FormClosed += (S, E) => Program.Running = false;
		}
	}
}
