using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryManagement.Business;


namespace InventoryReport
{
    public partial class frmFrontEnd : Form
    {
        public frmFrontEnd()
        {
            InitializeComponent();
            tableLayoutPanel1.Visible = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessRules businessRules;

            tableLayoutPanel1.Visible = false;

            openXMLFileDlg.Filter = "XML Files (*.xml)|*.xml";
            openXMLFileDlg.FileName = "Inventory";
            openXMLFileDlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (openXMLFileDlg.ShowDialog(Owner) == DialogResult.OK)
            {
                try
                {
                    businessRules = new BusinessRules(openXMLFileDlg.FileName);
                }
                catch (Exception ex)
                {
                    string m = string.Format("Error {0} reading file {1}", ex.Message, openXMLFileDlg.FileName);
                    MessageBox.Show(m, "Open file failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dataGridView1.DataSource = businessRules.sourceInventory.inventoryProducts;
                dataGridView2.DataSource = businessRules.updatedInventory().reportProducts;

                labSource.Text = openXMLFileDlg.FileName;
                tableLayoutPanel1.Visible = true;
            }

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
