/* File="MultiIcon"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Common.Media {
    public class MultiIcon : IDisposable {
        public enum DisplayType {
            Largest = 0,
            Smallest = 1
        }

        private readonly IconHeader icoHeader;
        private bool disposing;
        private MemoryStream icoStream;

        public MultiIcon(string filename) {
            IconsInfo = new ArrayList();
            if (!LoadFile(filename)) {
                return;
            }

            icoHeader = new IconHeader(icoStream);
#if DEBUG
            Debug.WriteLine($"There are {icoHeader.Count} images in this icon file");
#endif

            for (var counter = 0; counter < icoHeader.Count; counter++) {
                var entry = new IconEntry(icoStream);
                IconsInfo.Add(entry);
#if DEBUG
                Debug.WriteLine($"This entry has a width of {entry.Width} and a height of {entry.Height}");
#endif
            }
        }

        public int Count => IconsInfo.Count;

        public ArrayList IconsInfo { get; }

        public void Dispose() {
            if (disposing) {
                return;
            }

            disposing = true;
            icoStream = null;
            icoStream?.Dispose();
            disposing = false;
        }

        public Icon BuildIcon(int index) {
            var thisIcon = (IconEntry)IconsInfo[index];
            var newIcon = new MemoryStream();
            var writer = new BinaryWriter(newIcon);

            // New Values
            const short newNumber = 1;
            const int newOffset = 22;

            // Write it
            writer.Write(icoHeader.Reserved);
            writer.Write(icoHeader.Type);
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
            icoStream.Position = thisIcon.ImageOffset;
            icoStream.Read(tmpBuffer, 0, thisIcon.BytesInRes);
            writer.Write(tmpBuffer);

            // Finish up
            writer.Flush();
            newIcon.Position = 0;
            return new Icon(newIcon, thisIcon.Width, thisIcon.Height);
        }

        public Icon FindIcon(DisplayType searchKey) => SearchIcons(searchKey);

        private bool LoadFile(string filename) {
            try {
                using (var icoFile = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
                    var icoArray = new byte[icoFile.Length];
                    icoFile.Read(icoArray, 0, (int)icoFile.Length);
                    icoStream = new MemoryStream(icoArray);
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
                var current = (IconEntry)IconsInfo[foundIndex];

                if (searchKey == DisplayType.Largest) {
                    if (thisIcon.Width > current.Width && thisIcon.Height > current.Height) {
                        foundIndex = counter;
                    }
#if DEBUG
                    Debug.Write("Search for the largest");
#endif
                }
                else {
                    if (thisIcon.Width < current.Width && thisIcon.Height < current.Height) {
                        foundIndex = counter;
                    }
#if DEBUG
                    Debug.Write("Search for the smallest");
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
