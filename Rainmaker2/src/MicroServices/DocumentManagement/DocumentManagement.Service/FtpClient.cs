using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public class FtpClient : IFtpClient
    {
        private string _host;
        private string _user;
        private string _pass;
        private FtpWebRequest _ftpRequest;
        private FtpWebResponse _ftpResponse;
        private Stream _ftpStream;
        private const int BufferSize = 2048;

        /* Construct Object */
        public void Setup(string hostIp, string userName, string password) { _host = hostIp; _user = userName; _pass = password; }

        /* Download File */
        public async Task DownloadAsync(string remoteFile, MemoryStream localFileStream)
        {
            try
            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + remoteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)await _ftpRequest.GetResponseAsync();
                /* Get the FTP Server's Response Stream */
                _ftpStream = _ftpResponse.GetResponseStream();
                /* Open a File Stream to Write the Downloaded File */
                //using var localFileStream = new FileStream(localFile, FileMode.Create);
                /* Buffer for the Downloaded Data */
                var byteBuffer = new byte[BufferSize];
                if (_ftpStream != null)
                {
                    int bytesRead = await _ftpStream.ReadAsync(byteBuffer, 0, BufferSize);
                    /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
                    try
                    {
                        while (bytesRead > 0)
                        {
                            await localFileStream.WriteAsync(byteBuffer, 0, bytesRead);
                            bytesRead = await _ftpStream.ReadAsync(byteBuffer, 0, BufferSize);
                        }
                    }
                    finally
                    {
                        _ftpStream.Close();
                    }
                    localFileStream.Position = 0;
                }
                else
                    throw new DocumentManagementException("Ftp Stream is null");
            }
            finally
            { 
                /* Resource Cleanup */
                if(_ftpResponse!=null) _ftpResponse.Close();
                _ftpRequest = null;
            }

        }


        /* Upload File */
        public async Task UploadAsync(string remoteFile, MemoryStream localFileStream)
        {
            try
            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + remoteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                _ftpStream = _ftpRequest.GetRequestStream();
                /* Open a File Stream to Read the File for Upload */
                //using var localFileStream = new FileStream(localFile, FileMode.Open);
                /* Buffer for the Downloaded Data */
                var byteBuffer = new byte[BufferSize];
                int bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, BufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                while (bytesSent != 0)
                {
                    await _ftpStream.WriteAsync(byteBuffer, 0, bytesSent);
                    bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, BufferSize);
                }
                localFileStream.Position = 0;
            }
            finally
            { 
                /* Resource Cleanup */
                if(_ftpStream!=null) _ftpStream.Close();
                _ftpRequest = null;
            }
        }

    }
}
