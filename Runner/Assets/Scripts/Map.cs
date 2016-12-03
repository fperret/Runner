using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public List<GameObject> elements = new List<GameObject>();

    public List<GameObject> neighbors = new List<GameObject>();
    public Edge[] edges = new Edge[4];
    public Edge topEdge;
    public Edge rightEdge;
    public Edge bottomEdge;
    public Edge leftEdge;

    void Awake()
    {
        edges = GetComponentsInChildren<Edge>();
    }

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (topEdge.exitTrigger == true)
        {
            Game.instance.createNewMap(topEdge, true);
            topEdge.exitTrigger = false;
        }
        else if (rightEdge.exitTrigger == true)
        {
            Game.instance.createNewMap(rightEdge, true);
            rightEdge.exitTrigger = false;
        }
        else if (bottomEdge.exitTrigger == true)
        {
            Game.instance.createNewMap(bottomEdge, true);
            bottomEdge.exitTrigger = false;
        }
        else if (leftEdge.exitTrigger == true)
        {
            Game.instance.createNewMap(leftEdge, true);
            leftEdge.exitTrigger = false;
        }

        checkEnterTrigger(topEdge);
        checkEnterTrigger(rightEdge);
        checkEnterTrigger(bottomEdge);
        checkEnterTrigger(leftEdge);
	}

    void checkEnterTrigger(Edge edge)
    {
        if (edge.enterTrigger == true)
        {
            Game.instance.updateMap(this);
            edge.enterTrigger = false;
        }
    }
}
