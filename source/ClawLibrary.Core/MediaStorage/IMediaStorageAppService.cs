using System;
using System.Collections.Generic;
using System.Text;

namespace ClawLibrary.Core.MediaStorage
{
    public interface IMediaStorageAppService
    {
        string RootPath { get; }
        byte[] GetMedia(string id);
        string SaveMedia(byte[] content);
        void DeleteMedia(string mediaId);
    }
}
