using System.Windows.Input;

namespace FusionEngine.Engine
{
    public static class Input
    {
        public static bool GetKeyDown(Key key)
        {
            return Keyboard.IsKeyDown(key);
        }

        public static bool GetKeyUp(Key key)
        {
            return Keyboard.IsKeyUp(key);
        }
    }
}
