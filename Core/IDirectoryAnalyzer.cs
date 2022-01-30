namespace Local_C.Core {
    public interface IDirectoryAnalyzer {
        IEnumerable<string> GetSubDirectories(string root);
    }
}
