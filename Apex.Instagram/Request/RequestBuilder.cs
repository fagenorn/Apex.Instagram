using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using Apex.Instagram.Request.Exception;
using Apex.Instagram.Request.Internal;
using Apex.Instagram.Request.Model;
using Apex.Instagram.Request.Signature;
using Apex.Instagram.Utils;

using Version = Apex.Instagram.Constants.Version;

namespace Apex.Instagram.Request
{
    internal class RequestBuilder : IDisposable
    {
        public RequestBuilder(Account account) { _account = account; }

        public void Dispose()
        {
            _content?.Dispose();
            foreach ( var file in _files )
            {
                file.Content?.Dispose();
            }
        }

        public RequestBuilder SetUrl(string url) { return SetUrl(new Uri(url, UriKind.RelativeOrAbsolute)); }

        public RequestBuilder SetUrl(Uri url)
        {
            _url = url;

            return this;
        }

        public RequestBuilder SetVersion(int version)
        {
            if ( !Constants.Request.Instance.ApiUrl.ContainsKey(version) )
            {
                throw new RequestBuilderException("{0} isn't a supported API version.", version);
            }

            _apiVersion = version;

            return this;
        }

        public RequestBuilder SetAddDefaultHeaders(bool flag)
        {
            _addDefaultHeaders = flag;

            return this;
        }

        public RequestBuilder SetNeedsAuth(bool flag)
        {
            _needsAuth = flag;

            return this;
        }

        public RequestBuilder SetSignedPost(bool flag)
        {
            _signedPost = flag;

            return this;
        }

        public RequestBuilder SetSignedGet(bool flag)
        {
            _signedGet = flag;

            return this;
        }

        public RequestBuilder SetIsBodyCompressed(bool flag)
        {
            _isBodyCompressed = flag;

            return this;
        }

        public RequestBuilder SetBody(HttpContent body)
        {
            _content = body;

            return this;
        }

        public RequestBuilder AddParam(string name, string value, bool sign = false)
        {
            _gets[name] = new Parameter(value, sign);

            return this;
        }

        public RequestBuilder AddParam(string name, bool value, bool sign = false) { return AddParam(name, value ? "true" : "false", sign); }

        public RequestBuilder AddParam(string name, int value, bool sign = false) { return AddParam(name, value.ToString(), sign); }

        public RequestBuilder AddPost(string name, string value, bool sign = true)
        {
            _posts[name] = new Parameter(value, sign);

            return this;
        }

        public RequestBuilder AddPost(string name, bool value, bool sign = true) { return AddPost(name, value ? "true" : "false", sign); }

        public RequestBuilder AddPost(string name, int value, bool sign = true) { return AddPost(name, value.ToString(), sign); }

        public RequestBuilder AddPost(string name, ulong value, bool sign = true) { return AddPost(name, value.ToString(), sign); }

        public RequestBuilder AddFile(string name, string path, string fileName = null, Dictionary<string, string> headers = null)
        {
            var fileContent = new StreamContent(File.OpenRead(path))
                              {
                                  Headers =
                                  {
                                      ContentType = new MediaTypeHeaderValue(@"application/octet-stream"),
                                      ContentEncoding =
                                      {
                                          @"binary"
                                      }
                                  }
                              };

            if ( headers != null )
            {
                foreach ( var header in headers )
                {
                    fileContent.Headers.Add(header.Key, header.Value);
                }
            }

            _files.Add(new Files(name, Path.GetFileName(path), fileContent));

            return this;
        }

        public RequestBuilder AddHeader(string name, string value)
        {
            _headers[name] = value;

            return this;
        }

