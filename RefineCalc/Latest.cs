using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Windows.Forms;

namespace RefineCalc
{
    public class Latest
    {
        public const string FILE_NAME = "latest.json";
        public int EquipGrade { get; set; }
        public int EquipPart { get; set; }
        public int EquipLevel { get; set; }
        public double Prob { get; set; }
        public int WpStone { get; set; }
        public int ArmStone { get; set; }
        public int Enhance { get; set; }
        public int Oreha { get; set; }
        public int HonourLevel { get; set; }
        public int Honour { get; set; }
        public int SubBig { get; set; }
        public int SubMed { get; set; }
        public int SubSmall { get; set; }
        public int WpBook { get; set; }
        public int ArmBook { get; set; }
        public double JangGi { get; set; }
        public bool Research1 { get; set; }
        public bool Research2 { get; set; }
        public bool Details { get; set; }

        public static void WriteFile(Latest obj)
        {
            TextWriter writer = null;

            try
            {
                writer = new StreamWriter(FILE_NAME);
                writer.WriteLine(JsonSerializer.Serialize(obj));
                writer.Close();
            }
            catch
            {
                throw new Exception("Error from WriteFile");
            }
        }

        public static Latest ReadFile()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(FILE_NAME);
                Latest obj = JsonSerializer.Deserialize<Latest>(reader.ReadLine());
                reader.Close();
                return obj;
            }
            catch
            {
                throw new Exception("Error from ReadFile");
            }
        }
    }
}
