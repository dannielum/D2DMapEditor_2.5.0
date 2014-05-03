using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace D2DMapEditor
{
    /* TO DO */
    /* Simplified the input handling code in D2DMapEditor.cs */

    class InputHelper
    {
        public static Point TrackMouse;
        public static Point MapPosition;
        public static Point OldMapPosition;

        public static void UpdateOldMapPosition()
        {
            if (OldMapPosition.X < MapPosition.X) OldMapPosition.X++;
            if (OldMapPosition.X > MapPosition.X) OldMapPosition.X--;
            if (OldMapPosition.Y < MapPosition.Y) OldMapPosition.Y++;
            if (OldMapPosition.Y > MapPosition.Y) OldMapPosition.Y--;
        }
    }
}
