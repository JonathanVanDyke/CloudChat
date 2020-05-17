using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int maxMessages = 25;
    public string username;
    private GameObject _player;
    private Rigidbody _playerRb;
    public GameObject chatPanel, textObject;
    public InputField chatBox;
    public Color playerMessage, info;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    void Start()
    {
        _player = GameObject.Find("Player");
        _playerRb = _player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        chatBox.interactable = true;
        if (chatBox.text != "")
        {
            
            _playerRb.isKinematic = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(username + ": " +chatBox.text, Message.MessageType.playerMessage);
                chatBox.interactable = false;
                chatBox.text = "";
            }
        }
        else
        {
            _playerRb.isKinematic = false;
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }

            if (chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.DeactivateInputField();
            }
        }



        if(!chatBox.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessageToChat(username + " pooted upwards", Message.MessageType.info);
            }
            
        }

    }

    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if(messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
            Debug.Log("Space");
        }
        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = MessageTypeColor(messageType);

        messageList.Add(newMessage);

    }

    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color = info;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;
        }

        return color;
    }
}




[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
    public MessageType messageType;


    public enum MessageType
    {
        playerMessage,
        info
    }
}
