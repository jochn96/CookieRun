using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;

    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    public GameObject[] obstaclePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌체에 적용
    {
        Debug.Log("Triggerd:" + collision.name);

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(collision.transform.position, obstacleCount);
        }

        
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += (widthOfBgObject+ (widthOfBgObject*0.3f)) * numBgCount;            
            collision.transform.position = pos;
            return;
        }
        else if(collision.CompareTag("Ground"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += (widthOfBgObject + (widthOfBgObject * 2.6f)) * numBgCount;
            collision.transform.position = pos;
            return;
        }

        else if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.CompareTag("BackGruond"))
        //{
        //    float widthOfBgObject = ((BoxCollider2D)collision).size.x;
        //    Vector3 pos = collision.transform.position;

        //    pos.x += (widthOfBgObject + (widthOfBgObject * 0.3f)) * numBgCount;
        //    Debug.Log("numBgCount " + numBgCount);
        //    collision.transform.position = pos;
        //    return;
        //}
        

    }

}