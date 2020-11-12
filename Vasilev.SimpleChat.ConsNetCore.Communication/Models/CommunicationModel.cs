using System;
using System.Net.Sockets;
using System.Text;
using Vasilev.SimpleChat.ConsNetCore.Communication.Interfaces;

namespace Vasilev.SimpleChat.ConsNetCore.Communication.Models
{
    public class CommunicationModel : ITransmitable, IReceiveable
    {
        private TcpClient _client = null;


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="client"></param>
        public CommunicationModel(TcpClient client)
        {
            this._client = client;
        }


        /// <summary>
        /// Get message
        /// </summary>
        /// <param name="msg"></param>
        public string ReceiveMessage()
        {
            NetworkStream stream = null;
            StringBuilder response = new StringBuilder();
            try
            {
                stream = _client?.GetStream();
                if (stream.CanRead)
                {
                    byte[] bytes = new byte[256];

                    int bytesLength = 0;
                    while ((bytesLength = stream.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        response.Append(Encoding.UTF8.GetString(bytes, 0, bytesLength));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Клиент отключился");
                //return string.Empty;
            }
            finally
            {
                //stream?.Close();
            }

            return response.ToString();
        }

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="msg"></param>
        public bool TransmitMessage(string msg)
        {
            NetworkStream stream = null;
            try
            {
                stream = _client?.GetStream();
                if (stream.CanWrite)
                {
                    try
                    {
                        byte[] data = Encoding.UTF8.GetBytes(msg?.ToString());      // преобразуем сообщение в массив байтов              
                        stream.Write(data, 0, data.Length);                        // отправка сообщения
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Close Communication
        /// </summary>
        public void CloseCommunication()
        {            
            _client.Close();
        }

        /// <summary>
        /// Get client stream
        /// </summary>
        /// <returns></returns>
        public NetworkStream GetStream()
        {
            return _client.GetStream();
        }
    }
}

