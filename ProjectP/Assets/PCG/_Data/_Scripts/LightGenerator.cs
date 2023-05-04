using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightGenerator : MonoBehaviour
{
    public GameObject pointLightPrefab; // This should be a prefab of a Light2D Point Light.

    void Start()
    {
        StartCoroutine(InstantiateLightsWithDelay());
    }

    IEnumerator InstantiateLightsWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 1 second.

        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("Light");

        foreach (GameObject obj in lightObjects)
        {
            // Instantiate a new Light2D Point Light and attach it to the GameObject.
            GameObject newLight = Instantiate(pointLightPrefab, obj.transform.position, Quaternion.identity);
            newLight.transform.parent = obj.transform;
            Color lightColor = new Color(224f / 255f, 103f / 255f, 136f / 255f);
            newLight.GetComponent<Light2D>().intensity = 1f;
            newLight.GetComponent<Light2D>().color = lightColor;
            newLight.GetComponent<Light2D>().pointLightInnerRadius = 0.1f;
            newLight.GetComponent<Light2D>().pointLightOuterRadius = 5f;
        }
    }
}