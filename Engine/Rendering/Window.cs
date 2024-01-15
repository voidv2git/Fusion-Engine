using System.Windows.Forms;

namespace FusionEngine.Engine.Rendering
{
    public class Window : Form
    {
        public Window()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }
    }
}
