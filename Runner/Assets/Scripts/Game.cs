using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum Direction
{
    Top,
    Right,
    Bot,
    Left
};

public class Game : MonoBehaviour {

    public static Game instance;

    public GameObject[] prefabMap = new GameObject[2];

    [HideInInspector]
    public float score;

    private Map currentMap;
    private PlayerMovement player;

    void Awake()
    {
        instance = this;
        score = 0.0f;
        Time.timeScale = 0;
    }

	// Use this for initialization
	void Start ()
    {
        currentMap = FindObjectOfType<Map>();
        createMapNeighbors(currentMap, currentMap.bottomEdge);
        player = PlayerMovement.instance;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
        if (Input.touchCount > 0)
        {
            Time.timeScale = 1;
        }
        score = player.dist;
	}

    private Direction getOppositeDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Top:
                return (Direction.Bot);

            case Direction.Right:
                return (Direction.Left);

            case Direction.Bot:
                return (Direction.Top);

            case Direction.Left:
                return (Direction.Right);

            default:
                Debug.Log("Should not be here");
                return (Direction.Top);
        }
    }

    public void createMapNeighbors(Map map, Edge source)
    {
        foreach (Edge edge in map.edges)
        {
            if (edge.direction != source.direction)
            {
                GameObject neighbor = createNewMap(edge, false);
                map.neighbors.Add(neighbor);
            }
        }
    }

    private GameObject createNewMap(Edge source, bool createNeighbors)
    {

        GameObject tmp = Instantiate(prefabMap[Random.Range(0, 2)]);
        Vector3 diff = source.transform.position;
        switch (source.direction)
        {
            case Direction.Top:
                diff -= tmp.GetComponent<Map>().bottomEdge.transform.position;
                diff += new Vector3(0, 1, 0);
                tmp.GetComponent<Map>().bottomEdge.canBeEnterTriggered = true;
                break;

            case Direction.Right:
                diff -= tmp.GetComponent<Map>().leftEdge.transform.position;
                diff += new Vector3(1, 0, 0);
                tmp.GetComponent<Map>().leftEdge.canBeEnterTriggered = true;
                break;

            case Direction.Bot:
                diff -= tmp.GetComponent<Map>().topEdge.transform.position;
                diff += new Vector3(0, -1, 0);
                tmp.GetComponent<Map>().topEdge.canBeEnterTriggered = true;
                break;

            case Direction.Left:
                diff -= tmp.GetComponent<Map>().rightEdge.transform.position;
                diff += new Vector3(-1, 0, 0);
                tmp.GetComponent<Map>().rightEdge.canBeEnterTriggered = true;
                break;

            default:
                break;
        }
        tmp.transform.Translate(diff);
        if (createNeighbors)
        {
            createMapNeighbors(tmp.GetComponent<Map>(), source);
        }
        return (tmp);
    }

    public void updateMap(Map newMap)
    {
        player.speed += 0.3f;
        foreach (GameObject neighbor in currentMap.neighbors)
        {
            if (neighbor.GetInstanceID() != newMap.gameObject.GetInstanceID())
            {
                Destroy(neighbor);
            }
        }
        Destroy(currentMap.gameObject);
        currentMap = newMap;
    }

    public void gameOver()
    {
        Invoke("endGame", 2);
   }

    private void endGame()
    {
        FindObjectOfType<CameraFollow>().enabled = false;
        Destroy(player.gameObject);
        SceneManager.LoadScene("Game");
    }
}
