using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    GameManager gameManagerr;
    GameObject player;
    float enemyHealth=100f;
    float enemyMoveSpeed=2f;
    Quaternion targetRotation;
    bool disableEnemy=false;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerr=GameObject.Find("GameManager").GetComponent<GameManager>();
        player=GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManagerr.gameOver && !disableEnemy)
        {
            MoveEnemy();
            RotateEnemy();

        }
    }

    void MoveEnemy()
    {
        transform.position=Vector2.MoveTowards(transform.position,player.transform.position,enemyMoveSpeed*Time.deltaTime);

    }
    void RotateEnemy()
    {
        moveDirection=player.transform.position-transform.position;
        moveDirection.Normalize();

        targetRotation=Quaternion.LookRotation(Vector3.forward,moveDirection);

        if(transform.rotation!=targetRotation)
        {
            transform.rotation=Quaternion.RotateTowards(transform.rotation,targetRotation,200*Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Bullet")
        {
            StartCoroutine(Damaged());
            
            enemyHealth-=40f;
            if(enemyHealth<=0f)
            {
                Destroy(gameObject);
                KillManager.instance.AddPoints();
            }
            Destroy(other.gameObject);
            
        }else if(other.gameObject.tag=="Player")
        {
            gameManagerr.gameOver=true;
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator Damaged()
    {
        disableEnemy=true;
        yield return new WaitForSeconds(0.5f);
        disableEnemy=false;
        
    }
}
