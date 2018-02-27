using Catel.Data;

namespace CameraEmulator.UI.Models
{
    public class LoadFilesModel : ModelBase
    {
        public string ItemsFile { get; set; }
        public string FullItemsFile { get; set; }

        public string CaseCode { get; set; }
        public int SleevesPerCase { get; set; } = 1;
        public int ItemsPerSleeve { get; set; } = 2;
    }
}
