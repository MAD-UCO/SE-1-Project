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

// File containing messaging classes and functions for the MoveBit application

namespace MoveBitMessaging
{

    /// <summary>
    /// Enum serving as an identifier for what specific type of message
    /// a generic message object should be cast to.
    /// </summary>
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

    /// <summary>
    /// Generic Message object meant to be inherited by all other messages
    /// </summary>
    [Serializable]
    public class Message
    {
        // TODO: Change to MoveBitMessage to eliminate ambiguity with .NET 'Message'
        public MessageIdentifier id;
    }

    /// <summary>
    /// enum for the server to set in the Client Connect Response Message
    /// so the client appplication can react accordingly.
    /// </summary>
    [Serializable]
    public enum serverConnectResponse
    {
        success,
        invalidCredentials,
        serverBusy,
        usernameTaken,
        unknownError,
    }

    /// <summary>
    /// Client Connect Request message:
    /// Message for when the client application is trying to connect to the server
    /// Client --> Server
    /// </summary>
    [Serializable]
    public class ClientConnectRequest : Message
    {
        public string userName;
        public string password;
        // If flag is true, then the user is attempting to
        //      create the account specified
        public bool createAccountFlag;

        public ClientConnectRequest(string userName, string password, bool createAccountFlag = false)
        {
            id = MessageIdentifier.ID_ClientConnectRequest;
            this.userName = userName;
            this.password = password;
            this.createAccountFlag = createAccountFlag;
            
        }
    }

    /// <summary>
    /// Client Connect Response message:
    /// Reply to a ClientConnectRequest Message telling the user if their login attempt 
    /// was successful
    /// Serever --> Client
    /// </summary>
    [Serializable]
    public class ClientConnectResponse : Message
    {
        // Enum for what the server's response is
        public serverConnectResponse response;

        public ClientConnectResponse(serverConnectResponse response)
        {
            id = MessageIdentifier.ID_ClientConnectResponse;
            this.response = response;
        }
    }

    /// <summary>
    /// Test List Active Users Request Message:
    /// Test message for asking for a list of all the active users currently
    /// connected to the server.
    /// Client --> Server
    /// </summary>
    [Serializable]
    public class TestListActiveUsersRequest : Message
    {
        // Message is practically empty, just the instance is enough to
        //  initiate the logic
        public TestListActiveUsersRequest() 
        {
            id = MessageIdentifier.ID_TestListUsersRequest;
        }
    }

    /// <summary>
    /// Test List Active Users Response Message:
    /// A reply to the TestListActiveUsersRequest message
    /// Server tells the client all the active users connected to the server
    /// Server --> Client
    /// </summary>
    [Serializable]
    public class TestListActiveUsersResponse : Message
    {
        // List of all usernames connected to the server at query time
        public List<string> activeUsers;
        public TestListActiveUsersResponse(List<string> activeUsers)
        {
            id = MessageIdentifier.ID_ClientConnectResponse;
            this.activeUsers = activeUsers; 
        }
    }

    /// <summary>
    /// Simple Text Message:
    /// Simple message for sending text
    /// Client --> Server
    /// </summary>
    [Serializable]
    public class SimpleTextMessage : Message
    {
        // User(name) we are sending the message to
        public string recipient;
        // User(name) who sent this message
        public string sender;
        // Text message
        public string message;

        public SimpleTextMessage(string recipient, string sender, string message)
        {
            id = MessageIdentifier.ID_SimpleTextMessage;
            this.recipient = recipient;
            this.sender = sender;
            this.message = message;
        }
    }

    /// <summary>
    /// Enum for the server to specify what the result of a SimpleTextMessage was
    /// </summary>
    [Serializable]
    public enum SendResult
    {
        sendSuccess,
        sendFailure,
    }

    /// <summary>
    /// Simple Text Message Result
    /// Reply to SimpleTextMessage
    /// Server --> Client
    /// </summary>
    [Serializable]
    public class SimpleTextMessageResult : Message
    {
        // Result telling us how our send request went
        public SendResult sendResult;

        public SimpleTextMessageResult(SendResult result)
        {
            id = MessageIdentifier.ID_SimpleTextMessageResult;
            sendResult = result;
        }
    }


    /// <summary>
    /// Inbox List Update Message:
    /// Message containing messages that are in the user's inbox on the server
    /// Server --> Client
    /// </summary>
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


    // TODO the only point of this class is because I can't declare a function inside
    //  the namespace... Find a better alternative.
    public class MessageOperator
    {
        /// <summary>
        /// Function returns a Deserialized message object from a network stream that
        /// is being used to converse between the client and server application.
        /// If the message type is unknown, an Exception is thrown
        /// </summary>
        /// <param name="stream">The network stream being used to converse between the client and 
        /// server</param>
        /// <returns>A deserialized message from the networkStream</returns>
        /// <exception cref="InvalidDataException"></exception>
        public static Message netStreamToMessage(NetworkStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            // TODO: BinaryFormatter.Deserialize() is deprecated... However, using the XML Deserializer was not working
            //  Find a replacement to avoid warning
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
