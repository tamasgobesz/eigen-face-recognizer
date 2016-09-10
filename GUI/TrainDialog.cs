using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FaceDetection
{
    public partial class TrainDialog : Form
    {
        private MainWindow parent;
        public TrainDialog(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            btOk.DialogResult = DialogResult.OK;
            btCancel.DialogResult = DialogResult.Cancel;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            parent.createNewSubject(tbName.Text);
        }
    }
}
