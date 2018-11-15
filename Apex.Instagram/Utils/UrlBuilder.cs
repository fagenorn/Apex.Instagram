﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apex.Instagram.Utils
{
    public class UrlBuilder
    {
        public UrlBuilder SetScheme(string scheme)
        {
            _scheme = scheme;

            return this;
        }

        public UrlBuilder SetScheme(Uri uri) { return SetScheme(uri.Scheme); }

        public UrlBuilder SetHost(string host)
        {
            _host = host;

            return this;
        }

        public UrlBuilder SetHost(Uri uri) { return SetHost(uri.Host); }

        public UrlBuilder AddSegmentAfter(string path, string toAddAfter)
        {
            if ( _pathSegments == null )
            {
                _pathSegments = new List<string>();
            }

            var index = _pathSegments.IndexOf(toAddAfter);
            if ( index != -1 )
            {
                _pathSegments.Insert(index + 1, path);
            }

            return this;
        }

        public UrlBuilder AddSegments(params string[] paths)
        {
            if ( _pathSegments == null )
            {
                _pathSegments = new List<string>();
            }

            _pathSegments.AddRange(paths);

            return this;
        }

        public UrlBuilder AddSegments(Uri uri) { return AddSegments(uri.Segments); }

        public UrlBuilder AddPathString(string path)
        {
            AddSegments(ParsePathString(path));

            return this;
        }

        public UrlBuilder AddQueryParams(Dictionary<string, string> query)
        {
            if ( _queryParameters == null )
            {
                _queryParameters = new Dictionary<string, string>();
            }

            foreach ( var keyValue in query )
            {
                _queryParameters[keyValue.Key] = keyValue.Value;
            }

            return this;
        }

        public UrlBuilder AddQueryParams(Uri uri) { return AddQueryParams(ParseQueryString(uri.Query)); }

        public Uri Build()
        {
            if ( string.IsNullOrWhiteSpace(_scheme) )
            {
                throw new ArgumentException("Scheme can't be empty");
            }

            if ( string.IsNullOrWhiteSpace(_host) )
            {
                throw new ArgumentException("Host can't be empty");
            }

            var finalUri = new StringBuilder(_scheme);
            finalUri.Append(Uri.SchemeDelimiter);
            finalUri.Append(_host);

            if ( _pathSegments != null && _pathSegments.Count > 0 )
            {
                finalUri.Append(string.Join("", _pathSegments));
            }

            if ( _queryParameters != null && _queryParameters.Count > 0 )
            {
                finalUri.Append('?');
                finalUri.Append(string.Join("&", _queryParameters.Select(x => $"{x.Key}={x.Value}")));
            }

            return new Uri(finalUri.ToString(), UriKind.Absolute);
        }

        #region Constructor

        public UrlBuilder() { }

        public UrlBuilder(Uri uri)
        {
            SetScheme(uri);
            SetHost(uri);
            AddSegments(uri);
            AddQueryParams(uri);
        }

        #endregion

        #region Private Methods

        private Dictionary<string, string> ParseQueryString(string query)
        {
            return query.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries))
                        .Where(x => x.Length == 2)
                        .ToDictionary(x => x[0], x => x[1]);
        }

        private string[] ParsePathString(string path)
        {
            var pathSegments = new List<string>();
            var current      = 0;
            while ( current < path.Length )
            {
                var next = path.IndexOf('/', current);
                if ( next == -1 )
                {
                    next = path.Length - 1;
                }

                pathSegments.Add(path.Substring(current, next - current + 1));
                current = next + 1;
            }

            return pathSegments.ToArray();
        }

        #endregion

        #region Fields

        private string _host;

        private List<string> _pathSegments;

        private Dictionary<string, string> _queryParameters;

        private string _scheme;

        #endregion
    }
}