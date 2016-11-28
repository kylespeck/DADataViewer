using System.IO;

namespace DarkAgesLib.Data
{
    public class DataArchiveEntry
    {
        #region Fields

        private DataArchive _archive;
        private string _entryName;
        private int _address;
        private int _fileSize;

        #endregion Fields


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="archive"></param>
        /// <param name="entryName"></param>
        /// <param name="address"></param>
        /// <param name="fileSize"></param>
        public DataArchiveEntry(DataArchive archive, string entryName, int address, int fileSize)
        {
            this._archive = archive;
            this._entryName = entryName;
            this._address = address;
            this._fileSize = fileSize;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EntryName
        {
            get
            {
                return _entryName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Address
        {
            get
            {
                return _address;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int FileSize
        {
            get
            {
                return _fileSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Stream Open()
        {
            _archive.ArchiveStream.Seek(_address, SeekOrigin.Begin);
            byte[] buffer = _archive.ArchiveReader.ReadBytes(_fileSize);
            return new MemoryStream(buffer);
        }

        #endregion Public/Protected
    }
}
