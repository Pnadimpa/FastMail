using Microsoft.Extensions.FileProviders;

namespace FastMail.Core.Infrastructure
{
    public interface IFastMailFileProvider : IFileProvider
    {
        /// <summary>
        /// Combines an array of strings into a path
        /// </summary>
        /// <param name="paths">An array of parts of the path</param>
        /// <returns>The combined paths</returns>
        string Combine(params string[] paths);

        /// <summary>
                /// Creates all directories and subdirectories in the specified path unless they already exist
                /// </summary>
                /// <param name="path">The directory to create</param>
        void CreateDirectory(string path);

        /// <summary>
                /// Determines whether the specified file exists
                /// </summary>
                /// <param name="filePath">The file to check</param>
                /// <returns>
                /// True if the path contains the name of an existing file; otherwise false
                /// </returns>
        bool FileExists(string filePath);

        /// <summary>
                /// Determines whether the specified file exists
                /// </summary>
                /// <param name="fileUrl">The file url</param>
                /// <returns>
                /// True if the path contains the name of an existing file; otherwise false
                /// </returns>
        public bool FileUrlExists(string fileUrl);

        /// <summary>
                /// Gets a virtual path from a physical disk path
                /// </summary>
                /// <param name="path">The physical disk path</param>
                /// <returns>The virtual path</returns>
        string GetVirtualPath(string path);

        /// <summary>
                /// Checks if the path is directory
                /// </summary>
                /// <param name="path">Path for check</param>
                /// <returns>True, if the path is a directory, otherwise false</returns>
        bool IsDirectory(string path);

        /// <summary>
                /// Determines whether the given path refers to an existing directory on disk
                /// </summary>
                /// <param name="path">The path to test</param>
                /// <returns>True or False</returns>
        bool DirectoryExists(string path);

        /// <summary>
                /// Deletes the specified file
                /// </summary>
                /// <param name="filePath">The name of the file to be deleted</param>
        void DeleteFile(string filePath);

        /// <summary>
                /// Copies an existing file to a new file.  Overwriting a  file of the same name is allowed
                /// </summary>
                /// <param name="sourceFileName">The file to copy</param>
                /// <param name="destFileName">The name of the destination file.  This cannot be a directory</param>
                /// <param name="overwrite">true if the destination file can be overwitten, otherwise, false</param>
        void FileCopy(string sourceFileName, string destFileName, bool overwrite = false);

        /// <summary>
                /// Returns the extension of the spicified path string
                /// </summary>
                /// <param name="filePath">The path string from which to obtain the file name and extension</param>
                /// <returns>The extension of the specified path including the period "."</returns>
        string GetFileExtension(string filePath);

        /// <summary>
                /// Returns the file name and extension of the specified path string
                /// </summary>
                /// <param name="path">The path string from which to obtain the file name and extension</param>
                /// <returns>File name</returns>
        string GetFileName(string path);

        /// <summary>
                /// Returns the names of files that match the specified sear pattern
                /// </summary>
                /// <param name="directoryPath">The path to the directory to search</param>
                /// <param name="searchPattern"></param>
                /// <param name="topDirectoryOnly"></param>
                /// <returns>Array of file names</returns>
        string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true);

        /// <summary>
                /// Returns the absolute path to the directory
                /// </summary>
                /// <param name="paths"></param>
                /// <returns></returns>
        string GetAbsolutePath(params string[] paths);

        /// <summary>
                /// Writes the specified byte array to the file
                /// </summary>
                /// <param name="filePath">The file to write to</param>
                /// <param name="bytes">The bytes to write to the file</param>
        void WriteAllBytes(string filePath, byte[] bytes);
    }

}

