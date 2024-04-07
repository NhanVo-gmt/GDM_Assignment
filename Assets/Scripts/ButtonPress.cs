using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] public GameObject bridge;

    private void Start()
    {
        bridge.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bridge.SetActive(true);
        }
    }
}
