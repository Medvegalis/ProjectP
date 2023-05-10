using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public int sceneBuildIndex;
    public float teleportDelay=.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.isTrigger)
        {
            return;
        }

		if (collision.tag == "Player")
		{
            StartCoroutine(DelayedTeleport());
        }
    }
    private IEnumerator DelayedTeleport()
    {
        yield return new WaitForSeconds(teleportDelay);

        yield return new WaitForSeconds(0.05f);
        SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
    }
}
