using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 0f;
    public float lowPosY = 0f;

    //오브젝트 위아래의 공간
    //public float holeSizeMin = 1f;  
    //public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject1;
    public Transform bottomObject2;
        

    public float widthPadding = 20f;  //오브젝트 사이의 폭

    public float totalPadding = 60f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    {
        
        

        Vector3 placePosition = lastPosition + new Vector3(widthPadding + totalPadding, 0);
        

        transform.position = placePosition;

        return placePosition;


    }
    //public Vector3 SetRandomPlace(Vector3 lastPosition, int obstaclCount)
    //{
    //    //float holeSize = Random.Range(holeSizeMin, holeSizeMax);
    //    //float halfHoleSize = holeSize / 2;

    //    //topObject.localPosition = new Vector3(0, halfHoleSize);
    //    //bottomObject1.localPosition = new Vector3(0, -halfHoleSize);
    //    //bottomObject2.localPosition = new Vector3(0, -halfHoleSize);
    //    //bottomObject3.localPosition = new Vector3(0, -halfHoleSize);

    //    Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
    //    //placePosition.y = Random.Range(lowPosY, highPosY);

    //    transform.position = placePosition;

    //    return placePosition;


    //}




}
