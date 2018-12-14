namespace Ceilidh.LibCriPak
{
    public sealed class CriPakEntry
    {
        public string FileName { get; }
        public long FileOffset { get; }
        public long FileSize { get; }
        public bool Encrypted { get; }

        public CriPakEntry(string fileName, long fileOffset, long fileSize, bool encrypted)
        {
            FileName = fileName;
            FileOffset = fileOffset;
            FileSize = fileSize;
            Encrypted = encrypted;
        }
    }
}
