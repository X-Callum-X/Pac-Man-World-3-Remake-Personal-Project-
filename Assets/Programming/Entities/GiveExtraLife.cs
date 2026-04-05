using TMPro;
using UnityEngine;

public class GiveExtraLife : MonoBehaviour
{
    public int numberOfLives;

    PlayerController player;

    public AudioSource source;
    public AudioClip eat;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pac-Man")
        {
            source.PlayOneShot(eat);

            player.lives += numberOfLives;

            Destroy(gameObject);
        }
    }
}
