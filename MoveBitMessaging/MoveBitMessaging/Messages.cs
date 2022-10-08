using MoveBitMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoveBitMessaging
{

    [Serializable]
    public enum MessageIdentifier
    {
        ID_ClientConnectRequest,
        ID_ClientConnectResponse,
        ID_TestListUsersRequest,
        ID_TestListUsersResponse,
        ID_SimpleTextMessage,
        ID_SimpleTextMessageResult,
        ID_InboxListUpdate,

        ID_UndefinedMessage,
    }

    [Serializable]
    public class Message
    {
        public MessageIdentifier id;
    }

    [Serializable]
    public enum serverConnectResponse
    {
        success,
        invalidCredentials,
        serverBusy,
        usernameTaken,
        unknownError,
    }

    [Serializable, XmlRoot("ClientRequest")]
    public class ClientConnectRequest : Message
    {
        public string userName;
        public string password;
        public bool createAccountFlag;

        public ClientConnectRequest(string userName, string password, bool createAccountFlag = false)
        {
            id = MessageIdentifier.ID_ClientConnectRequest;
            this.userName = userName;
            this.password = password;
            this.createAccountFlag = createAccountFlag;
            
        }
    }

    [Serializable]
    public class ClientConnectResponse : Message
    {
        public serverConnectResponse response;

        public ClientConnectResponse(serverConnectResponse response)
        {
            id = MessageIdentifier.ID_ClientConnectResponse;
            this.response = response;
        }
    }

    [Serializable]
    public class TestListActiveUsersRequest : Message
    {
        public TestListActiveUsersRequest() 
        {
            id = MessageIdentifier.ID_TestListUsersRequest;
        }
    }

    [Serializable]
    public class TestListActiveUsersResponse : Message
    {
        public List<string> activeUsers;
        public TestListActiveUsersResponse(List<string> activeUsers)
        {
            id = MessageIdentifier.ID_ClientConnectResponse;
            this.activeUsers = activeUsers; 
        }
    }

    [Serializable]
    public class SimpleTextMessage : Message
    {
        public string recipient;
        public string sender;
        public string message;

        public SimpleTextMessage(string recipient, string sender, string message)
        {
            id = MessageIdentifier.ID_SimpleTextMessage;
            this.recipient = recipient;
            this.sender = sender;
            this.message = message;
        }
    }

    [Serializable]
    public enum SendResult
    {
        sendSuccess,
        sendFailure,
    }

    [Serializable]
    public class SimpleTextMessageResult : Message
    {
        public SendResult sendResult;

        public SimpleTextMessageResult(SendResult result)
        {
            id = MessageIdentifier.ID_SimpleTextMessageResult;
            sendResult = result;
        }
    }


    [Serializable]
    public class InboxListUpdate : Message
    {
        public List<Message> messages;

        public InboxListUpdate(List<Message> messages)
        {
            id = MessageIdentifier.ID_InboxListUpdate;
            this.messages = messages;
        }
    }


    public class MessageOperator
    {
        public static Message netStreamToMessage(NetworkStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            Message msg = (Message)formatter.Deserialize(stream);
            Message deserialzed = null;

            if (msg.id == MessageIdentifier.ID_ClientConnectRequest)
                deserialzed = (ClientConnectRequest)msg;
            else if (msg.id == MessageIdentifier.ID_ClientConnectResponse)
                deserialzed = (ClientConnectResponse)msg;
            else if (msg.id == MessageIdentifier.ID_TestListUsersRequest)
                deserialzed = (TestListActiveUsersRequest)msg;
            else if (msg.id == MessageIdentifier.ID_TestListUsersResponse)
                deserialzed = (TestListActiveUsersResponse)msg;
            else if (msg.id == MessageIdentifier.ID_SimpleTextMessage)
                deserialzed = (SimpleTextMessage)msg;
            else if (msg.id == MessageIdentifier.ID_SimpleTextMessageResult)
                deserialzed = (SimpleTextMessageResult)msg;
            else if (msg.id == MessageIdentifier.ID_InboxListUpdate)
                deserialzed = (InboxListUpdate)msg;
            else
                throw new InvalidDataException($"No handle for message with ID '{msg.id.ToString()}'");


            return deserialzed;
        }
    }

}
