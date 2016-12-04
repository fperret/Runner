using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIManager : MonoBehaviour {

    private Game game;
    private Text score;

    void Awake()
    {
        score = transform.Find("Score background").Find("Score").GetComponent<Text>();
    }

	// Use this for initialization
	void Start ()
    {
        game = Game.instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        score.text = ((int)game.score).ToString();
	}
}
