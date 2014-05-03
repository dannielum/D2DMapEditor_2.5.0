using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace D2DMapEditor
{
    enum SelectionState
    {
        StartSelection = 1,
        Dragging = 2,
        EndSelection = 3,
        SelectAll = 4,
        ClearSelection = 5
    }

    class EditingTool
    {
        public static void Selection(D2DMapEditor d2d, Map map, MapInfo mapInfo, int layerIndex, SelectionState state)
        {
            switch (state)
            {
                case SelectionState.StartSelection:
                    mapInfo.Selection = new SelectionTool();
                    mapInfo.Selection.IsDragging = true;
                    mapInfo.Selection.StartDrag = new Point(InputHelper.MapPosition.X, InputHelper.MapPosition.Y);
                    mapInfo.Selection.StopDrag = new Point(InputHelper.MapPosition.X, InputHelper.MapPosition.Y);
                    break;
                case SelectionState.Dragging:
                    if (mapInfo.Selection.IsDragging)
                    {
                        mapInfo.Selection.StopDrag = new Point(InputHelper.MapPosition.X, InputHelper.MapPosition.Y);
                        d2d.RenderMap();
                    }
                    break;
                case SelectionState.EndSelection:
                    mapInfo.Selection.IsDragging = false;
                    mapInfo.Selection.StopDrag = new Point(InputHelper.MapPosition.X, InputHelper.MapPosition.Y);
                    d2d.RenderMap();
                    break;
                case SelectionState.SelectAll:
                    mapInfo.Selection = new SelectionTool();
                    mapInfo.Selection.StartDrag = new Point(0, 0);
                    mapInfo.Selection.StopDrag = new Point(map.Layers[layerIndex].Width, map.Layers[layerIndex].Height);
                    d2d.RenderMap();
                    break;
                case SelectionState.ClearSelection:
                    mapInfo.Selection = new SelectionTool();
                    break;
                default:
                    break;
            }
        }

        public static void Brush(D2DMapEditor d2d, Map map, History history, int layerIndex, int tileIndex)
        {
            history.ClearRedo();
            int id = history.UndoNextId;

            while (InputHelper.OldMapPosition.X != InputHelper.MapPosition.X || 
                   InputHelper.OldMapPosition.Y != InputHelper.MapPosition.Y)
            {
                if (map.Layers[layerIndex].LayerData[InputHelper.OldMapPosition.X, InputHelper.OldMapPosition.Y] != tileIndex)
                {
                    history.PushUndo(id, map.Layers[layerIndex].LayerId,
                                      InputHelper.OldMapPosition.X, InputHelper.OldMapPosition.Y,
                                      map.Layers[layerIndex].LayerData[InputHelper.OldMapPosition.X, InputHelper.OldMapPosition.Y],
                                      ActionType.Brush);
                    map.Layers[layerIndex].LayerData[InputHelper.OldMapPosition.X, InputHelper.OldMapPosition.Y] = tileIndex;
                }

                InputHelper.UpdateOldMapPosition();
            }
            if (map.Layers[layerIndex].LayerData[InputHelper.MapPosition.X, InputHelper.MapPosition.Y] != tileIndex)
            {
                history.PushUndo(map.Layers[layerIndex].LayerId,
                                  InputHelper.MapPosition.X, InputHelper.MapPosition.Y,
                                  map.Layers[layerIndex].LayerData[InputHelper.MapPosition.X, InputHelper.MapPosition.Y],
                                  ActionType.Brush);
                map.Layers[layerIndex].LayerData[InputHelper.MapPosition.X, InputHelper.MapPosition.Y] = tileIndex;
            }
        }

        public static void Fill(Map map, MapInfo mapInfo, History history, int layerIndex, int tileIndex)
        {
            if (mapInfo.Selection.IsWithinSelection())
            {
                history.ClearRedo();
                int id = history.UndoNextId;

                mapInfo.Selection.ForceWithinSelection(map.Layers[layerIndex].Width, map.Layers[layerIndex].Height);
                for (int i = mapInfo.Selection.TopLeftX; i < mapInfo.Selection.BottomRightX; i++)
                {   // fill selected tiles
                    for (int j = mapInfo.Selection.TopLeftY; j < mapInfo.Selection.BottomRightY; j++)
                    {
                        history.PushUndo(id, map.Layers[layerIndex].LayerId, i, j, map.Layers[layerIndex].LayerData[i, j], ActionType.Fill);
                        map.Layers[layerIndex].LayerData[i, j] = tileIndex;
                    }
                }
            }
        }

        public static void Eraser(Map map, MapInfo mapInfo, History history, int layerIndex)
        {
            if (mapInfo.Selection.IsWithinSelection())
            {
                if (map.Layers[layerIndex].LayerData[InputHelper.MapPosition.X, InputHelper.MapPosition.Y] != -1)
                {
                    history.PushUndo(map.Layers[layerIndex].LayerId, InputHelper.MapPosition.X, InputHelper.MapPosition.Y,
                                     map.Layers[layerIndex].LayerData[InputHelper.MapPosition.X, InputHelper.MapPosition.Y],
                                     ActionType.Erase);
                    map.Layers[layerIndex].LayerData[InputHelper.MapPosition.X, InputHelper.MapPosition.Y] = -1;
                }
            }
        }

        public static void KeyboardShortCut(D2DMapEditor d2d, Map map, MapInfo mapInfo, History history, int layerIndex, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                d2d.SelectTool(ToolType.selection);
            }
            else if (e.KeyCode == Keys.B)
            {
                d2d.SelectTool(ToolType.brush);
            }
            else if (e.KeyCode == Keys.E)
            {
                d2d.SelectTool(ToolType.eraser);
            }
            else if (e.KeyCode == Keys.F)
            {
                d2d.SelectTool(ToolType.fill);
            }
            else if (e.KeyCode == Keys.T)
            {
                d2d.SelectTool(ToolType.selectTile);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (mapInfo.Selection.StartDrag.X != -1 && mapInfo.Selection.StartDrag.Y != -1 && mapInfo.Selection.StopDrag.X != -1 && mapInfo.Selection.StopDrag.Y != -1)
                {   // selection was made
                    history.ClearRedo();
                    int id = history.UndoNextId;

                    mapInfo.Selection.ForceWithinSelection(map.Layers[layerIndex].Width, map.Layers[layerIndex].Height);
                    for (int i = mapInfo.Selection.TopLeftX; i < mapInfo.Selection.BottomRightX; i++)
                    {   // delete selected tiles
                        for (int j = mapInfo.Selection.TopLeftY; j < mapInfo.Selection.BottomRightY; j++)
                        {
                            history.PushUndo(id, map.Layers[layerIndex].LayerId, i, j, map.Layers[layerIndex].LayerData[i, j], ActionType.Erase);
                            map.Layers[layerIndex].LayerData[i, j] = -1;
                        }
                    }

                    d2d.RenderMap();
                }
            }
        }
    }
}
