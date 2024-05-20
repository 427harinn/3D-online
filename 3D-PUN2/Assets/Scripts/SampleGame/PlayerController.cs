using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : BasicPlayerController
{
    PhotonView myPhotonView;

    void Start()
    {
        this.myPhotonView = GetComponent<PhotonView>();
        player_rg = GetComponent<Rigidbody>();
        //Debug.Log("get"+mainCamera.gameObject.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStop) return;

        if (this.myPhotonView.IsMine)
        {
            Jump();
            Move();
            CameraControll();
        }
    }

    public void SetMyCamera(Camera camera)
    {
        myCamera = camera;
    }
}
