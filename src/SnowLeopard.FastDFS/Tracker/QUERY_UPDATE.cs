﻿using System;

namespace SnowLeopard.FastDFS
{
    /// <summary>
    /// query which storage server to update the file
    /// 
    /// Reqeust 
    ///     Cmd: TRACKER_PROTO_CMD_SERVICE_QUERY_UPDATE 103
    ///     Body:
    ///     @ FDFS_GROUP_NAME_MAX_LEN bytes:  group name
    ///     @ filename bytes: filename
    /// Response
    ///     Cmd: TRACKER_PROTO_CMD_RESP
    ///     Status: 0 right other wrong
    ///     Body: 
    ///     @ FDFS_GROUP_NAME_MAX_LEN bytes: group name
    ///     @ IP_ADDRESS_SIZE - 1 bytes: storage server ip address
    ///     @ FDFS_PROTO_PKG_LEN_SIZE bytes: storage server port
    /// </summary>
    public class QUERY_UPDATE : FDFSRequest
    {
        private static QUERY_UPDATE _instance = null;
        public static QUERY_UPDATE Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QUERY_UPDATE();
                return _instance;
            }
        }
        private QUERY_UPDATE()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramList">
        /// 1,string groupName
        /// 2,string fileName
        /// </param>
        /// <returns></returns>
        public override FDFSRequest GetRequest(params object[] paramList)
        {
            if (paramList.Length != 2)
                throw new FDFSException("param count is wrong");

            QUERY_UPDATE result = new QUERY_UPDATE();
            string groupName = (string)paramList[0];
            string fileName = (string)paramList[1];
            if (groupName.Length > Consts.FDFS_GROUP_NAME_MAX_LEN)
                throw new FDFSException("GroupName is too long");

            byte[] groupNameBuffer = FDFSUtil.StringToByte(groupName);
            byte[] fileNameBuffer = FDFSUtil.StringToByte(fileName);
            int length = Consts.FDFS_GROUP_NAME_MAX_LEN + fileNameBuffer.Length;
            byte[] body = new byte[length];

            Array.Copy(groupNameBuffer, 0, body, 0, groupNameBuffer.Length);
            Array.Copy(fileNameBuffer, 0, body, Consts.FDFS_GROUP_NAME_MAX_LEN, fileNameBuffer.Length);

            result.Body = body;
            result.Header = new FDFSHeader(length,
                Consts.TRACKER_PROTO_CMD_SERVICE_QUERY_UPDATE, 0);
            return result;
        }

        public class Response
        {
            public string GroupName;
            public string IPStr;
            public int Port;
            public Response(byte[] responseByte)
            {
                byte[] groupNameBuffer = new byte[Consts.FDFS_GROUP_NAME_MAX_LEN];
                Array.Copy(responseByte, groupNameBuffer, Consts.FDFS_GROUP_NAME_MAX_LEN);
                GroupName = FDFSUtil.ByteToString(groupNameBuffer).TrimEnd('\0');
                byte[] ipAddressBuffer = new byte[Consts.IP_ADDRESS_SIZE - 1];
                Array.Copy(responseByte, Consts.FDFS_GROUP_NAME_MAX_LEN, ipAddressBuffer, 0, Consts.IP_ADDRESS_SIZE - 1);
                IPStr = new string(FDFSConfig.Charset.GetChars(ipAddressBuffer)).TrimEnd('\0');
                byte[] portBuffer = new byte[Consts.FDFS_PROTO_PKG_LEN_SIZE];
                Array.Copy(responseByte, Consts.FDFS_GROUP_NAME_MAX_LEN + Consts.IP_ADDRESS_SIZE - 1,
                    portBuffer, 0, Consts.FDFS_PROTO_PKG_LEN_SIZE);
                Port = (int)FDFSUtil.BufferToLong(portBuffer, 0);
            }
        }
    }
}
