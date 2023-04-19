using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{

    public GameObject gameDiagnostics;

    void Update()
    

    {
        //Press R to restart the level
        if (Input.GetKeyDown(KeyCode.R))
        {
           //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
           GameManagerScript.Instance.RestartLevel();
        }

        //Press L to add a life
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManagerScript.Instance.Lives++;
            //GameManagerScript.Instance.UpdateLives();
        }

        //Press M to test MultiBalls
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (BallScript ball in BallsManagerScript.Instance.BallsList.ToList())
            {
                BallsManagerScript.Instance.MultiBalls(ball.gameObject.transform.position, 2);
            }
        }
    }
}
