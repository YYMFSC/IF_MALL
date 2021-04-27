

namespace IF_Model
{

    #region 系统基础


    #endregion

    /// <summary>
    /// 是否
    /// </summary>
    public enum Enum_YesOrNo : int
    {
        /// <summary>
        /// 是
        /// </summary>
        [EnumAttribute(true, "是")]
        Yes = 1,
        /// <summary>
        /// 否
        /// </summary>
        [EnumAttribute(true, "否")]
        No = 0
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Enum_Sex : int
    {
        /// <summary>
        /// 男
        /// </summary>
        [EnumAttribute(true, "男")]
        Man = 1,
        /// <summary>
        /// 否
        /// </summary>
        [EnumAttribute(true, "女")]
        Woman = 0
    }

    /// <summary>
    /// 系统业务状态的枚举类型
    /// </summary>
    public enum Enum_BasicInfoStatus : int
    {
        /// <summary>
        /// 启用
        /// </summary>
        [EnumAttribute(true, "启用")]
        Enable = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [EnumAttribute(true, "禁用")]
        Disable = 0,
        /// <summary>
        /// 删除
        /// </summary>
        [EnumAttribute(true, "删除")]
        Delete = -1,
        /// <summary>
        /// 替换
        /// </summary>
        [EnumAttribute(false, "替换")]
        Replace = 10
    }



    /// <summary>
    /// 多条件查询中字段的类型
    /// </summary>
    public enum Enum_SearchFieldType : int
    {
        /// <summary>
        /// 文本型
        /// </summary>
        [EnumAttribute(true, "")]
        Text = 1,
        /// <summary>
        /// 数值型
        /// </summary>
        [EnumAttribute(true, "")]
        Num = 2,
        /// <summary>
        /// 下拉型
        /// </summary>
        [EnumAttribute(true, "")]
        Combobox = 3,
        /// <summary>
        /// 日期型
        /// </summary>
        [EnumAttribute(true, "")]
        Datetime = 4,
        /// <summary>
        /// 多选型
        /// </summary>
        [EnumAttribute(true, "")]
        Checkbox = 5,
        /// <summary>
        /// 浮点型
        /// </summary>
        [EnumAttribute(true, "")]
        Decimal = 6,
    }

    /// <summary>
    /// 多条件查询中字段的操作类型
    /// </summary>
    public enum Enum_SearchFieldOPType : int
    {
        /// <summary>
        /// 大于
        /// </summary>
        [EnumAttribute(true, "大于")]
        bigger = 1,
        /// <summary>
        /// 等于
        /// </summary>
        [EnumAttribute(true, "等于")]
        equal = 2,
        /// <summary>
        /// 小于
        /// </summary>
        [EnumAttribute(true, "小于")]
        smaller = 4,
        /// <summary>
        /// 约等于
        /// </summary>
        [EnumAttribute(true, "约等于")]
        like = 8,
        /// <summary>
        /// 不等于
        /// </summary>
        [EnumAttribute(true, "不等于")]
        notequal = 16,
    }

    /// <summary>
    /// 多条件查询中拼接ANDOR
    /// </summary>
    public enum Enum_SearchFieldAndOr : int
    {
        /// <summary>
        /// 并且
        /// </summary>
        [EnumAttribute(true, "并且")]
        And = 1,
        /// <summary>
        /// 或者
        /// </summary>
        [EnumAttribute(true, "或者")]
        Or = 2,
    }

    /// <summary>
    /// 系统模块和菜单的类型的枚举
    /// </summary>
    public enum Enum_MenuType : int
    {
        /// <summary>
        /// 页面
        /// </summary>
        [EnumAttribute(true, "页面")]
        Page = 0,
        /// <summary>
        /// 模块
        /// </summary>
        [EnumAttribute(true, "模块")]
        Module = 1,
    }

    /// <summary>
    /// 是否审核通过的枚举
    /// </summary>
    public enum Enum_IsExamine : int
    {
        [EnumAttribute(true,"未审核")]
        un = 0,

        [EnumAttribute(true, "审核通过")]
        pass = 1,

