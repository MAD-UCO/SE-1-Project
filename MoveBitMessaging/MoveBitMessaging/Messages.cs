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
    public enum messageIdentifier
    {
        ID_ClientConnectRequest,
        ID_ClientConnectResponse,

        ID_UndefinedMessage,
    }

    [Serializable]
    public abstract class Message
    {
    }

    [Serializable]
    public enum serverConnectResponse
    {
        success,
        invalidCredentials,
        serverBusy,
        usernameTaken,
    }

    [Serializable, XmlRoot("ClientRequest")]
    public class ClientConnectRequest : Message
    {
        
        public string userName;
        public string password;
        public bool createAccountFlag;

        public ClientConnectRequest(string userName, string password, bool createAccountFlag = false)
        {
            this.userName = userName;
            this.password = password;
            this.createAccountFlag = createAccountFlag;
        }

        public ClientConnectRequest()
        {
            this.userName = "Default";
            this.password = "Default";
            this.createAccountFlag = false;
        }
    }

    [Serializable]
    public class ClientConnectResponse : Message
    {
        public serverConnectResponse response;
        public int servicerPortNumber;

        public ClientConnectResponse(int servicerPortNumber, serverConnectResponse response)
        {
            this.response = response;
            this.servicerPortNumber = servicerPortNumber;
        }
    }

    [Serializable]
    public class SystemMessage
    {
        public int messageId;
        public Message msg;

        public SystemMessage(Message msg)
        {
            messageId = messageToId(msg);
            this.msg = msg;
        }

        public SystemMessage(int messageIdentifier, Message msg)
        {
            this.messageId = messageIdentifier;
            this.msg = msg;
        }

        private int messageToId(Message msg)
        {
            messageIdentifier id = messageIdentifier.ID_UndefinedMessage;
            if (msg.GetType() == typeof(MoveBitMessaging.ClientConnectRequest))
                id = messageIdentifier.ID_ClientConnectRequest;
            else if (msg.GetType() == typeof(MoveBitMessaging.ClientConnectResponse))
                id = messageIdentifier.ID_ClientConnectResponse;

            return (int)id;
        }


    }
}
