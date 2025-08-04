using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakpts : MonoBehaviour
{
    [SerializeField] public GameObject correspondingEnemy;
    private Transform pointSpawn;
    [SerializeField] public GameObject pointPrefab;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pointSpawn = correspondingEnemy.transform;
            PointsDisperse();
            Destroy(correspondingEnemy);
            Destroy(gameObject);
        }
    }

    void PointsDisperse()
    {
        GameObject SpawnedPts =  Instantiate(pointPrefab, pointSpawn.position, Quaternion.identity);
        
        int childcount = SpawnedPts.transform.childCount;
         Transform currentChild;
        Rigidbody2D rbCurrent;

        for (int i = 0; i < childcount; i++)
        {
            currentChild = SpawnedPts.transform.GetChild(i);
            rbCurrent = currentChild.gameObject.GetComponent<Rigidbody2D>();
            rbCurrent.velocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
        }
    }
}
