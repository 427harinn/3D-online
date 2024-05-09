using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListManager : MonoBehaviour
{
    [SerializeField] Transform RoomParent;

    [SerializeField] GameObject RoomPrefab;

    RoomList getRoomList(string roomName)
    {
        for (int i = 0; i < RoomParent.childCount; i++)
        {
            RoomList tmp = RoomParent.GetChild(i).gameObject.GetComponent<RoomList>();
            if (tmp.GetRoomName() == roomName)
            {
                return tmp.GetComponent<RoomList>();
            }
        }
        return null;
    }
    
    public void SetRoomList(List<RoomInfo> roomList)
    {
        Debug.Log("�ύX���ꂽ���[�����F"+roomList.Count);
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomList tmp = getRoomList(roomList[i].Name);
            if (tmp == null)
            {
                //�V�K�쐬���[���m��
                tmp = Instantiate(RoomPrefab, RoomParent).GetComponent<RoomList>();

                var session = roomList[i];
                //�{�^���ɃC�x���g��ݒ肷��
                tmp.gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GetComponent<GameLauncher>().JoinRoom(session.Name);
                    GetComponent<MatchmakingManager>().RoomJoinButton();
                });

            }

            if (!roomList[i].RemovedFromList)
            {
                Debug.Log($"���[�����e�X�V: {roomList[i].Name}({roomList[i].PlayerCount}/{roomList[i].MaxPlayers})");

                var session = roomList[i];
                Debug.Log(session.Name);
                tmp.SetInfo(session.Name, session.PlayerCount.ToString());
            }
            else
            {
                Debug.Log($"���[���폜: {roomList[i].Name}");
                Destroy(tmp.gameObject);
            }
        }
    }
}
