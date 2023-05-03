namespace GregsTest.Models {
    public static class ModelFactory {
        public static T CreateModel<T>() where T : IModel {
            var result = default(IModel);
            if (typeof(T) == typeof(HomeModel))
                result = new HomeModel();
            return (T)result;
        }
    }
}