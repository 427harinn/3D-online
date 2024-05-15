using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork ���g������
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup ���g������
using ExitGames.Client.Photon;  // SendOptions ���g������

/// <summary>
/// �C�x���g�� raise/fire ���邽�߂̃R���|�[�l���g
/// �C�x���g���N�����ɂ� PhotonNetwork.RaiseEvent ���\�b�h���Ăяo���B
/// �C�x���g���N�����R���|�[�l���g�̓l�b�g���[�N�R���|�[�l���g��I�u�W�F�N�g�ł���K�v�͂Ȃ��B
/// �iMonoBehaviourPunCallbacks ���p��������APhoton View ���A�^�b�`����K�v�͂Ȃ��j
/// </summary>
public class RaiseEvent : MonoBehaviour
{
    /// <summary>�C�x���g�ő��郁�b�Z�[�W</summary>
    [SerializeField] List<string> m_message;

    /// <summary>
    /// �C�x���g���N����
    /// </summary>
    public void Raise()
    {
        //�C�x���g�Ƃ��đ�����̂����
        byte eventCode = 0; // �C�x���g�R�[�h 0~199 �܂Ŏw��ł���B200 �ȏ�̓V�X�e���Ŏg���Ă���̂Ŏg���Ȃ��B
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,  // �S�̂ɑ��� ���� MasterClient, Others ���w��ł���
        };  // �C�x���g�̋N������
        SendOptions sendOptions = new SendOptions(); // �I�v�V���������A���ɉ����w�肵�Ȃ�

        // �C�x���g���N����
        PhotonNetwork.RaiseEvent(eventCode, m_message, raiseEventOptions, sendOptions);
    }
}