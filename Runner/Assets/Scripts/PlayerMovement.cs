using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 1.0f;

    private Turn currentTurn;
    public bool inTurn;

    void Awake()
    {
        inTurn = false;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(transform.up * Time.deltaTime * this.speed, Space.World);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, 90);
            processTurn();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -90);
            processTurn();
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Turn"))
        {
            inTurn = true;
            currentTurn = other.GetComponent<Turn>();
        }
        else if (other.CompareTag("Deathzone"))
        {
            lose();
        }
        else if (other.CompareTag("Edge"))
        {
            if (other.GetComponent<Edge>().canBeExitTriggered == true)
            {
                other.GetComponent<Edge>().exitTrigger = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Turn"))
        {
            inTurn = false;
            currentTurn = null;
        }
        else if (other.CompareTag("Edge")
            && other.GetComponent<Edge>().canBeEnterTriggered == true)
        {
            other.GetComponent<Edge>().enterTrigger = true;
        }
    }

    void processTurn()
    {
        // Check exception currentTurn access when its destroyed
        if (inTurn && currentTurn != null && currentTurn.usable == true)
        {
            transform.position = currentTurn.transform.position;
            currentTurn.usable = false;
        }
    }

    // Move in GAME / UI GESTION
    void    lose()
    {
        Debug.Log("here");
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
    }
}
