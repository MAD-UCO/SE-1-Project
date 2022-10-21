using MoveBitMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{
    /// <summary>
    /// A class meant to represent an active connection with a user
    /// A user may be able to log in on multiple devices, so this helps
    /// ensure that all their active connections are handled.
    /// </summary>
    internal class UserConnection
    {
        private TcpClient client;                   // Object representing this users networking client information
        private NetworkStream netStream;            // Object for user's network stream to send and recieve messages from.
        public byte[] sessionID;                    // A unique session ID for this connection
        private Object streamLock = new Object();   // Lock for accessing the network stream 

        public UserConnection(byte[] sessionID, TcpClient client)
        {
            this.client = client;
            this.sessionID = sessionID;
            this.netStream = client.GetStream();
        }

        /// <summary>
        /// Tests if we have an active, living network connection with the client.
        /// </summary>
        /// <returns></returns>
        public bool IsActive()
        {
            return (netStream.CanRead || netStream.CanWrite)
                && !(client.Client.Poll(1000, SelectMode.SelectRead) && client.Client.Available == 0);
        }

        /// <summary>
        /// Ends this connection
        /// </summary>
        public void End()
        {
            lock (streamLock)
            {
                this.netStream.Close();
                this.client.Close();
            }
        }

        /// <summary>
        /// Returns true if this connectoin has information we may read from its network stream
        /// </summary>
        /// <returns></returns>
        public bool DataReady()
        {
            return netStream.DataAvailable && netStream.CanRead;
        }

        /// <summary>
        /// Given a message this method attempts to send the message to the user via this connection.
        /// Returns if it was successful.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TrySendMessage(MoveBitMessage message)
        {
            bool success = false;
            lock (streamLock)
            {


                if (netStream.CanWrite)
                {
                    MessageManager.WriteMessageToNetStream(message, netStream);
                    success = true;
                }
            }
            return success;
        }

        /// <summary>
        /// Retrieves a message from this connection's stream, if one exists 
        /// </summary>
        /// <returns></returns>
        public MoveBitMessage GetMessage()
        {
            MoveBitMessage newMessage;
            lock (streamLock)
                newMessage = MessageManager.GetMessageFromStream(netStream);

            return newMessage;
        }
    }
}
