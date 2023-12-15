using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : MonoBehaviour
{

    public float range = 4.5f;
    public float punchRate;
    private float nextTimeToPunch;
    public float punchDamage = 1f;

    private BoxCollider fistTrigger;

    public EnemyManager enemyManager;

    public LayerMask raycastLayerMask;

    void Start()
    {
        fistTrigger = GetComponent<BoxCollider>();
        fistTrigger.size = new Vector3 (0.3f, 0.3f, range);
        fistTrigger.center = new Vector3(0, 0, range * 0.5f);
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToPunch)
        {
            Punch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // add potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        // remove potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }

    void Punch()
    {
        //damage enemies
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {
            var dir = enemy.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, range, raycastLayerMask))
            {
                if(hit.transform == enemy.transform)
                {
                    //damage enemy
                    enemy.TakeDamage(punchDamage);

                    Debug.DrawRay(transform.position, dir, Color.green);
                    //Debug.Break();
                }
            }
            
        }

        //reset punch timer
        nextTimeToPunch = Time.time + punchRate;
    }
}   
