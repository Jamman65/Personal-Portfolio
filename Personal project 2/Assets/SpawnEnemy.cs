using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{

    public class SpawnEnemy : MonoBehaviour
    {
        public bool isDead;
        public GameObject enemy;
        public Transform spawnPoint;
        public EnemyStats stats;


        // Start is called before the first frame update
        void Start()
        {
            isDead = false;

        }

        // Update is called once per frame
        //void Update()
        //{
            
        //    //if(isDead == true)
        //    //{
        //    //    Spawn();
        //    //    Destroy(gameObject);
                
        //    //}
        //    else
        //    {
        //        return;
        //    }
        //}

       private void Spawn()
        {
            GameObject enemyPrefab = Instantiate(enemy) as GameObject;
            enemyPrefab.name = "EnemySpawned";
            enemyPrefab.transform.parent = spawnPoint;
            enemyPrefab.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);

            //Instantiate(enemy, spawnPoint);
            isDead = false;
            return;
        }
    }
}
