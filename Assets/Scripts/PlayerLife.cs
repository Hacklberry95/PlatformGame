using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource respawnSoundEffect;
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
        {
            Debug.Log("Should die!!!!!");
            Die();
        }
    }
    private void Die()
    {
        deathSoundEffect.Play();
        Debug.Log("Should be dead!!!!!");
        rigidbody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }
    private void RespawnCharacter()
    {
        respawnSoundEffect.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
