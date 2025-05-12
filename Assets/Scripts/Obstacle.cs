using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    //������Ʈ ���Ʒ��� ����
    //public float holeSizeMin = 1f;  
    //public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject1;
    public Transform bottomObject2;
        

    public float widthPadding = 4f;  //������Ʈ ������ ��

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        //float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        //float halfHoleSize = holeSize / 2;

        //topObject.localPosition = new Vector3(0, halfHoleSize);
        //bottomObject1.localPosition = new Vector3(0, -halfHoleSize);
        //bottomObject2.localPosition = new Vector3(0, -halfHoleSize);
        //bottomObject3.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        //placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;


    }




}
