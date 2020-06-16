using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildersHome : MonoBehaviour
{
    private static int idCount;
    public int _builderID;

    private void Awake()
    {
        _builderID = idCount++;
    }

}
