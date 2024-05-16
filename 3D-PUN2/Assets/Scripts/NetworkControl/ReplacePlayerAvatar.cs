using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ReplacePlayerAvatar : MonoBehaviour
{
    //���X�g�̏��Ƀv���C���[�P�A�v���C���[�Q�E�E�E�ƒu�������܂��B
    [SerializeField] List<GameObject> PlayerAvatarList;

    [SerializeField] GameObject Camera;
    // Start is called before the first frame update

    private void Start()
    {
        ReplaceMyAvater();
    }

    //�v���C���[�̃l�b�g���[�N�I�u�W�F�N�g�𐶐����_�~�[�A�o�^�[�ƒu��������
    void ReplaceMyAvater()
    {
        GameObject myAvatar = PhotonNetwork.Instantiate("GamePlayerAvatar", Vector3.zero, Quaternion.identity);
        myAvatar.transform.position = PlayerAvatarList[Resources.Load<PlayerInfo_s>("PlayerInfo").playerID].transform.position;

        //�J�������Z�b�g
        myAvatar.GetComponent<PlayerController>().SetMyCamera(Camera.transform.GetChild(0).gameObject.GetComponent<Camera>());
        Camera.transform.parent = myAvatar.transform;
        Camera.transform.localPosition = new Vector3(0, 2.4f, 0);

        //���X�g�̒��g���폜
        foreach (GameObject obj in PlayerAvatarList)
        {
            Destroy(obj);
        }
        PlayerAvatarList.Clear();
    }
}
