using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portaria
{
    public class Class1 : PictureBox
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            GraphicsPath h = new GraphicsPath();
            h.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(h);
            base.OnPaintBackground(pevent);
        }
    }
}
