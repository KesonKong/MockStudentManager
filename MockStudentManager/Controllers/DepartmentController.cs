using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MockStudentManager.Controllers
{
    [Authorize] //Identity授权
    public class DepartmentController : Controller
    {
        public string List()
        {
            return "这里是Deparment/List";
        }

        public string Detail()
        {
            return "这里是Deparment/Detail";
        }
    }
}
