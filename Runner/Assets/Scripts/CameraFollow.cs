using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private GameObject target;
    private Vector2 originalOffset;

    void Awake()
    {
        this.target = GameObject.Find("Player");
        this.originalOffset = this.target.transform.position - this.transform.position;
    }

	// Use this for initialization
	void Start ()
    {

	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 newPos = this.target.transform.position;
        if (target.transform.eulerAngles.z == 0)
        {
            newPos -= this.originalOffset;
        }
        else if (target.transform.eulerAngles.z == 90)
        {
            newPos += new Vector2(this.originalOffset.y, this.originalOffset.x);
        }
        else if (target.transform.eulerAngles.z == 180)
        {
            newPos += this.originalOffset;
        }
        else if (target.transform.eulerAngles.z == 270)
        {
            newPos -= new Vector2(this.originalOffset.y, this.originalOffset.x);
        }
        this.transform.rotation = this.target.transform.rotation;
        this.transform.position = new Vector3(newPos.x, newPos.y, -10);
	}
}
