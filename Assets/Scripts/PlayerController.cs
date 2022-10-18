using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]GameManager gameManagerr;

    Rigidbody2D rb;
    Camera mainCamera;


    float moveVertical;
    float moveHorizontal;
    float moveSpeed=5f;
    float speedLimiter=0.7f;
    Vector2 moveVelocity;


    Vector2 mousePos;
    Vector2 offset;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawn;

    bool isShooting=false;
    float bulletSpeed=15f;



    // Start is called before the first frame update
    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        mainCamera=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal=Input.GetAxisRaw("Horizontal");
        moveVertical=Input.GetAxisRaw("Vertical");

        moveVelocity=new Vector2(moveHorizontal,moveVertical)*moveSpeed;

        if(Input.GetMouseButtonDown(0))
        {
            isShooting=true;
        }
    }

    void FixedUpdate() 
    {
        MovePlayer();
        RotatePlayer();

        if(isShooting)
        {
            StartCoroutine(Fire());
        }
        
    }

    void MovePlayer()
    {
        if(moveHorizontal!=0 || moveVertical!=0)
        {
            if(moveHorizontal!=0 && moveVertical!=0)
            {
                moveVelocity=moveVelocity*speedLimiter;
            }
            rb.velocity=moveVelocity;
        }else{
            moveVelocity=new Vector2(0f,0f);
            rb.velocity=moveVelocity;
        }
    }


    void RotatePlayer()
    {
        mousePos=Input.mousePosition;

        Vector3 screenPoint=mainCamera.WorldToScreenPoint(transform.localPosition);

        offset=new Vector2(mousePos.x-screenPoint.x,mousePos.y-screenPoint.y).normalized;

        float angle=Mathf.Atan2(offset.y,offset.x)*Mathf.Rad2Deg;

        transform.rotation=Quaternion.Euler(0f,0f,angle-90f);
    }

    IEnumerator Fire(){
        isShooting=false;
        
        GameObject bullets=Instantiate(bullet,bulletSpawn.transform.position,Quaternion.identity);

        bullets.GetComponent<Rigidbody2D>().velocity=offset*bulletSpeed;

        yield return new WaitForSeconds(3f);

        Destroy(bullets);
    }
}

