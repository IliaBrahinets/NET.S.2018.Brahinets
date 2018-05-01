using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLParser
{
    public class DefaultURLParser : IURLParser
    {
        public URL Parse(string url)
        {
            string[] parts = url.Split(new[] { "?" }, StringSplitOptions.RemoveEmptyEntries);

            string address = parts[0];

            string parameters = parts.Length > 1 ? parts[1] : null;

            URL parsedUrl = new URL();

            HandleAddressPart(parsedUrl, address);
            HandleParametersPart(parsedUrl, parameters);

            return parsedUrl;

        }

        private void HandleAddressPart(URL url, string address)
        {
            string[] parts = address.Split(new[] { "://", "/" }, StringSplitOptions.RemoveEmptyEntries);

            url.Scheme = parts[0];

            url.Host = parts[1];

            url.UrlPathSegments = parts.Skip(2);
        }

        private void HandleParametersPart(URL url, string parameters)
        {
            if (parameters == null)
            {
                return;
            }

            string[] pairs = parameters.Split('&');

            var parametersArray = new List<KeyValuePair<string, string>>();

            foreach(var pair in pairs)
            {
                string[] parts = pair.Split('=');
                parametersArray.Add(new KeyValuePair<string, string>(parts[0], parts[1]));
            }

            url.Parameters = parametersArray;
        }
    }
}
