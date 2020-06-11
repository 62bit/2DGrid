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
    private BuilderComp baseBuilder;
    public float buildingTime;
    public Vector3 destination = new Vector3();
    public float speed;
    public Queue<Vector2> units = new Queue<Vector2>();


    private void Start()
    {
        baseBuilder = new BuilderComp(Random.Range(1, 5), Random.Range(1, 6));
        InitBuilder();
        ID = BuilderID.GrabID();
        GameManager.builders.Add(this);
    }

    private void InitBuilder()
    {
        buildingTime = baseBuilder.GetBuildingTime();
        speed = baseBuilder.GetSpeed();
    }
}
