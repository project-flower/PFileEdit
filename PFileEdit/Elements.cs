using System;
using System.ComponentModel;
using System.IO;

namespace PFileEdit
{
    internal class Elements
    {
        #region Public Fields

        public const string CategoryOfAttributes = "Attributes";
        public const string CategoryOfDateTime = "DateTime";
        public readonly FileSystemInfo FileSystemInfo;

        #endregion

        #region Privte Fields

        private readonly Elements Current;

        #endregion

        #region Public Properties

        [Category(CategoryOfAttributes)]
        public bool Archive { get; set; }
        [Category(CategoryOfAttributes)]
        public bool Compressed { get; set; }
        [Category(CategoryOfDateTime)]
        public DateTime CreationTime { get; set; }
        [Category(CategoryOfAttributes)]
        public bool Encrypted { get; set; }
        [Category(CategoryOfAttributes)]
        public bool Hidden { get; set; }
        [Category(CategoryOfDateTime)]
        public DateTime LastAccessTime { get; set; }
        [Category(CategoryOfDateTime)]
        public DateTime LastWriteTime { get; set; }
        [Category(CategoryOfAttributes)]
        public bool ReadOnly { get; set; }
        [Category(CategoryOfAttributes)]
        public bool System { get; set; }

        #endregion

        #region Public Methods

        public Elements(string path = "")
        {
            if (string.IsNullOrEmpty(path)) return;

            if (Directory.Exists(path))
            {
                FileSystemInfo = new DirectoryInfo(path);
            }
            else
            {
                FileSystemInfo = new FileInfo(path);
            }

            CreationTime = FileSystemInfo.CreationTime;
            LastAccessTime = FileSystemInfo.LastAccessTime;
            LastWriteTime = FileSystemInfo.LastWriteTime;

            {
                FileAttributes attributes = FileSystemInfo.Attributes;
                Archive = attributes.HasFlag(FileAttributes.Archive);
                Compressed = attributes.HasFlag(FileAttributes.Compressed);
                Encrypted = attributes.HasFlag(FileAttributes.Encrypted);
                Hidden = attributes.HasFlag(FileAttributes.Hidden);
                ReadOnly = attributes.HasFlag(FileAttributes.ReadOnly);
                System = attributes.HasFlag(FileAttributes.System);
                Current = (MemberwiseClone() as Elements);
            }
        }

        #endregion

        #region Private Methods

        private void ResetArchive()
        {
            if (Current == null) return;

            Archive = Current.Archive;
        }

        private void ResetCompressed()
        {
            if (Current == null) return;

            Compressed = Current.Compressed;
        }

        private void ResetCreationTime()
        {
            if (Current == null) return;

            CreationTime = Current.CreationTime;
        }

        private void ResetEncrypted()
        {
            if (Current == null) return;

            Encrypted = Current.Encrypted;
        }

        private void ResetHidden()
        {
            if (Current == null) return;

            Hidden = Current.Hidden;
        }

        private void ResetLastAccessTime()
        {
            if (Current == null) return;

            LastAccessTime = Current.LastAccessTime;
        }

        private void ResetLastWriteTime()
        {
            if (Current == null) return;

            LastWriteTime = Current.LastWriteTime;
        }

        private void ResetReadOnly()
        {
            if (Current == null) return;

            ReadOnly = Current.ReadOnly;
        }

        private void ResetSystem()
        {
            if (Current == null) return;

            System = Current.System;
        }

        private bool ShouldSerializeArchive()
        {
            return ((Current == null) || (Archive != Current.Archive));
        }

        private bool ShouldSerializeCompressed()
        {
            return ((Current == null) || (Compressed != Current.Compressed));
        }

        private bool ShouldSerializeCreationTime()
        {
            return ((Current == null) || (CreationTime != Current.CreationTime));
        }

        private bool ShouldSerializeEncrypted()
        {
            return ((Current == null) || (Encrypted != Current.Encrypted));
        }

        private bool ShouldSerializeHidden()
        {
            return ((Current == null) || (Hidden != Current.Hidden));
        }

        private bool ShouldSerializeLastAccessTime()
        {
            return ((Current == null) || (LastAccessTime != Current.LastAccessTime));
        }

        private bool ShouldSerializeLastWriteTime()
        {
            return ((Current == null) || (LastWriteTime != Current.LastWriteTime));
        }

        private bool ShouldSerializeReadOnly()
        {
            return ((Current == null) || (ReadOnly != Current.ReadOnly));
        }

        private bool ShouldSerializeSystem()
        {
            return ((Current == null) || (System != Current.System));
        }

        #endregion
    }
}
