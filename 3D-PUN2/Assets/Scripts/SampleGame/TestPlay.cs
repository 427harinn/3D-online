using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPlay : MonoBehaviour
{
    [SerializeField] bool IsTestPlaying = false;
    [SerializeField] GameObject debugLogCanvas;
    [SerializeField] TMP_Text indexText;
    GameObject myCamera;
    List<GameObject> dammyPlayers;
    ReplacePlayerAvatar replacePlayerAvatar;
    int index = 0; 
    private void Awake()
    {
        if (IsTestPlaying)
        {
            //�Q��
            replacePlayerAvatar = GetComponent<ReplacePlayerAvatar>();

            //�I�u�W�F�N�g���
            myCamera = replacePlayerAvatar.GetCameraObj();
            dammyPlayers = replacePlayerAvatar.GetPlayerAvatarList();
            

            //enabled false
            replacePlayerAvatar.enabled = false;

            //�J�������_�~�[�Ɉړ�
            myCamera.transform.parent = dammyPlayers[index].transform;

            //�f�o�b�O�p�L�����p�X�\��
            debugLogCanvas.SetActive(true);
        }
    }

    //�ق��̃v���C���[���_�ɐ؂�ւ���
    public void ChangeTargetDammyPlayer()
    {
        index++;
        if (index >= dammyPlayers.Count) index = 0;

        myCamera.transform.parent = dammyPlayers[index].transform;
        GetComponent<TestPlayerController>().SetRigidbody(dammyPlayers[index].GetComponent<Rigidbody>());

        //���ݑ��쒆�̃v���C���[�̔ԍ���\��
        indexText.text = index.ToString();
    }
}
