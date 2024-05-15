using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class GameLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerInfo_s playerInfo;

    [SerializeField] GameObject loadingImg;

    [SerializeField] Button gameStartButton;

    [HideInInspector] public ChatManager chatManager;

    [HideInInspector] public InfoPanel infoPanel;

    GameObject myPlayerAvatar;

    private void Awake()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
        //�V�[���ړ��ɕK�v�H
        PhotonNetwork.AutomaticallySyncScene = true;
        loadingImg.SetActive(true);
    }

    private void Start()
    {
        chatManager = GameObject.FindGameObjectWithTag("ChatPanel").GetComponent<ChatManager>();
        infoPanel = GameObject.FindGameObjectWithTag("RoomInfoPanel").GetComponent<InfoPanel>();

        // ���ݒ�̏ꍇ�����_���ȃv���C���[����ݒ肷��
        if (playerInfo.playerName == "")
        {
            playerInfo.playerName = $"�v���C���[{Random.Range(0, 10)}";
        }
        PhotonNetwork.NickName = playerInfo.playerName;
    }

    public void JoinRoom(string roomName)
    {
        loadingImg.SetActive(true);
        //���[���ɎQ������
        bool isSuccess = PhotonNetwork.JoinRoom(roomName);

        if (isSuccess)
        {
            Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{roomName}�F�����ɓ���܂���");
            infoPanel.ShowRoomName(roomName);
            chatManager.SetUserName(playerInfo.playerName);
            chatManager.SetChannel(roomName);
            //�`���b�g���J�n����
            chatManager.ChatStart();
        }
        else
        {
            Debug.Log("�����ɓ���̂Ɏ��s���܂���");
        }

    }

    public void CreateRoom(string roomName, int playerNum)
    {
        loadingImg.SetActive(true);
        // ���[���̎Q���l����ݒ肷��
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = playerNum;
        bool isSuccess = PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);

        if (isSuccess)
        {
            Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>���������܂����B�F{roomName}");
            infoPanel.ShowRoomName(roomName);
            chatManager.SetUserName(playerInfo.playerName);
            chatManager.SetChannel(roomName);
            //�`���b�g���J�n����
            chatManager.ChatStart();
        }
        else
        {
            Debug.Log("����������̂Ɏ��s���܂���");
        }
    }

    void InteractableStartButton(bool isMasterClient)
    {
        //Debug.Log("isMasterClient:"+isMasterClient);
        //startButton.interactable = isMasterClient;
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            gameStartButton.interactable = isMasterClient;
        }

    }

    public void LeftRoom()
    {
        //�������甲����
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>��������ޏo���܂����B");
        Destroy(myPlayerAvatar);
        PhotonNetwork.LeaveRoom();
        chatManager.ClearChatDisplay();

    }

    //�}�X�^�[�N���C�A���g���ύX���ꂽ�Ƃ��ɌĂ΂��R�[���o�b�N
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        InteractableStartButton(PhotonNetwork.IsMasterClient);
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>�}�X�^�[�T�[�o�[�ւ̐ڑ��ɐ������܂����B");
        //���r�[�ɎQ������
        PhotonNetwork.JoinLobby();
        loadingImg.SetActive(false);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        var position = new Vector3(0, 1, 10);
        myPlayerAvatar = PhotonNetwork.Instantiate("PlayerAvatar", position, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>���Ȃ����}�X�^�[�N���C�A���g�ł��B");
            // PhotonNetwork.ServerTimestamp ���g���Č��݂̃^�C���X�^���v���擾
            ExitGames.Client.Photon.Hashtable startTimeProps = new ExitGames.Client.Photon.Hashtable();
            startTimeProps["StartTime"] = PhotonNetwork.ServerTimestamp;

            // ���[���̃J�X�^���v���p�e�B�Ƃ��ĊJ�n������ݒ�
            PhotonNetwork.CurrentRoom.SetCustomProperties(startTimeProps);
            //networkmanager����������
            PhotonNetwork.Instantiate("NetworkController", Vector3.zero, Quaternion.identity);
        }

        infoPanel.InfoPanelSetup();
        infoPanel.ShowPlayerName();
        InteractableStartButton(PhotonNetwork.IsMasterClient);
        loadingImg.SetActive(false);

    }

    // ���v���C���[�����[���֎Q���������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{newPlayer.NickName}���Q�����܂����B");
        infoPanel.ShowPlayerName();
        infoPanel.ShowPlayerNum();
        InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserSubscribed("", newPlayer.NickName);

    }

    // ���v���C���[�����[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{otherPlayer.NickName}���ޏo���܂����B");
        infoPanel.ShowPlayerName();
        infoPanel.ShowPlayerNum();
        InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserUnsubscribed("", otherPlayer.NickName);

    }

    //���[�����X�V���ꂽ�Ƃ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>���[�����X�V����܂����B");
        GetComponent<RoomListManager>().SetRoomList(roomList);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>���r�[�ɎQ�����܂����B");
    }

    public override void OnLeftLobby()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>���r�[���甲���܂����B");
    }
}