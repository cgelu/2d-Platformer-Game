using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int startingLives = 5;
    private int lives;
    [SerializeField] private TMPro.TextMeshProUGUI livesText;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lives = PlayerPrefs.GetInt("lives", 5);
        livesText.text = "Lives: " + lives;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            LoseLife();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    void LoseLife()
    {
        lives--;
        PlayerPrefs.SetInt("lives", lives);
        //livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            EndGame();
            PlayerPrefs.SetInt("lives", startingLives);
        }
        else
        {
            Die();
            Invoke("RestartLevel", 2f);
        }

    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EndGame()
    {
        print("Lives in EndGame: " + lives);
        PlayerPrefs.SetInt("lives", startingLives);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

