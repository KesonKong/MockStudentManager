using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MockStudentManager.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        /// <summary>
        /// 通过ASP.NET CORE 依赖注入ILogger 接口 
        /// 将指定类型的控制器作为参数
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            //通过IStatusCodeReExecuteFeature获取页面路径，链接字符串
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404: 
                    ViewBag.ErrorMessage = "抱歉，您访问的页面不存在";
                    logger.LogWarning($"发生一个404错误，路径：{statusCodeResult.OriginalPath},查询字符串：{statusCodeResult.OriginalQueryString}");

                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.BasePath = statusCodeResult.OriginalPathBase;
                    ViewBag.QueryStr = statusCodeResult.OriginalQueryString;

                    break;
            }
            return View("NotFound");
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            logger.LogError($"路径：{exceptionHandlerFeature.Path},产生了一个错误：{exceptionHandlerFeature.Error.Message}");

            ViewBag.Path = exceptionHandlerFeature.Path;
            ViewBag.Message = exceptionHandlerFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerFeature.Error.StackTrace;


            return View("Error");

        }
    }
}
