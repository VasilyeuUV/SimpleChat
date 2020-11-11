namespace Vasilev.SimpleChat.ConsNetCore.Communication.Interfaces
{
    public interface ITransmitable
    {
        public bool TransmitMessage(string msg);
    }
}
