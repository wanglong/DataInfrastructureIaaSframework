using System;
using System.Collections.Generic;
using System.Text;

namespace MyDapperComp.CommonHelper
{
    /// <summary>
    /// 数据库类型定义
    /// </summary>
    public enum DatabaseType
    {
        SqlServer,
        MySql,
        Oracle,
        SqlLite,
    }

    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLev
    {
        Debug,
        Error,
        Fatal,
        Info,
        Warn
    }
}
