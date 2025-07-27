using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class playerScript : MonoBehaviour
{
    private const bool V = false;
    [SerializeField]
    private float speed = 15f;
    [SerializeField]
    Sprite[] sprites = new Sprite[3];
    int currentSprite = 0;
    SpriteRenderer _sr;
    GameObject _ballGameObject;
    GameObject GameManager;
    GameObject spawner;
    public UIDocument doc1;
    Label lifeText;
    int life = 3;
    


    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = sprites[currentSprite];
        _ballGameObject = GameObject.FindGameObjectWithTag("BallTag");
        GameManager = GameObject.FindGameObjectWithTag("GameManagerTag");
        spawner = GameObject.FindGameObjectWithTag("SpawnerTag");
        lifeText = doc1.rootVisualElement.Q<Label>("LifeText");

    }

    // Update is called once per frame
    void Update()
    {

        spriteChange(); //Player Color Change
    }

    void FixedUpdate()
    {
        playerMov(); //Player Movement
        transformClamp();


    }

    void playerMov()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate((Vector2.right * horizontal * Time.deltaTime) * speed);


    }

    void spriteChange()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentSprite = (currentSprite + 1) % sprites.Length;
            _sr.sprite = sprites[currentSprite];
        }
    }

    void transformClamp()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -7.5f, 7.5f);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BallTag"))
        {
            ballScript ball = other.GetComponent<ballScript>();
            if (ball != null && currentSprite == ball.currentBall)
            {

                Destroy(other.gameObject);
                GameManager.GetComponent<gameManagerScript>().trueColor();

            }
            else if (ball != null && currentSprite != ball.currentBall)
            {

                life--;
                lifeText.text = "Life: " + life.ToString();
                Destroy(other.gameObject);
                GameManager.GetComponent<gameManagerScript>().wrongColor();
                

                if (life <= 0)
                {
                    Destroy(gameObject);
                    spawner.GetComponent<spawnerScript>().canSpawnBall = false;
                    GameManager.GetComponent<gameManagerScript>().StartCoroutine(GameManager.GetComponent<gameManagerScript>().restartGameforSeconds());


                }

            }
            
        }
        else if (other.CompareTag("BombTag"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameManager.GetComponent<gameManagerScript>().bombTrigger();
            spawner.GetComponent<spawnerScript>().canSpawnBall = false;
            life = 0;
            lifeText.text = "Life: " + life.ToString();
            GameManager.GetComponent<gameManagerScript>().StartCoroutine(GameManager.GetComponent<gameManagerScript>().restartGameforSeconds());
            

        }




    }
}
