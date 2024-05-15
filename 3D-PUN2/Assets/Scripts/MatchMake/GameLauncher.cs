using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class GameLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerInfo_s playerInfo;

    [SerializeField] GameSetting gameSetting;

    [SerializeField] GameObject loadingImg;

    [SerializeField] ChatManager chatManager;

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
        // "Room"�Ƃ������O�̃��[���ɎQ������
        bool isSuccess = PhotonNetwork.JoinRoom(roomName);
        GetComponent<InfoPanel>().ShowRoomName(roomName);
        chatManager.SetUserName(playerInfo.playerName);
        chatManager.SetChannel(roomName);

        if (isSuccess)
        {
            Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{roomName}�F�����ɓ���܂���");
        }
        else
        {
            Debug.Log("�����ɓ���̂Ɏ��s���܂���");
        }

    }

    public void CreateRoom(string roomName, int playerNum)
    {
        loadingImg.SetActive(true);
        // ���[���̎Q���l����4�l�ɐݒ肷��
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = playerNum;
        bool isSuccess = PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        GetComponent<InfoPanel>().ShowRoomName(roomName);
        chatManager.SetUserName(playerInfo.playerName);
        chatManager.SetChannel(roomName);

        if (isSuccess)
        {
            Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>���������܂����B�F{roomName}");
        }
        else
        {
            Debug.Log("����������̂Ɏ��s���܂���");
        }
    }

    public void StartMainGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(gameSetting.SceneName);
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

        GetComponent<InfoPanel>().InfoPanelSetup();
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);
        loadingImg.SetActive(false);

    }

    // ���v���C���[�����[���֎Q���������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{newPlayer.NickName}���Q�����܂����B");
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().ShowPlayerNum();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserSubscribed("", newPlayer.NickName);

    }

    // ���v���C���[�����[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{otherPlayer.NickName}���ޏo���܂����B");
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().ShowPlayerNum();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);

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