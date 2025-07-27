using UnityEngine;

public class bombScript : MonoBehaviour
{
    AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y < -5.77f)
        {
            Destroy(gameObject);
            _audioSource.Stop();
        }
    }
}
