using System.Windows.Media.Imaging;

namespace XPressWPF.Shared
{
    public static class User
    {
        public static int ID { get; set; }
        public static string UserName { get; set; }
        public static string UserPassword { get; set; }
        public static string FacebookID { get; set; }
        public static string FacebookFullName { get; set; }
        public static BitmapImage FacebookProfileImageSmall { get; set; }
        public static BitmapImage FacebookProfileImageLarge { get; set; }
        public static int StyleId { get; set; }
    }
}
