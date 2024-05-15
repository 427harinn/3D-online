using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SynLoadScene : MonoBehaviourPunCallbacks
{
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
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}
