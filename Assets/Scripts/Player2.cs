using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetMouseWorldPosition(Input.mousePosition);
    }

    Vector2 GetMouseWorldPosition(Vector2 mousePos)
    {
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
