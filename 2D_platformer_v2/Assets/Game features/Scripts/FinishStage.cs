using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStage : MonoBehaviour
{
    private Animator anim;
    private int count = 0;
    private bool completeLevel = false;

    private AudioSource reachCheckpoint;
    private void Start()
    {
        anim = GetComponent<Animator>();
        reachCheckpoint = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Makes sure that it is only called once
        if (collision.gameObject.CompareTag("Player") && !completeLevel)
        {
            // Make it sso that the player cannot collide with the checkpoint
            anim.SetTrigger("checkpointReached");
            reachCheckpoint.Play();
            count++;
            completeLevel = true;
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
