using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace RefineCalc
{
    public static class TableManager
    {
        public static List<Table> tbWp1302 = new List<Table>();
        public static List<Table> tbWp1340 = new List<Table>();
        public static List<Table> tbArm1302 = new List<Table>();
        public static List<Table> tbArm1340 = new List<Table>();

        private static void FileToTable(List<Table> tb, Range range)
        {
            try
            {
                object[,] obj = range.Value2;
                for (int i = 1; i <= range.Rows.Count; i++)
                {
                    tb.Add(new Table()
                    {
                        Level = Convert.ToInt32(obj[i, 1]),
                        StoneNeeds = Convert.ToInt32(obj[i, 2]),
                        EnhanceNeeds = Convert.ToInt32(obj[i, 3]),
                        OrehaNeeds = Convert.ToInt32(obj[i, 4]),
                        HonourNeeds = Convert.ToInt32(obj[i, 5]),
                        GoldNeeds = Convert.ToInt32(obj[i, 6]),
                        BaseProb = Convert.ToDouble(obj[i, 7]),
                        SubProbBig = Convert.ToDouble(obj[i, 8]),
                        SubProbMed = Convert.ToDouble(obj[i, 9]),
                        SubProbSmall = Convert.ToDouble(obj[i, 10]),
                        SubNumBig = Convert.ToInt32(obj[i,11]),
                        SubNumMed = Convert.ToInt32(obj[i, 12]),
                        SubNumSmall = Convert.ToInt32(obj[i, 13]),
                    });
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error from FileToTable()\n" + ex.Message);
            }
        }

        public static void LoadFromFile(string filePath)
        {
            Excel.Application app = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;
            try
            {
                app = new Excel.Application();
                wb = app.Workbooks.Open(filePath);
                ws = wb.Sheets[1];
                app.Visible = false;

                Range range = ws.Range["B2:N20"];
                FileToTable(tbWp1302, range);
                DeleteObject(range);

                range = ws.Range["B21:N39"];
                FileToTable(tbWp1340, range);
                DeleteObject(range);

                DeleteObject(ws);
                ws = wb.Sheets[2];

                range = ws.Range["B2:N20"];
                FileToTable(tbArm1302, range);
                DeleteObject(range);

                range = ws.Range["B21:N39"];
                FileToTable(tbArm1340, range);
                DeleteObject(range);
                DeleteObject(ws);
                wb.Close(true);
                DeleteObject(wb);
                app.Quit();
                DeleteObject(app);

            }
            catch(Exception ex)
            {
                throw new Exception("Error from LoadFromFile()\n" + ex.Message);
            }
        }

        private static void DeleteObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch(Exception ex)
            {
                obj = null;
                throw new Exception("Error from DeleteObject()\n" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
