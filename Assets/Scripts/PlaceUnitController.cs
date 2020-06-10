using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PlaceUnitController : MonoBehaviour
{
    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private KeyCode hotKey;

    public List<GameObject> objectList;

    public static Grid grid;

    private void Awake()
    {
        grid = new Grid(64, 64, 1f);
    }

    public static void PassToBuilders(List<Vector2> units)
    { 
        int unitsPerBuilder = Math.Abs(units.Count / BuilderID.ReturnID());
        int counter = 0;
        foreach(var builder in GameManager.builders)
        {
            if(GameManager.builders.IndexOf(builder)!= GameManager.builders.Count - 1)
            {
                for(int i = 0; i < unitsPerBuilder; i++)
                {
                    builder.units.Enqueue(units[counter]);
                    counter++;
                }
            }
            else
            {
                int lastUnits = units.Count - counter;

                for(int i = 0; i < lastUnits; i++)
                {
                    builder.units.Enqueue(units[counter]);
                    counter++;
                }
            }
        }
        Debug.Log(units.Count);
    }

    public void PlaceObject(Vector2 pos)
    {
        Instantiate(placeableObjectPrefab, pos, Quaternion.identity);
    }

}
