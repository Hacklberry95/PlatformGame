using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int diamonds = 0;
    [SerializeField] private Text diamondsText;
    [SerializeField] private AudioSource diamondsAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Diamond"))
        {
            diamondsAudioSource.Play();
            Destroy(collision.gameObject);
            diamonds++;
            diamondsText.text = "Diamonds: " + diamonds;
            Debug.Log("Diamonds: " + diamonds);
        }
    }
}
