using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ODIS.Controls
{
    public partial class MyNumericUpDown : NumericUpDown
    {
        private int _ChangeSignDecPlaces;

        public int ChangeSignDecPlaces
        {
            get { return _ChangeSignDecPlaces; }
            set { _ChangeSignDecPlaces = value; }
        }

        public MyNumericUpDown()
        {
            InitializeComponent();
            if (Value == 0) Value = 1;
            if (Maximum == 100) Maximum = 10000000000000;
            if (Minimum == 0) Minimum = -10000000000000;//(decimal)0.00000000000001;
            TextAlign = HorizontalAlignment.Right;
        }

        //protected override void OnChanged(object source, EventArgs e)
        //{
        //    base.OnChanged(source, e);
        //    AdjustIncrement();
        //}

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Select(0, 1000);
        }
        
        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            AdjustDecPlaces();
        }

        public override void DownButton()
        {
            AdjustIncrement(this.Value > 0);
            base.DownButton();
        }

        public override void UpButton()
        {
            AdjustIncrement(this.Value < 0);
            base.UpButton();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //base.OnMouseWheel(e);
            if (e.Delta > 0) this.UpButton();
            else this.DownButton();
        }

        private void AdjustDecPlaces()
        {
            decimal x = Math.Abs(Value);

            int p = CalculateP(x);

            if (p < 0) this.DecimalPlaces = -p - 1;

            //смена знака
            if ((double)x < Math.Pow(10, -this.ChangeSignDecPlaces))
            {
                try
                {
                    this.Value = (decimal)-Math.Pow(10, -this.ChangeSignDecPlaces) * (this.Value / x);
                    this.DecimalPlaces = this.ChangeSignDecPlaces + 1;
                }
                catch { }
            };
        }

        private void AdjustIncrement(Boolean IsDownIncrement)
        {
            if (Value != 0)
            {
                decimal x = Math.Abs(Value);
                int p = CalculateP(x);

                this.Increment = (decimal)Math.Pow(10, p + 1);

                if ((IsDownIncrement)&&(x - this.Increment == 0))
                {
                    this.Increment = (decimal)Math.Pow(10, p);
                }

            }
        }

        private int CalculateP(Decimal Value)
        {
            int p = 9;
            Decimal r = 1;

            //определяем наименьшую значащую степерь 10-ки в Value (максимум до 9-го знака)
            while ((r != 0) && (p > -10))
            {
                r = Value / (decimal)Math.Pow(10, p) - Math.Truncate(Value / (decimal)Math.Pow(10, p));
                p--;
            }

            return p;
        }

    }
}
