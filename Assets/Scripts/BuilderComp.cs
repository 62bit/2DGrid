using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderComp
{
    private float BuilingTime;
    private float speed;

    public BuilderComp(float bTime, float speed)
    {
        this.speed = speed;
        this.BuilingTime = bTime;
    }

    public float GetBuildingTime() { return BuilingTime; }
    public float GetSpeed() { return speed; }
}
