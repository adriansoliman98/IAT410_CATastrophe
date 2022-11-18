using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlFireBullets : MonoBehaviour


{

   
    public int bulletsAmount;


    public float startAngle;
    public float endAngle ;

    private Vector2 bulletMoveDirection;
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Fire" , 0f, 2f);

        
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
        {
            float bulDirx = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDiry = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirx, bulDiry, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BossBullet>().SetMoveDirection(bulDir);

            angle += angleStep;

        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
