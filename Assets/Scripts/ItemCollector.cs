using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioSource collectSound;
    private int scoreCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Banana"))
        {
            collectSound.Play();
            Destroy(collision.gameObject);
            scoreCount++;
            scoreText.text = "Score: " + scoreCount;
        }
    }
}
