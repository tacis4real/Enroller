using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace taxServeUtilities.FrameworkControls
{
   public class xMetroGridPlus: MetroGrid
    {
       public xMetroGridPlus()
        {
            DoubleBuffered = true;
        }
       protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
       {
           var strRowNumber = (e.RowIndex + 1).ToString(CultureInfo.InvariantCulture);
           while (strRowNumber.Length < RowCount.ToString(CultureInfo.InvariantCulture).Length) strRowNumber = "0" + strRowNumber;
           var size = e.Graphics.MeasureString(strRowNumber, Font);
           if (RowHeadersWidth < (int)(size.Width + 20)) RowHeadersWidth = (int)(size.Width + 20);
           var b = SystemBrushes.ControlText;
           e.Graphics.DrawString(strRowNumber, Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
           base.OnRowPostPaint(e);
       } 
    }
}
