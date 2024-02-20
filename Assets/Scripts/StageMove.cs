using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMove : MonoBehaviour
{
    public int sceneBuildIndex;

    // Level move zoned enter, if collider is a player
    // Move to another scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.GetComponent<PortalController>();
        Debug.Log("Trigger Entered");

        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if (other.tag == "Player" && gameObject.GetComponent<PortalController>().canWarp)
        {
            // Player entered, so move level
            Debug.Log("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}