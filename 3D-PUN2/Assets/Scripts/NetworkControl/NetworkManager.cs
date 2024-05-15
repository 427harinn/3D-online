using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    SynVariableStorage myStorage;

    private void Awake()
    {
        //���g���d�����Ă��邩�`�F�b�N
        CheckInstance();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        myStorage = new SynVariableStorage();
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
    /// �V�[���ړ�����Ƃ��͂�����g���B
    /// �}�X�^�[�N���C�A���g�������s�ł��Ȃ��B
    /// </summary>
    /// <param name="sceneName">�ړ�����V�[���̖��O</param>
    public void LoadScene(string sceneName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel("BattleScene");
        }
    }

    /// <summary>
    /// ���ԊǗ��������Ƃ��͂�������擾����B
    /// ���[�J�����Ԃ��Ɗ��ɂ���ăY����炵���B
    /// </summary>
    public float GetServerTime()
    {
        int currentTime = PhotonNetwork.ServerTimestamp;
        return currentTime;
    }

    /// <summary>
    /// ��������ϐ����܂Ƃ߂��N���X���擾
    /// </summary>
    public SynVariableStorage GetSynVariable() 
    { 
        return myStorage; 
    }

    [PunRPC]
    void Rpc(SynVariableStorage storage)
    {
        myStorage = storage;
        Debug.Log(myStorage.SampleString);
    }

    /// <summary>
    /// �ϐ��𓯊�
    /// </summary>
    public void RpcSendVariable()
    {
        photonView.RPC(nameof(Rpc), RpcTarget.All, myStorage);
    }
}
