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
            myCamera.transform.localPosition = new Vector3(0, 2.4f, 0);

            //rigidbody���Z�b�g
            GetComponent<TestPlayerController>().SetRigidbody(dammyPlayers[index].GetComponent<Rigidbody>());

            //���ݑ��쒆�̃v���C���[�̔ԍ���\��
            indexText.text = index.ToString();

            //�f�o�b�O�p�L�����p�X�\��
            debugLogCanvas.SetActive(true);

            //������悤�ɂ���
            GetComponent<TestPlayerController>().SetIsStop(false);
        }
    }

    //�ق��̃v���C���[���_�ɐ؂�ւ���
    public void ChangeTargetDammyPlayer()
    {
        index++;
        if (index >= dammyPlayers.Count) index = 0;

        myCamera.transform.parent = dammyPlayers[index].transform;
        myCamera.transform.localPosition = new Vector3(0, 2.4f, 0);
        GetComponent<TestPlayerController>().SetRigidbody(dammyPlayers[index].GetComponent<Rigidbody>());

        //���ݑ��쒆�̃v���C���[�̔ԍ���\��
        indexText.text = index.ToString();
    }
}
