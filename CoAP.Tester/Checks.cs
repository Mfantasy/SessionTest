using System;
using System.Collections.Generic;
using System.Text;

namespace CoAP.Tester
{
    static class Checks
    {

        public static GroupCheck Group()
        {
            return new GroupCheck();
        }

        public static ICheck MessageTypeEqual(MessageType messageType)
        {
            return new MessageTypeCheck() { Expected = messageType };
        }

        public static ICheck MessageIdMatch()
        {
            return new MessageIDCheck();
        }

        public static ICheck CodeEqual(Int32 code)
        {
            return new CodeCheck() { Expected = code };
        }

        public static ICheck PayLoadStringEqual(String payLoad)
        {
            return new PayLoadStringCheck() {Expected=payLoad };
        }

        public static ICheck PayLoadByteEqual(byte[] b)
        {
            return new PayLoadByteCheck() { Expected=b };
        }

        public static ICheck TokenStringEqual(String token)
        {
            return new TokenStringCheck() { Expected = token };
        }

        public static ICheck ExistCheckToken()
        {
            return new TokenExistCheck();
        }

        public static ICheck ExistCheckPayLoad()
        {
            return new PayLoadExistCheck();
        }

        public static ICheck TokenByteEqual(byte[] b)
        {
            return new TokenByteCheck() { Expected = b };
        }

        //public static ICheck ETagByteEqual(byte[] b)
        //{
        //    return new ETagByteCheck() { Expected=b};
        //}

        public static ICheck HasOption(OptionType optionType)
        {
            return new OptionCheck(optionType);
        }

        public static ICheck OptionEqual(OptionType optionType, Byte[] value)
        {
            return new OptionCheck(optionType, value);
        }

        public static ICheck OptionEqual(OptionType optionType, String value)
        {
            return new OptionCheck(optionType, value);
        }

        public static ICheck OptionEqual(OptionType optionType, Int32 value)
        {
            return new OptionCheck(optionType, value);
        }
    }

    abstract class EqualityCheck<T> : ICheck
    {
        public String Reason { get; private set; }
        public RowItem BaseItemInfo { get;  set; }
        public T Expected { get; set; }

        public abstract String Field { get; }
        public abstract String ToString(T value);
        
        public Boolean Check(Message msg, Message context,StepItem stepItem)
        {
            T actual = GetValue(msg);

            String expected;
            String received;

            Boolean success=false;
            Type type=null;
            Type expectedType = Expected.GetType();
            if (actual != null)
            {
                type = actual.GetType();
                if (type == typeof(byte[]))
                    success = this.ValueEqual(actual, Expected);
                else if (type == typeof(string))
                    success = this.ValueEqual(actual, Expected);
                else
                    success = Expected.Equals(actual);
            }
                if (!success)
                {
                    
                    if (expectedType == typeof(byte[]))
                        expected = this.ToString(Expected);
                    else
                        expected = Expected.ToString();

                    if (actual == null)
                        received = "";
                    else if (type == typeof(byte[]))
                        received = this.ToString(actual);
                    else
                        received = actual.ToString();
                    #region MyRegion
                    Reason = String.Format("期望{0}的值为\"{1}\",但得到\"{2}\"", Field, expected, received);
                    RowItem bi = new RowItem();
                    bi.ErrorInfo = String.Format("期望{0}的值为\"{1}\",但得到\"{2}\"", Field, expected, received);
                    bi.Expected = expected;
                    bi.Name = Field;
                    bi.IsPassed = false;
                    bi.Parent = null;
                    bi.Received = received;
                    BaseItemInfo = bi;
                    #endregion
                }
           
            return success;
        }

        protected abstract T GetValue(Message msg);
        protected abstract bool ValueEqual(T a, T b);
    }

    class MessageTypeCheck : EqualityCheck<MessageType>
    {
        public override String Field
        {
            get { return "type"; }
        }

        protected override MessageType GetValue(Message msg)
        {
            return msg.Type;
        }
        protected override bool ValueEqual(MessageType a, MessageType b)
        {
            return a == b;
        }
        public override string ToString(MessageType value)
        {
            return value.ToString();
        }
    }

    class PayLoadStringCheck : EqualityCheck<String>
    {
        public override string Field
        {
            get { return "payload"; }
        }
        protected override string GetValue(Message msg)
        {
            return msg.PayloadString;
        }

        protected override bool ValueEqual(String a, String b)
        {
            //char s;
            //for (int i = 0; i < a.Length; i++)
            //{
            //    if (a[i] != b[i])
            //    {
            //        s=a[i];
            //    }
            //}
            return a == b;
        }

        public override string ToString(string value)
        {
            return value.ToString();
        }
    }

