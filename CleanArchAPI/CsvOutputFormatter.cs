using Microsoft.AspNetCore.Mvc.Formatters;
using Shared.DataTransferObjects;
using System.Text;

namespace JWTAuthAPI
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(RestaurantDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<RestaurantDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<RestaurantDto>)
            {
                foreach (var restaurant in (IEnumerable<RestaurantDto>)context.Object)
                {
                    FormatCsv(buffer, restaurant);
                }
            }
            else
            {
                FormatCsv(buffer, (RestaurantDto?)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private void FormatCsv(StringBuilder buffer, RestaurantDto? restaurant)
        {
            if (restaurant is not null)
            {
                buffer.AppendLine($"{restaurant.Id},\"{restaurant.Name},\"{restaurant.Location}\"");
            }
        }
    }
}
