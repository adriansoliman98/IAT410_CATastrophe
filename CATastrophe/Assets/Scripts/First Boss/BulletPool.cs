using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstance;

  //  [SteralizedField]
    public GameObject pooledBullet;
        private bool notEnoughBulletsInPool = true;

    private List<GameObject> bullets;

    private void Awake()
    {
        bulletPoolInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();

    }

    public GameObject GetBullet()
    {
      
        GameObject bul = Instantiate(pooledBullet);
        bul.SetActive(false);
        bullets.Add(bul);
        return bul;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
