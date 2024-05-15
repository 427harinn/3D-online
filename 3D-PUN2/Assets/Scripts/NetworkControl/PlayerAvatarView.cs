using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerAvatarView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;

    private void Start()
    {
        // �v���C���[���ƃv���C���[ID��\������
        SetNickName($"{photonView.Owner.NickName}({photonView.OwnerActorNr})");
    }
    // �v���C���[�����e�L�X�g�ɐݒ肷��
    public void SetNickName(string nickName)
    {
        nameLabel.text = nickName;
    }
}