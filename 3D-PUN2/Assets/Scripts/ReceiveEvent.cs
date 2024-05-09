using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;    // IOnEventCallback ���g������
using ExitGames.Client.Photon;  // EventData ���g������
using Photon.Pun;   // PhotonNetwork ���g������

/// <summary>
/// �C�x���g���󂯎��R���|�[�l���g�i�p�^�[�� A�j
/// ����Ă��邱�ƁF
/// 1. MonoBehaviour �̑���� MonoBehaviourPunCallbacks �N���X���p������
/// 2. IOnEventCallback �C���^�[�t�F�C�X���p�����AIOnEventCallback.OnEvent(EventData) ����������
/// 3. �C�x���g�� Raise ������ OnEvent ���\�b�h���Ă΂��̂ŁA�Ă΂ꂽ���̏�������������
/// </summary>
public class ReceiveEvent : MonoBehaviourPunCallbacks, IOnEventCallback
{
    /// <summary>
    /// �C�x���g�� Raise �����ƌĂ΂��
    /// </summary>
    /// <param name="e">�C�x���g�f�[�^</param>
    void IOnEventCallback.OnEvent(EventData e)
    {
        if ((int)e.Code < 200)  // 200 �ȏ�̓V�X�e���Ŏg���Ă���̂ŏ������Ȃ�
        {
            // �C�x���g�Ŏ󂯎�������e�����O�ɏo�͂���
            string message = "OnEvent. EventCode: " + e.Code.ToString() + ", Message: " + e.CustomData.ToString() + ", From: " + e.Sender.ToString();
            Debug.Log(message);
        }
    }
}