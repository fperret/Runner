using UnityEngine;
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

    public GameObject prefabMap;

    private Map currentMap;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        currentMap = FindObjectOfType<Map>();
        createMapNeighbors(currentMap, currentMap.bottomEdge);
	}
	
	// Update is called once per frame
	void Update () {
	
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

    private void createMapNeighbors(Map map, Edge source)
    {
        foreach (Edge edge in map.edges)
        {
            if (edge.direction != getOppositeDirection(source.direction))
            {
                GameObject neighbor = createNewMap(edge, false);
                map.neighbors.Add(neighbor);
            }
        }
    }

    public GameObject createNewMap(Edge source, bool createNeighbors)
    {
        GameObject tmp = Instantiate(prefabMap);
        Vector3 diff = source.transform.position;
        switch (source.direction)
        {
            case Direction.Top:
                diff -= tmp.GetComponent<Map>().bottomEdge.transform.position;
                diff += new Vector3(0, 1, 0);
                //tmp.GetComponent<Map>().bottomEdge.GetComponent<Collider2D>().enabled = false;
                tmp.GetComponent<Map>().bottomEdge.canBeExitTriggered = false;
                tmp.GetComponent<Map>().bottomEdge.canBeEnterTriggered = true;
                break;

            case Direction.Right:
                diff -= tmp.GetComponent<Map>().leftEdge.transform.position;
                diff += new Vector3(1, 0, 0);
                //tmp.GetComponent<Map>().leftEdge.GetComponent<Collider2D>().enabled = false;
                tmp.GetComponent<Map>().leftEdge.canBeExitTriggered = false;
                tmp.GetComponent<Map>().leftEdge.canBeEnterTriggered = true;
                break;

            case Direction.Bot:
                diff -= tmp.GetComponent<Map>().topEdge.transform.position;
                diff += new Vector3(0, -1, 0);
                //tmp.GetComponent<Map>().topEdge.GetComponent<Collider2D>().enabled = false;
                tmp.GetComponent<Map>().topEdge.canBeExitTriggered = false;
                tmp.GetComponent<Map>().topEdge.canBeEnterTriggered = true;
                break;

            case Direction.Left:
                diff -= tmp.GetComponent<Map>().rightEdge.transform.position;
                diff += new Vector3(-1, 0, 0);
                //tmp.GetComponent<Map>().rightEdge.GetComponent<Collider2D>().enabled = false;
                tmp.GetComponent<Map>().rightEdge.canBeExitTriggered = false;
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
        foreach (GameObject neighbor in currentMap.neighbors)
        {
            Destroy(neighbor);
        }
        Destroy(currentMap.gameObject);
        currentMap = newMap;
    }
}
