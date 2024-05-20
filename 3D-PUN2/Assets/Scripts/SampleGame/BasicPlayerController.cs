using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BasicPlayerController : MonoBehaviour
{
    public Rigidbody player_rg;
    Vector3 movingDirecion;
    Vector3 movingVelocity;
    [SerializeField] float speedMagnification = 10.0f;

    public bool IsStop = false;
    public bool IsCameraReverse = false;

    Vector2 newAngle = Vector2.zero;
    Vector2 lastMousePosition = Vector2.zero;
    public Vector2 rotationSpeed = new Vector2(0.2f, 0.2f);
    public Camera myCamera;

    public void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        movingDirecion = new Vector3(x, 0, z);
        movingDirecion.Normalize();
        movingVelocity = movingDirecion * speedMagnification;
        Vector3 newPos = myCamera.transform.TransformVector(movingVelocity);
        //Debug.Log(tmp);

        //�ړ�
        player_rg.velocity = new Vector3(newPos.x, player_rg.velocity.y, newPos.z);
    }

    public void CameraControll()
    {
        //���_����
        if (Input.GetMouseButtonDown(1))
        {
            newAngle = myCamera.transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            if (IsCameraReverse)
            {
                newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * rotationSpeed.y;
                newAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * rotationSpeed.x;
            }
            else
            {
                newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * rotationSpeed.y;
                newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * rotationSpeed.x;
            }

            myCamera.transform.localEulerAngles = newAngle;
            lastMousePosition = Input.mousePosition;
        }
    }

    bool jumpNow;
    float jumpPower = 10; //調整必要 例850
    private void OnCollisionEnter(Collision other)
    {
        if (jumpNow == true)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                jumpNow = false;
            }
        }
    }

    public void Jump()
    {
        if (jumpNow == true) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player_rg.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            jumpNow = true;
        }
    }
}
