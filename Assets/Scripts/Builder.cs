using System.Collections.Generic;
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
    [SerializeField] private int ID;
    public GameObject _home;

    private BuilderComp baseBuilder;
    public float buildingTime;
    public Vector3 destination = new Vector3();
    public float speed;
    public Queue<Vector2> units = new Queue<Vector2>();


    private void Awake()
    {
        baseBuilder = new BuilderComp(Random.Range(1, 5), Random.Range(1, 6));
        InitBuilder();
        this.ID = BuilderID.GrabID();
        GameManager.Instance.builders.Add(this);
    }

    private void InitBuilder()
    {
        buildingTime = baseBuilder.GetBuildingTime();
        speed = baseBuilder.GetSpeed();
    }

    public int GetID()
    {
        return this.ID;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == _home)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _home)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
