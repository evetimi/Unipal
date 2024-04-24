namespace Unipal.Model.LinkResources {
    public class Resource {
        private string _id;
        private string _name;
        private string _description;
        private string _url;

        public string Id => _id;
        public string Name => _name;
        public string Description => _description;
        public string Url {
            get => _url;
            set => _url = value;
        }

        public Resource(string id, string name, string description, string url) {
            _id = id;
            _name = name;
            _description = description;
            _url = url;
        }
    }
}
