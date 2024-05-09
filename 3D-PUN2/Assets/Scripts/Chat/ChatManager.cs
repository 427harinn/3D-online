using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using TMPro;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    ChatClient chatClient;
    string username;
    string channel;
    bool isConnected;
    string currentChat;
    string privateReceiver = "";
    [SerializeField] TMP_InputField chatField;
    [SerializeField] TextMeshProUGUI chatDesplay;
    [SerializeField] Button sendButton;
    [SerializeField] GameObject waitText;

    private void OnEnable()
    {
        waitText.SetActive(true);
        ChatStart();
    }

    public void SetUserName(string username)
    {
        this.username = username;
    }

    public void SetChannel(string channel)
    {
        this.channel = channel;
    }

    public void ChatStart()
    {
        isConnected = true;
        sendButton.interactable = false;
        chatClient = new ChatClient(this);
        //chatClient.ChatRegion = "jp";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new Photon.Chat.AuthenticationValues(username));
        Debug.Log("Connectiong");
    }

    public void SubToChat()
    {
        Debug.Log("�`�����l���ɎQ�����܂���");
        chatClient.Subscribe(new string[] { channel});
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
            chatField.text = "";
            currentChat = "";
            Debug.Log("�S���Ƀ��b�Z�[�W�𑗂�܂���");
        }
    }

    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }

    public void SumitPrivateChat()
    {
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
            Debug.Log(privateReceiver + "�Ƀ��b�Z�[�W�𑗂�܂���");
        }
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
        chatDesplay.text = "";
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
        Debug.Log("Connected");
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
        Debug.Log("���b�Z�[�W����M���܂���");
        for (int i = 0; i < senders.Length; i++)
        {
            chatDesplay.text += $"{senders[i]}: {messages[i]}\n";
        }
    }

    //�v���C�x�[�g���b�Z�[�W���擾�����Ƃ�
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log("�v���C�x�[�g���b�Z�[�W����M���܂���");
        chatDesplay.text += $"<color=#{0xFF0000FF:X}>{sender}</color>: {message}\n";
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        
    }

    public void OnUnsubscribed(string[] channels)
    {

    }
    //���̃��[�U�[���Q�������Ƃ�
    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log("�Ȃ������s����Ȃ�");
        chatDesplay.text += $"<color=#{0x13FC03FF:X}>{user}</color> ���Q�����܂����B\n";
    }
    //���̃��[�U�[���މ���Ƃ�
    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log(user + "���ޏo���܂����B");
        chatDesplay.text += $"<color=#{0x13FC03FF:X}>{user}</color> ���ޏo���܂����B\n";
    }

}