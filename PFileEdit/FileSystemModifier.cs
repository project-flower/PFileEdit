using System;
using System.IO;

namespace PFileEdit
{
    internal static class FileSystemModifier
    {
        #region Internal Methods

        internal static void Update(Elements values)
        {
            FileSystemInfo fileSystemInfo = values.FileSystemInfo;
            UpdateDateTime(fileSystemInfo, values);
            var attributes = fileSystemInfo.Attributes;
            UpdateAttributeFlags(ref attributes, FileAttributes.Archive, values.Archive);
            UpdateAttributeFlags(ref attributes, FileAttributes.Compressed, values.Compressed);
            UpdateAttributeFlags(ref attributes, FileAttributes.Encrypted, values.Encrypted);
            UpdateAttributeFlags(ref attributes, FileAttributes.Hidden, values.Hidden);
            UpdateAttributeFlags(ref attributes, FileAttributes.ReadOnly, values.ReadOnly);
            UpdateAttributeFlags(ref attributes, FileAttributes.System, values.System);
            fileSystemInfo.Attributes = attributes;
        }

        #endregion

        #region Private Methods

        private static void UpdateAttributeFlags(ref FileAttributes prev, FileAttributes attribute, bool flag)

        {
            if (prev.HasFlag(attribute) != flag)
            {
                if (flag)
                {
                    prev |= attribute;
                }
                else
                {
                    prev ^= attribute;
                }
            }
        }

        private static void UpdateDateTime(FileSystemInfo fileSystemInfo, Elements values)
        {
            DateTime dateTime = values.CreationTime;

            if (fileSystemInfo.CreationTime != dateTime)
            {
                fileSystemInfo.CreationTime = dateTime;
            }

            dateTime = values.LastAccessTime;

            if (fileSystemInfo.LastAccessTime != dateTime)
            {
                fileSystemInfo.LastAccessTime = dateTime;
            }

            dateTime = values.LastWriteTime;

            if (fileSystemInfo.LastWriteTime != dateTime)
            {
                fileSystemInfo.LastWriteTime = dateTime;
            }
        }

        #endregion
    }
}
