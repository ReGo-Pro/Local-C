namespace Local_C.Core {
    using System.IO;

    public class DirectoryAnalizer : IDirectoryAnalyzer {
        public IEnumerable<string> GetSubDirectories(string root) {
            var directoryInfo = new DirectoryInfo(root);
            return directoryInfo.GetDirectories().Select(d => d.Name).ToList();
        }
    }
}
