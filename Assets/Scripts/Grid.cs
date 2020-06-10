using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Grid 
{
    private int width;
    private int height;
    private float cellSize;

    private int[,] gridArray;

    public Grid(int width , int height, float cellSize)
    {
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
        List<Vector2> units = new List<Vector2>();
        for (int w = 0; w < gridArray.GetLength(0); w++)
        {
            for (int h = 0; h < gridArray.GetLength(1); h++)
            {
                Vector2 unitPos = GetCellCoorToWorld(w, h);
                unitPos.x -= 0.5f;
                unitPos.y -= 0.5f;

                if (unitPos.x > min.x && unitPos.x < max.x && unitPos.y > min.y && unitPos.y < max.y)
                {
                    units.Add(unitPos);
                }
            }
        }

        return units;

    }
}
