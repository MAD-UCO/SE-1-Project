using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{
    /// <summary>
    /// Class meant to represent a user session
    /// A user session is a distinct login from a device through
    /// the client application
    /// </summary>
    internal class UserSession
    {
        private static long expirationTimeSeconds = 300;            // How long after issuing the session is no longer valid (unused)
        private string userName;                                    // The user's username, primary key
        public byte[] sessionID;                                    // The unique session ID for this session
        private long posixTimeIssued;                               // What time the session was issued (unused)

        public UserSession(string userName, byte[] sessionID, DateTime time)
        {
            this.userName = userName;
            this.sessionID = sessionID;
            this.posixTimeIssued = ((DateTimeOffset)time).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Function for setting the expiration time of a session
        /// Unused, but can be used later to implement a level of security
        /// </summary>
        /// <param name="expirationTime"></param>
        public static void SetExpirationTimeSeconds(long expirationTime)
        {
            expirationTimeSeconds = expirationTime;
        }

        /// <summary>
        /// Returns if the session has expired due to ageing.
        /// Unused, but can be used later to implement a level of security.
        /// </summary>
        /// <returns></returns>
        public bool IsExpired()
        {
            long now = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            return (now - posixTimeIssued) >= expirationTimeSeconds;
        }
    }
}
