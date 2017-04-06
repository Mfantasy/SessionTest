using System;

namespace CoAP.Tester
{
    public interface ICheck
    {
        String Reason { get; }
        RowItem BaseItemInfo { get; set; }
        Boolean Check(Message message, Message context,StepItem stepItem);
    }
}
