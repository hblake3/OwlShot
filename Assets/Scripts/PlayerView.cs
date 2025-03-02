using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerView : MonoBehaviour
{
    public void Move(Vector3 direction, float speed){
        transform.position += direction * speed * Time.deltaTime;
    }
}
