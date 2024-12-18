using System.Net.Sockets;

namespace AbxClientApp.Services
{
    public class RequestSender
    {
        public void SendRequest(NetworkStream stream, byte callType, int resendSeq)
        {
            byte[] request = [callType, (byte)resendSeq];
            stream.Write(request, 0, request.Length);
        }
    }
}