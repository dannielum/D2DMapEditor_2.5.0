using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace D2DMapEditor
{
    class History
    {
        public Stack<HistoryNode> Undo;
        public Stack<HistoryNode> Redo;

        public History()
        {
            ResetHistory();
        }

        public int UndoNextId
        {
            get
            {
                if (Undo.Count > 0)
                    return Undo.Peek().Id + 1;
                else
                    return 0;
            }
        }

        public void ResetHistory()
        {
            Undo = new Stack<HistoryNode>();
            Redo = new Stack<HistoryNode>();
        }

        public void ClearUndo()
        {
            Undo.Clear();
        }

        public void ClearRedo()
        {
            Redo.Clear();
        }

        public void PushUndo(int layerIndex, int layerIndex2, ActionType action)
        {   // undo action for swap layers positions
            ClearRedo();
            int id = UndoNextId;
            Undo.Push(new HistoryNode(id, layerIndex, layerIndex2, action));
        }

        public void PushUndo(int layerId, Layer layer, int layerIndex, ActionType action)
        {   // undo action for add/remove layer
            ClearRedo();
            int id = UndoNextId;
            Undo.Push(new HistoryNode(id, layerId, layer, layerIndex, action));
        }

        public int PushUndo(int layerId, int x, int y, int data, ActionType action)
        {   // undo action for brush, erase etc.
            ClearRedo();
            int id = UndoNextId;
            Undo.Push(new HistoryNode(id, layerId, x, y, data, action));

            return id;
        }

        public void PushUndo(int id, int layerId, int x, int y, int data, ActionType action)
        {
            Undo.Push(new HistoryNode(id, layerId, x, y, data, action));
        }

        public void DoUndo(Map map, D2DMapEditor d2d)
        {
            int nodeId = Undo.Peek().Id;
            int layerIndex = -1;

            if (Undo.Peek().Action == ActionType.AddLayer)
            {
                HistoryNode undo = Undo.Pop();
                Redo.Push(new HistoryNode(undo.Id, undo.LayerId, undo.Layer, undo.LayerIndex, undo.Action));
                layerIndex = map.FindLayerIndexWithId(undo.LayerId);
                map.Layers.RemoveAt(undo.LayerIndex);
                d2d.ReloadLayers(-1);
            }
            else if (Undo.Peek().Action == ActionType.DeleteLayer)
            {
                HistoryNode undo = Undo.Pop();
                Redo.Push(new HistoryNode(undo.Id, undo.LayerId, undo.Layer, undo.LayerIndex, undo.Action));
                map.AddLayer(undo.Layer, undo.LayerIndex);
                layerIndex = map.FindLayerIndexWithId(undo.LayerId);
                d2d.ReloadLayers(-1);
            }
            else if (Undo.Peek().Action == ActionType.SwapLayer)
            {
                HistoryNode undo = Undo.Pop();
                Redo.Push(new HistoryNode(undo.Id, undo.LayerIndex, undo.LayerIndex2, undo.Action));
                map.SwapLayers(map, undo.LayerIndex, undo.LayerIndex2);
                layerIndex = undo.LayerIndex;
                d2d.ReloadLayers(-1);
            }
            else
            {
                while (Undo.Count > 0 && nodeId == Undo.Peek().Id)
                {   // undo
                    HistoryNode undo = Undo.Pop();
                    layerIndex = map.FindLayerIndexWithId(undo.LayerId);

                    if (layerIndex >= 0)
                    {
                        // save for redo
                        Redo.Push(new HistoryNode(undo.Id, undo.LayerId, undo.X, undo.Y, map.Layers[layerIndex].LayerData[undo.X, undo.Y], undo.Action));
                        // render undo                
                        map.Layers[layerIndex].LayerData[undo.X, undo.Y] = undo.Value;
                    }
                }
            }

            if (layerIndex == -1)
                MessageBox.Show("Cannot undo! Layer was removed", "Undo Failed");
        }

        public void DoRedo(Map map, D2DMapEditor d2d)
        {
            int nodeId = Redo.Peek().Id;
            int layerIndex = -1;

            if (Redo.Peek().Action == ActionType.AddLayer)
            {
                HistoryNode redo = Redo.Pop();
                Undo.Push(new HistoryNode(redo.Id, redo.LayerId, redo.Layer, redo.LayerIndex, redo.Action));
                map.AddLayer(redo.Layer, redo.LayerIndex);
                layerIndex = map.FindLayerIndexWithId(redo.LayerId);
                d2d.ReloadLayers(-1);
            }
            else if (Redo.Peek().Action == ActionType.DeleteLayer)
            {
                HistoryNode redo = Redo.Pop();
                Undo.Push(new HistoryNode(redo.Id, redo.LayerId, redo.Layer, redo.LayerIndex, redo.Action));
                layerIndex = map.FindLayerIndexWithId(redo.LayerId);
                map.Layers.RemoveAt(redo.LayerIndex);
                d2d.ReloadLayers(-1);
            }
            else if (Redo.Peek().Action == ActionType.SwapLayer)
            {
                HistoryNode redo = Redo.Pop();
                Undo.Push(new HistoryNode(redo.Id, redo.LayerIndex, redo.LayerIndex2, redo.Action));
                map.SwapLayers(map, redo.LayerIndex, redo.LayerIndex2);
                layerIndex = redo.LayerIndex;
                d2d.ReloadLayers(-1);
            }
            else
            {
                while (Redo.Count > 0 && nodeId == Redo.Peek().Id)
                {   // redo
                    HistoryNode redo = Redo.Pop();
                    layerIndex = map.FindLayerIndexWithId(redo.LayerId);

                    if (layerIndex >= 0)
                    {
                        // save current map for undo
                        Undo.Push(new HistoryNode(redo.Id, redo.LayerId, redo.X, redo.Y, map.Layers[layerIndex].LayerData[redo.X, redo.Y], redo.Action));
                        // render redo
                        map.Layers[layerIndex].LayerData[redo.X, redo.Y] = redo.Value;
                    }

                }
            }

            if (layerIndex == -1)
                MessageBox.Show("Cannot redo! Layer was removed", "Redo Failed");
        }
    }
}
