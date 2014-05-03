using System;
using System.Collections.Generic;
using System.Text;

namespace D2DMapEditor
{
    class SelectionTool
    {
        private System.Drawing.Point _startDrag;
        private System.Drawing.Point _stopDrag;
        public Boolean IsDragging;
        public int TopLeftX;
        public int TopLeftY;
        public int BottomRightX;
        public int BottomRightY;

        public SelectionTool()
        {
            _startDrag = new System.Drawing.Point(-1, -1);
            _stopDrag = new System.Drawing.Point(-1, -1);
            IsDragging = false;
        }

        public System.Drawing.Point StartDrag
        {
            get
            {
                return _startDrag;
            }
            set
            {
                _startDrag = value;
            }
        }

        public System.Drawing.Point StopDrag
        {
            get
            {
                return _stopDrag;
            }
            set
            {
                _stopDrag = value;
                FinalizeSelection();
            }
        }

        public Boolean IsWithinSelection()
        {
            if (_startDrag.X != -1 && _startDrag.Y != -1 && _stopDrag.X != -1 && _stopDrag.Y != -1)
            {   // selection was made
                if (InputHelper.MapPosition.X >= TopLeftX && InputHelper.MapPosition.X < BottomRightX &&
                    InputHelper.MapPosition.Y >= TopLeftY && InputHelper.MapPosition.Y < BottomRightY)
                {
                    return true;
                }
            }
            else
            {   // no selection was made
                return true;
            }

            return false;
        }

        public void ForceWithinSelection(int width, int height)
        {
            if (BottomRightX > width)
                BottomRightX = width;
            if (BottomRightY > height)
                BottomRightY = height;
        }

        private void FinalizeSelection()
        {
            TopLeftX = _startDrag.X;
            TopLeftY = _startDrag.Y;
            BottomRightX = _stopDrag.X;
            BottomRightY = _stopDrag.Y;

            if (_startDrag.X > _stopDrag.X)
            {   // swap StartDragX and StopDragX
                TopLeftX = _stopDrag.X;
                BottomRightX = _startDrag.X;
            }

            if (_startDrag.Y > _stopDrag.Y)
            {   // swap StartDragY and StopDragY
                TopLeftY = _stopDrag.Y;
                BottomRightY = _startDrag.Y;
            }

            BottomRightX = BottomRightX + 1;
            BottomRightY = BottomRightY + 1;
        }
    }
}
