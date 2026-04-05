using UnityEngine;

public class HealthUI : MonoBehaviour
{
    PlayerController player;

    public GameObject wedge1;
    public GameObject wedge2;
    public GameObject wedge3;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.hitPoints == 4)
        {
            wedge1.SetActive(true);
            wedge2.SetActive(true);
            wedge3.SetActive(true);
        }
        if (player.hitPoints == 3)
        {
            wedge1.SetActive(false);
            wedge2.SetActive(true);
            wedge3.SetActive(true);
        }
        if (player.hitPoints == 2)
        {
            wedge1.SetActive(false);
            wedge2.SetActive(false);
            wedge3.SetActive(true);
        }
        if (player.hitPoints == 1)
        {
            wedge1.SetActive(false);
            wedge2.SetActive(false);
            wedge3.SetActive(false);
        }
    }
}
