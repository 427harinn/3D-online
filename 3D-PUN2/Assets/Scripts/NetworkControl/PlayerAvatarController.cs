using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerAvatarController : MonoBehaviourPunCallbacks
{
    // �v���C���[���̃l�b�g���[�N�v���p�e�B���`����

    [SerializeField]
    private PlayerAvatarView view;

	bool isStop = false;


    private void Start()
    {
        // �v���C���[���ƃv���C���[ID��\������
        view.SetNickName($"{photonView.Owner.NickName}({photonView.OwnerActorNr})");
    }

	[SerializeField]
	private Rigidbody rigidBody;
	//�@�ړ����x
	private Vector3 velocity;
	//�@���͒l
	private Vector3 input;
	//�@����
	[SerializeField]
	private float walkSpeed = 4f;
	void FixedUpdate()
	{
        if (isStop)
        {
			return;
        }

		if (photonView.IsMine)
		{
			//�@���͒l
			input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//�@�ړ����x�v�Z
			var clampedInput = Vector3.ClampMagnitude(input, 1f);
			velocity = clampedInput * walkSpeed;
			transform.LookAt(rigidBody.position + input);
			//�@�����͂���v�Z�������x���猻�݂�Rigidbody�̑��x������
			velocity = velocity - rigidBody.velocity;
			//�@���x��XZ��-walkSpeed��walkSpeed���Ɏ��߂čĐݒ�
			velocity = new Vector3(Mathf.Clamp(velocity.x, -walkSpeed, walkSpeed), 0f, Mathf.Clamp(velocity.z, -walkSpeed, walkSpeed));
			//�@�ړ�����
			rigidBody.AddForce(rigidBody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);


		}

	}

	public void SetStop(bool b)
    {
		isStop = b;
    }
}
