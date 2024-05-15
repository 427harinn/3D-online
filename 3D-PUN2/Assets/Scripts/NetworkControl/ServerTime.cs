using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerTime : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// ���ԊǗ��������Ƃ��͂�������擾����B
    /// ���[�J�����Ԃ��Ɗ��ɂ���ăY����炵���B
    /// </summary>
    public float GetServerTime()
    {
        int currentTime = PhotonNetwork.ServerTimestamp;
        return currentTime;
    }
}
