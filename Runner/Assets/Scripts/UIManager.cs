using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class UIManager : MonoBehaviour {

    private Game game;
    private Text score;
    private GameObject bestScoreBackground;
    private Slider musicSlider;
    private Slider sfxSlider;

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
        bestScoreBackground = transform.Find("Best score background").gameObject;
        musicSlider = transform.Find("Music volume").GetComponent<Slider>();
        sfxSlider = transform.Find("SFX volume").GetComponent<Slider>();
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
            displayBestScore();
        }
        musicSlider.value = game.musicVolume;
        sfxSlider.value = game.sfxVolume;
	}

    public void displayBestScore()
    {
        bestScoreBackground.SetActive(true);
        bestScoreBackground.GetComponentInChildren<Text>().text = Constants.bestScoreText + PlayerPrefs.GetInt(Constants.bestScoreKey).ToString();
    }

    public void musicButtonClick()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite == musicOn)
        {
            game.musicVolume = -1;
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            game.musicVolume = PlayerPrefs.GetFloat(Constants.musicVolumeKey);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = musicOn;
        }
    }

    public void sfxButtonClick()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite == soundOn)
        {
            game.sfxVolume = -1;
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            game.sfxVolume = PlayerPrefs.GetFloat(Constants.sfxVolumeKey);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = soundOn;
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
