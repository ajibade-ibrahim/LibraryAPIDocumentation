using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace Library.API.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        public RequestHeaderMatchesMediaTypeAttribute(
            string requestHeaderToMatch,
            string mediaType,
            params string[] otherMediaTypes)
        {
            _requestHeaderToMatch = requestHeaderToMatch
                ?? throw new ArgumentNullException(nameof(requestHeaderToMatch));

            if (mediaType == null)
            {
                throw new ArgumentNullException(nameof(mediaType));
            }

            // check if the inputted media types are valid media types
            // and add them to the _mediaTypes collection

            if (MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            {
                _mediaTypes.Add(parsedMediaType);
            }
            else
            {
                throw new ArgumentException(nameof(mediaType));
            }

            foreach (var otherMediaType in otherMediaTypes)
            {
                if (MediaTypeHeaderValue.TryParse(otherMediaType, out var parsedOtherMediaType))
                {
                    _mediaTypes.Add(parsedOtherMediaType);
                }
                else
                {
                    throw new ArgumentException(nameof(otherMediaTypes));
                }
            }
        }

        private readonly MediaTypeCollection _mediaTypes = new MediaTypeCollection();
        private readonly string _requestHeaderToMatch;

        public int Order
        {
            get => 0;
        }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;

            if (!requestHeaders.ContainsKey(_requestHeaderToMatch))
            {
                return false;
            }

            var parsedRequestMediaType = new MediaType(requestHeaders[_requestHeaderToMatch]);

            // if one of the media types matches, return true
            return _mediaTypes.Select(mediaType => new MediaType(mediaType)).Contains(parsedRequestMediaType);
        }
    }
}