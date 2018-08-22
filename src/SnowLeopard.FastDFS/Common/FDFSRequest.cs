using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SnowLeopard.FastDFS
{
    /// <summary>
    /// FDFSRequest
    /// </summary>
    public class FDFSRequest
    {
        public FDFSHeader Header { get; set; }

        public byte[] Body { get; set; }

        public Connection Connection { get; set; }

        public FDFSRequest()
        {

        }

        public byte[] ToByteArray()
        {
            throw new NotImplementedException();
        }

        public virtual FDFSRequest GetRequest(params object[] paramList)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<byte[]> GetResponseAsync()
        {
            if (Connection == null)
                Connection = ConnectionManager.GetTrackerConnection();
            Connection.Open();
            try
            {
                NetworkStream stream = Connection.GetStream();
                byte[] headerBuffer = Header.ToByte();
                await stream.WriteAsync(headerBuffer, 0, headerBuffer.Length);
                await stream.WriteAsync(Body, 0, Body.Length);

                var header = new FDFSHeader(stream);
                if (header.Status != 0)
                    throw new FDFSException(string.Format("Get Response Error,Error Code:{0}", header.Status));

                int count = (int)header.Length;
                byte[] body = new byte[count];
                var offset = 0;
                while (count > 0)
                {
                    var readCount = await stream.ReadAsync(body, offset, count);
                    if (readCount <= 0)
                        throw new FDFSException("Get Response Error,readCount <= 0");

                    offset += readCount;
                    count -= readCount;
                }

                Connection.Close();
                return body;
            }
            catch (Exception ex)
            {
                Connection.Release();
                throw ex;
                //throw ex;//可以看Storage节点的log看
                //22    -〉下载字节数超过文件长度 invalid download file bytes: 10 > file remain bytes: 4
                //      -> 或者 pkg length is not correct
                //2     -〉没有此文件 error info: No such file or directory.
            }
        }

        public virtual byte[] GetResponse()
        {
            return GetResponseAsync().Result;
        }
    }
}