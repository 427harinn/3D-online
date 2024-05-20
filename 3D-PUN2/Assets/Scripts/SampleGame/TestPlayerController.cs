using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : BasicPlayerController
{
    private void Start()
    {
        //myCamera = Camera.main;
        player_rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStop) return;
        Jump();
        Move();
        //CameraControll();
    }

    public void SetIsStop(bool b)
    {
        IsStop = b;
    }
}
