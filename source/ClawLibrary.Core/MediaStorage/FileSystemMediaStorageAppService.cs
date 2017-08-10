using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;

namespace ClawLibrary.Core.MediaStorage
{
    public class FileSystemMediaStorageAppService : IMediaStorageAppService
    {
        public string RootPath { get; set; }

        public byte[] GetMedia(string id)
        {

            try
            {
                var folder = GetFolderFromId(id);
                var fileInfo = new FileInfo(Path.Combine(folder, id));
                return File.ReadAllBytes(fileInfo.FullName);
            }
            catch (Exception)
            {
                throw new BusinessException(ErrorCode.FileNotFound, "File was not found!");
            }

        }

        public string SaveMedia(byte[] content)
        {
            var id = Guid.NewGuid().ToString();
            var folder = GetFolderFromId(id);
            var fileInfo = new FileInfo(Path.Combine(folder, id));
            fileInfo.Directory.Create();
            File.WriteAllBytes(fileInfo.FullName, content);
            return id;
        }

        public void DeleteMedia(string mediaId)
        {
            var folder = GetFolderFromId(mediaId);
            var fileInfo = new FileInfo(Path.Combine(folder, mediaId));
            if (fileInfo.Exists)
                fileInfo.Delete();
        }

        private string GetFolderFromId(string id)
        {
            var name = id.Substring(0, 5);
            return Path.Combine(RootPath, name);
        }
    }

}
