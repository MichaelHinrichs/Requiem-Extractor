using System.IO;
using System.IO.Compression;

namespace Requiem_Extractor
{
    class Program
    {
        public static BinaryReader map;
        static void Main(string[] args)
        {
            map = new(File.OpenRead(Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]) + ".MAP"));
            BinaryReader vdk = new(File.OpenRead(Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]) + ".VDK"));
            int fileCount = (int)(map.BaseStream.Length / 136);

            System.Collections.Generic.List<Map> maps = new();
            string path = Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]);
            for (int i = 0; i < fileCount; i++)
            {
                Map map = Map.Read();
                vdk.BaseStream.Position = map.nameStart;
                vdk.ReadByte();
                string name = new string(vdk.ReadChars(128)).TrimEnd('\0');
                int sizeUncompressed = vdk.ReadInt32();
                int sizeCompressed = vdk.ReadInt32();
                int isCompressed = vdk.ReadInt32();
                int unknown = vdk.ReadInt32();


                Directory.CreateDirectory(path + "//" + Path.GetDirectoryName(name));
                FileStream fs = File.Create(path + "//" + name);
                vdk.ReadInt16();
                using (var ds = new DeflateStream(new MemoryStream(vdk.ReadBytes(sizeCompressed - 2)), CompressionMode.Decompress))
                    ds.CopyTo(fs);

                fs.Close();
            }
        }
    }
}
