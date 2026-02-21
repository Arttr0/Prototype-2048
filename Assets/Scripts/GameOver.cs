using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameStateManager gameStateManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cubelaunched")) // только обычный куб
        {
            gameStateManager.GameOver();
        }
    }*/
    private bool gameOverTriggered = false;

    void OnTriggerStay(Collider other)
    {
        if (gameOverTriggered) return;
        if (other.CompareTag("Cubelaunched"))
        {
            gameOverTriggered = true;
            gameStateManager.GameOver();
            //Debug.Log("В зоне триггера сейчас находится: " + other.tag);
        }
    }
}
