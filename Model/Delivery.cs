//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Delivery
    {
        public int id { get; set; }
        public string uid { get; set; }
        public string sid { get; set; }
        public Nullable<int> payway { get; set; }
        public Nullable<int> isPay { get; set; }
        public string cartIdList { get; set; }
        public Nullable<decimal> originPrice { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<decimal> deliveryFee { get; set; }
        public string addressDm { get; set; }
        public string remark { get; set; }
        public Nullable<System.DateTime> submitTime { get; set; }
    }
}
