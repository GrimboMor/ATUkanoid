using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour
{

    #region Singleton
    //creating a single instance of the FogScript script

    private static FogScript _instance;

    public static FogScript Instance => _instance;

    //checking that there is no other instances of the game manger running
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion


    // Reference to the animated fog prefab
    public GameObject fogPrefab;

    // Positions for the fog instances
    private Vector3[] fogPositions = {
        new Vector3(0, -0.3f, 3),
        new Vector3(0, 1.1f, 3.1f),
        new Vector3(0, 3, 3.2f)
    };

    // Array to store the instantiated fog objects
    private GameObject[] fogInstances;

    public void CreateFog()
    {
        fogInstances = new GameObject[fogPositions.Length];

        for (int i = 0; i < fogPositions.Length; i++)
        {
            // Instantiate the fog prefab at the specified position and rotation
            fogInstances[i] = Instantiate(fogPrefab, fogPositions[i], Quaternion.identity);

            // If it's the second fog, flip it along the x-axis
            if (i == 1)
            {
                SpriteRenderer fogSpriteRenderer = fogInstances[i].GetComponent<SpriteRenderer>();
                if (fogSpriteRenderer != null)
                {
                    fogSpriteRenderer.flipX = true;
                }
            }

            // Get the Animator component from the instantiated fog
            Animator fogAnimator = fogInstances[i].GetComponent<Animator>();

            // Calculate the animation length
            float animationLength = fogAnimator.runtimeAnimatorController.animationClips[0].length;

            // Start the coroutine to destroy the fog after the animation plays once
            StartCoroutine(DestroyFogAfterDelay(fogInstances[i], animationLength-1));
        }
    }

    private IEnumerator DestroyFogAfterDelay(GameObject fogInstance, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the fog instance
        Destroy(fogInstance);
    }
}
