// http://www.codeproject.com/KB/selection/DerivedNumericUpDn.aspx
// Here are some steps to easily use this derived control in your project:
// Create a Visual C# Windows Form project.
// Copy the NumericUpDnWithUpDnBtnEventHandlers.cs class to your project directory.
// Now choose “Project->Add existing item” from the VS menu and add the class to your project.
// Once the class is successfully added, build your project/solution.
// You should see a component NumericUpDnWithUpDnBtnEventHandlers in the toolbox.
// Just drag and drop the new control onto your form.
// Click on the control and set its properties. All properties are standard except CallBase which defaults to true but you can set it to false if you want.
// Now in the properties window, click on Events.
// You will see UpButtonClicked and DownButtonClicked events besides all the standard NumericUpDown events.
// Double click on each one and the event handlers will be added for you.
// Now you can do whatever you need in these event handlers.

using System;
using System.Windows.Forms;

namespace PrismCalc
{
    public class NumericUpDnWithUpDnBtnEventHandlers : NumericUpDown
    {
        public event EventHandler UpButtonClicked = null;
        public event EventHandler DownButtonClicked = null;
        private bool bCallBase = true;

        public void OnUpButtonClicked(EventArgs e) => UpButtonClicked?.Invoke(this, e);

        public void OnDownButtonClicked(EventArgs e) => DownButtonClicked?.Invoke(this, e);

        public NumericUpDnWithUpDnBtnEventHandlers()
        {
        }

        public override void DownButton()
        {
            try
            {
                if (bCallBase) { base.DownButton(); }
                OnDownButtonClicked(new EventArgs());
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public override void UpButton()
        {
            try
            {
                if (bCallBase) { base.UpButton(); }
                OnUpButtonClicked(new EventArgs());
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public bool CallBase
        {
            get => bCallBase;
            set => bCallBase = value;
        }
    }
}
