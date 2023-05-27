using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int fruits = 0;

    private TMP_Text fruitText;

    [SerializeField] private AudioSource collectSoundEffect;

    private void Start()
    {
        fruitText = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision.gameObject refers to the object that the character is colliding with
        if (collision.gameObject.CompareTag("Fruits"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            fruits++;
            fruitText.text = "Fruits:" + fruits;
        }
    }
}
