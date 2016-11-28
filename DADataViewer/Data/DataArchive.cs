using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace DarkAgesLib.Data
{
    public class DataArchive : IDisposable, IEnumerable<DataArchiveEntry>
    {
        #region Fields

        const int EntryNameLength = 13;

        private Stream _archiveStream;
        private BinaryReader _archiveReader;

        private List<DataArchiveEntry> _entries;
        private ReadOnlyCollection<DataArchiveEntry> _entriesCollection;
        private Dictionary<string, DataArchiveEntry> _entriesDictionary;

        private bool disposed;

        #endregion Fields


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public DataArchive(Stream stream)
        {
            Init(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="archiveFileName"></param>
        /// <returns></returns>
        public static DataArchive Open(string archiveFileName)
        {
            FileStream stream = new FileStream(archiveFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            return new DataArchive(stream);
        }

        ~DataArchive()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<DataArchiveEntry> Entries
        {
            get
            {
                ThrowIfDisposed();

                return _entriesCollection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public DataArchiveEntry GetEntry(string entryName)
        {
            DataArchiveEntry result;
            _entriesDictionary.TryGetValue(entryName, out result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_archiveStream != null)
                {
                    _archiveStream.Dispose();
                    _archiveStream = null;
                }

                if (_archiveReader != null)
                {
                    _archiveReader.Dispose();
                    _archiveReader = null;
                }
            }

            disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<DataArchiveEntry> GetEnumerator()
        {
            return ((IEnumerable<DataArchiveEntry>)_entries).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<DataArchiveEntry>)_entries).GetEnumerator();
        }

        #endregion Public/Protected


        #region Private/Internal

        internal Stream ArchiveStream
        {
            get
            {
                return _archiveStream;
            }
        }

        internal BinaryReader ArchiveReader
        {
            get
            {
                return _archiveReader;
            }
        }

        private void Init(Stream stream)
        {
            _archiveStream = stream;

            _archiveReader = new BinaryReader(stream);

            _entries = new List<DataArchiveEntry>();
            _entriesCollection = new ReadOnlyCollection<DataArchiveEntry>(_entries);
            _entriesDictionary = new Dictionary<string, DataArchiveEntry>(StringComparer.CurrentCultureIgnoreCase);

            stream.Seek(0, SeekOrigin.Begin);

            int expectedNumberOfEntries = _archiveReader.ReadInt32() - 1;

            for (int i = 0; i < expectedNumberOfEntries; ++i)
            {
                int startAddress = _archiveReader.ReadInt32();

                byte[] nameBytes = new byte[EntryNameLength];
                _archiveReader.Read(nameBytes, 0, EntryNameLength);

                string name = Encoding.ASCII.GetString(nameBytes);
                int nullChar = name.IndexOf('\0');

                name = name.Substring(0, nullChar);

                int endAddress = _archiveReader.ReadInt32();

                stream.Seek(-4, SeekOrigin.Current);

                var entry = new DataArchiveEntry(this, name, startAddress, endAddress - startAddress);

                _entries.Add(entry);

                _entriesDictionary[name] = entry;
            }
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().ToString());
            }
        }

        #endregion Private/Internal
    }
}
