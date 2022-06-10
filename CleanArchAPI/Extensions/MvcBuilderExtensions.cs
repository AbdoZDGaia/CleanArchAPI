namespace JWTAuthAPI.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config => config.OutputFormatters.Add(new
             CsvOutputFormatter()));
        }
    }
}
