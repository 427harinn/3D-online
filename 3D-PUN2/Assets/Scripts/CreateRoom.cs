using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] TMP_InputField roomInputField;
    [SerializeField] TMP_InputField playerNumInputField;
    [SerializeField] GameLauncher gameLauncher;
    int num = 2;
    public void OnRoomCreated()
    {
        gameLauncher.CreateRoom(roomInputField.text, num);
    }

    public void CheckFormat(string textIn)
    {
        //�Q���l�������������l���m�F
        bool result = int.TryParse(playerNumInputField.text, out num);
        if (!result || num < 2)
        {
            ConfirmWindow_s.SetWindow("ConWinDatas/ConWinData8");
        }
    }

    public void CheckExistRoom(string input)
    {
        //���ɓ������O�̕��������݂��Ȃ����m�F
        if (GetComponent<RoomListManager>().GetRoomList(input) != null)
        {
            ConfirmWindow_s.SetWindow("ConWinDatas/ConWinData9");
        }
    }
}
