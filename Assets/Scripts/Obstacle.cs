using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 0f;
    public float lowPosY = 0f;

    

    public Transform topObject;
    public Transform bottomObject1;
    public Transform bottomObject2;
        

    public float widthPadding = 25f;  //오브젝트 사이의 폭

    public float totalPadding = 65f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        // X 방향으로만 이동 (Y는 현재 Y 유지)
        float newX = lastPosition.x + widthPadding + totalPadding;
        float currentY = transform.position.y; // 현재 Y 그대로 유지

        Vector3 placePosition = new Vector3(newX, currentY, transform.position.z);

        transform.position = placePosition;

        return placePosition;




        //Vector3 placePosition = lastPosition + new Vector3(widthPadding + totalPadding, 0);


        //transform.position = placePosition;

        //return placePosition;


    }
    




}
