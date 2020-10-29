using System;

namespace ChatServer.Models
{
    public class UserModel
    {
        public Guid id { get; internal set; }

        public string NickName { get; internal set; }
        
    }
}
