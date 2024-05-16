using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SynVariable : MonoBehaviourPunCallbacks
{
    public static SynVariable instance;
    public VariableStorage MyStorage;

    private void Awake()
    {
        //���g���d�����Ă��邩�`�F�b�N
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        MyStorage = new VariableStorage();
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [PunRPC]
    void Rpc(VariableStorage storage)
    {
        MyStorage = storage;
        Debug.Log(MyStorage.SampleShortInt);
    }

    /// <summary>
    /// �ϐ��𓯊�
    /// </summary>
    public void RpcSendVariable()
    {
        photonView.RPC(nameof(Rpc), RpcTarget.All, MyStorage);
    }
}