    class PayLoadByteCheck : EqualityCheck<byte[]>
    {
        public override string Field
        {
            get { return "payload"; }
        }
        protected override byte[] GetValue(Message msg)
        {
            return msg.Payload;
        }
        public override String ToString(byte[] value)
        {
            return Utils.ToHexString(value);
        }
        protected override bool ValueEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }
    }

    class TokenStringCheck : EqualityCheck<String>
    {
        public override string Field
        {
            get { return "token"; }
        }
        protected override string GetValue(Message msg)
        {
            return msg.TokenString;
        }

        protected override bool ValueEqual(String a, String b)
        {
            return a == b;
        }
        public override string ToString(string value)
        {
            return value;
        }
    }

    class TokenByteCheck : EqualityCheck<byte[]>
    {
        public override string Field
        {
            get { return "token"; }
        }
        protected override byte[] GetValue(Message msg)
        {
            return msg.Token;
        }
        public override String ToString(byte[] value)
        {
            return Utils.ToHexString(value);
        }
        protected override bool ValueEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }        
    }

    //class ETagByteCheck : EqualityCheck<byte[]>
    //{
    //    public override string Field
    //    {
    //        get { return "eTag"; }
    //    }
    //    protected override byte[] GetValue(Message msg)
    //    {
    //        return msg.ETags.GetEnumerator().Current;
    //    }
    //    public override String ToString(byte[] value)
    //    {
    //        return Utils.ToHexString(value);
    //    }
    //    protected override bool ValueEqual(byte[] a, byte[] b)
    //    {
    //        if (a.Length != b.Length)
    //            return false;
    //        for (int i = 0; i < a.Length; i++)
    //        {
    //            if (a[i] != b[i])
    //                return false;
    //        }
    //        return true;
    //    }
    //}

    class CodeCheck : EqualityCheck<Int32>
    {
        public override String Field
        {
            get { return "code"; }
        }
        
        protected override Int32 GetValue(Message msg)
        {
            return msg.Code;
        }
        protected override bool ValueEqual(Int32 a, Int32 b)
        {
            return a == b;
        }
        public override string ToString(int value)
        {
            return value.ToString();

        }  
    }

    class TokenExistCheck : ICheck
    {
        public String Reason { get; private set; }
        public RowItem BaseItemInfo { get; set; }
        public Boolean Check(Message msg, Message context, StepItem stepItem)
        {
            Boolean success=(msg.Token!=null&&msg.Token.Length>0)||(msg.TokenString!=null&&msg.TokenString.Length>0);
            if (!success)
            {
                Reason = "期望Token(任意值),但没有获得任意数据";
                RowItem bi = new RowItem();
                bi.ErrorInfo = "期望Token(任意值),但没有获得任意数据";
                bi.Expected = "Token(任意值)";
                bi.Received = msg.ID.ToString();
                bi.Name = "Token";
                bi.IsPassed = false;
                bi.Parent = null;
                BaseItemInfo = bi;
                //stepItem.RowItemList.Add(BaseItemInfo);
            }

            return success;
        }
    }

    class PayLoadExistCheck : ICheck
    {
        public String Reason { get; private set; }
        public RowItem BaseItemInfo { get; set; }
        public Boolean Check(Message msg, Message context, StepItem stepItem)
        {
            Boolean success = (msg.Payload != null && msg.Payload.Length > 0) || (msg.PayloadString != null && msg.PayloadString.Length > 0);
            if (!success)
            {
                Reason = "期望PayLoad(任意值),但没有获得任意数据";
                RowItem bi = new RowItem();
                bi.ErrorInfo = "期望PayLoad(任意值),但没有获得任意数据";
                bi.Expected = "PayLoad(任意值)";
                //bi.Received = msg.ID.ToString();
                bi.Received = "";
                bi.Name = "PayLoad";
                bi.IsPassed = false;
                bi.Parent = null;
                BaseItemInfo = bi;
            }

            return success;
        }
    }

    class OptionCheck : ICheck
    {
        private Option _expected;
        public RowItem BaseItemInfo { get; set; }
        public String Reason { get; private set; }
        public OptionType OptionType { get; set; }
        public Int32? IntValue { get; set; }
        public String StringValue { get; set; }
        public Byte[] RawValue { get; set; }

        public OptionCheck(OptionType optionType)
        {
            OptionType = optionType;
        }

        public OptionCheck(OptionType optionType, Byte[] value)
        {
            OptionType = optionType;
            RawValue = value;
            _expected = Option.Create(optionType, value);
        }

        public OptionCheck(OptionType optionType, String value)
        {
            OptionType = optionType;
            StringValue = value;
            _expected = Option.Create(optionType, value);
        }

        public OptionCheck(OptionType optionType, Int32 value)
        {
            OptionType = optionType;
            IntValue = value;
            _expected = Option.Create(optionType, value);
        }

        public Boolean Check(Message msg, Message context,StepItem stepItem)
        {
            Boolean success;
            if (_expected == null)
            {
                success = msg.HasOption(OptionType);
                if (!success)
                {
                    Reason = "得到的响应没有" + Option.ToString(OptionType);

                    RowItem bi = new RowItem();
                    bi.Name = Option.ToString(OptionType);
                    bi.ErrorInfo = "得到的响应没有" + Option.ToString(OptionType);
                    bi.IsPassed = false;
                    bi.Parent = null;
                    BaseItemInfo = bi;
                }
            }
            else
            {
                Option actual = msg.GetFirstOption(OptionType);
                success = _expected.Equals(actual);
                if (!success)
                {
                    Reason = String.Format("期望{0}的值为\"{1}\",但得到\"{2}\"", Option.ToString(OptionType),
                        _expected, actual == null ? "" : actual.ToString());

                    RowItem bi = new RowItem();
                    bi.Name = Option.ToString(OptionType);
                    if (actual != null)
                    {
                        bi.ErrorInfo = String.Format("期望{0}的值为\"{1}\",但得到\"{2}\"", Option.ToString(OptionType),
                        _expected, actual.ToString());
                    }
                    else
                    {
                        bi.ErrorInfo = String.Format("期望{0}的值为\"{1}\",但没得到数据", Option.ToString(OptionType), _expected);
                    }
                    bi.ErrorInfo = String.Format("期望{0}的值为\"{1}\",但得到\"{2}\"", Option.ToString(OptionType),
                        _expected, actual == null ? "" : actual.ToString());
                    bi.Expected = _expected.ToString();
                    bi.Received = actual == null ? "" : actual.ToString();
                    bi.IsPassed = false;
                    bi.Parent = null;
                    BaseItemInfo=bi;
                   // stepItem.RowItemList.Add(BaseItemInfo);
                }
            }
            

            return success;
        }
    }

    class MessageIDCheck : ICheck
    {
        public String Reason { get; private set; }
        public RowItem BaseItemInfo { get; set; }
        public Boolean Check(Message msg, Message context,StepItem stepItem)
        {
            Boolean success = msg.ID == context.ID;
            if (!success)
            {
                Reason = String.Format("期望message ID的值为{0},但得到\"{1}\"", context.ID, msg.ID);
                RowItem bi = new RowItem();
                bi.ErrorInfo = String.Format("期望message ID的值为{0},但得到\"{1}\"", context.ID, msg.ID);
                bi.Expected = context.ID.ToString();
                bi.Received = msg.ID.ToString();
                bi.Name = "message ID";
                bi.IsPassed = false;
                bi.Parent = null;
                BaseItemInfo = bi;
                //stepItem.RowItemList.Add(BaseItemInfo);
            }
            
            return success;
        }
    }

    class GroupCheck : ICheck, ICollection<ICheck>
    {
        private IList<ICheck> _groupedChecks = new List<ICheck>();
        private List<String> _reasons = new List<String>();
        private List<RowItem> _rowItems = new List<RowItem>();
        
        public List<RowItem> RowItems
        {
            get { return _rowItems; }
            set { _rowItems = value; }
        }
        public RowItem BaseItemInfo {get;set; }

        public String Reason
        {
            get
            {
                if (_reasons.Count == 0)
                    return null;
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in _reasons)
                    {
                        sb.AppendLine(item);
                    }
                    return sb.ToString();
                }
            }
        }

        public IEnumerable<String> Reasons
        {
            get { return _reasons; }
        }

        

        public GroupCheck Add(ICheck check)
        {
            _groupedChecks.Add(check);
            return this;
        }

        public Boolean Check(Message msg, Message context,StepItem stepItem)
        {
            _reasons.Clear();
            _rowItems.Clear();
            foreach (ICheck check in _groupedChecks)
            {
                if (!check.Check(msg, context,stepItem))
                {
                    _reasons.Add(check.Reason);
                    _rowItems.Add(check.BaseItemInfo);
                }
            }
            stepItem.RowItemList.AddRange(_rowItems);
            return _reasons.Count == 0;
        }

        public void Clear()
        {
            _groupedChecks.Clear();
        }

        public Boolean Contains(ICheck item)
        {
            return _groupedChecks.Contains(item);
        }

        public void CopyTo(ICheck[] array, Int32 arrayIndex)
        {
            _groupedChecks.CopyTo(array, arrayIndex);
        }

        public Int32 Count
        {
            get { return _groupedChecks.Count; }
        }

        public Boolean IsReadOnly
        {
            get { return _groupedChecks.IsReadOnly; }
        }

        public Boolean Remove(ICheck item)
        {
            return _groupedChecks.Remove(item);
        }

        public IEnumerator<ICheck> GetEnumerator()
        {
            return _groupedChecks.GetEnumerator();
        }

        void ICollection<ICheck>.Add(ICheck item)
        {
            _groupedChecks.Add(item);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _groupedChecks.GetEnumerator();
        }
    }
}
