using static Requiem_Extractor.Program;

namespace Requiem_Extractor
{
    class Map
    {
        public string name;
        public string container;
        public int sizeUncompressed;
        public int sizeCompressed;
        public int unknown1;
        public int nameStart;

        public static Map Read()
        {
            map.ReadByte();
            return new Map()
            {
                name = new string(map.ReadChars(96)).TrimEnd('\0'),
                container = new string(map.ReadChars(23)).TrimEnd('\0'),
                sizeUncompressed = map.ReadInt32(),
                sizeCompressed = map.ReadInt32(),
                unknown1 = map.ReadInt32(),
                nameStart = map.ReadInt32()
            };
        }
    }
}
