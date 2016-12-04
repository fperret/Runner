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
            Game.instance.createMapNeighbors(this, edge);
            edge.enterTrigger = false;
        }
    }
}
