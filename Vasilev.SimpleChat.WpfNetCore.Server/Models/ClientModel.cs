using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Vasilev.SimpleChat.ConsNetCore.Communication.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class ClientModel
    {
        internal Guid Id { get; private set; }

        internal string NickName { get; private set; }

        internal CommunicationModel Communication { get; set; }

        internal ICollection<MessageModel> ChatHistory { get; }




        /// <summary>
        /// CTOR
        /// </summary>
        internal ClientModel(string nickName = "NoName")
        {
            this.Id = new Guid();
            this.NickName = nickName;
            this.ChatHistory = new List<MessageModel>();
        }


    }
}
