using TMPro;
using UnityEngine;

public class PlayerAvatarView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;

    private void Start()
    {
        
    }
    // �v���C���[�����e�L�X�g�ɐݒ肷��
    public void SetNickName(string nickName)
    {
        nameLabel.text = nickName;
    }
}