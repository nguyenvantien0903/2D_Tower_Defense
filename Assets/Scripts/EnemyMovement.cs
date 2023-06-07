using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = LevelManager.instance.path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {
        // khoang cach giua ke dich va noi ke dich can den
        if(Vector2.Distance(target.position, transform.position) <=0.1f)
        {
            pathIndex++;
            //Neu da di den cuoi
            if(pathIndex==LevelManager.instance.path.Length)
            {
                EnemySpawner.OnEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.instance.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity= direction * moveSpeed;
    }
}
