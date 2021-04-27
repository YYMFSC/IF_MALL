using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eflying.Application.Services.Dto
{
   public interface IPageRequest
    {
        /// <summary>
        /// 排序方式 asc desc
        /// </summary>
        string order { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        string field { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        int page { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        int limit { get; set; }
        int SkipCount { get; }
        void SetDefault();
    }
}
