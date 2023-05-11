using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public bool canInteract = false;
    public int sceneBuildIndex;
    public float teleportDelay=.25f;
    public PlayerControls playerControls;
    public GameObject interactText;

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControls.Player.Use.WasPerformedThisFrame() && canInteract)
        {
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(DelayedTeleport());
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.isTrigger)
        {
            return;
        }

		if (collision.tag == "Player")
		{
            canInteract = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            return;
        }

        if (collision.tag == "Player")
        {
            canInteract = false;
            interactText.SetActive(false);
        }
    }

    private IEnumerator DelayedTeleport()
    {
        yield return new WaitForSeconds(teleportDelay);

        yield return new WaitForSeconds(0.05f);
        SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
    }
}
