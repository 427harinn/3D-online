using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SynVariable : MonoBehaviourPunCallbacks
{
    public static SynVariable instance;
    VariableStorage myStorage;

    private void Awake()
    {
        //���g���d�����Ă��邩�`�F�b�N
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        myStorage = new VariableStorage();
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

    /// <summary>
    /// ��������ϐ����܂Ƃ߂��N���X���擾
    /// </summary>
    public VariableStorage GetSynVariable()
    {
        return myStorage;
    }

    [PunRPC]
    void Rpc(VariableStorage storage)
    {
        myStorage = storage;
        Debug.Log(myStorage.SampleShortInt);
    }

    /// <summary>
    /// �ϐ��𓯊�
    /// </summary>
    public void RpcSendVariable()
    {
        photonView.RPC(nameof(Rpc), RpcTarget.All, myStorage);
    }
}
