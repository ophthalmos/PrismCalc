﻿namespace PrismCalc
{
  partial class frmHilfe
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHilfe));
            this.tbHilfe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // tbHilfe
            // 
            this.tbHilfe.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbHilfe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbHilfe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbHilfe.Location = new System.Drawing.Point(12, 10);
            this.tbHilfe.Multiline = true;
            this.tbHilfe.Name = "tbHilfe";
            this.tbHilfe.ReadOnly = true;
            this.tbHilfe.Size = new System.Drawing.Size(224, 206);
            this.tbHilfe.TabIndex = 0;
            this.tbHilfe.TabStop = false;
            this.tbHilfe.Text = resources.GetString("tbHilfe.Text");
            this.tbHilfe.Click += new System.EventHandler(this.tbHilfe_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Website/License:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(28, 231);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(151, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://prismcalc.codeplex.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // frmHilfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(241, 251);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbHilfe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHilfe";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Help";
            this.Click += new System.EventHandler(this.Hilfe_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Hilfe_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbHilfe;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.LinkLabel linkLabel1;

  }
}