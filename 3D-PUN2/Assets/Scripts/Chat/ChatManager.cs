using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using TMPro;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;
    ChatLog chatLog;
    Command command;
    string username;
    string channel;
    bool isConnected;
    string currentChat;
    string privateReceiver = "";
    [SerializeField] TMP_InputField chatField;
    [SerializeField] TextMeshProUGUI chatDisplay;
    [SerializeField] Button sendButton;
    [SerializeField] GameObject waitText;

    private void Start()
    {
        chatLog = GetComponent<ChatLog>();
        command = GetComponent<Command>();
    }

    public void SetUserName(string username)
    {
        this.username = username;
    }

    public void SetChannel(string channel)
    {
        this.channel = channel;
    }

    /// <summary>
    /// �`���b�g���J�n����Ƃ��Ɏ��s����
    /// </summary>
    /// <param name="sceneName">�ړ�����V�[���̖��O</param>
    public void ChatStart()
    {
        waitText.SetActive(true);
        sendButton.interactable = false;
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "ASIA";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>�`���b�g�T�[�o�[�ɐڑ����Ă��܂�......");
    }

    public void SubToChat()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>�`�����l���ɎQ�����܂����B");
        chatClient.Subscribe(new string[] { channel});
        //�������}�X�^�[�N���C�A���g�Ȃ�
        if (PhotonNetwork.IsMasterClient)
        {
            //�J�X�^���v���p�e�B��ݒ�
            chatLog.SetupProperty();
        }
        //�}�X�^�[�N���C�A���g�ł͂Ȃ�
        else
        {
            //�v���p�e�B����ߋ��̃`���b�g���O���擾
            chatDisplay.text = chatLog.GetChatlog();
        }
    }

    public void TypePublicChatOnValueChange(string ChatIn)
    {
        currentChat = ChatIn;
    }

    public void SubmitPublicChat()
    {
        if (privateReceiver == "")
        {
            chatClient.PublishMessage(channel, currentChat);
            command.OnChatSubmitted(currentChat);
            chatField.text = "";
            currentChat = "";
            Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>�S���Ƀ��b�Z�[�W�𑗂�܂����B");
        }
    }

    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }

    public void SubmitPrivateChat()
    {
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
            Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{privateReceiver}�Ƀ��b�Z�[�W�𑗂�܂����B");
        }
    }

    public void SendSystemLog(string log)
    {
        chatClient.PublishMessage(channel, log);
        chatField.text = "";
        currentChat = "";
    }

    public string GetChatDisplay()
    {
        return chatDisplay.text;
    }

    private void Update()
    {
        if (!isConnected)
        {
            return;
        }

        chatClient.Service();
    }

    public void ClearChatDisplay()
    {
        chatDisplay.text = "";
        //�`�����l������ړ�
        chatClient.Unsubscribe(new string[] { channel });
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>�`���b�g�T�[�o�[�ɐڑ����܂����B");
        sendButton.interactable = true;
        waitText.SetActive(false);
        SubToChat();
    }

    public void OnDisconnected()
    {
        
    }

    //���b�Z�[�W���擾�����Ƃ�
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>���b�Z�[�W����M���܂����B");
        for (int i = 0; i < senders.Length; i++)
        {
            if (messages[i].ToString().Contains("/"))
            {
                command.OnCommandReceived(messages[i].ToString());
            }
            chatDisplay.text += $"{senders[i]}:\n{messages[i]}\n";

            //�������}�X�^�[�N���C�A���g�Ȃ�o�b�t�@�Ƀ��b�Z�[�W��ۊǂ��Ă���
            if (PhotonNetwork.IsMasterClient)
            {
                chatLog.AddChatTextBuffer($"{senders[i]}:\n{messages[i]}\n");
            }
        }
        
    }

    //�v���C�x�[�g���b�Z�[�W���擾�����Ƃ�
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>�v���C�x�[�g���b�Z�[�W����M���܂����B");
        chatDisplay.text += $"<color=#{0xFF0000FF:X}>{sender}</color>:\n{message}\n";
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    //�������`�����l���ɎQ�������Ƃ�
    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {

    }
    //���̃��[�U�[�����[���ɎQ�������Ƃ��i�{���̓`���b�g�`�����l���ɎQ�������Ƃ��j
    public void OnUserSubscribed(string channel, string user)
    {
        //�Ȃ����R�[���o�b�N����Ȃ�
        //gameLauncher������s�����Ă���
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{user} ���Q�����܂����B"); 
        chatDisplay.text += $"<color=#{0x2A48F5FF:X}>�y�V�X�e���z</color><color=#{0x13FC03FF:X}>{user}</color><color=#{0x2A48F5FF:X}> ���񂪎Q�����܂����B\n</color>";
        //�������}�X�^�[�N���C�A���g�Ȃ烋�[���v���p�e�B���X�V����
        if (PhotonNetwork.IsMasterClient)
        {
            chatLog.AddChatTextBuffer($"<color=#{0x2A48F5FF:X}>�y�V�X�e���z</color><color=#{0x13FC03FF:X}>{user}</color><color=#{0x2A48F5FF:X}> ���񂪎Q�����܂����B\n</color>");
            chatLog.SetChatlog();
        }
    }
    //���̃��[�U�[�����[������ޏo�����Ƃ��i�{���̓`���b�g�`�����l������ޏo�����Ƃ��j
    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log($"<color=#{0x42F2F5FF:X}>�yNetworkInfo�z</color>{user} ���ޏo���܂����B");
        chatDisplay.text += $"<color=#{0x2A48F5FF:X}>�y�V�X�e���z</color><color=#{0x13FC03FF:X}>{user}</color><color=#{0x2A48F5FF:X}> ���񂪑ޏo���܂����B\n</color>";
    }

}