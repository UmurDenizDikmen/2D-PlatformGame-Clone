using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    public AudioSource collectVoice;
    public TextMeshProUGUI textCherryCount;
    private void Start()
    {
        textCherryCount.text = cherries.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            
            Destroy(collision.gameObject);
            cherries++;
            textCherryCount.text = cherries.ToString();
            collectVoice.Play();

        }
    }
}
