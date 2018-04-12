using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BioEnumerator.FrameworkControls
{
    public sealed class xPanel : Panel
    {
        public xPanel()
        {
            DoubleBuffered = true;
            BorderStyle = BorderStyle.FixedSingle;

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var sf = new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Center, };
            var g = e.Graphics;

            g.DrawString(Text, Font, new SolidBrush(ForeColor), ClientRectangle, sf);

        }
        [Description("Title text of the panel")]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Refresh();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            BorderStyle = BorderStyle.FixedSingle;
            Refresh();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            BorderStyle = BorderStyle.Fixed3D;
            Refresh();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseEnter(e);
            BorderStyle = BorderStyle.Fixed3D;
            Refresh();
        }
    }
}
