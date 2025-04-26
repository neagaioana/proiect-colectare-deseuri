using OfficeOpenXml;
using colectare_deseuri.Models;

namespace colectare_deseuri.Services
{
    public static class MatrixLoader
    {
        public static int[,] LoadMatrix(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(filePath));
            var ws = package.Workbook.Worksheets["Matrix"];

            int n = ws.Dimension.Rows - 1;
            int[,] matrice = new int[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrice[i, j] = int.TryParse(ws.Cells[i + 2, j + 2].Text, out int val) ? val : 0;

            return matrice;
        }
    }
}
