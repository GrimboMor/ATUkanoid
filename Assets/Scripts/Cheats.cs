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
    public bool CheatsEnabled = false;


    void Update()
    {
        //Press R to restart the level
        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GameManagerScript.Instance.RestartLevel();
            GameManagerScript.Instance.LifeLost();
        }

        if (CheatsEnabled == true)
        {
            //Press L to add a life
            if (Input.GetKeyDown(KeyCode.L))
            {
                GameManagerScript.Instance.Lives++;
                //GameManagerScript.Instance.UpdateLives();
            }
            // Check for growing or shrinking the paddle size
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                PaddleScript.Instance.ChangePaddleSizeAnimated(3.6f);
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                PaddleScript.Instance.ChangePaddleSizeAnimated(2f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                PaddleScript.Instance.ChangePaddleSizeAnimated(1.2f);
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
}