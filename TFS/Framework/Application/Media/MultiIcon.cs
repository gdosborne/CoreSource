namespace GregOsborne.Application.Media {
	using System;
	using System.Collections;
	using System.Drawing;
	using System.IO;

	public class MultiIcon : IDisposable {
		public enum DisplayType {
			Largest = 0,
			Smallest = 1
		}

		private readonly IconHeader icoHeader;
		private bool disposing;
		private MemoryStream icoStream;

		public MultiIcon(string filename) {
			this.IconsInfo = new ArrayList();
			if (!this.LoadFile(filename)) {
				return;
			}

			this.icoHeader = new IconHeader(this.icoStream);
#if DEBUG
            Console.WriteLine($"There are {icoHeader.Count} images in this icon file");
#endif

			for (var counter = 0; counter < this.icoHeader.Count; counter++) {
				var entry = new IconEntry(this.icoStream);
				this.IconsInfo.Add(entry);
#if DEBUG
                Console.WriteLine($"This entry has a width of {entry.Width} and a height of {entry.Height}");
#endif
			}
		}

		public int Count => this.IconsInfo.Count;

		public ArrayList IconsInfo { get; }

		public void Dispose() {
			if (this.disposing) {
				return;
			}

			this.disposing = true;
			this.icoStream = null;
			this.icoStream?.Dispose();
			this.disposing = false;
		}

		public Icon BuildIcon(int index) {
			var thisIcon = (IconEntry)this.IconsInfo[index];
			var newIcon = new MemoryStream();
			var writer = new BinaryWriter(newIcon);

			// New Values
			const short newNumber = 1;
			const int newOffset = 22;

			// Write it
			writer.Write(this.icoHeader.Reserved);
			writer.Write(this.icoHeader.Type);
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
			this.icoStream.Position = thisIcon.ImageOffset;
			this.icoStream.Read(tmpBuffer, 0, thisIcon.BytesInRes);
			writer.Write(tmpBuffer);

			// Finish up
			writer.Flush();
			newIcon.Position = 0;
			return new Icon(newIcon, thisIcon.Width, thisIcon.Height);
		}

		public Icon FindIcon(DisplayType searchKey) => this.SearchIcons(searchKey);

		private bool LoadFile(string filename) {
			try {
				using (var icoFile = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
					var icoArray = new byte[icoFile.Length];
					icoFile.Read(icoArray, 0, (int)icoFile.Length);
					this.icoStream = new MemoryStream(icoArray);
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

			foreach (IconEntry thisIcon in this.IconsInfo) {
				var current = (IconEntry)this.IconsInfo[foundIndex];

				if (searchKey == DisplayType.Largest) {
					if (thisIcon.Width > current.Width && thisIcon.Height > current.Height) {
						foundIndex = counter;
					}
#if DEBUG
                    Console.Write("Search for the largest");
#endif
				} else {
					if (thisIcon.Width < current.Width && thisIcon.Height < current.Height) {
						foundIndex = counter;
					}
#if DEBUG
                    Console.Write("Search for the smallest");
#endif
				}

				counter++;
			}

			return this.BuildIcon(foundIndex);
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
					this.Width = icoFile.ReadByte();
					this.Height = icoFile.ReadByte();
					this.ColorCount = icoFile.ReadByte();
					this.Reserved = icoFile.ReadByte();
					this.Planes = icoFile.ReadInt16();
					this.BitCount = icoFile.ReadInt16();
					this.BytesInRes = icoFile.ReadInt32();
					this.ImageOffset = icoFile.ReadInt32();
				}
			}
		}

		private class IconHeader {
			public readonly short Count;
			public readonly short Reserved;
			public readonly short Type;

			public IconHeader(Stream icoStream) {
				using (var icoFile = new BinaryReader(icoStream)) {
					this.Reserved = icoFile.ReadInt16();
					this.Type = icoFile.ReadInt16();
					this.Count = icoFile.ReadInt16();
				}
			}
		}
	}
}