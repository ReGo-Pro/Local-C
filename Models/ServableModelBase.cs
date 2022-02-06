using Local_C.Core;

namespace Local_C.Models {
    public abstract class ServableModelBase {
        public ServableModelBase(ServableType type) {
            Type = type;
        }

        public ServableType Type { get; private set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
