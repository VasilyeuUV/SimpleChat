using System;
using System.Collections.Generic;

namespace ChatServer.Models
{
    internal class UserModel
    {
        internal Guid Id { get; private set; }

        internal string NickName { get; private set; }

        internal ICollection<string> ChatHistory { get; }


        /// <summary>
        /// CTOR
        /// </summary>
        internal UserModel(string nickName = "NoName")
        {
            this.Id = new Guid();
            this.NickName = nickName;
            this.ChatHistory = new List<string>();
        }

        
    }
}
