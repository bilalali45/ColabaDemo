using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
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


        public async Task<bool> ExistsAsync(string remoteFile)
        {
            var request = (FtpWebRequest)WebRequest.Create(_host + "/" + remoteFile);
            request.Credentials = new NetworkCredential(_user, _pass);
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            bool exists = false;
            try
            {
                using (await request.GetResponseAsync());
                exists = true;
            }
            catch (WebException ex)
            {
                var response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    exists = false;
                }
            }
            return exists;
        }

        /* Download File */
        public async Task DownloadAsync(string remoteFile, string localFile)
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
                using var localFileStream = new FileStream(localFile, FileMode.Create);
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
                }
                else
                    throw new DocManagerException("Ftp Stream is null");
            }
            finally
            { 
                /* Resource Cleanup */
                if(_ftpResponse!=null) _ftpResponse.Close();
                _ftpRequest = null;
            }

        }

        public async Task<Stream> DownloadStreamAsync(string remoteFile)
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

                return _ftpStream;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return null;
        }
        /* Download Loan document*/
        public async Task DownloadLoanDocAsync(string remoteFile, string localFile)
        {
            try
            {
                var resourceName = string.Format("RainMaker.Common.Resources.{0}", remoteFile);

                _ftpStream = this.GetType().Assembly.GetManifestResourceStream(resourceName);

                /* Open a File Stream to Write the Downloaded File */
                var localFileStream = new FileStream(localFile, FileMode.Create);

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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    _ftpStream.Close();
                }


                /* Resource Cleanup */
                localFileStream.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }

        /* Upload File */
        public async Task UploadAsync(string remoteFile, string localFile)
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
                using var localFileStream = new FileStream(localFile, FileMode.Open);
                /* Buffer for the Downloaded Data */
                var byteBuffer = new byte[BufferSize];
                int bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, BufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                while (bytesSent != 0)
                {
                    await _ftpStream.WriteAsync(byteBuffer, 0, bytesSent);
                    bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, BufferSize);
                }
            }
            finally
            { 
                /* Resource Cleanup */
                if(_ftpStream!=null) _ftpStream.Close();
                _ftpRequest = null;
            }
        }

        /* Upload File */
        public async Task UploadStringAsync(string remoteFile, Stream localFileStream, bool closeStream = true)
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
                /* Buffer for the Downloaded Data */
                var byteBuffer = new byte[BufferSize];
                int bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, BufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                try
                {
                    while (bytesSent != 0)
                    {
                        await _ftpStream.WriteAsync(byteBuffer, 0, bytesSent);
                        bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, BufferSize);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                if (closeStream)
                    localFileStream.Close();
                _ftpStream.Close();
                _ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        //upload file when FTP connection not establish
        public async Task UploadToXDriveAsync(string remoteFile, Stream localFileStream, string xDrivePath, bool closeStream = true)
        {
            var folderPath = string.Format("{0}/{1}", xDrivePath, remoteFile);

            var uriPath = @folderPath;
            var localPath = new Uri(uriPath).LocalPath;

            var fileStream = File.Create(localPath, (int)localFileStream.Length);

            var bytesInStream = new byte[localFileStream.Length];
            await localFileStream.ReadAsync(bytesInStream, 0, bytesInStream.Length);
            await fileStream.WriteAsync(bytesInStream, 0, bytesInStream.Length);
            if (closeStream)
                localFileStream.Close();
            fileStream.Close();
        }

        /* Delete File */
        public async Task DeleteAsync(string deleteFile)
        {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + deleteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)await _ftpRequest.GetResponseAsync();
                /* Resource Cleanup */
                _ftpResponse.Close();
                _ftpRequest = null;
        }

        /* Rename File */
        public async Task RenameAsync(string currentFileNameAndPath, string newFileName)
        {
            try
            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + currentFileNameAndPath);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                /* Rename the File */
                _ftpRequest.RenameTo = newFileName;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)await _ftpRequest.GetResponseAsync();
                /* Resource Cleanup */
                _ftpResponse.Close();
                _ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }

        /* Create a New Directory on the FTP Server */
        public async Task CreateDirectoryAsync(string newDirectory)
        {
            try
            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + newDirectory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)await _ftpRequest.GetResponseAsync();
                /* Resource Cleanup */
                _ftpResponse.Close();
                _ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        /* Get the Date/Time a File was Created */
        public async Task<string> GetFileCreatedDateTimeAsync(string fileName)
        {
            try
            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + fileName);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)await _ftpRequest.GetResponseAsync();
                /* Establish Return Communication with the FTP Server */
                _ftpStream = _ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                if (_ftpStream != null)
                {
                    var ftpReader = new StreamReader(_ftpStream);
                    /* Store the Raw Response */
                    string fileInfo = null;
                    /* Read the Full Response Stream */
                    try { fileInfo = await ftpReader.ReadToEndAsync(); }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                    /* Resource Cleanup */
                    ftpReader.Close();
                    _ftpStream.Close();
                    _ftpResponse.Close();
                    _ftpRequest = null;
                    /* Return File Created Date Time */
                    return fileInfo;
                }
                Console.WriteLine("Ftp Stream is null");
                return "";
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return "";
        }

        /* Get the Size of a File */
        public async Task<string> GetFileSizeAsync(string fileName)
        {
            try
            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + fileName);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)await _ftpRequest.GetResponseAsync();
                /* Establish Return Communication with the FTP Server */
                _ftpStream = _ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                if (_ftpStream != null)
                {
                    var ftpReader = new StreamReader(_ftpStream);
                    /* Store the Raw Response */
                    string fileInfo = null;
                    /* Read the Full Response Stream */
                    try { while (ftpReader.Peek() != -1) { fileInfo = await ftpReader.ReadToEndAsync(); } }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                    /* Resource Cleanup */
                    ftpReader.Close();
                    _ftpStream.Close();
                    _ftpResponse.Close();
                    _ftpRequest = null;
                    /* Return File Size */
                    return fileInfo;
                }
                Console.WriteLine("Ftp Stream is null");
                return "";
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return "";
        }

        /* List Directory Contents File/Folder Name Only */
        public async Task<string[]> DirectoryListSimpleAsync(string directory)
        {
            try
            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + directory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)await _ftpRequest.GetResponseAsync();
                /* Establish Return Communication with the FTP Server */
                _ftpStream = _ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                if (_ftpStream != null)
                {
                    var ftpReader = new StreamReader(_ftpStream);
                    /* Store the Raw Response */
                    StringBuilder directoryRaw = new StringBuilder();
                    /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                    try { while (ftpReader.Peek() != -1) { directoryRaw.Append(await ftpReader.ReadLineAsync() + "|"); } }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                    /* Resource Cleanup */
                    ftpReader.Close();
                    _ftpStream.Close();
                    _ftpResponse.Close();
                    _ftpRequest = null;
                    /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
                    try
                    {
                        if (directoryRaw.Length > 0)
                        {
                            string[] directoryList = directoryRaw.ToString().Split("|".ToCharArray());
                            return directoryList;
                        }
                        Console.WriteLine("Ftp Stream is null");
                        return new[] { "" };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return new[] { "" };
                    }
                }
                Console.WriteLine("Ftp Stream is null");
                return new[] { "" };
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return new[] { "" };
        }

        /* List Directory Contents in Detail (Name, Size, Created, etc.) */
        public async Task<string[]> DirectoryListDetailedAsync(string directory)
        {
            try
            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + directory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)await _ftpRequest.GetResponseAsync();
                /* Establish Return Communication with the FTP Server */
                _ftpStream = _ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                if (_ftpStream != null)
                {
                    var ftpReader = new StreamReader(_ftpStream);
                    /* Store the Raw Response */
                    StringBuilder directoryRaw = new StringBuilder();
                    /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                    try { while (ftpReader.Peek() != -1) { directoryRaw.Append(await ftpReader.ReadLineAsync() + "|"); } }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                    /* Resource Cleanup */
                    ftpReader.Close();
                    _ftpStream.Close();
                    _ftpResponse.Close();
                    _ftpRequest = null;
                    /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
                    try
                    {
                        if (directoryRaw.Length > 0)
                        {
                            string[] directoryList = directoryRaw.ToString().Split("|".ToCharArray());
                            return directoryList;
                        }
                        Console.WriteLine("Ftp Stream is null");
                        return new[] { "" };
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                }
                Console.WriteLine("Ftp Stream is null");
                return new[] { "" };
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return new[] { "" };
        }
    }
}
