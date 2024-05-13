/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using System.Drawing.Printing;

namespace OzFramework.Printing {
    public static class Extensions {
        public static PageSettings GetPrinterPageInfo(string printerName, bool isLandscape) {
            PrinterSettings settings;

            // If printer name is not set, look for default printer
            if (printerName.IsNull()) {
                foreach (var printer in PrinterSettings.InstalledPrinters) {
                    settings = new PrinterSettings {
                        PrinterName = printer.ToString()                        
                    };
                    if (settings.IsDefaultPrinter) {
                        var ps = settings.DefaultPageSettings;
                        ps.Landscape = isLandscape;

                        if (isLandscape) {
                            ps.PaperSize = new PaperSize("DefaultLandscape",
                                ps.Bounds.Width, ps.Bounds.Height);
                        }
                        return ps;
                    }
                }

                return null; // <- No default printer  
            }

            // printer by its name 
            settings = new PrinterSettings {
                PrinterName = printerName
            };

            return settings.DefaultPageSettings;
        }

        // Default printer default page info
        public static PageSettings GetPrinterPageInfo(bool isLandscape) {
            return GetPrinterPageInfo(null, isLandscape);
        }
    }
}
