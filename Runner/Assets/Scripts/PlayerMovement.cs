using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;

    public float speed = 1.0f;
    public float dist;
    public bool inTurn;

    private Turn currentTurn;
    private Vector2 fingerPosStart;
    private Vector2 fingerPosLast;
    private bool isTouch;

    void Awake()
    {
        inTurn = false;
        instance = this;
        dist = 0.0f;
        isTouch = true;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale != 0)
        {
            transform.Translate(transform.up * Time.deltaTime * this.speed, Space.World);
            dist += (transform.up * Time.deltaTime * this.speed).magnitude;
            Debug.Log("aah");

            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        fingerPosStart = touch.position;
                        break;

                    case TouchPhase.Moved:
                        fingerPosLast = touch.position;
                        break;

                    case TouchPhase.Ended:
                        float distHorizontal = fingerPosLast.x - fingerPosStart.x;
                        // Test value
                        // swipe long enough
                        if (Mathf.Abs(distHorizontal) > 2)
                        {
                            // swipe right
                            if (distHorizontal > 0 && !isTouch)
                            {
                                isTouch = true;
                                transform.Rotate(0, 0, -90);
                                processTurn();
                            }
                            // swipe left
                            else if (!isTouch)
                            {
                                isTouch = true;
                                transform.Rotate(0, 0, 90);
                                processTurn();
                            }
                        }
                        isTouch = false;
                        break;
                    
                    default:
                        break;
                        
                }
            }
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

    void    lose()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        enabled = false;
        Game.instance.gameOver();
    }
}
