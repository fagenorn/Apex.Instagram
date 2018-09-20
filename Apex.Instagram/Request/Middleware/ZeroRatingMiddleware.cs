using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Apex.Instagram.Request.Middleware
{
    internal class ZeroRatingMiddleware : IMiddleware
    {
        private readonly Dictionary<string, string> _defaultRewrite = new Dictionary<string, string>
                                                                      {
                                                                          {
                                                                              @"^(https?:\/\/)(i)(\.instagram\.com/.*)$", "$1b.$2$3"
                                                                          }
                                                                      };

        private readonly Dictionary<string, string> _rules;

        public ZeroRatingMiddleware()
        {
            _rules = new Dictionary<string, string>();
            Reset();
        }

        public async Task<HttpResponseMessage> Invoke(HttpRequestMessage request, Func<HttpRequestMessage, Task<HttpResponseMessage>> next)
        {
            if ( _rules.Count == 0 )
            {
                return await next(request);
            }

            var oldUri = request.RequestUri;

            var newUri = Rewrite(oldUri);
            if ( newUri != oldUri )
            {
                request.RequestUri = newUri;
            }

            return await next(request);
        }

        public void Reset() { Update(_defaultRewrite); }

        public void Update(Dictionary<string, string> rules)
        {
            _rules.Clear();
            foreach ( var rule in rules )
            {
                var regex = $"{rule.Key}";
                var test  = Regex.Match(regex, string.Empty);
                if ( !test.Success )
                {
                    continue;
                }

                _rules[regex] = rule.Value;
            }
        }

        public Uri Rewrite(Uri uri)
        {
            foreach ( var rule in _rules )
            {
                var result = Regex.Replace(uri.AbsoluteUri, rule.Key, rule.Value);
                if ( result != uri.AbsoluteUri )
                {
                    return new Uri(result);
                }
            }

            return uri;
        }
    }
}