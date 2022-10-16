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
    /// Generic Message class meant to be inherited by all other messages
    /// </summary>
    [Serializable]
    public class MoveBitMessage
    {
        // TODO
        //  Add user name
        //  Add session ID
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
        public byte[] password;
        // If flag is true, then the user is attempting to
        //      create the account specified
        public bool createAccountFlag;

        public ClientConnectRequest(string userName, byte[] password, bool createAccountFlag = false)
        {
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
        public byte[] assignedSessionID;

        public ClientConnectResponse(serverConnectResponse response, byte[]? assignedSessionID = null)
        {
            this.response = response;
            this.assignedSessionID = assignedSessionID;
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
            sendResult = result;
        }
    }


    [Serializable]
    public class ServerToClientLogoffCommand : MoveBitMessage
    {
        public ServerToClientLogoffCommand()
        {
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
            this.messages = messages;
        }
    }

    /// <summary>
    /// Class for sending another user a media message
    /// Contains fields for SMIL/XML string data as well as binary
    /// file data
    /// Client --> Server
    /// </summary>
    [Serializable]
    public class MediaMessage   : MoveBitMessage
    {
        public string senderName;
        public string recipientName;
        public string smilData;
        public Dictionary<string, byte[]> mediaFileData;
        public MediaMessage(string senderName, string recipientName, string smilData, Dictionary<string, byte[]> mediaFileData = null)
        {
            this.senderName = senderName;
            this.recipientName = recipientName;
            this.smilData = smilData;
            this.mediaFileData = mediaFileData;
        }

        public void AddNecessaryFile(string fileName, byte[] fileData)
        {
            // TODO: may need to make this data structure more robust in order to 
            //  handle instances where two different files have the same name
            mediaFileData[fileName] = fileData;
        }
    }

    /// <summary>
    /// Class meant to serve as a response from the server 
    /// on if our media message was sent successfully
    /// Server --> Client
    /// </summary>
    [Serializable]
    public class MediaMessageResponse : MoveBitMessage
    {
        public SendResult result;
        
        public MediaMessageResponse(SendResult result)
        {
            this.result =   result;
        }
    }


    public class MessageManager
    {

#if SIMULATE_LAG

        private static int minLagMilliseconds = 0;
        private static int maxLagMilliseconds = 1500;
        private static Random randLag = new Random();

#endif

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
        public static MoveBitMessage GetMessageFromStream(NetworkStream stream)
        {
#if SIMULATE_LAG
            Thread.Sleep(randLag.Next(minLagMilliseconds, maxLagMilliseconds));
            MoveBitMessage message = (MoveBitMessage)binaryFormatter.Deserialize(stream);
            Thread.Sleep(randLag.Next(minLagMilliseconds, maxLagMilliseconds));
#else
            MoveBitMessage message = (MoveBitMessage)binaryFormatter.Deserialize(stream);
#endif
            stream.FlushAsync();
            return message;
        }
    

        /// <summary>
        /// Function for writing a given message to the network.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="netStream"></param>
        public static void WriteMessageToNetStream(MoveBitMessage message, NetworkStream netStream)
        {
            // Though this function is extremly short, the static formatter helps reduce clutter in other areas
#if SIMULATE_LAG
            Thread.Sleep(randLag.Next(minLagMilliseconds, maxLagMilliseconds));
            binaryFormatter.Serialize(netStream, message);
            Thread.Sleep(randLag.Next(minLagMilliseconds, maxLagMilliseconds));
#else
            binaryFormatter.Serialize(netStream, message);
#endif
            netStream.FlushAsync();
        }

        /// <summary>
        /// Function that writes a given message and then returns a new message
        /// from the stream. Useful for serial back and forth messaging when an immediate
        /// response is required
        /// </summary>
        /// <param name="message"></param>
        /// <param name="netStream"></param>
        /// <returns></returns>
        public static MoveBitMessage WriteAndRecieveMessage(MoveBitMessage message, NetworkStream netStream)
        {
            WriteMessageToNetStream(message,netStream);
            return GetMessageFromStream(netStream);
        }
    }

}
