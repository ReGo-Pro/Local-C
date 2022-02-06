namespace Local_C.Models {
    public class DirectoryModel : ServableModelBase {
        public DirectoryModel() : base(Core.ServableType.Directory) { }

        public IEnumerable<DirectoryModel> SubDirectories { get; set; }

        public IEnumerable<FileModel> Files { get; set; }
    }
}
