using DarkAgesLib.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DADataViewer
{
    public class PaletteTable : IEnumerable<PaletteTableEntry>
    {
        private List<PaletteTableEntry> _entries;

        public PaletteTable()
        {
            _entries = new List<PaletteTableEntry>();
        }

        public void AddTable(DataArchiveEntry entry)
        {
            using (var stream = entry.Open())
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    int minimum, second, third;
                    int maximum, palette, misc;

                    if (parts.Length < 2)
                    {
                        continue;
                    }

                    minimum = int.Parse(parts[0]);

                    if (parts.Length == 2)
                    {
                        maximum = minimum;
                        palette = int.Parse(parts[1]);
                        misc = 0;
                    }
                    else if (parts.Length == 3)
                    {
                        second = int.Parse(parts[1]);
                        third = int.Parse(parts[2]);

                        if (third < 0)
                        {
                            maximum = minimum;
                            palette = second;
                            misc = third;
                        }
                        else
                        {
                            maximum = second;
                            palette = third;
                            misc = 0;
                        }
                    }
                    else
                    {
                        maximum = int.Parse(parts[1]);
                        palette = int.Parse(parts[2]);
                        misc = int.Parse(parts[3]);
                    }

                    _entries.Add(new PaletteTableEntry(minimum, maximum, palette, misc));
                }
            }
        }

        public int GetPaletteNumber(int index, int misc = 0)
        {
            foreach (var entry in _entries)
            {
                if (index >= entry.Minimum && index <= entry.Maximum && (misc == entry.Misc || entry.Misc == 0))
                {
                    return entry.PaletteNumber;
                }
            }

            return 0;
        }

        public IEnumerator<PaletteTableEntry> GetEnumerator()
        {
            return ((IEnumerable<PaletteTableEntry>)_entries).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<PaletteTableEntry>)_entries).GetEnumerator();
        }
    }

    public class PaletteTableEntry
    {
        private int _minimum;
        private int _maximum;
        private int _paletteNumber;
        private int _misc;

        public PaletteTableEntry(int minimum, int maximum, int paletteNumber, int misc = 0)
        {
            _minimum = minimum;
            _maximum = maximum;
            _paletteNumber = paletteNumber;
            _misc = misc;
        }

        public int Minimum
        {
            get
            {
                return _minimum;
            }
        }

        public int Maximum
        {
            get
            {
                return _maximum;
            }
        }

        public int PaletteNumber
        {
            get
            {
                return _paletteNumber;
            }
        }

        public int Misc
        {
            get
            {
                return _misc;
            }
        }
    }
}
