using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace GregOsborne.Application.Media {
    public class MultiIcon : IDisposable {
        public enum DisplayType {
            Largest = 0,
            Smallest = 1
        }

        private readonly IconHeader _icoHeader;
        private bool _disposing;
        private MemoryStream _icoStream;

        public MultiIcon(string filename) {
            IconsInfo = new ArrayList();
            if (!LoadFile(filename)) return;
            _icoHeader = new IconHeader(_icoStream);
#if DEBUG
            Console.WriteLine($"There are {_icoHeader.Count} images in this icon file");
#endif

            for (var counter = 0; counter < _icoHeader.Count; counter++) {
                var entry = new IconEntry(_icoStream);
                IconsInfo.Add(entry);
#if DEBUG
                Console.WriteLine($"This entry has a width of {entry.Width} and a height of {entry.Height}");
#endif
            }
        }

        public int Count => IconsInfo.Count;

        public ArrayList IconsInfo { get; }

        public void Dispose() {
            if (_disposing)
                return;
            _disposing = true;
            _icoStream = null;
            _icoStream?.Dispose();
            _disposing = false;
        }

        public Icon BuildIcon(int index) {
            var thisIcon = (IconEntry) IconsInfo[index];
            var newIcon = new MemoryStream();
            var writer = new BinaryWriter(newIcon);

            // New Values
            const short newNumber = 1;
            const int newOffset = 22;

            // Write it
            writer.Write(_icoHeader.Reserved);
            writer.Write(_icoHeader.Type);
            writer.Write(newNumber);
            writer.Write(thisIcon.Width);
            writer.Write(thisIcon.Height);
            writer.Write(thisIcon.ColorCount);
            writer.Write(thisIcon.Reserved);
            writer.Write(thisIcon.Planes);
            writer.Write(thisIcon.BitCount);
            writer.Write(thisIcon.BytesInRes);
            writer.Write(newOffset);

            // Grab the icon
            var tmpBuffer = new byte[thisIcon.BytesInRes];
            _icoStream.Position = thisIcon.ImageOffset;
            _icoStream.Read(tmpBuffer, 0, thisIcon.BytesInRes);
            writer.Write(tmpBuffer);

            // Finish up
            writer.Flush();
            newIcon.Position = 0;
            return new Icon(newIcon, thisIcon.Width, thisIcon.Height);
        }

        public Icon FindIcon(DisplayType searchKey) {
            return SearchIcons(searchKey);
        }

        private bool LoadFile(string filename) {
            try {
                using (var icoFile = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
                    var icoArray = new byte[icoFile.Length];
                    icoFile.Read(icoArray, 0, (int) icoFile.Length);
                    _icoStream = new MemoryStream(icoArray);
                }
            }
            catch {
                return false;
            }
            return true;
        }

        private Icon SearchIcons(DisplayType searchKey) {
            var foundIndex = 0;
            var counter = 0;

            foreach (IconEntry thisIcon in IconsInfo) {
                var current = (IconEntry) IconsInfo[foundIndex];

                if (searchKey == DisplayType.Largest) {
                    if (thisIcon.Width > current.Width && thisIcon.Height > current.Height)
                        foundIndex = counter;
#if DEBUG
                    Console.Write("Search for the largest");
#endif
                }
                else {
                    if (thisIcon.Width < current.Width && thisIcon.Height < current.Height)
                        foundIndex = counter;
#if DEBUG
                    Console.Write("Search for the smallest");
#endif
                }

                counter++;
            }

            return BuildIcon(foundIndex);
        }

        public class IconEntry {
            public short BitCount;
            public int BytesInRes;
            public byte ColorCount;
            public byte Height;
            public int ImageOffset;
            public short Planes;
            public byte Reserved;
            public byte Width;

            public IconEntry(Stream icoStream) {
                using (var icoFile = new BinaryReader(icoStream)) {
                    Width = icoFile.ReadByte();
                    Height = icoFile.ReadByte();
                    ColorCount = icoFile.ReadByte();
                    Reserved = icoFile.ReadByte();
                    Planes = icoFile.ReadInt16();
                    BitCount = icoFile.ReadInt16();
                    BytesInRes = icoFile.ReadInt32();
                    ImageOffset = icoFile.ReadInt32();
                }
            }
        }

        private class IconHeader {
            public readonly short Count;
            public readonly short Reserved;
            public readonly short Type;

            public IconHeader(Stream icoStream) {
                using (var icoFile = new BinaryReader(icoStream)) {
                    Reserved = icoFile.ReadInt16();
                    Type = icoFile.ReadInt16();
                    Count = icoFile.ReadInt16();
                }
            }
        }
    }
}