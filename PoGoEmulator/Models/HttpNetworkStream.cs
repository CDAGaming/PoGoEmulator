﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulator.Models
{
    public class HttpNetworkStream : IDisposable
    {
        private byte[] _sendBuffer = new byte[0];

        /// <summary>
        /// </summary>
        /// <param name="enc">
        /// default: utf8 
        /// </param>
        public HttpNetworkStream(Encoding enc = null)
        {
            if (enc != null)
                Encode = enc;
        }

        /// <summary>
        /// </summary>
        /// <param name="ns">
        /// network connection 
        /// </param>
        /// <param name="enc">
        /// default: utf8 
        /// </param>
        public HttpNetworkStream(NetworkStream ns, Encoding enc = null) : this(enc)
        {
            NStream = ns;
            Sw = new StreamWriter(NStream);
        }

        public Encoding Encode { get; } = Encoding.UTF8;
        public int Position { get; private set; }
        public byte[] SendBuffer { get { return _sendBuffer; } }
        private NetworkStream NStream { get; }
        private StreamWriter Sw { get; }

        public void Close()
        {
            Dispose();
        }

        public byte[] CopyBuffer(int startIndex)
        {
            int lenght = Math.Abs(startIndex - Position);
            byte[] selected = new byte[lenght];
            Array.Copy(_sendBuffer, startIndex, selected, 0, selected.Length);
            return selected;
        }

        public void Dispose()
        {
            NStream.Close();
            _sendBuffer = null;
        }

        public int Read(byte[] buffer, int offset, int size)
        {
            if (NStream == null) return -1;
            return NStream.Read(buffer, offset, size);
        }

        public byte[] ReadBuffer(int maxLenght = 1024 * 1024)
        {
            byte[] b = new byte[maxLenght];
            int len = Read(b, 0, b.Length);
            Array.Resize(ref b, len);
            return b;
        }

        private void SendHttpResponse()
        {
            if (Position == 0) return;
            NStream.Write(SendBuffer, 0, Position);
            NStream.Close();
        }

        public void Write(StringBuilder sb)
        {
            Write(sb.ToString());
        }

        public void Write(string textData)
        {
            if (string.IsNullOrWhiteSpace(textData)) return;
            Write(Encode.GetBytes(textData.ToCharArray()));
        }

        public void Write(byte[] byteData)
        {
            if (byteData.Length == 0) return;
            Array.Resize(ref _sendBuffer, _sendBuffer.Length + byteData.Length);
            Array.Copy(byteData, 0, _sendBuffer, Position, byteData.Length);
            Position += byteData.Length;
        }

        private HttpStreamResult WriteHttpResponse(ByteString responseBody)
        {
            List<String> headers = new List<string>()
            {
                "HTTP/1.1 200 OK",
                "Cache-Control: no-cache",
                "Content-Type: text/html",
                "Expires: -1",
                "Pragma: no-cache",
                "PoGoEmulator: .NET 4.6.2 ConsoleApplication",
                $"Date: {string.Format(new CultureInfo("en-GB"), "{0:ddd, dd MMM yyyy hh:mm:ss}", DateTime.UtcNow)} GMT",
                $"Content-Length: {responseBody.Length}",
                "\r\n"
            };

            var responseString = string.Join("\r\n", headers);

            this.Write(responseString);
            this.Write(responseBody.ToArray());
            this.SendHttpResponse();
            return new HttpStreamResult();
        }

        public HttpStreamResult WriteProtoResponse(ResponseEnvelope responseToUser)
        {
            return this.WriteHttpResponse(responseToUser.ToByteString());
        }
    }
}