        public HttpRequestMessage Build()
        {
            if ( _url == null )
            {
                throw new RequestBuilderException("You need to have a url specified.");
            }

            if ( _needsAuth && !_account.LoginClient.LoginInfo.IsLoggedIn )
            {
                throw new RequestBuilderException("You aren't logged in currently. Login and try again.");
            }

            if ( _signedGet )
            {
                _gets = Signer.Instance.Sign(_gets);
            }

            AddDefaultHeaders();
            _content = BuildRequestBody();

            if ( _content == null )
            {
                var temp = new HttpRequestMessage(HttpMethod.Get, BuildUrl());
                foreach ( var header in _headers )
                {
                    temp.Headers.Add(header.Key, header.Value);
                }

                return temp;
            }
            else
            {
                if ( _isBodyCompressed )
                {
                    _content = new CompressedContent(_content, CompressionType.Gzip);
                }

                var temp = new HttpRequestMessage(HttpMethod.Post, BuildUrl())
                           {
                               Content = _content
                           };

                foreach ( var header in _headers )
                {
                    temp.Headers.Add(header.Key, header.Value);
                }

                return temp;
            }
        }

        private class Files
        {
            public Files(string name, string fileName, StreamContent content)
            {
                Name     = name;
                FileName = fileName;
                Content  = content;
            }

            public string Name { get; }

            public string FileName { get; }

            public HttpContent Content { get; }
        }

        #region Private Methods

        private void AddDefaultHeaders()
        {
            if ( !_addDefaultHeaders )
            {
                return;
            }

            AddHeader("X-IG-App-ID", Constants.Request.Instance.HeaderFacebookAnalyticsApplicationId);
            AddHeader("X-IG-Capabilities", Version.Instance.HeaderCapabilities);
            AddHeader("X-IG-Connection-Type", Constants.Request.Instance.HeaderConnectionType);
            AddHeader("X-IG-Connection-Speed", Randomizer.Instance.Number(3701, 1000) + "kbps");
            AddHeader("X-IG-Bandwidth-Speed-KBPS", "-1.000");
            AddHeader("X-IG-Bandwidth-TotalBytes-B", "0");
            AddHeader("X-IG-Bandwidth-TotalTime-MS", "0");
        }

        private HttpContent BuildRequestBody()
        {
            // return raw body if specified.
            if ( _content != null )
            {
                return _content;
            }

            if ( _posts.Count == 0 && _files.Count == 0 )
            {
                // GET request
                return null;
            }

            if ( _signedPost )
            {
                _posts = Signer.Instance.Sign(_posts);
            }

            if ( _files.Count == 0 )
            {
                return BuildFormUrlEncodedContent();
            }

            return BuildMultipartContent();
        }

        private MultipartContent BuildMultipartContent()
        {
            var multipartContent = new MultipartFormDataContent(Utils.Instagram.Instance.GenerateMultipartBoundary());

            foreach ( var post in _posts )
            {
                var stringContent = new StringContent(post.Value.ToString());
                multipartContent.Add(stringContent, post.Key);
            }

            foreach ( var file in _files )
            {
                multipartContent.Add(file.Content, file.Name, file.FileName);
            }

            return multipartContent;
        }

        private BetterFormUrlEncodedContent BuildFormUrlEncodedContent()
        {
            var postData = new Dictionary<string, string>();
            foreach ( var post in _posts )
            {
                postData[post.Key] = post.Value.ToString();
            }

            var content = new BetterFormUrlEncodedContent(postData);

            return content;
        }

        private Uri BuildUrl()
        {
            if ( !_url.IsAbsoluteUri )
            {
                _url = new Uri(Constants.Request.Instance.ApiUrl[_apiVersion], _url);
            }

            return new UrlBuilder(_url).AddQueryParams(_gets.ToDictionary(x => x.Key, x => x.Value.ToString()))
                                       .Build();
        }

        #endregion

        #region Fields

        private readonly Account _account;

        private readonly List<Files> _files = new List<Files>();

        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        private Dictionary<string, Parameter> _gets = new Dictionary<string, Parameter>();

        private Dictionary<string, Parameter> _posts = new Dictionary<string, Parameter>();

        private bool _addDefaultHeaders = true;

        private int _apiVersion = 1;

        private HttpContent _content;

        private bool _needsAuth = true;

        private bool _signedGet;

        private bool _signedPost = true;

        private bool _isBodyCompressed;

        private Uri _url;

        #endregion
    }
}