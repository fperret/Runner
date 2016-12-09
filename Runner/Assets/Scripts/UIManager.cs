using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class UIManager : MonoBehaviour {

    private Game game;
    private Text score;
    private GameObject gameOverText;
    private GameObject homeText;
    private GameObject bestScoreInMenu;
    private Slider musicSlider;
    private Slider sfxSlider;
    private GameObject musicButton;
    private GameObject sfxButton;

    [SerializeField]
    private Sprite musicOn;
    [SerializeField]
    private Sprite musicOff;
    [SerializeField]
    private Sprite soundOn;
    [SerializeField]
    private Sprite soundOff;


    void Awake()
    {
        score = transform.Find("Score background").Find("Score").GetComponent<Text>();
        homeText = transform.Find("Home text background").gameObject;
        gameOverText = transform.Find("GameOver text").gameObject;
        bestScoreInMenu = transform.Find("Best score inmenu").gameObject;
        musicSlider = transform.Find("Music volume").GetComponent<Slider>();
        sfxSlider = transform.Find("SFX volume").GetComponent<Slider>();
        musicButton = transform.Find("Music on off").gameObject;
        sfxButton = transform.Find("SFX on off").gameObject;
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
        if (game.isGameOver)
        {
            setGameOverScreen();
        }
        else if (Time.timeScale != 0)
        {
            setGameScreen();
        }
        bestScoreInMenu.GetComponentInChildren<Text>().text = Constants.bestScoreText + "\n" + PlayerPrefs.GetInt(Constants.bestScoreKey).ToString();
        musicSlider.value = game.musicVolume;
        sfxSlider.value = game.sfxVolume;
	}

    private void setGameScreen()
    {
        score.transform.parent.gameObject.SetActive(true);
        homeText.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
        musicButton.SetActive(false);
        sfxButton.SetActive(false);
        bestScoreInMenu.SetActive(false);
    }

    private void setGameOverScreen()
    {
        score.transform.parent.gameObject.SetActive(false);
        gameOverText.SetActive(true);
        gameOverText.GetComponentInChildren<Text>().text =
            Constants.gameOverText +
            Constants.scoreText + ((int)game.score) + "\n" + 
            Constants.bestScoreText + " : " + PlayerPrefs.GetInt(Constants.bestScoreKey).ToString();
    }

    public void musicButtonClick()
    {
        if (musicButton.GetComponent<Image>().sprite == musicOn)
        {
            game.musicVolume = -1;
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            game.musicVolume = PlayerPrefs.GetFloat(Constants.musicVolumeKey);
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
    }

    public void sfxButtonClick()
    {
        if (sfxButton.GetComponent<Image>().sprite == soundOn)
        {
            game.sfxVolume = -1;
            sfxButton.GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            game.sfxVolume = PlayerPrefs.GetFloat(Constants.sfxVolumeKey);
            sfxButton.GetComponent<Image>().sprite = soundOn;
        }
    }

    public void onMusicVolumeChanged()
    {
        game.musicVolume = musicSlider.value;
    }

    public void onSFXVolumeChanged()
    {
        game.sfxVolume = sfxSlider.value;
    }

    public void saveExitButton()
    {
        PlayerPrefs.SetFloat(Constants.musicVolumeKey, game.musicVolume);
        PlayerPrefs.SetFloat(Constants.sfxVolumeKey, game.sfxVolume);
        // Need exit
    }
}
