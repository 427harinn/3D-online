using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class GameLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerInfo_s playerInfo;

    [SerializeField] GameObject myPlayerAvatar;

    [SerializeField] ChatManager chatManager;

    [SerializeField] GameObject loadingImg;
    private void Awake()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
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
            Debug.Log(roomName + "�F�����ɓ���܂���");
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
            Debug.Log(roomName + "�F���������܂���");
        }
        else
        {
            Debug.Log("����������̂Ɏ��s���܂���");
        }
    }

    public void LeftRoom()
    {
        //�������甲����
        Debug.Log("�������甲���܂���");
        Destroy(myPlayerAvatar);
        PhotonNetwork.LeaveRoom();
        chatManager.ClearChatDisplay();

    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        Debug.Log("�}�X�^�[�T�[�o�[�ւ̐ڑ��ɐ������܂����B");
        //���r�[�ɎQ������
        PhotonNetwork.JoinLobby();
        loadingImg.SetActive(false);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        var position = new Vector3(0, 1, 0);
        myPlayerAvatar = PhotonNetwork.Instantiate("PlayerAvatar", position, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("your masterclient");
            // PhotonNetwork.ServerTimestamp ���g���Č��݂̃^�C���X�^���v���擾
            ExitGames.Client.Photon.Hashtable startTimeProps = new ExitGames.Client.Photon.Hashtable();
            startTimeProps["StartTime"] = PhotonNetwork.ServerTimestamp;

            // ���[���̃J�X�^���v���p�e�B�Ƃ��ĊJ�n������ݒ�
            PhotonNetwork.CurrentRoom.SetCustomProperties(startTimeProps);
        }

        GetComponent<InfoPanel>().InfoPanelSetup();
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);
        loadingImg.SetActive(false);

    }

    // ���v���C���[�����[���֎Q���������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName}���Q�����܂���");
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().ShowPlayerNum();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserSubscribed("", newPlayer.NickName);

    }

    // ���v���C���[�����[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName}���ޏo���܂���");
        GetComponent<InfoPanel>().ShowPlayerName();
        GetComponent<InfoPanel>().ShowPlayerNum();
        GetComponent<InfoPanel>().InteractableStartButton(PhotonNetwork.IsMasterClient);

        chatManager.OnUserUnsubscribed("", otherPlayer.NickName);

    }

    //���[�����X�V���ꂽ�Ƃ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("���[�����X�V����܂����B");
        GetComponent<RoomListManager>().SetRoomList(roomList);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("���r�[�ɎQ�����܂����B");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("���r�[���甲���܂����B");
    }
}