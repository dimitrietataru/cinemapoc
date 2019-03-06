namespace CMS.Utils
{
    public static class IntToSizeExtension
    {
        public static int KB(this int kiloBytes)
        {
            return 1024 * kiloBytes;
        }

        public static int MB(this int megaBytes)
        {
            return 1024 * 1024 * megaBytes;
        }

        public static int GB(this int gigaBytes)
        {
            return 1024 * 1024 * 1024 * gigaBytes;
        }
    }
}
