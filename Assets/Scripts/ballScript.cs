using UnityEngine;

public class ballScript : MonoBehaviour
{
    SpriteRenderer bSr;
    [SerializeField]
    private Sprite[] bSprite = new Sprite[3];
    public int currentBall;
    
    
    void Start()
    {
        bSr = GetComponent<SpriteRenderer>();
        currentBall=Random.Range(0,bSprite.Length);
        bSr.sprite = bSprite[currentBall];
        


    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-7f)
        {
            Destroy(gameObject);
        }
    }
}
