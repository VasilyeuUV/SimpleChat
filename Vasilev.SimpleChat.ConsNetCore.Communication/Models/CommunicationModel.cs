using System;
using System.Net.Sockets;
using System.Text;
using Vasilev.SimpleChat.ConsNetCore.Communication.Interfaces;

namespace Vasilev.SimpleChat.ConsNetCore.Communication.Models
{
    public class CommunicationModel : ITransmitable, IReceiveable
    {
        private NetworkStream _stream = null;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="client"></param>
        public CommunicationModel(TcpClient client)
        {
            this._stream = client.GetStream();
        }



        /// <summary>
        /// Get message
        /// </summary>
        /// <param name="msg"></param>
        public string ReceiveMessage()
        {
            byte[] bytes = new byte[64];                    // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytesLength = 0;
            do
            {
                bytesLength = _stream.Read(bytes, 0, bytes.Length);
                builder.Append(Encoding.Unicode.GetString(bytes, 0, bytesLength));
            }
            while (_stream.DataAvailable);

            return builder.ToString();
        }

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="msg"></param>
        public void TransmitMessage(string msg)
        {
            try
            {                
                byte[] data = Encoding.UTF8.GetBytes(msg?.ToString());      // преобразуем сообщение в массив байтов              
                _stream.Write(data, 0, data.Length);                        // отправка сообщения
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