        [EnumAttribute(true, "审核未通过")]
        not = -1
    }
    public enum Enum_Enum_Branch : int
    {
        [EnumAttribute(true, "未审")]
        un = 0,

        [EnumAttribute(true, "审核通")]
        pass = 1,

        [EnumAttribute(true, "审核未通")]
        not = -1
    }

    /// <summary>
    /// 系统模块和菜单的类型的枚举
    /// </summary>
    public enum Enum_BusinessLogOpType : int
    {
        /// <summary>
        /// 查询
        /// </summary>
        [EnumAttribute(true, "查询")]
        Query = 0,
        /// <summary>
        /// 新增
        /// </summary>
        [EnumAttribute(true, "新增")]
        Add = 10,
        /// <summary>
        /// 修改
        /// </summary>
        [EnumAttribute(true, "修改")]
        Edit = 20,
        /// <summary>
        /// 删除
        /// </summary>
        [EnumAttribute(true, "删除")]
        Del = 30,
        /// <summary>
        /// 状态操作
        /// </summary>
        [EnumAttribute(true, "状态操作")]
        State = 40,
        /// <summary>
        /// 其他
        /// </summary>
        [EnumAttribute(true, "其他")]
        Other = 50,
    }

    /// <summary>
    /// Icon图标类型
    /// </summary>
    public enum Enum_IconType : int
    {
        /// <summary>
        /// 无图标
        /// </summary>
        [EnumAttribute(true, "无")]
        None = 0,
        /// <summary>
        /// 小图标（16*16）
        /// </summary>
        [EnumAttribute(true, "小图标（16*16）")]
        SmallImg = 10,
        /// <summary>
        /// 大图标（32*32）
        /// </summary>
        [EnumAttribute(true, "大图标（32*32）")]
        BigImg = 20,
    }

    /// <summary>
    /// 数据变更标记
    /// </summary>
    public enum Enum_DataChangeState : int
    {
        /// <summary>
        /// 未操作
        /// </summary>
        [EnumAttribute(true, "未操作")]
        None = 0,
        /// <summary>
        /// 修改
        /// </summary>
        [EnumAttribute(true, "修改")]
        Edit = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [EnumAttribute(true, "删除")]
        Del = 2
    }


    /// <summary>
    /// 表类型
    /// </summary>
    public enum Enum_TableType : int
    {
        /// <summary>
        /// 系统表
        /// </summary>
        [EnumAttribute(true, "系统表")]
        System = 0,

        /// <summary>
        /// 业务表
        /// </summary>
        [EnumAttribute(true, "业务表")]
        Business = 1,

        /// <summary>
        /// 关联表
        /// </summary>
        [EnumAttribute(true, "关联表")]
        Link = 2,
    }

    #region 统计相关

    /// <summary>
    /// 统计列数据类型
    /// </summary>
    public enum Enum_ColumnDataType : int
    {
        /// <summary>
        /// 字符串
        /// </summary>
        [EnumAttribute(true, "string")]
        stringcol = 1,

        /// <summary>
        /// 整型
        /// </summary>
        [EnumAttribute(true, "int")]
        interger = 2,

        /// <summary>
        /// 精度数
        /// </summary>
        [EnumAttribute(true, "decimal")]
        decimalcol = 3,

        /// <summary>
        /// 双精度
        /// </summary>
        [EnumAttribute(true, "double")]
        doublecol = 4,

        /// <summary>
        /// 长整型
        /// </summary>
        [EnumAttribute(true, "long")]
        longcol = 5,
        /// <summary>
        /// 去重列
        /// </summary>
        [EnumAttribute(true, "distinct")]
        distinctcol = 6,
        /// <summary>
        /// 数量
        /// </summary>
        [EnumAttribute(true, "count")]
        countcol = 7,
        /// <summary>
        /// 最大值
        /// </summary>
        [EnumAttribute(true, "max")]
        maxcol = 8,
        /// <summary>
        /// 最小值
        /// </summary>
        [EnumAttribute(true, "min")]
        mincol = 9,
        /// <summary>
        /// 日期
        /// </summary>
        [EnumAttribute(true, "date")]
        datecol = 10
    }

