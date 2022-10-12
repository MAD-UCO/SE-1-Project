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
        ID_ServerToClientLogoffCommand,


        ID_UndefinedMessage,
    }

    /// <summary>
    /// Generic Message class meant to be inherited by all other messages
    /// </summary>
    [Serializable]
    public class MoveBitMessage
    {
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
        userBanned,
        unknownError,
    }

    /// <summary>
    /// Client Connect Request message:
    /// Message for when the client application is trying to connect to the server
    /// Client --> Server
    /// </summary>
    [Serializable]
    public class ClientConnectRequest : MoveBitMessage
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
    public class ClientConnectResponse : MoveBitMessage
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
    public class TestListActiveUsersRequest : MoveBitMessage
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
    public class TestListActiveUsersResponse : MoveBitMessage
    {
        // List of all usernames connected to the server at query time
        public List<string> activeUsers;
        public TestListActiveUsersResponse(List<string> activeUsers)
        {
            id = MessageIdentifier.ID_TestListUsersResponse;
            this.activeUsers = activeUsers; 
        }
    }

    /// <summary>
    /// Simple Text Message:
    /// Simple message for sending text
    /// Client --> Server
    /// </summary>
    [Serializable]
    public class SimpleTextMessage : MoveBitMessage
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
    public class SimpleTextMessageResult : MoveBitMessage
    {
        // Result telling us how our send request went
        public SendResult sendResult;

        public SimpleTextMessageResult(SendResult result)
        {
            id = MessageIdentifier.ID_SimpleTextMessageResult;
            sendResult = result;
        }
    }


    [Serializable]
    public class ServerToClientLogoffCommand : MoveBitMessage
    {
        public ServerToClientLogoffCommand()
        {
            id = MessageIdentifier.ID_ServerToClientLogoffCommand;
        }
    }


    /// <summary>
    /// Inbox List Update Message:
    /// Message containing messages that are in the user's inbox on the server
    /// Server --> Client
    /// </summary>
    [Serializable]
    public class InboxListUpdate : MoveBitMessage
    {
        public List<MoveBitMessage> messages;

        public InboxListUpdate(List<MoveBitMessage> messages)
        {
            id = MessageIdentifier.ID_InboxListUpdate;
            this.messages = messages;
        }
    }


    public class MessageManager
    {

        // TODO: BinaryFormatter.Deserialize() is deprecated... However, using the XML Deserializer was not working
        //  Find a replacement to avoid warning
        private static IFormatter binaryFormatter = new BinaryFormatter();


        /// <summary>
        /// Function returns a Deserialized message object from a network stream that
        /// is being used to converse between the client and server application.
        /// </summary>
        /// <param name="stream">The network stream being used to converse between the client and 
        /// server</param>
        /// <returns>A deserialized message from the networkStream</returns>
        public static MoveBitMessage netStreamToMessage(NetworkStream stream)
        {
            return (MoveBitMessage)binaryFormatter.Deserialize(stream);
            stream.Flush();
        }
    

        /// <summary>
        /// Function for writing a given message to the network.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="netStream"></param>
        public static void writeMessageToNetStream(MoveBitMessage message, NetworkStream netStream)
        {
            // Though this function ius extremly short, the static formatter helps reduce clutter in other areas
            binaryFormatter.Serialize(netStream, message);
            netStream.FlushAsync();
        }

        /// <summary>
        /// Function that writes a given message and then returns a new message
        /// from the stream. Useful for serial back and forth messaging
        /// </summary>
        /// <param name="message"></param>
        /// <param name="netStream"></param>
        /// <returns></returns>
        public static MoveBitMessage writeAndRecieveMessage(MoveBitMessage message, NetworkStream netStream)
        {
            writeMessageToNetStream(message,netStream);
            return netStreamToMessage(netStream);
        }
    }

}
