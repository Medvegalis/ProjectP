using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum BossStages
{
   WaitingToStart,
   Stage1,
   Stage2
}

public class BossScript : MonoBehaviour
{
    private FireBullets fireBullets;
    private EnemyHealth healthScript;
    private int maxHealth;

    private BossStages stage;
    public string stageS;

    private List<Vector3> spawnPositionList;
    public GameObject EnemyGameObject;
    private List<GameObject> aliveGoons;

    public bool FightStarted = false;
    public bool Stage1Started = false;
    public bool Stage2Started = false;

    public Slider healthSlider;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 11);// ignores collisions enemy and enemyProjectile
        Physics2D.IgnoreLayerCollision(10, 10);// ignores collisions enemyProjectile and enemyProjectile


        fireBullets = GetComponent<FireBullets>();
        healthScript = GetComponent<EnemyHealth>();
        maxHealth = healthScript.health;
        stage = BossStages.WaitingToStart;

        aliveGoons = new List<GameObject>();
        spawnPositionList = new List<Vector3>();

        foreach (Transform spawnPosition in transform.Find("SpawnPositions"))
        { 
            spawnPositionList.Add(spawnPosition.position);
        }

        healthScript.setInvincible(true);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (!FightStarted)
            {
                FightStarted = true;
            }
        }
    }

    private void Update()
    {
        if (!FightStarted)
            return;

        stageS = stage.ToString();
        switch(stage)
        {
            case BossStages.WaitingToStart:
                IntroStart();
                break;
            case BossStages.Stage1:
                Stage1();
                if(healthScript.health <= maxHealth / 2)
                {
                    StartNextStage();
                    InvokeRepeating(nameof(SpawnGoons), 3f, 4f);

                }
                break;
            case BossStages.Stage2:                
                if (healthScript.health == 0)
                {
                    fireBullets.StopFire();
                    CancelInvoke();
                }
                else
                    Stage2();
                break;
        }

        healthSlider.value = healthScript.health;

    }

    private void IntroStart()
    {

        if(fireBullets.timesFired == 0)
        {
            fireBullets.setAngles(90, 270);
            fireBullets.setBulletAmout(1);
            fireBullets.InvokeFireStart(0f, 0.5f);
        }
        if (fireBullets.timesFired == 2)
        {
            fireBullets.setAngles(110, 250);
        }
        if (fireBullets.timesFired == 4)
        {
            fireBullets.setAngles(130, 230);
        }
        if (fireBullets.timesFired == 6)
        {
            fireBullets.setAngles(150, 210);
        }
        if (fireBullets.timesFired == 8)
        {
            fireBullets.setAngles(175, 185);
        }
        if (fireBullets.timesFired == 10)
        {
            fireBullets.StopFire();
            StartNextStage();
        }
    }

    private void Stage1()
    {

        if (fireBullets.timesFired == 3)
        {
            fireBullets.setBulletAmout(3);
            fireBullets.setAngles(140, 220);

        }

        if (fireBullets.timesFired == 9)
        {
            fireBullets.setBulletAmout(10);
            fireBullets.setAngles(0, 360);
        }

        if (fireBullets.timesFired == 15)
        {
            fireBullets.timesFired = 0;

            fireBullets.setBulletAmout(7);
            fireBullets.setAngles(90, 270);
        }
    }

    private void Stage2()
    {

        if (fireBullets.timesFired == 3 )
        {
            fireBullets.setBulletAmout(10);
            fireBullets.setAngles(90, 270);
        }

        if (fireBullets.timesFired == 9 )
        {
            fireBullets.setBulletAmout(5);
            fireBullets.setAngles(70, 290);
        }

        if (fireBullets.timesFired == 15 )
        {
            fireBullets.timesFired = 0;

            fireBullets.setBulletAmout(20);
            fireBullets.setAngles(100, 260);
        }
    }


    private void StartNextStage()
    {
        switch(stage)
        {
            case BossStages.WaitingToStart:
                stage = BossStages.Stage1;
                Fire();
                break;
            case BossStages.Stage1:
                stage = BossStages.Stage2;
                Fire();
                break;
        }
    }

    private void Fire()
    {
        if (stage == BossStages.Stage1)
        {
            fireBullets.StopFire();
            healthScript.setInvincible(false);

            fireBullets.setBulletAmout(5);
            fireBullets.setAngles(90, 270);
            fireBullets.InvokeFireStart(2f,1f);

        }
        
        if(stage == BossStages.Stage2)
        {
            fireBullets.timesFired = 0;
            fireBullets.StopFire();

            fireBullets.setBulletAmout(20);
            fireBullets.setAngles(90, 270);
            fireBullets.InvokeFireStart(1f,0.5f);
        }    

    }

    private void SpawnGoons()
    {

        Vector3 spawnPosition = spawnPositionList[Random.Range(0, spawnPositionList.Count)];

        Instantiate(EnemyGameObject, spawnPosition, Quaternion.identity, transform.Find("EnemyList"));


    }

}
