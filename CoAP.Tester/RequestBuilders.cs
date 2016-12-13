using System;
using System.Collections.Generic;

namespace CoAP.Tester
{
    static class RequestBuilders
    {
        public static GroupBuilder Group()
        {
            return new GroupBuilder();
        }

        public static IRequestBuilder Method(Method method)
        {
            return new MethodBuilder() { Method = method };
        }

        public static IRequestBuilder MessageType(MessageType messageType)
        {
            return new MessageTypeBuilder() { MessageType = messageType };
        }

        public static IRequestBuilder Token(Byte[] token)
        {
            return new TokenBuilder() { Token = token };
        }

        public static IRequestBuilder Option(OptionType optionType)
        {
            return new OptionBuilder(optionType);
        }

        public static IRequestBuilder Option(OptionType optionType, Byte[] value)
        {
            return new OptionBuilder(optionType, value);
        }

        public static IRequestBuilder Option(OptionType optionType, String value)
        {
            return new OptionBuilder(optionType, value);
        }

        public static IRequestBuilder Option(OptionType optionType, Int32 value)
        {
            return new OptionBuilder(optionType, value);
        }

        public static IRequestBuilder Payload(Byte[] payload, Int32 contentType)
        {
            return new PayloadBuilder() { Payload = payload, ContentType = contentType };
        }

        public static IRequestBuilder Payload(String payload, Int32 contentType)
        {
            return new PayloadBuilder() { PayloadString = payload, ContentType = contentType };
        }
    }

    class MethodBuilder : IRequestBuilder
    {
        public Method Method { get; set; }

        public Request Build(Request request)
        {
            if (Method == request.Method)
                return request;

            Request newReq = new Request(Method);
            newReq.URI = request.URI;
            newReq.SetOptions(request.GetOptions());
            return newReq;
        }
    }

    class MessageTypeBuilder : IRequestBuilder
    {
        public MessageType MessageType { get; set; }

        public Request Build(Request request)
        {
            request.Type = MessageType;
            return request;
        }
    }

    class TokenBuilder : IRequestBuilder
    {
        public Byte[] Token { get; set; }

        public Request Build(Request request)
        {
            if (Token != null)
                request.Token = Token;
            return request;
        }
    }

    class OptionBuilder : IRequestBuilder
    {
        public OptionType OptionType { get; set; }
        public Int32? IntValue { get; set; }
        public String StringValue { get; set; }
        public Byte[] RawValue { get; set; }

        public OptionBuilder(OptionType optionType)
        {
            OptionType = optionType;
        }

        public OptionBuilder(OptionType optionType, Byte[] value)
        {
            OptionType = optionType;
            RawValue = value;
        }

        public OptionBuilder(OptionType optionType, String value)
        {
            OptionType = optionType;
            StringValue = value;
        }

        public OptionBuilder(OptionType optionType, Int32 value)
        {
            OptionType = optionType;
            IntValue = value;
        }

        public Request Build(Request request)
        {
            Option opt = null;
            if (RawValue != null)
                opt = Option.Create(OptionType, RawValue);
            else if (StringValue != null)
            {
                if (OptionType == CoAP.OptionType.UriPath)
                    request.SetOptions(Option.Split(OptionType, StringValue, "/"));
                else if (OptionType == CoAP.OptionType.UriQuery)
                    request.SetOptions(Option.Split(OptionType, StringValue, "&"));
                else
                    opt = Option.Create(OptionType, StringValue);
            }
            else if (IntValue.HasValue)
                opt = Option.Create(OptionType, IntValue.Value);
            else
                opt = Option.Create(OptionType);

            if (opt != null)
                request.AddOption(opt);

            return request;
        }
    }

    class PayloadBuilder : IRequestBuilder
    {
        public Byte[] Payload { get; set; }
        public String PayloadString { get; set; }
        public Int32 ContentType { get; set; }

        public Request Build(Request request)
        {
            if (Payload != null)
                request.Payload = Payload;
            else
                request.SetPayload(PayloadString);
            request.ContentType = ContentType;
            return request;
        }
    }

    class GroupBuilder : IRequestBuilder, ICollection<IRequestBuilder>
    {
        private IList<IRequestBuilder> _groupedBuilders = new List<IRequestBuilder>();

        public GroupBuilder Add(IRequestBuilder builder)
        {
            _groupedBuilders.Add(builder);
            return this;
        }

        public Request Build(Request request)
        {
            foreach (var builder in _groupedBuilders)
            {
                request = builder.Build(request);
            }
            return request;
        }

        public void Clear()
        {
            _groupedBuilders.Clear();
        }

        public Boolean Contains(IRequestBuilder item)
        {
            return _groupedBuilders.Contains(item);
        }

        public void CopyTo(IRequestBuilder[] array, Int32 arrayIndex)
        {
            _groupedBuilders.CopyTo(array, arrayIndex);
        }

        public Int32 Count
        {
            get { return _groupedBuilders.Count; }
        }

        public Boolean IsReadOnly
        {
            get { return _groupedBuilders.IsReadOnly; }
        }

        public Boolean Remove(IRequestBuilder item)
        {
            return _groupedBuilders.Remove(item);
        }

        public IEnumerator<IRequestBuilder> GetEnumerator()
        {
            return _groupedBuilders.GetEnumerator();
        }

        void ICollection<IRequestBuilder>.Add(IRequestBuilder item)
        {
            _groupedBuilders.Add(item);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
