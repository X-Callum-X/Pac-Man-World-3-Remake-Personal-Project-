using UnityEngine;

public class GiveHealth : MonoBehaviour
{
    PlayerController player;

    public AudioSource source;
    public AudioClip eat;

    public bool isSmallWedge;
    public bool isLargeWedge;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pac-Man" && player.hitPoints < player.maxHitPoints && isSmallWedge)
        {
            source.PlayOneShot(eat);

            player.hitPoints += 1;

            Destroy(gameObject);
        }

        else if (other.gameObject.name == "Pac-Man" && player.hitPoints < player.maxHitPoints && isLargeWedge)
        {
            source.PlayOneShot(eat);

            player.hitPoints = player.maxHitPoints;

            Destroy(gameObject);
        }
    }
}
