using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class gameManagerScript : MonoBehaviour
{
    public GameObject _basicHit_Fx, _destroyHit_Fx, _bombHit_Fx;
    public GameObject player;
    AudioSource _audiosource;
    public AudioClip basicHit_audio, destroyHit_audio, bombExplosion_audio;
    public AudioClip fuseSound;
    public AudioClip newGameButton_audio, exitButtonaudio;
    public GameObject _bomb;
    public UIDocument doc;
    public Label scoreText, highScoreText;
    Label titleText, how2Play, how2PlayText, gameOverText;
    Button _startGame, _exit;
    public int life=3;
    [SerializeField]
    int score, highScore;
    bool CanRestartGame = false;
    GameObject _spawner;

    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        scoreText = doc.rootVisualElement.Q<Label>("ScoreText");
        highScoreText = doc.rootVisualElement.Q<Label>("HighScoreText");
        titleText= doc.rootVisualElement.Q<Label>("Title");
        how2Play = doc.rootVisualElement.Q<Label>("How2Play");
        how2PlayText = doc.rootVisualElement.Q<Label>("How2PlayText");
        gameOverText = doc.rootVisualElement.Q<Label>("GameOverText");
        _startGame = doc.rootVisualElement.Q<Button>("StartGame");
        _startGame.clicked += StartGameButton;
        _exit = doc.rootVisualElement.Q<Button>("Exit");
        _exit.clicked += exitGameButton;
        _spawner = GameObject.FindGameObjectWithTag("SpawnerTag");
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "HScore: " + highScore.ToString();



        Time.timeScale = 0; //Stop the game
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(score>=highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            
        }
        
    }

    public void trueColor () //True Color Match
    {
        GameObject mainHit=Instantiate(_basicHit_Fx,player.transform.position,Quaternion.identity);
        Destroy(mainHit, 0.5f);
        _audiosource.PlayOneShot(basicHit_audio);
        score++;
        scoreText.text = "Score: " + score.ToString();
        if (score > 10) // Gamespeed by score
        {
            _spawner.GetComponent<spawnerScript>().ballSpawnRate = 1.7f;
        }
        if (score > 20) 
        {
            _spawner.GetComponent<spawnerScript>().ballSpawnRate = 1.5f;
        }
        if (score > 40) 
        {
            _spawner.GetComponent<spawnerScript>().ballSpawnRate = 1f;
        }

    }

    public void wrongColor () //Wrong Color Match
    {
        Instantiate(_destroyHit_Fx, player.transform.position, Quaternion.identity);
        _audiosource.PlayOneShot(destroyHit_audio);
       
        
    }
    
    public void bombTrigger () //If player can touch the bomb
    {
        Instantiate(_bombHit_Fx, player.transform.position, Quaternion.identity);
        _audiosource.PlayOneShot(bombExplosion_audio);
       
    }

    

    void StartGameButton ()
    {
        _audiosource.PlayOneShot(newGameButton_audio);
        Time.timeScale = 1f;
        titleText.style.display = DisplayStyle.None;
        how2Play.style.display = DisplayStyle.None;
        how2PlayText.style.display = DisplayStyle.None;
        _startGame.style.display = DisplayStyle.None;
        _exit.style.display = DisplayStyle.None;
        
    }

    void exitGameButton ()
    {
        _audiosource.PlayOneShot(exitButtonaudio);
        Application.Quit();
    }

    public IEnumerator restartGameforSeconds ()
    {
        gameOverText.style.display = DisplayStyle.Flex;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }

}

