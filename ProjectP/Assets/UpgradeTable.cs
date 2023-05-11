using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class UpgradeTable : MonoBehaviour, IDataPersistence
{

    [SerializeField] private bool canInteract = false;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject interactionText;
    public PlayerControls playerControls;
    public PlayerScript playerScript;

    public TextMeshProUGUI currentHealthLevelText;
    public TextMeshProUGUI currentDamageLevelText;
    public TextMeshProUGUI currentSpeedLevelText;

    public TextMeshProUGUI maxCurrentHealthLevelText;
    public TextMeshProUGUI maxCurrentDamageLevelText;
    public TextMeshProUGUI maxCurrentSpeedLevelText;

    private int healthLevel;
    private int damageLevel;
    private int speedLevel;

    private int maxHealthLevel;
    private int maxDamageLevel;
    private int maxSpeedLevel;

    public TextMeshProUGUI pointsText;
    public int pointsCount = 0;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {

        Collectable.OnCollected += PointColleted;

        StartCoroutine(nameof(StartDelayed));

        pointsText.text = pointsCount.ToString();
    }
    IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(0.6f);

        maxHealthLevel = playerScript.maxHealth.maxLevel;
        maxDamageLevel = playerScript.damage.maxLevel;
        maxSpeedLevel = playerScript.speed.maxLevel;

        healthLevel = playerScript.maxHealth.currentLevel;
        damageLevel = playerScript.damage.currentLevel;
        speedLevel = playerScript.speed.currentLevel;

        maxCurrentHealthLevelText.text = maxHealthLevel.ToString();
        maxCurrentDamageLevelText.text = maxDamageLevel.ToString();
        maxCurrentSpeedLevelText.text = maxSpeedLevel.ToString();

        currentHealthLevelText.text = playerScript.maxHealth.currentLevel.ToString();
        currentDamageLevelText.text = playerScript.damage.currentLevel.ToString();
        currentSpeedLevelText.text = playerScript.speed.currentLevel.ToString();

        pointsText.text = pointsCount.ToString();
    }
   private void Update()

    {
        if(canInteract)
        {
            
            if(playerControls.Player.Use.WasPerformedThisFrame())
            {
                Debug.Log("UPGRADE MENU");
                pointsText.text = pointsCount.ToString();
                PauseMenu.instance.PauseGameOther(upgradeMenu);
            }
        }
    }
    public void HealthUpgrade()
    {
        if (pointsCount > 0 && playerScript.maxHealth.currentLevel < maxHealthLevel)
        {
            playerScript.maxHealth.LevelUp();
            currentHealthLevelText.text = playerScript.maxHealth.currentLevel.ToString();
            pointsCount--;
            pointsText.text = pointsCount.ToString();
            
        }
    }

    public void DamageUpgrade()
    {

        if (pointsCount > 0 && playerScript.damage.currentLevel < maxDamageLevel)
        {
            playerScript.damage.LevelUp();
            currentDamageLevelText.text = playerScript.damage.currentLevel.ToString();
            pointsCount--;
            pointsText.text = pointsCount.ToString();
        }

    }
    public void SpeedUpgrade()
    {
        if(pointsCount > 0 && playerScript.speed.currentLevel < maxSpeedLevel)
        {
            playerScript.speed.LevelUp();
            currentSpeedLevelText.text = playerScript.speed.currentLevel.ToString();
            pointsCount--;
            pointsText.text = pointsCount.ToString();
        }
    }

    private void PointColleted(typeOfLoot type,int value)
    {
        if(type == typeOfLoot.statCoin)
            pointsCount+=value;
    }

    public void LoadData(GameData data)
    {
        this.pointsCount = data.pointsCount;
    }

    public void SaveData(GameData data)
    {
        data.pointsCount = this.pointsCount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
            interactionText.SetActive(true);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
            interactionText.SetActive(false);
        }
        

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
