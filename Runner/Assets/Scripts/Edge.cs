using UnityEngine;
using System.Collections;

public class Edge : MonoBehaviour
{
    public bool enterTrigger;
    public bool canBeEnterTriggered;
    public Direction direction;

    void Awake()
    {
        enterTrigger = false;
        canBeEnterTriggered = false;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
