namespace FastMail.Web.Files
{
    public static class FileDefaults
    {
        public static int BarcodeWidth => 300;
        public static int BarcodeHeight => 90;
        public static int LabelHeight => 150;
        public static int BarcodeTop => 30;
        public static int QRHeight => 250;
        public static int QRWidth => 250;

        /// <summary>
        /// Path for barcode image file
        /// </summary>
        public static string BarcodeImagePath => "/Files/Barcodes/label_{0}.png";

        /// <summary>
        /// Virtual path for barcode image file
        /// </summary>
        public static string BarcodeImageVirtualPath => "/Files/Barcodes/label_{0}.png";

        /// <summary>
        /// Path for QR code image file
        /// </summary>
        public static string QRCodeImagePath => "/Files/QRcodes/location_{0}.png";

        /// <summary>
        /// Virtual path for QR code image file
        /// </summary>
        public static string QRCodeImageVirtualPath => "/Files/QRcodes/location_{0}.png";
    }

}
