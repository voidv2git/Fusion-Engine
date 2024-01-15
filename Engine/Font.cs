using System.Drawing;

namespace FusionEngine.Engine
{
    public class Font
    {
        public string value;
        public System.Drawing.Font font;
        public Color color;

        public Font(string value, System.Drawing.Font font, Color color)
        {
            this.value = value;
            this.font = font;
            this.color = color;
        }
    }
}
