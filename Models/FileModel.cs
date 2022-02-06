using Local_C.Core;

namespace Local_C.Models {
    public class FileModel : ServableModelBase {
        public FileModel() : base(ServableType.File) { }

        public string Extension { get; set; }
    }
}
