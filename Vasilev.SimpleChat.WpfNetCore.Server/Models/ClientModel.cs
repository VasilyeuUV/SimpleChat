using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class ClientModel
    {
        internal Guid Id { get; private set; }

        internal string NickName { get; private set; }

        internal NetworkStream Stream { get; set; }

        internal ICollection<MessageModel> ChatHistory { get; }




        /// <summary>
        /// CTOR
        /// </summary>
        internal ClientModel(/*NetworkStream stream,*/ string nickName = "NoName")
        {
            this.Id = new Guid();
            this.NickName = nickName;
            this.ChatHistory = new List<MessageModel>();
            //this.Stream = stream;
        }


    }
}
