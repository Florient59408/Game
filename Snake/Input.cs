using System.Collections;
using System.Windows.Forms;

namespace SnakeGame
{
    class Input
    {
        public static Hashtable keyTable = new Hashtable();
        public static bool KeyPress(Keys key)
        {
            if (keyTable[key] == null)
                return false;
            return (bool)keyTable[key];
        }

        public static void ChangeState(Keys key, bool state) => keyTable[key] = state; 
    }
}
