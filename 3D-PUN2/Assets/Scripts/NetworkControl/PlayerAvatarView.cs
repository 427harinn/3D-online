using TMPro;
using UnityEngine;

public class PlayerAvatarView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;

    [SerializeField]
    private Camera myCamera;

    private void Start()
    {
        if (myCamera == null)
        {
            myCamera = Camera.main;
        }
    }
    // �v���C���[�����e�L�X�g�ɐݒ肷��
    public void SetNickName(string nickName)
    {
        nameLabel.text = nickName;
    }

    private void LateUpdate()
    {
        // �v���C���[���̃e�L�X�g���A��ɃJ�����̐��ʌ����ɂ���
        nameLabel.transform.rotation = myCamera.transform.rotation;
    }
}