using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndzoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                SceneManager.LoadScene(7);
                return;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5 && !SaveLoadManager.Instance.HasEnoughBook())
            {
                SceneManager.LoadScene(7);
                return;
            }
            
            SceneManager.LoadScene(1);
        }
    }
}
