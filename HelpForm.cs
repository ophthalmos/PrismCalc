using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Diagnostics;
//using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrismCalc
{
  public partial class frmHilfe : Form
  {
    public frmHilfe()
    {
      InitializeComponent();
    }

    private void Hilfe_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space || e.KeyCode == Keys.F1) this.Close();
    }

    private void Hilfe_Click(object sender, EventArgs e) { this.Close(); }
    private void tbHilfe_Click(object sender, EventArgs e) { this.Close(); }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        try { Process.Start("https://www.netradio.info/medical/#prismcalc"); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
    }

  }
}
