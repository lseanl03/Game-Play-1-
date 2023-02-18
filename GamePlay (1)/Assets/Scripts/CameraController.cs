using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private void Start()
    {
    }
    void Update()
    {
        transform.position = new Vector3(this.player.position.x, this.player.position.y+2f,this.transform.position.z);

    }
}
