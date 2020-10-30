using System;
using System.Collections.Generic;

namespace ChatServer.Models
{
    public class UserModel
    {
        public Guid Id { get; private set; }

        public string NickName { get; private set; }

        public ICollection<string> ChatHistory { get; }


        /// <summary>
        /// CTOR
        /// </summary>
        public UserModel(string nickName = "NoName")
        {
            this.Id = new Guid();
            this.NickName = nickName;
            this.ChatHistory = new List<string>();
        }

        
    }
}
