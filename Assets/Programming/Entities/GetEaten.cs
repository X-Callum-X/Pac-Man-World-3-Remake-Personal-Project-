using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetEaten : MonoBehaviour
{
    public int pointValue;

    public TMP_Text scoreUI;

    PlayerController player;

    public AudioSource source;
    public AudioClip eat;

    public GameObject risingScore;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pac-Man")
        {
            if (risingScore != null)
            {
                TextMeshProUGUI risingScoreText = risingScore.GetComponentInChildren<TextMeshProUGUI>();

                risingScoreText.text = pointValue.ToString();

                Instantiate(risingScore, transform.position, Quaternion.identity);
            }

            source.PlayOneShot(eat);

            player.score += pointValue;

            scoreUI.text = player.score.ToString();

            Destroy(gameObject);
        }
    }
}