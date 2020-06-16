using System.Collections.Generic;
using UnityEngine;

struct GridLoc
{
    public int x;
    public int y;

    public GridLoc(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Grid
{
    private static Grid _istance;
    public static Grid Instance
    {
        get
        {
            if (_istance == null)
                Debug.Log("Grid is null");

            return _istance;
        }
    }
    public Dictionary<GameObject, Vector2> gridObjects = new Dictionary<GameObject, Vector2>();
    private int width;
    private int height;
    private float cellSize;

    private int[,] gridArray;

    // <T>
    public Grid(int width, int height, float cellSize)
    {
        _istance = this;

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        //for(int i=0; i < gridArray.GetLength(0); i++)
        //{
        //    for(int a = 0; a < gridArray.GetLength(1); a++)
        //    {
        //        //CreateGridUtils.Spawn(GetCellCoorToWorld(i, a));
        //        Debug.DrawLine(GetCellCoorToWorld(i, a), GetCellCoorToWorld(i, a + 1), Color.white, 100f);
        //        Debug.DrawLine(GetCellCoorToWorld(i, a), GetCellCoorToWorld(i + 1, a), Color.white, 100f);
        //    }

        //}
        //Debug.DrawLine(GetCellCoorToWorld(0, height), GetCellCoorToWorld(width, height), Color.white, 100f);
        //Debug.DrawLine(GetCellCoorToWorld(width, 0), GetCellCoorToWorld(width, height), Color.white, 100f);
    }

    public Vector3 GetCellCoorToWorld(int w, int h)
    {
        return new Vector3(w, h) * cellSize;
    }

    public List<Vector2> CheckSelectionArea(Vector2 min, Vector2 max)
    {
        //Can be cashed
        List<Vector2> units = new List<Vector2>();
        List<GridLoc> selectedBlocks = new List<GridLoc>();
        //-

        for (int w = 0; w < gridArray.GetLength(0); w++)
        {
            for (int h = 0; h < gridArray.GetLength(1); h++)
            {
                Vector2 unitPos = GetCellCoorToWorld(w, h);
                unitPos.x -= 0.5f;
                unitPos.y -= 0.5f;

                if (unitPos.x > min.x && unitPos.x < max.x && unitPos.y > min.y && unitPos.y < max.y)
                {
                    var block = new GridLoc(w, h);
                    units.Add(unitPos);
                    selectedBlocks.Add(block);
                }
            }
        }
        //foreach(var block in selectedBlocks)
        //{
        //    Debug.Log(block.x + " " + block.y);
        //}
        return units;

    }

    public bool ValidateArea(List<Vector2> locations)
    {
        foreach (var loc in locations)
        {
            if (gridObjects.ContainsValue(loc))
                return false;
        }
        return true;
    }
}