    /// <summary>
    /// 统计报表自动过滤类型
    /// </summary>
    public enum Enum_StatFilterType : int
    {
        /// <summary>
        /// 文本匹配
        /// </summary>
        [EnumAttribute(true, "textbox")]
        textbox = 1,

        /// <summary>
        /// 下拉列表
        /// </summary>
        [EnumAttribute(true, "combobox")]
        combobox = 3,

        /// <summary>
        /// 复选框
        /// </summary>
        [EnumAttribute(true, "checkbox")]
        checkbox = 5,

        /// <summary>
        /// 过滤输入框
        /// </summary>
        /// <remarks>用到关系运算符 Enum_RelationOperator</remarks>
        [EnumAttribute(true, "filteredit")]
        filteredit = 17,

        /// <summary>
        /// 树形下拉选择框
        /// </summary>
        [EnumAttribute(true, "treeselect")]
        treeselect = 21
    }

    /// <summary>
    /// 关系运算符
    /// </summary>
    public enum Enum_RelationOperator : int
    {
        /// <summary>
        /// 等于
        /// </summary>
        [EnumAttribute(true, "等于")]
        EQ = 1,

        /// <summary>
        /// 不等于
        /// </summary>
        [EnumAttribute(true, "不等于")]
        NE = 2,

        /// <summary>
        /// 大于
        /// </summary>
        [EnumAttribute(true, "大于")]
        GT = 3,

        /// <summary>
        /// 大于等于
        /// </summary>
        [EnumAttribute(true, "大于等于")]
        GE = 4,

        /// <summary>
        /// 小于
        /// </summary>
        [EnumAttribute(true, "小于")]
        LT = 5,

        /// <summary>
        /// 小于等于
        /// </summary>
        [EnumAttribute(true, "小于等于")]
        LE = 6
    }

    /// <summary>
    /// 自定义查询条件类型
    /// </summary>
    public enum Enum_StatConditionType : int
    {
        /// <summary>
        /// 自定义控件查询
        /// </summary>
        [EnumAttribute(true, "自定义控件查询")]
        UserDefined = 1,

        /// <summary>
        /// 多条件高级查询
        /// </summary>
        [EnumAttribute(true, "多条件高级查询")]
        MultiSearch = 2
    }

    /// <summary>
    /// 统计自定义控件查询控件类型
    /// </summary>
    public enum Enum_StatConditionControlType : int
    {
        /// <summary>
        /// 文本匹配
        /// </summary>
        [EnumAttribute(true, "textbox")]
        textbox = 1,

        /// <summary>
        /// 下拉列表
        /// </summary>
        [EnumAttribute(true, "combobox")]
        combobox = 3,

        /// <summary>
        /// 日期选择输入框
        /// </summary>
        [EnumAttribute(true, "datepicker")]
        datepicker = 4,

        /// <summary>
        /// 复选框
        /// </summary>
        [EnumAttribute(true, "checkbox")]
        checkbox = 5,

        ///// <summary>
        ///// 过滤输入框
        ///// </summary>
        ///// <remarks>用到关系运算符 Enum_RelationOperator</remarks>
        //[EnumAttribute(true, "filteredit")]
        //filteredit = 17,

        /// <summary>
        /// 树形下拉选择框
        /// </summary>
        [EnumAttribute(true, "treeselect")]
        treeselect = 21
    }

    #endregion

#region 工作流枚举

    /// <summary>
    /// 流程一般情况状态
    /// </summary>
    public enum Enum_WorkFlowStatus : int
    {
        /// <summary>
        /// 未达到条件
        /// </summary>
        [EnumAttribute(true, "未达到条件")]
        Notreach = -10,
        /// <summary>
        /// 草稿
        /// </summary>
        [EnumAttribute(true, "草稿")]
        Enable = 0,
        /// <summary>
        /// 审批中
        /// </summary>
        [EnumAttribute(true, "审批中")]
        Running = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [EnumAttribute(true, "删除")]
        Delete = -1,
        /// <summary>
        /// 办结
        /// </summary>
        [EnumAttribute(true, "办结")]
        Complated = 99
    }

#endregion

}