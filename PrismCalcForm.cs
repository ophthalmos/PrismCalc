using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PrismCalc
{
    public partial class Form1 : Form
    {
        public frmHilfe f2 = new frmHilfe();
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            //SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            this.linkLabel1.TabStop = false;
            rbPrsmHorizontRE.MouseWheel += new MouseEventHandler(MouseWheelHandlerHorizont);
            rbPrsmHorizontLE.MouseWheel += new MouseEventHandler(MouseWheelHandlerHorizont);
            rbPrsmHorizontOt.MouseWheel += new MouseEventHandler(MouseWheelHandlerHorizont);
            rbPrsmHorizontIn.MouseWheel += new MouseEventHandler(MouseWheelHandlerHorizont);
            rbPrsmVerticalRE.MouseWheel += new MouseEventHandler(MouseWheelHandlerVertical);
            rbPrsmVerticalLE.MouseWheel += new MouseEventHandler(MouseWheelHandlerVertical);
            rbPrsmVerticalUp.MouseWheel += new MouseEventHandler(MouseWheelHandlerVertical);
            rbPrsmVerticalDn.MouseWheel += new MouseEventHandler(MouseWheelHandlerVertical);
        }

        double maxPwr = 50;                   // maximale Prismenstärke
        bool reyHorizont, reyVertical;
        double powerHorizont, powerVertical, basisHorizont, basisVertical;
        double reyPower, leyPower, resPower, reyBase, leyBase;
        double pt = 19.0;                     // prismthickness = Maßfaktor für die Grafiken
        int x0RE = 50, y0RE = 115;            // absolute Bezugskoordinaten
        int x0LE = 144, y0LE = 115;           // absolute Bezugskoordinaten
        int x1 = 205, y1 = 184;               // Zentrum des Halbkreises auf TabPage2
        int nd = 36, d5 = 180;                // Abstand der Halbkreise
        string cbTxt;                         // Clipboard-Text, wird beim Beenden erstellt

        void Form1_KeyDown(object sender, KeyEventArgs e)
        { // MessageBox.Show(e.KeyCode.ToString()); // nützlich um KeyCode zu finden
            if (e.KeyCode == Keys.Escape) this.Close(); // Formular schließen
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // dadurch wird Beep-Ton unterdrückt!
                e.Handled = true;
                if (tabControl1.SelectedIndex == 0)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                else if (ActiveControl.Name.Contains("nudOblPwr"))
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                else if (ActiveControl.Name.Contains("nudOblBas"))
                {
                    this.SelectNextControl(this.ActiveControl, false, true, true, true);
                }
                else if (ActiveControl.Name.Contains("nudConvSphere"))
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                else if (ActiveControl.Name.Contains("nudConversion"))
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
            }
            else if (e.KeyCode == Keys.F1) btnF1.PerformClick();
            else if (e.KeyCode == Keys.F2) btnF2.PerformClick();
            else if (e.KeyCode == Keys.F3) btnF3.PerformClick();
            else if ((e.KeyCode == Keys.OemPeriod) ||
                (e.Alt && e.Control && e.KeyCode == Keys.OemBackslash) || // |
                (e.Alt && e.Control && e.KeyCode == Keys.Oemplus) ||      // ~
                (e.Alt && e.Control && e.KeyCode == Keys.E) ||            // €
                (e.Alt && e.Control && e.KeyCode == Keys.M) ||            // µ
                (e.Alt && e.Control && e.KeyCode == Keys.Q))              // @
            {  // Eingabe eines Punkt, @, € oder µ-Zeichens verhindern
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.H && tabControl1.SelectedIndex == 0)
            { nudPowerHorizont.Focus(); e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.T)
            {
                if (tabControl1.SelectedIndex == 0) { tabControl1.SelectedIndex = 1; }
                else { tabControl1.SelectedIndex = 0; }
                e.SuppressKeyPress = true; e.Handled = true;
            }
            else if (e.KeyCode == Keys.V && tabControl1.SelectedIndex == 0)
            { nudPowerVertical.Focus(); e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.O && tabControl1.SelectedIndex == 0)
            { rbPrsmVerticalUp.Checked = true; e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.U && tabControl1.SelectedIndex == 0)
            { rbPrsmVerticalDn.Checked = true; e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.A && tabControl1.SelectedIndex == 0)
            { rbPrsmHorizontOt.Checked = true; e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.I && tabControl1.SelectedIndex == 0)
            { rbPrsmHorizontIn.Checked = true; e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.R && ActiveControl.Name.Contains("Horizont") && rbPrsmHorizontLE.Checked)
            { rbPrsmHorizontRE.Checked = true; e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.L && ActiveControl.Name.Contains("Horizont") && rbPrsmHorizontRE.Checked)
            { rbPrsmHorizontLE.Checked = true; e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.R && ActiveControl.Name.Contains("Vertical") && rbPrsmVerticalLE.Checked)
            { rbPrsmVerticalRE.Checked = true; e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.L && ActiveControl.Name.Contains("Vertical") && rbPrsmVerticalRE.Checked)
            { rbPrsmVerticalLE.Checked = true; e.SuppressKeyPress = true; e.Handled = true; Calculation(); }
            else if (e.KeyCode == Keys.Home)
            {
                if (this.TopMost == true)
                {
                    this.TopMost = false;
                    this.Text = "Prismenrechner";
                }
                else
                {
                    this.TopMost = true;
                    this.Text = this.Text + " (immer im Vordergrund)";
                }
            }
        }

        // Fokus setzten
        private void TabPage1_Enter(object sender, EventArgs e) { nudPowerHorizont.Focus(); }
        private void MouseWheelHandlerHorizont(object sender, MouseEventArgs e) { nudPowerHorizont.Focus(); }
        private void MouseWheelHandlerVertical(object sender, MouseEventArgs e) { nudPowerVertical.Focus(); }
        // Inhalt bei Fokuseintritt markieren
        private void NudConversion_Enter(object sender, EventArgs e) { nudConversion.Select(0, nudConversion.Text.Length); }
        private void NudConvSphere_Enter(object sender, EventArgs e) { nudConvSphere.Select(0, nudConvSphere.Text.Length); }
        private void NudOblBas_Enter(object sender, EventArgs e) { nudOblBas.Select(0, nudOblBas.Text.Length); }
        private void NudOblPwr_Enter(object sender, EventArgs e) { nudOblPwr.Select(0, nudOblPwr.Text.Length); }
        private void NudPowerHorizont_Enter(object sender, EventArgs e) { nudPowerHorizont.Select(0, nudPowerHorizont.Text.Length); }
        private void NudPowerVertical_Enter(object sender, EventArgs e) { nudPowerVertical.Select(0, nudPowerVertical.Text.Length); }
        private void NudResultPowerLE_Enter(object sender, EventArgs e) { nudResultPowerLE.Select(0, nudResultPowerLE.Text.Length); }
        private void NudResultPowerRE_Enter(object sender, EventArgs e) { nudResultPowerRE.Select(0, nudResultPowerRE.Text.Length); }
        // Berechnung auslösen
        private void NudConversion_DownButtonClicked(object sender, EventArgs e) { Conversion(); }
        private void NudConversion_KeyUp(object sender, KeyEventArgs e) { Conversion(); }
        private void NudConversion_UpButtonClicked(object sender, EventArgs e) { Conversion(); }
        private void NudConvSphere_DownButtonClicked(object sender, EventArgs e) { Conversion(); }
        private void NudConvSphere_KeyUp(object sender, KeyEventArgs e) { Conversion(); }
        private void NudConvSphere_UpButtonClicked(object sender, EventArgs e) { Conversion(); }
        
        private void NudOblBas_DownButtonClicked(object sender, EventArgs e)
        {
            Double.TryParse(nudOblBas.Text, out double axs);
            axs = axs == 0 ? axs + 360 : axs;
            nudOblBas.Text = axs.ToString();
            CalcDivide();
        }
        private void NudOblBas_UpButtonClicked(object sender, EventArgs e)
        {
            Double.TryParse(nudOblBas.Text, out double axs);
            axs = axs == 360 ? axs = 0 : axs;
            nudOblBas.Text = axs.ToString();
            CalcDivide();
        }
        private void NudOblBas_KeyUp(object sender, KeyEventArgs e) { CalcDivide(); }
        private void NudOblPwr_DownButtonClicked(object sender, EventArgs e) { CalcDivide(); }
        private void NudOblPwr_KeyUp(object sender, KeyEventArgs e) { CalcDivide(); }
        private void NudOblPwr_UpButtonClicked(object sender, EventArgs e) { CalcDivide(); }

        private void NudPowerHorizont_DownButtonClicked(object sender, EventArgs e) { Calculation(); }
        private void NudPowerHorizont_KeyUp(object sender, KeyEventArgs e) { Calculation(); }
        private void NudPowerHorizont_UpButtonClicked(object sender, EventArgs e) { Calculation(); }
        private void NudPowerVertical_DownButtonClicked(object sender, EventArgs e) { Calculation(); }
        private void NudPowerVertical_KeyUp(object sender, KeyEventArgs e) { Calculation(); }
        private void NudPowerVertical_UpButtonClicked(object sender, EventArgs e) { Calculation(); }
        private void RbPrsmHorizontOt_CheckedChanged(object sender, EventArgs e) { Calculation(); }
        private void RbPrsmHorizontRE_CheckedChanged(object sender, EventArgs e) { Calculation(); }
        private void RbPrsmHorizontUp_CheckedChanged(object sender, EventArgs e) { Calculation(); }
        private void RbPrsmVerticalRE_CheckedChanged(object sender, EventArgs e) { Calculation(); }

        private void NudResultPowerLE_DownButtonClicked(object sender, EventArgs e) { SpreadOut(); }
        private void NudResultPowerLE_KeyUp(object sender, KeyEventArgs e) { SpreadOut(); }
        private void NudResultPowerLE_UpButtonClicked(object sender, EventArgs e) { SpreadOut(); }
        private void NudResultPowerRE_DownButtonClicked(object sender, EventArgs e) { SpreadOut(); }
        private void NudResultPowerRE_KeyUp(object sender, KeyEventArgs e) { SpreadOut(); }
        private void NudResultPowerRE_UpButtonClicked(object sender, EventArgs e) { SpreadOut(); }

        private void LinklblWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            {
                try { System.Diagnostics.Process.Start("http://www.schielen.de/software.htm"); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Prismenrechner"); }
            }
        }

        private void NudPowerHorizont_KeyDown(object sender, KeyEventArgs e)
        {
            double pwr;
            if (e.Shift && e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true; // Shift-Down würde Zeichen markieren
                Double.TryParse(nudPowerHorizont.Text, out pwr);
                pwr = e.Control ? pwr + 10 : pwr + 3;
                nudPowerHorizont.Text = pwr.ToString();
            }
            else if (e.Shift && e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true; // Shift-Down würde Zeichen markieren
                Double.TryParse(nudPowerHorizont.Text, out pwr);
                pwr = e.Control ? pwr - 10 : pwr - 3;
                nudPowerHorizont.Text = pwr.ToString();
            }
        }

        private void NudPowerVertical_KeyDown(object sender, KeyEventArgs e)
        {
            double pwr;
            if (e.Shift && e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true; // Shift-Down würde Zeichen markieren
                Double.TryParse(nudPowerVertical.Text, out pwr);
                pwr = e.Control ? pwr + 10 : pwr + 3;
                nudPowerVertical.Text = pwr.ToString();
            }
            else if (e.Shift && e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true; // Shift-Down würde Zeichen markieren
                Double.TryParse(nudPowerVertical.Text, out pwr);
                pwr = e.Control ? pwr - 10 : pwr - 3;
                nudPowerVertical.Text = pwr.ToString();
            }
        }

        private void CalcDivide()
        {
            string cbTxH, cbTxV;
            int nm = 5, ns = nd, nz = 1, ds = 5;
            double.TryParse(nudOblPwr.Text, out double powerObl);
            if (powerObl > maxPwr) { powerObl = maxPwr; nudOblPwr.Text = powerObl.ToString("0.00"); }
            double.TryParse(nudOblBas.Text, out double basisObl);
            if (basisObl > 360) { basisObl = 360; nudOblBas.Text = basisObl.ToString("0"); }
            double rhpPwr = Math.Round(Math.Cos(Deg2Rad(basisObl)) * powerObl, 2);
            double rvpPwr = Math.Round(Math.Sin(Deg2Rad(basisObl)) * powerObl, 2);
            double rhpBas = Math.Round(90 - rhpPwr / rhpPwr * 90);
            double rvpBas = Math.Round(180 - rvpPwr / rvpPwr * 90);
            rhpBas = rhpPwr < 0 ? 180 : 0;
            rvpBas = rvpPwr < 0 ? 270 : 90;
            tbOblHrzPwr.Text = Math.Abs(rhpPwr).ToString("0.00");
            tbOblVrtPwr.Text = Math.Abs(rvpPwr).ToString("0.00");
            tbOblHrzBas.Text = rhpBas.ToString("0");
            tbOblVrtBas.Text = rvpBas.ToString("0"); // tbOblVrtBas.Text = rvpBas.Equals(90) ? "90 (oben)" : "270 (unten)";
            int wid = pictureBox2.ClientSize.Width, hgt = pictureBox2.ClientSize.Height;
            Bitmap bm = new Bitmap(wid, hgt); // pictureBox2.Image = null; 
            using (Graphics g = Graphics.FromImage(bm)) // Compiler baut Dispose ein
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                g.Clear(pictureBox2.BackColor);
                // TABO-Schema zeichnen
                Pen blackPen = new Pen(Color.Black, 1), redPen = new Pen(Color.Red, 3)
                {
                    StartCap = LineCap.ArrowAnchor,
                    EndCap = LineCap.SquareAnchor
                };
                Pen bluePen = new Pen(Color.Blue, 1);
                Font sansFont = new Font("MS Sans Serif", 10);
                SolidBrush fontBrush = new SolidBrush(Color.Blue);
                bluePen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F }; // custom dash pattern
                if (powerObl > 25) { nm = 10; ns = nd / 2; nz = 5; }
                else if (powerObl > 10) { nm = 5; ns = nd; nz = 5; }
                else if (powerObl > 5) { nm = 10; ns = nd / 2; nz = 1; }
                if (rvpPwr >= 0)
                {
                    float startAngle = 0.0F; // 180.0F;
                    float sweepAngle = 360F; // 180.0F;
                    for (int i = nm; i >= 1; i--) // Anzahl der Halbkreise, Bogen nach oben
                    {
                        g.DrawArc(blackPen, new Rectangle(x1 - i * ns, y1 - i * ns, 2 * i * ns, 2 * i * ns), startAngle, sweepAngle);
                        int nt = i * nz;
                        g.DrawString(nt.ToString(), sansFont, fontBrush, x1 + 1, y1 - i * ns + 1);
                    }

                    int x2 = (int)(x1 + rhpPwr * ns / nz), y2 = (int)(y1 - rvpPwr * ns / nz);
                    int wh = Math.Abs(x2 - x1), ht = Math.Abs(y1 - y2);
                    blackPen.EndCap = LineCap.ArrowAnchor;
                    g.DrawLine(blackPen, x1, y1 + ds, x1, y1 - d5 - ds); // Koordinatensystem (y)
                    g.DrawLine(blackPen, x1 - d5 - ds, y1, x1 + d5 + ds, y1); // Koordinatensystem (x)
                    if (rhpPwr >= 0) g.DrawRectangle(bluePen, x1, y1 - ht, wh, ht);
                    else g.DrawRectangle(bluePen, x1 - wh, y1 - ht, wh, ht);
                    g.DrawLine(redPen, x1, y1, x2, y2);
                }
                else
                {
                    float startAngle = 0.0F;
                    float sweepAngle = 360.0F; // 180.0F;
                    for (int i = nm; i >= 1; i--) // Anzahl der Halbkreise, Bogen nach unten
                    {
                        g.DrawArc(blackPen, new Rectangle(x1 - i * ns, y1 - d5 - i * ns, 2 * i * ns, 2 * i * ns), startAngle, sweepAngle);
                        int nt = i * nz;
                        g.DrawString(nt.ToString(), sansFont, fontBrush, x1 + 1, y1 - d5 - 16 + i * ns);
                    }
                    int x2 = (int)(x1 + rhpPwr * ns / nz), y2 = (int)(y1 - d5 - rvpPwr * ns / nz);
                    int wh = Math.Abs(x1 - x2), ht = Math.Abs(y1 - d5 - y2);
                    blackPen.EndCap = LineCap.ArrowAnchor;
                    g.DrawLine(blackPen, x1, y1 + ds, x1, y1 - d5 - ds); // Koordinatensystem (y)
                    g.DrawLine(blackPen, x1 - d5 - ds, y1 - d5, x1 + d5 + ds, y1 - d5); // Koordinatensystem (x)
                    if (rhpPwr >= 0) g.DrawRectangle(bluePen, x1, y1 - d5, wh, ht);
                    else g.DrawRectangle(bluePen, x2, y1 - d5, wh, ht);
                    g.DrawLine(redPen, x1, y1 - d5, x2, y2);
                }
                pictureBox2.Image = bm; // pictureBox2.Refresh();
                cbTxH = rhpPwr > 0 ? "horizon. " + rhpPwr.ToString("0.00") + " cm/m Base " + rhpBas.ToString("0") + "°" : "";
                cbTxV = rvpPwr > 0 ? "vertikal " + rvpPwr.ToString("0.00") + " cm/m Base " + rvpBas.ToString("0") + "°" : "";
                cbTxt = cbTxH != "" && cbTxV != "" ? cbTxH + "; " + cbTxV : cbTxH != "" ? cbTxH : cbTxV != "" ? cbTxV : "";
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) nudPowerHorizont.Focus(); else nudOblPwr.Focus();
        }

        private void NudOblPwr_KeyDown(object sender, KeyEventArgs e)
        {
            double pwr;
            if (e.Shift && e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true; // Shift-Down würde Zeichen markieren
                Double.TryParse(nudOblPwr.Text, out pwr);
                pwr = e.Control ? pwr + 10 : pwr + 3;
                nudOblPwr.Text = pwr.ToString();
            }
            else if (e.Shift && e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true; // Shift-Down würde Zeichen markieren
                Double.TryParse(nudOblPwr.Text, out pwr);
                pwr = e.Control ? pwr - 10 : pwr - 3;
                nudOblPwr.Text = pwr.ToString();
            }
        }

        private void NudOblBas_KeyDown(object sender, KeyEventArgs e)
        {
            Double.TryParse(nudOblBas.Text, out double axs);
            if (e.Shift && e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                axs = axs == 360 ? axs = 0 : axs;
                axs = e.Control ? axs + 30 : axs + 10;
                nudOblBas.Text = axs.ToString();
            }
            else if (e.Shift && e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                axs = axs == 0 ? axs + 360 : axs;
                axs = e.Control ? axs - 30 : axs - 10;
                nudOblBas.Text = axs.ToString();
            }
            else if (axs == 0 && e.KeyCode == Keys.Down)
            {
                axs = 360; nudOblBas.Text = axs.ToString();
            }
            else if (axs == 360 && e.KeyCode == Keys.Up)
            {
                axs = 0; nudOblBas.Text = axs.ToString();
            }
        }

        //public static double Decentration(double angle)
        //{ // 2.6 ist der Abstand bis zum Drehpunkt des Auges in cm
        //    return (3.0 * Math.Sin(Deg2Rad(angle))); // Dezentrierung in cm/m
        //}

        //public static double Prentice(double decentration, double sph)
        //{
        //    double pdpt = decentration * sph;
        //    return (Math.Round(Rad2Deg(Math.Atan(pdpt / 100)), 2)); // RadianToDegree
        //}

        public static double Deg2Rad(double degrees) { return (Math.PI / 180) * degrees; }
        public static double Rad2Deg(double radians) { return (180 / Math.PI) * radians; }

        private void Conversion()
        { // C# rechnet in Radians (nicht Grad) => jeweils Umrechnung erforderlich.
            bool FromDegrees = lblConvertFrom.Text.Contains("degree") ? true : false;
            double angle, result; //, sph;
            if (FromDegrees)
            { // von Grad nach Prismendioptrie
                Double.TryParse(nudConversion.Text, out angle);
                result = Math.Round(Math.Tan(Deg2Rad(angle)) * 100, 2);
                tbConversion.Text = result >= 0 ? result.ToString() : "-";
                cbTxt = result.ToString() + " cm/m"; //String.Empty;
            }
            else
            { // von Prismendioptrie nach Grad
                Double.TryParse(nudConversion.Text, out result);
                angle = Math.Round(Rad2Deg(Math.Atan(result / 100)), 2); // RadianToDegree
                tbConversion.Text = angle.ToString();
                cbTxt = angle.ToString() + "°"; //String.Empty;
            }
        }

        private void BtnConversion_Click(object sender, EventArgs e)
        {
            string foo = lblConvertFrom.Text;
            string bar = lblConvertTo.Text;
            lblConvertFrom.Text = bar;
            lblConvertTo.Text = foo;
            Conversion();
            nudConvSphere.Focus(); // HIER FEHLT NOCH ANWEISUNG WAS PASSIEREN SOLL WENN NICHT VISIBLE!!!
            //this.SelectNextControl(this.ActiveControl, false, true, true, true); // focus previous Control
        }

        public void DrawComplete()
        {
            string cbTxR = "", cbTxL = "";
            int wid = pictureBox1.ClientSize.Width, hgt = pictureBox1.ClientSize.Height;
            Bitmap bm = new Bitmap(wid, hgt); // pictureBox1.Image = null; 
            using (Graphics g = Graphics.FromImage(bm)) // Compiler sorgt für Dispose
            {
                DrawDummy(g); // enthält Befehl zum Löschen der Grafiken
                // EINGABE               //double pt2 = pt;
                double pt2 = powerHorizont > 0 && powerVertical > 0 ? pt * 1.5 : pt;
                if (Math.Abs(powerHorizont) > 0)
                {
                    DrawPrism(g, (float)basisHorizont, reyHorizont, powerHorizont, 1, (int)(powerHorizont * pt2 / (powerHorizont + powerVertical)));
                }
                if (Math.Abs(powerVertical) > 0)
                {
                    DrawPrism(g, (float)basisVertical, reyVertical, powerVertical, 2, (int)(powerVertical * pt2 / (powerHorizont + powerVertical)));
                }
                // AUSGABE
                double ptx = resPower > 0 ? resPower / (powerHorizont + powerVertical) : 1;
                pt2 = reyPower > 0 && leyPower > 0 ? pt2 * ptx : pt * ptx;
                if (reyPower > 0 && (leyPower > 0 || !(reyBase == 0 || reyBase == 180 || reyBase == basisVertical)))
                {
                    DrawPrism(g, (float)reyBase, true, reyPower, 3, (int)(reyPower * pt2 / (reyPower + leyPower)));
                }
                if (leyPower > 0 && (reyPower > 0 || !(leyBase == 0 || leyBase == 180 || leyBase == basisVertical)))
                {
                    DrawPrism(g, (float)leyBase, false, leyPower, 4, (int)(leyPower * pt2 / (leyPower + reyPower)));
                }
                pictureBox1.Image = bm; // pictureBox1.Refresh();
                cbTxR = reyPower > 0 ? "RA " + reyPower.ToString("0.00") + " cm/m Base " + reyBase.ToString("0") + "°" : "";
                cbTxL = leyPower > 0 ? "LA " + leyPower.ToString("0.00") + " cm/m Base " + leyBase.ToString("0") + "°" : "";
                cbTxt = cbTxR != "" && cbTxL != "" ? cbTxR + "; " + cbTxL : cbTxR != "" ? cbTxR : cbTxL != "" ? cbTxL : "";
            }
        }

        public void DrawDummy(Graphics g)
        {
            g.Clear(pictureBox1.BackColor); // Zeichnung löschen
            Brush bg = new SolidBrush(Color.LightSteelBlue), bw = new SolidBrush(Color.White), bb = new SolidBrush(Color.Black);
            Pen pb = new Pen(Color.DarkGray);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawEllipse(pb, 2, 2, 192, 244); // g.FillEllipse(bf, 2, 2, 192, 233); Kopf
            g.DrawEllipse(pb, x0RE - 15, y0RE - 15, 30, 30);  // re. Auge
            g.DrawEllipse(pb, x0LE - 15, y0LE - 15, 30, 30);  // li. Auge
            g.FillEllipse(bg, x0RE - 7, y0RE - 7, 14, 14);  // re. Pupille
            g.FillEllipse(bg, x0LE - 7, y0LE - 7, 14, 14);  // li. Pupille
            g.FillEllipse(bb, x0RE - 3, y0RE - 3, 6, 6);  // re. Pupille
            g.FillEllipse(bb, x0LE - 3, y0LE - 3, 6, 6);  // li. Pupille
            g.FillRectangle(bw, x0RE - 1, y0RE - 1, 1, 1); // HH-Scheitel, Drehpunkt 
            g.FillRectangle(bw, x0LE - 1, y0LE - 1, 1, 1); // HH-Scheitel, Drehpunkt 
            g.DrawBezier(pb, 105, 130, 125, 170, 95, 190, 85, 170); // Nase
            g.DrawArc(pb, x0RE + 12, 175, x0LE - x0RE - 24, 30, 180, -180); // Mund
        }

        public void DrawPrism(Graphics g, float alf, bool re, double pwr, int art, int pt)
        {
            bool err = false; int x0, y0;
            if (re) { x0 = x0RE; y0 = y0RE; } else { x0 = x0LE; y0 = y0LE; }
            PointF rPoint = new PointF(x0, y0); Matrix mtrx = new Matrix();
            mtrx.RotateAt(-alf, rPoint, MatrixOrder.Append);
            try { g.Transform = mtrx; }
            catch (System.ArgumentException) { err = true; }
            if (err || pwr == 0)
            { return; }
            else
            {
                Point[] points = new Point[3];
                points[0] = new Point(x0 - 45, y0);
                points[1] = new Point(x0 + 45, y0 - pt);
                points[2] = new Point(x0 + 45, y0 + pt);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                if (art == 1) // Prisma ERSTES Auge
                {
                    //g.FillPolygon(new SolidBrush(Color.FromArgb(240, 255, 240)), points);
                    g.FillPolygon(new SolidBrush(Color.FromArgb(32, Color.FromArgb(132, 255, 132))), points);
                    g.DrawPolygon(new Pen(Color.Green), points);
                }
                else if (art == 2) // Prisma ZWEITES Auge
                {
                    //g.FillPolygon(new SolidBrush(Color.FromArgb(250, 250, 210)), points);
                    g.FillPolygon(new SolidBrush(Color.FromArgb(45, Color.FromArgb(228,228,0))), points);
                    g.DrawPolygon(new Pen(Color.Peru), points);
                }
                else if (art == 3) // Ergebnis RECHTES Auge
                {
                    Pen p = new Pen(Color.Red); g.DrawPolygon(p, points);
                    p.DashStyle = DashStyle.Dot; g.DrawLine(p, x0 + 30, y0, x0 + 45, y0);
                }
                else if (art == 4) // Ergebnis LINKES Auge
                {
                    Pen p = new Pen(Color.Blue); g.DrawPolygon(p, points);
                    p.DashStyle = DashStyle.Dot; g.DrawLine(p, x0 + 30, y0, x0 + 45, y0);
                }
            }
        }

        private void Calculation() // Berechnung bei schräger Prismenbasislage
        {
            double resBase;
            // Prüfen ob erstes und zweites Prisma vor rechtem Auge sind
            reyHorizont = rbPrsmHorizontRE.Checked ? true : false;
            reyVertical = rbPrsmVerticalRE.Checked ? true : false;
            // Horizontalwert einlesen
            double.TryParse(nudPowerHorizont.Text, out powerHorizont);
            // Werte > maxPwr korrigieren
            if (powerHorizont > maxPwr) { powerHorizont = maxPwr; nudPowerHorizont.Text = powerHorizont.ToString("0.00"); }
            // Vertikalprisma einlesen
            double.TryParse(nudPowerVertical.Text, out powerVertical);
            // Werte > maxPwr korrigieren
            if (powerVertical > maxPwr) { powerVertical = maxPwr; nudPowerVertical.Text = powerVertical.ToString("0.00"); }
            // Radiobutton-Werte in Grad umwandeln; basisHorizontal und basisVertikal werden für Zeichnung verwandt
            if (reyHorizont) basisHorizont = rbPrsmHorizontOt.Checked ? 180 : 0;
            else basisHorizont = rbPrsmHorizontOt.Checked ? 0 : 180;
            basisVertical = rbPrsmVerticalUp.Checked ? 90 : 270;
            // Stärke des schrägen Prismas berechenen (Satz des Pythagoras)
            resPower = Math.Round(Math.Sqrt(Math.Pow(powerHorizont, 2) + Math.Pow(powerVertical, 2)), 2);
            // Gegensinnige Bewegungen vermeiden
            if (reyHorizont && rbPrsmHorizontOt.Checked) powerHorizont *= -1; // prooved
            else if (!reyHorizont && rbPrsmHorizontOt.Checked) powerHorizont *= -1;
            if (reyVertical && !rbPrsmVerticalUp.Checked) powerVertical *= -1;
            else if (!reyVertical && rbPrsmVerticalUp.Checked) powerVertical *= -1;
            // Basislage des resultierenden Prismas für RA berechenen; 1 rad = 57,3°
            if (powerHorizont == 0) reyBase = reyVertical ? basisVertical : basisVertical - 180; // Division durch 0 vermeiden
            else if (powerVertical == 0) reyBase = reyHorizont ? basisHorizont : basisHorizont - 180;
            else
            {
                resBase = Math.Round(Rad2Deg(Math.Atan(powerVertical / powerHorizont)), 0);
                reyBase = rbPrsmHorizontOt.Checked ? resBase - 180 : resBase;
            }
            // Minuswerte korrigieren
            if (reyBase < 0) reyBase = 360 + reyBase;
            // Basis für LA spiegeln
            leyBase = reyBase >= 180 ? reyBase - 180 : reyBase + 180;
            // Minuswerte vermeiden
            powerHorizont = Math.Abs(powerHorizont); powerVertical = Math.Abs(powerVertical);
            // Ergebnis auf 2 Augen verteilen wenn 2 Augen aktiviert sind
            reyPower = reyHorizont && reyVertical ? resPower : !reyHorizont && !reyVertical ? 0 : resPower / 2;
            leyPower = !reyHorizont && !reyVertical ? resPower : reyHorizont && reyVertical ? 0 : resPower / 2;
            // Ausgabe
            nudResultPowerRE.Text = reyPower.ToString("0.00");
            nudResultPowerLE.Text = leyPower.ToString("0.00");
            tbResultBaseRE.Text = reyPower > 0 ? reyBase.ToString("0") : "n. def.";
            tbResultBaseLE.Text = leyPower > 0 ? leyBase.ToString("0") : "n. def.";
            // Zeichnen
            DrawComplete();
        }

        private void SpreadOut()
        {
            double.TryParse(nudResultPowerRE.Text, out reyPower);
            double.TryParse(nudResultPowerLE.Text, out leyPower);
            if (ActiveControl.Name.Contains("nudResultPowerRE"))
            {
                if (reyPower > resPower) nudResultPowerRE.Text = resPower.ToString("0.00");
                leyPower = resPower - reyPower;
                nudResultPowerLE.Text = leyPower.ToString("0.00");
            }
            else
            {
                if (leyPower > resPower) nudResultPowerLE.Text = resPower.ToString("0.00");
                reyPower = resPower - leyPower;
                nudResultPowerRE.Text = reyPower.ToString("0.00");
            }
            tbResultBaseRE.Text = reyPower > 0 ? reyBase.ToString("0") : "n. def.";
            tbResultBaseLE.Text = leyPower > 0 ? leyBase.ToString("0") : "n. def.";
            DrawComplete();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CalcDivide(); DrawComplete(); // Startgrafiken zeichenen
        }

        private void NudConversion_KeyDown(object sender, KeyEventArgs e)
        {
            double pwr;
            if (e.Shift && e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true; // Shift-Down würde Zeichen markieren
                Double.TryParse(nudConversion.Text, out pwr);
                pwr = e.Control ? pwr + 10 : pwr + 3;
                nudConversion.Text = pwr.ToString();
            }
            else if (e.Shift && e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true; // Shift-Down würde Zeichen markieren
                Double.TryParse(nudConversion.Text, out pwr);
                pwr = e.Control ? pwr - 10 : pwr - 3;
                nudConversion.Text = pwr.ToString();
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            {
                try { System.Diagnostics.Process.Start("http://www.schielen.de/software.htm"); }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void BtnF1_Click(object sender, EventArgs e)
        {
            if (f2 != null) f2.ShowDialog(); // F1 würde mehrfach Form öffnen
        }

        private void BtnF2_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTxt.Length > 0)
                {
                    Clipboard.SetText(cbTxt, TextDataFormat.Text);
                    MessageBox.Show(cbTxt, "Inhalt der Zwischenablage");
                }
                else
                {
                    MessageBox.Show("Es liegt keine Berechnung vor!", "Prismenrechner", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Prismenrechner", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void BtnF3_Click(object sender, EventArgs e)
        { // Eingabe-Controls auf 0 setzen
            if (tabControl1.SelectedIndex == 0)
            {
                foreach (Control c in grbxHorizontPrism.Controls) if (c is NumericUpDown) c.Text = "0";
                foreach (Control c in grbxVerticalPrism.Controls) if (c is NumericUpDown) c.Text = "0";
                Calculation(); // Ergebnisfelder anpassen
            }
            else
            {
                foreach (Control c in grbxOblique.Controls) if (c is NumericUpDown) c.Text = "0";
                CalcDivide(); // Ergebnisfelder anpassen
            }
        }

    }
}
