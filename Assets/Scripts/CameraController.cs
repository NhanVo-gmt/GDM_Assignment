using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player1;
    private Transform player2;

    private void Update()
    {
        transform.position = new Vector3(player1.position.x, transform.position.y, transform.position.z);
    }
}
