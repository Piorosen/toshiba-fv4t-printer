using System;
using System.Drawing;
using System.Drawing.Printing;

namespace BarcodePrinter.Print
{
    public class Printer
    {
        private static Printer? _Instance = null;
        public static Printer Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new Printer();
                }
                return _Instance;
            }
        }
        public bool PrintImage(Bitmap image)
        {
#pragma warning disable CA1416 // 플랫폼 호환성 유효성 검사
            PrintDocument document = new PrintDocument();
            document.PrintPage += (s, e) =>
            {
                Console.WriteLine(e.PageSettings.Bounds);
                Console.WriteLine(e.PageSettings.Margins);
                Console.WriteLine(e.PageBounds);
                Console.WriteLine(e.MarginBounds);

                var page = new Rectangle(75, 10, 255, 180);
                var a = image;
                var p = new Bitmap(a, new Size(255, 170));
                e.Graphics?.DrawImage(a, page);
            };
            document.Print();
#pragma warning restore CA1416 // 플랫폼 호환성 유효S성 검사
            return true;
        }



    }
}
