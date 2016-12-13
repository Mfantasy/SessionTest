using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoAP.Tester
{
    public partial class ResponseCheckStepForm : Form
    {
        private Dictionary<OptionType, Tuple<CheckBox, Control>> _optionControlMap = new Dictionary<OptionType, Tuple<CheckBox, Control>>();

        public ResponseCheckStep Step { get; set; }

        public ResponseCheckStepForm()
        {
            InitializeComponent();
        }

        private static Tuple<Int32, String> CreateCode(Int32 code)
        {
            String name = String.Format("[{0}] {1}", code, Code.ToString(code));
            return Tuple.Create(code, name);
        }

        private void ResponseCheckStepForm_Load(object sender, EventArgs e)
        {
            SetupComboBox();
            SetupOptionControlMap();

            Display(Step.ResponseCheck);
        }

        private void SetupOptionControlMap()
        {
            _optionControlMap[OptionType.ContentType] = Tuple.Create<CheckBox, Control>(checkContentType, cbxContentType);
            _optionControlMap[OptionType.LocationPath] = Tuple.Create<CheckBox, Control>(checkLocationPath, tbxLocationPath);
            _optionControlMap[OptionType.LocationQuery] = Tuple.Create<CheckBox, Control>(checkLocationQuery, tbxLocationQuery);
            _optionControlMap[OptionType.Observe] = Tuple.Create<CheckBox, Control>(checkObserve, tbxObserve);
            _optionControlMap[OptionType.Token] = Tuple.Create<CheckBox, Control>(checkToken, txtToken);

            _optionControlMap[OptionType.Accept] = Tuple.Create<CheckBox, Control>(checkAccept, cbAccept);
            _optionControlMap[OptionType.Block1] = Tuple.Create<CheckBox, Control>(checkBlock1, txtBlock1);
            _optionControlMap[OptionType.Block2] = Tuple.Create<CheckBox, Control>(checkBlock2, txtBlock2);
            //_optionControlMap[OptionType.ContentType] = Tuple.Create<CheckBox, Control>(checkContentType, cbxContentType);
            _optionControlMap[OptionType.ETag] = Tuple.Create<CheckBox, Control>(checkETag, txtETag);
            _optionControlMap[OptionType.IfMatch] = Tuple.Create<CheckBox, Control>(checkIfMatch, txtIfMatch);
            _optionControlMap[OptionType.MaxAge] = Tuple.Create<CheckBox, Control>(checkMaxAge, txtMaxAge);
            _optionControlMap[OptionType.ProxyUri] = Tuple.Create<CheckBox, Control>(checkProxyUri, txtProxyUri);
            _optionControlMap[OptionType.UriHost] = Tuple.Create<CheckBox, Control>(checkUriHost, txtUriHost);
            _optionControlMap[OptionType.UriPath] = Tuple.Create<CheckBox, Control>(checkUriPath, txtUriPath);
            _optionControlMap[OptionType.UriPort] = Tuple.Create<CheckBox, Control>(checkUriPort, txtUriPort);
            _optionControlMap[OptionType.UriQuery] = Tuple.Create<CheckBox, Control>(checkUriQuery,txtUriQuery);


        }

        private void SetupComboBox()
        {
            cbxContentType.DataSource = RequestBuildStepForm.MediaTypes;
            cbxContentType.DisplayMember = "Item2";
            cbxContentType.ValueMember = "Item1";

            cbAccept.DataSource = RequestBuildStepForm.MediaTypes.Clone();
            cbAccept.DisplayMember = "Item2";
            cbAccept.ValueMember = "Item1";


            cbxType.DataSource = new[] { 
                
                Tuple.Create(MessageType.CON, "CON"),
                Tuple.Create(MessageType.ACK, "ACK"),
                Tuple.Create(MessageType.NON, "NON"),
                Tuple.Create(MessageType.RST, "RST")
            };
            cbxType.DisplayMember = "Item2";
            cbxType.ValueMember = "Item1";

            cbxCode.DataSource = new[] {
                CreateCode(Code.Empty),
                CreateCode(Code.Created),
                CreateCode(Code.Deleted),
                CreateCode(Code.Valid),
                CreateCode(Code.Changed),
                CreateCode(Code.Content),
                CreateCode(Code.BadRequest),
                CreateCode(Code.Unauthorized),
                CreateCode(Code.BadOption),
                CreateCode(Code.Forbidden),
                CreateCode(Code.NotFound),
                CreateCode(Code.MethodNotAllowed),
                CreateCode(Code.NotAcceptable),
                CreateCode(Code.RequestEntityIncomplete),
                CreateCode(Code.RequestEntityTooLarge),
                CreateCode(Code.UnsupportedMediaType),
                CreateCode(Code.InternalServerError),
                CreateCode(Code.NotImplemented),
                CreateCode(Code.BadGateway),
                CreateCode(Code.ServiceUnavailable),
                CreateCode(Code.GatewayTimeout),
                CreateCode(Code.ProxyingNotSupported),
            };
            cbxCode.DisplayMember = "Item2";
            cbxCode.ValueMember = "Item1";
        }

        private void Display(ICheck check)
        {
            if (check is GroupCheck)
            {
                foreach (ICheck ck in check as GroupCheck)
                {
                    Display(ck);
                }
            }
            else if (check is CodeCheck)
                cbxCode.SelectedValue = (check as CodeCheck).Expected;
            else if (check is MessageTypeCheck)
                cbxType.SelectedValue = (check as MessageTypeCheck).Expected;
            else if (check is MessageIDCheck)
                checkMessageIdMatch.Checked = true;
            else if (check is PayLoadStringCheck)
            {
                checkPayLoad.Checked = true;
                txtPayLoad.Text = (check as PayLoadStringCheck).Expected;
            }
            else if (check is PayLoadByteCheck)
            {
                checkPayLoad.Checked = true;
                txtPayLoad.Text = Utils.ToHexString((check as PayLoadByteCheck).Expected);
            }
            else if (check is TokenStringCheck)
            {
                checkToken.Checked = true;
                txtToken.Text = (check as TokenStringCheck).Expected;
            }
            else if (check is TokenByteCheck)
            {
                checkToken.Checked = true;
                txtToken.Text = Utils.ToHexString((check as TokenByteCheck).Expected);
            }
            else if (check is TokenExistCheck)
            {
                checkToken.Checked = true;
            }
            else if (check is PayLoadExistCheck)
            {
                checkPayLoad.Checked = true;
            }
            else if (check is OptionCheck)
            {
                OptionCheck oc = check as OptionCheck;
                if (_optionControlMap.ContainsKey(oc.OptionType))
                {
                    Tuple<CheckBox, Control> tuple = _optionControlMap[oc.OptionType];
                    tuple.Item1.Checked = true;
                    if (tuple.Item2 is ComboBox)
                    {
                        if (oc.IntValue.HasValue)
                            (tuple.Item2 as ComboBox).SelectedValue = oc.IntValue;
                    }
                    else if (tuple.Item2 is TextBox)
                    {
                        TextBox tbx = tuple.Item2 as TextBox;
                        switch (oc.OptionType)
                        {
                            case OptionType.ETag:
                                tbx.Text = oc.RawValue == null ? oc.StringValue : Utils.ToHexString(oc.RawValue);
                                break;
                            case OptionType.IfMatch:
                            case OptionType.LocationPath:
                            case OptionType.LocationQuery:
                            case OptionType.ProxyUri:
                            case OptionType.UriHost:
                            case OptionType.UriPath:
                            case OptionType.UriQuery:
                                tbx.Text = oc.StringValue;
                                break;
                            case OptionType.Block1:
                            case OptionType.Block2:
                            case OptionType.MaxAge:
                            case OptionType.UriPort:
                            case OptionType.Observe:
                                tbx.Text = oc.IntValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : oc.IntValue.ToString();
                                break;
                            case OptionType.Token:
                                tbx.Text = oc.RawValue == null ? oc.StringValue : Utils.ToHexString(oc.RawValue);
                                break;
                        }
                    }
                }
            }
        }

        private void Save()
        {
            GroupCheck group = Step.ResponseCheck as GroupCheck;
            if (group == null)
                group = Checks.Group();
            else
                group.Clear();

            if (cbxCode.SelectedIndex >= 0)
                group.Add(Checks.CodeEqual((Int32)cbxCode.SelectedValue));
            if (cbxType.SelectedIndex >= 0)
                group.Add(Checks.MessageTypeEqual((MessageType)cbxType.SelectedValue));
            if (checkMessageIdMatch.Checked)
                group.Add(Checks.MessageIdMatch());

            if (checkContentType.Checked)
            {
                if (cbxContentType.SelectedIndex > 0)
                    group.Add(Checks.OptionEqual(OptionType.ContentType, (Int32)cbxContentType.SelectedValue));
                else
                    group.Add(Checks.HasOption(OptionType.ContentType));
            }
            if (checkAccept.Checked)
            {
                if (cbAccept.SelectedIndex > 0)
                    group.Add(Checks.OptionEqual(OptionType.Accept, (Int32)cbAccept.SelectedValue));
                else
                    group.Add(Checks.HasOption(OptionType.Accept));
            }

            if (checkPayLoad.Checked)
            {
                string text = txtPayLoad.Text.Trim();
                if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    Byte[] bytes = Utils.ToBytes(text);
                    if (bytes != null)
                        group.Add(Checks.PayLoadByteEqual(bytes));
                }
                else if (text.Length > 0)
                {
                    text = text.Replace("\r\n", "\n");
                    group.Add(Checks.PayLoadStringEqual(text));
                }
                else
                {
                    group.Add(Checks.ExistCheckPayLoad());
                }

            }
            if (checkToken.Checked)
            {
                String token = txtToken.Text.Trim();
                if (token.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    Byte[] bytes = Utils.ToBytes(token);
                    if (bytes != null)
                        group.Add(Checks.TokenByteEqual(bytes));
                }
                else if (token.Length > 0)
                {
                    //group.Add(Checks.TokenStringEqual(token));
                    group.Add(Checks.TokenByteEqual(Encoding.UTF8.GetBytes(token)));
                }
                else
                    group.Add(Checks.ExistCheckToken());
            }
            if (checkETag.Checked)
            {
                String eTag = txtETag.Text.Trim();
                if (eTag.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    Byte[] bytes = Utils.ToBytes(eTag);
                    if (bytes != null)
                    {                        
                        //group.Add(Checks.ETagByteEqual(bytes));
                        group.Add(Checks.OptionEqual(OptionType.ETag, bytes));
                    }
                }
                else if (eTag.Length > 0)
                {
                    //group.Add(Checks.ETagByteEqual(Encoding.UTF8.GetBytes(eTag)));
                    group.Add(Checks.OptionEqual(OptionType.ETag, Encoding.UTF8.GetBytes(eTag)));
                }
                else
                    group.Add(Checks.HasOption(OptionType.ETag));
            }
            
            CheckStringOption(group, OptionType.LocationPath, checkLocationPath, tbxLocationPath);
            CheckStringOption(group, OptionType.LocationQuery, checkLocationQuery, tbxLocationQuery);
            CheckIntOption(group, OptionType.Observe, checkObserve, tbxObserve);
            //CheckHexStringOption(group, OptionType.Token, checkToken, txtToken);

            CheckStringOption(group, OptionType.UriQuery, checkUriQuery, txtUriQuery);
            CheckStringOption(group, OptionType.UriPath, checkUriPath, txtUriPath);

            CheckIntOption(group, OptionType.Block1, checkBlock1, txtBlock1);
            CheckIntOption(group, OptionType.Block2, checkBlock2, txtBlock2);

            CheckStringOption(group, OptionType.UriHost, checkUriHost, txtUriHost);

            CheckIntOption(group, OptionType.UriPort, checkUriPort, txtUriPort);

            CheckStringOption(group, OptionType.ProxyUri, checkProxyUri, txtProxyUri);

            CheckIntOption(group, OptionType.MaxAge,checkMaxAge,txtMaxAge);

            CheckStringOption(group, OptionType.IfMatch, checkIfMatch, txtIfMatch);
            //CheckStringOption(group, OptionType.ETag,checkETag, txtETag);



            Step.ResponseCheck = group;
        }

        private static void CheckStringOption(GroupCheck group, OptionType type, CheckBox checkBox, TextBox textBox)
        {
            if (checkBox.Checked)
            {
                String text = textBox.Text.Trim();
                if (String.IsNullOrEmpty(text))
                    group.Add(Checks.HasOption(type));
                else
                    group.Add(Checks.OptionEqual(type, text));
            }
        }

        private static void CheckHexStringOption(GroupCheck group, OptionType type, CheckBox checkBox, TextBox textBox)
        {
            if (checkBox.Checked)
            {
                String text = textBox.Text.Trim();
                if (String.IsNullOrEmpty(text))
                    group.Add(Checks.HasOption(type));
                else
                {
                    if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    {
                        Byte[] bytes = Utils.ToBytes(text);
                        if (bytes != null)
                            group.Add(Checks.OptionEqual(type, bytes));
                    }
                    else
                        group.Add(Checks.OptionEqual(type, text));
                }
            }
        }

        private static void CheckIntOption(GroupCheck group, OptionType type, CheckBox checkBox, TextBox textBox)
        {
            if (checkBox.Checked)
            {
                String text = textBox.Text.Trim();
                Int32 val = 0;

                if (String.IsNullOrEmpty(text))
                    group.Add(Checks.HasOption(type));
                else if (Int32.TryParse(text, out val))
                    group.Add(Checks.OptionEqual(type, val));
                else
                    group.Add(Checks.OptionEqual(type,text));
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        
        
    }
}
