namespace Local_C.Core {
    using System.IO;

    public class DirectoryAnalizer : IDirectoryAnalyzer {
        private readonly DirectoryInfo _dirInfo;
        public DirectoryAnalizer(string root) {
            _dirInfo = new DirectoryInfo(root);
        }

        public IEnumerable<string> GetFiles(string root) {
            return _dirInfo.GetFiles().Select(f => f.Name);
        }

        public IEnumerable<string> GetSubDirectories(string root) {
            return _dirInfo.GetDirectories().Select(d => d.Name).ToList();
        }
    }
}
