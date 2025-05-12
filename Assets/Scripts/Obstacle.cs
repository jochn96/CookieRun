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
        

    public float widthPadding = 25f;  //������Ʈ ������ ��

    public float totalPadding = 65f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        // X �������θ� �̵� (Y�� ���� Y ����)
        float newX = lastPosition.x + widthPadding + totalPadding;
        float currentY = transform.position.y; // ���� Y �״�� ����

        Vector3 placePosition = new Vector3(newX, currentY, transform.position.z);

        transform.position = placePosition;

        return placePosition;




        //Vector3 placePosition = lastPosition + new Vector3(widthPadding + totalPadding, 0);


        //transform.position = placePosition;

        //return placePosition;


    }
    




}
