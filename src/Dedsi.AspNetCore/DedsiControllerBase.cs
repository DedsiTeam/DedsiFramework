using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Dedsi.AspNetCore;

public abstract class DedsiControllerBase : AbpControllerBase
{
    /// <summary>
    /// 导出 excel
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    protected FileContentResult FileByExcel(byte[] bytes,string fileName)
    {
        return File(bytes,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
    }
    
    /// <summary>
    /// 导出 excel
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    protected FileStreamResult FileByExcel(Stream fileStream,string fileName)
    {
        return File(fileStream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
    }

    /// <summary>
    /// 导出 pdf
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    protected FileContentResult FileByPdf(byte[] bytes, string fileName)
    {
        return File(bytes, "application/pdf", fileName);
    }

}