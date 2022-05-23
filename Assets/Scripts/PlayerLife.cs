using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;

    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.y < -10)
        {
            DeathTrigger();
            RestartScene();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            DeathTrigger();
        }
    }

    private void DeathTrigger()
    {
        deathSound.Play();
        animator.SetTrigger("deathTrigger");
        rb.bodyType = RigidbodyType2D.Static;

    }

    private void RestartScene() //Called within the animation window
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
