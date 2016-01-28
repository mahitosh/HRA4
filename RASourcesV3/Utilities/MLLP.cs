using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace RiskApps3.Utilities
{
    public class MLLP
    {
        private static byte[] StartBlock = new byte[] { 0x0B };
        private static byte[] EndBlock = new byte[] { 0x1C, 0x0D };
        private static byte[] ACK = new byte[] { 0x0B, 0x06, 0x1C, 0x0D };
        private static byte[] NAK = new byte[] { 0x0B, 0x15, 0x1C, 0x0D };

        private Stream _stream;
        private bool _version3;

        public MLLP(Stream stream, bool version3)
        {
            _stream = stream;
            _version3 = version3;
        }

        public bool Send(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            _stream.Write(StartBlock, 0, StartBlock.Length);
            _stream.Write(bytes, 0, bytes.Length);
            _stream.Write(EndBlock, 0, EndBlock.Length);
            _stream.Flush();
            if (_version3)
            {
                byte[] rsp = new byte[4];
                if (_stream.Read(rsp, 0, 4) != 4)
                    return false;
                return rsp[1] == 0x06;
            }
            return true;
        }

        public string Receive()
        {
            int ib = 0x00;
            MemoryStream ms = new MemoryStream();
            for (; _stream.ReadByte() != 0x0B; ) ;
            while (true)
            {
                if (ib == 0x1C)
                {
                    ib = _stream.ReadByte();
                    if (ib == 0x0D)
                        break;
                    ms.WriteByte(0x1C);
                    ms.WriteByte((byte)ib);
                }
                else
                {
                    ib = _stream.ReadByte();
                    if (ib != 0x1C)
                        ms.WriteByte((byte)ib);
                }
            }
            if (_version3)
            {
                _stream.Write(ACK, 0, ACK.Length);
                _stream.Flush();
            }
            return Encoding.ASCII.GetString(ms.ToArray());
        }
    }

}
