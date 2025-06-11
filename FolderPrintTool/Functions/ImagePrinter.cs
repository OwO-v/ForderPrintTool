using System;
using System.Drawing;
using System.Drawing.Printing;

namespace ForderPrintTool.Function
{
    internal class ImagePrinter
    {
        private Image _imageToPrint;

        public void PrintImage(string imagePath)
        {
            if (!System.IO.File.Exists(imagePath))
            {
                throw new ArgumentException("파일이 존재하지 않습니다.");
            }

            _imageToPrint = Image.FromFile(imagePath);

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += PrintPage;
            pd.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle printArea = e.MarginBounds;

            if (_imageToPrint.Width > _imageToPrint.Height)
            {
                _imageToPrint.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            try
            {
                e.Graphics.DrawImage(_imageToPrint, printArea);
                e.HasMorePages = false;
            }
            finally
            {
                _imageToPrint?.Dispose();
            }
        }
    }
}
