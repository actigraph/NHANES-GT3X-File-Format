using System;
using System.IO;

namespace GT3X.Parsing.Library
{
    /// <summary>
    /// Exception that is fired when the file passed in doesn't exists
    /// </summary>
    [Serializable]
    public class FileDoesNotExist : IOException
    {
        public FileDoesNotExist(string message, string filepath = "") :
			base(message + (string.IsNullOrEmpty(filepath) ? "" : " (" + filepath + ")")) { }

        public FileDoesNotExist(string message, Exception innerException, string filepath = "") :
			base(message + (string.IsNullOrEmpty(filepath) ? "" : " (" + filepath + ")"), innerException) { }
    }

    /// <summary>
    /// Exception that is fired when the file passed in isn't a zip file
    /// </summary>
    [Serializable]
    public class FileIsNotAZipFile : IOException
    {
        public FileIsNotAZipFile(string message, string filepath = "") :
			base(message + (string.IsNullOrEmpty(filepath) ? "" : " (" + filepath + ")")) { }

        public FileIsNotAZipFile(string message, Exception innerException, string filepath = "") :
			base(message + (string.IsNullOrEmpty(filepath) ? "" : " (" + filepath + ")"), innerException) { }
    }

    /// <summary>
    /// Exception that is fired when the zip file passed in has missing required files.
    /// </summary>
    [Serializable]
    public class MissingRequiredFiles : IOException
    {
        public MissingRequiredFiles(string message, string filepath = "") :
			base(message + (string.IsNullOrEmpty(filepath) ? "" : " (" + filepath + ")")) { }

        public MissingRequiredFiles(string message, Exception innerException, string filepath = "") :
			base(message + (string.IsNullOrEmpty(filepath) ? "" : " (" + filepath + ")"), innerException) { }
    }

    /// <summary>
    /// Exception that is fired when the info.txt file in the zip file contains invalid data.
    /// </summary>
    [Serializable]
    public class InvalidInfoFile : IOException
    {
        public InvalidInfoFile(string message, string filepath = "") :
			base(message + (string.IsNullOrEmpty(filepath) ? "" : " (" + filepath + ")")) { }

        public InvalidInfoFile(string message, Exception innerException, string filepath = "") :
			base(message + (string.IsNullOrEmpty(filepath) ? "" : " (" + filepath + ")"), innerException) { }
    }
}
