using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    [SerializeField] private int bulletAmout = 10;
    [SerializeField] private float startAngle = 90f, endAngle = 270f;
    public bool fireDone = false;
    public int timesFired = 0;
    public void InvokeFireStart(float waitTime, float attackSpeed)
    {
        InvokeRepeating("Fire", waitTime, attackSpeed);
    }
    public void StopFire()
    {
        CancelInvoke();
    }

    public void setBulletAmout(int newAmount)
    {
        bulletAmout = newAmount;
    }
    public void setAngles(float start, float end)
    {
        startAngle = start;
        endAngle = end;
    }

    private void Fire() 
    {
        fireDone = false;

        float angleStep = (endAngle - startAngle) / bulletAmout;

        float angle = startAngle;

        for(int i = 0; i < bulletAmout + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.instance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<BossBullet>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
        timesFired++;
        fireDone = true;
    }
}
