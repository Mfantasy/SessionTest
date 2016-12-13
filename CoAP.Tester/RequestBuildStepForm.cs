using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoAP.Tester
{
    public partial class RequestBuildStepForm : Form
    {
        internal static Tuple<Int32, String>[] MediaTypes = new Tuple<Int32, String>[] {
            CreateMediaType(MediaType.Undefined),
            CreateMediaType(MediaType.TextPlain),
            CreateMediaType(MediaType.TextXml),
            CreateMediaType(MediaType.TextCsv),
            CreateMediaType(MediaType.TextHtml),
            CreateMediaType(MediaType.ImageGif),
            CreateMediaType(MediaType.ImageJpeg),
            CreateMediaType(MediaType.ImagePng),
            CreateMediaType(MediaType.ImageTiff),
            CreateMediaType(MediaType.AudioRaw),
            CreateMediaType(MediaType.VideoRaw),
            CreateMediaType(MediaType.ApplicationLinkFormat),
            CreateMediaType(MediaType.ApplicationXml),
            CreateMediaType(MediaType.ApplicationOctetStream),
            CreateMediaType(MediaType.ApplicationRdfXml),
            CreateMediaType(MediaType.ApplicationSoapXml),
            CreateMediaType(MediaType.ApplicationAtomXml),
            CreateMediaType(MediaType.ApplicationXmppXml),
            CreateMediaType(MediaType.ApplicationExi),
            CreateMediaType(MediaType.ApplicationFastinfoset),
            CreateMediaType(MediaType.ApplicationSoapFastinfoset),
            CreateMediaType(MediaType.ApplicationJson),
            CreateMediaType(MediaType.ApplicationXObixBinary)
        };

        public RequestBuildStep Step { get; set; }

        public RequestBuildStepForm()
        {
            InitializeComponent();
        }

        private static Tuple<Int32, String> CreateMediaType(Int32 mediaType)
        {
            String name = mediaType == MediaType.Undefined ? "not set"
                : String.Format("[{0}] {1}", mediaType, MediaType.ToString(mediaType));
            return Tuple.Create(mediaType, name);
        }

        private void RequestBuildStepForm_Load(object sender, EventArgs e)
        {
            cbxAccept.DataSource = MediaTypes;
            cbxAccept.DisplayMember = "Item2";
            cbxAccept.ValueMember = "Item1";
            cbxContentType.DataSource = MediaTypes.Clone();
            cbxContentType.DisplayMember = "Item2";
            cbxContentType.ValueMember = "Item1";

            cbxMethod.DataSource = new[] {
                Tuple.Create(Method.GET, "GET"),
                Tuple.Create(Method.POST, "POST"),
                Tuple.Create(Method.PUT, "PUT"),
                Tuple.Create(Method.DELETE, "DELETE")
            };
            cbxMethod.DisplayMember = "Item2";
            cbxMethod.ValueMember = "Item1";

            cbxType.DataSource = new[] { 
                Tuple.Create(MessageType.CON, "CON"),
                Tuple.Create(MessageType.ACK, "ACK"),
                Tuple.Create(MessageType.NON, "NON"),
                Tuple.Create(MessageType.RST, "RST")
            };
            cbxType.DisplayMember = "Item2";
            cbxType.ValueMember = "Item1";

            Display(Step.RequestBuilder);
        }

        private void Display(IRequestBuilder builder)
        {
            if (builder is GroupBuilder)
            {
                foreach (IRequestBuilder rb in builder as GroupBuilder)
                {
                    Display(rb);
                }
            }
            else if (builder is MethodBuilder)
                cbxMethod.SelectedValue = (builder as MethodBuilder).Method;
            else if (builder is MessageTypeBuilder)
                cbxType.SelectedValue = (builder as MessageTypeBuilder).MessageType;
            else if (builder is TokenBuilder)
            {
                tbxToken.Text = Utils.ToHexString((builder as TokenBuilder).Token);
            }
            else if (builder is PayloadBuilder)
            {
                PayloadBuilder pb = builder as PayloadBuilder;
                if (pb.Payload != null)
                    tbxPayload.Text = Utils.ToHexString(pb.Payload);
                else
                    tbxPayload.Text = pb.PayloadString;
                cbxContentType.SelectedValue = pb.ContentType;
            }
            else if (builder is OptionBuilder)
            {
                OptionBuilder ob = builder as OptionBuilder;
                switch (ob.OptionType)
                {
                    case OptionType.UriPath:
                        tbxUriPath.Text = ob.StringValue;
                        break;
                    case OptionType.UriQuery:
                        tbxUriQuery.Text = ob.StringValue;
                        break;
                    case OptionType.Accept:
                        cbxAccept.SelectedValue = ob.IntValue;
                        break;
                    case OptionType.ContentType:
                        cbxContentType.SelectedValue = ob.IntValue;
                        break;
                    case OptionType.ETag:
                        tbxETag.Text = ob.RawValue == null ? ob.StringValue : Utils.ToHexString(ob.RawValue);
                        break;
                    case OptionType.IfMatch:
                        tbxIfMatch.Text = ob.RawValue == null ? ob.StringValue : Utils.ToHexString(ob.RawValue);
                        break;
                    case OptionType.IfNoneMatch:
                        checkIfNoneMatch.Checked = true;
                        break;
                    case OptionType.Observe:
                        tbxObserve.Text = ob.IntValue == null ? ob.StringValue : ob.IntValue.ToString();
                        break;
                    case OptionType.Block2:
                        tbxBlockDown.Text = ob.IntValue == null ? ob.StringValue : ob.IntValue.ToString();
                        break;
                    case OptionType.Block1:
                        tbxBlockUp.Text = ob.IntValue == null ? ob.StringValue : ob.IntValue.ToString();
                        break;
                    case OptionType.LocationPath:
                        txtLocationPath.Text = ob.StringValue.ToString();
                        break;
                    case OptionType.LocationQuery:
                        txtLocationQuery.Text = ob.StringValue.ToString();
                        break;
                    case OptionType.MaxAge:
                        txtMaxAge.Text = ob.IntValue == null ? ob.StringValue : ob.IntValue.ToString();
                        break;
                    case OptionType.ProxyScheme:
                        checkProxyScheme.Checked = true;
                        break;
                    case OptionType.ProxyUri:
                        txtProxyUri.Text = ob.StringValue.ToString();
                        break;
                    case OptionType.Token:
                        tbxToken.Text = ob.StringValue.ToString();
                        break;
                    case OptionType.UriHost:
                        txtUriHost.Text = ob.StringValue.ToString();
                        break;                   
                    case OptionType.UriPort:
                        txtUriPort.Text = ob.IntValue == null ? ob.StringValue : ob.IntValue.ToString();
                        break;
                }
            }
        }

        private void Save()
        {
            GroupBuilder group = Step.RequestBuilder as GroupBuilder;
            if (group == null)
                group = RequestBuilders.Group();
            else
                group.Clear();

            if (cbxMethod.SelectedIndex >= 0)
                group.Add(RequestBuilders.Method((Method)cbxMethod.SelectedValue));
            if (cbxType.SelectedIndex >= 0)
                group.Add(RequestBuilders.MessageType((MessageType)cbxType.SelectedValue));

            String tokenString = tbxToken.Text.Trim();
            if (!String.IsNullOrEmpty(tokenString))
            {
                Byte[] token;
                if (tokenString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    token = Utils.ToBytes(tokenString);
                else
                    token = System.Text.Encoding.UTF8.GetBytes(tokenString);
                group.Add(RequestBuilders.Token(token));
            }

            Int32 mediaType = MediaType.Undefined;
            if (cbxContentType.SelectedIndex > 0)
            {
                mediaType = (Int32)cbxContentType.SelectedValue;
                group.Add(RequestBuilders.Option(OptionType.ContentType, mediaType));
            }
            String payloadString = tbxPayload.Text.Trim();
            if (!String.IsNullOrEmpty(payloadString))
            {
                if (payloadString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    Byte[] payload = Utils.ToBytes(payloadString);
                    if (payload != null)
                        group.Add(RequestBuilders.Payload(payload, mediaType));
                }
                else
                    group.Add(RequestBuilders.Payload(tbxPayload.Text, mediaType));
            }

            if (cbxAccept.SelectedIndex > 0)
                group.Add(RequestBuilders.Option(OptionType.Accept, (Int32)cbxAccept.SelectedValue));

            CheckStringOption(group, OptionType.UriPath, tbxUriPath);
            CheckStringOption(group, OptionType.UriQuery, tbxUriQuery);
            CheckHexStringOption(group, OptionType.ETag, tbxETag);
            CheckHexStringOption(group, OptionType.IfMatch, tbxIfMatch);
            if (checkIfNoneMatch.Checked)
                group.Add(RequestBuilders.Option(OptionType.IfNoneMatch));
            CheckIntOption(group, OptionType.Observe, tbxObserve);
            CheckIntOption(group, OptionType.Block2, tbxBlockDown);
            //CheckIntOption(group, OptionType.Block1, tbxBlockUp);

            CheckStringOption(group,OptionType.UriHost,txtUriHost);

            CheckIntOption(group, OptionType.UriPort, txtUriPort);

            CheckStringOption(group, OptionType.ProxyUri, txtProxyUri);

            CheckIntOption(group, OptionType.MaxAge, txtMaxAge);

            CheckStringOption(group, OptionType.LocationPath, txtLocationPath);
            CheckStringOption(group, OptionType.LocationQuery, txtLocationQuery);

            if (checkProxyScheme.Checked)
                group.Add(RequestBuilders.Option(OptionType.ProxyScheme));
            Step.RequestBuilder = group;
        }

        private void CheckIntOption(GroupBuilder group, OptionType type, TextBox textBox)
        {
            String text = textBox.Text.Trim();
            if (!String.IsNullOrEmpty(text))
            {
                Int32 val = 0;
                if (Int32.TryParse(text, out val))
                    group.Add(RequestBuilders.Option(type, val));
                else
                    group.Add(RequestBuilders.Option(type,text));
            }
        }

        private void CheckStringOption(GroupBuilder group, OptionType type, TextBox textBox)
        {
            String text = textBox.Text.Trim();
            if (!String.IsNullOrEmpty(text))
                group.Add(RequestBuilders.Option(type, text));
        }

        private void CheckHexStringOption(GroupBuilder group, OptionType type, TextBox textBox)
        {
            String text = textBox.Text.Trim();
            if (!String.IsNullOrEmpty(text))
            {
                if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    Byte[] bytes = Utils.ToBytes(text);
                    if (bytes != null)
                        group.Add(RequestBuilders.Option(type, bytes));
                }
                else
                    group.Add(RequestBuilders.Option(type, text));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
            DialogResult = DialogResult.OK;
        }

    }
}
