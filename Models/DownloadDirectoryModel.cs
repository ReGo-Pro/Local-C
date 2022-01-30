namespace Local_C.Models {
    public class DownloadDirectoryModel {
        public string RootDir { get; set; }
        public IEnumerable<string> SubDirectories { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}