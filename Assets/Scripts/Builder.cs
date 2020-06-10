using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public static class BuilderID
{
    private static int ID = 0;

    public static int GrabID()
    {
        return ID++;
    }

    public static int ReturnID() { return ID; }
}

public class Builder : MonoBehaviour
{
    private int ID;
    public float buildingTime;
    public Vector3 destination = new Vector3();
    private bool isWorking = false;
    public float speed = 5.0f;
    public Queue<Vector2> units = new Queue<Vector2>();


    private void Start()
    {
        ID = BuilderID.GrabID();
        GameManager.builders.Add(this);
    }

}
