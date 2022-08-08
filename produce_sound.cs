using UnityEngine;


public class produce_sound : MonoBehaviour
{
    private AudioClip audio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        audio = Resources.Load<AudioClip>("457039__breviceps__hit-a-ball");
        audioSource.clip = audio;
    }

    public void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    } 
}
