using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour {

    public bool usable;

    void Awake()
    {
        usable = true;
        tag = "Turn";
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
