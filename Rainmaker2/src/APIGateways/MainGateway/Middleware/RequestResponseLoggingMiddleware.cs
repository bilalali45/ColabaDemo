using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MainGateway.Middleware
{
    public class RequestResponseLoggingMiddleware : RainsoftGateway.Core.Middleware.RequestResponseLoggingMiddleware
    {
        public RequestResponseLoggingMiddleware(RequestDelegate next) : base(next, new string[]{
                "/api/identity/token/authorize",
                "/api/DocManager/Request/submit",
                "/api/docmanager/thumbnail/SaveWorkbenchDocument",
                "/api/docmanager/thumbnail/SaveTrashDocument",
                "/api/docmanager/thumbnail/SaveCategoryDocument",
                "/api/DocumentManagement/File/Submit",
                "/api/DocumentManagement/File/SubmitByBorrower",
                "/api/identity/CustomerAccount/Register",
                "/api/identity/CustomerAccount/Signin",
                "/api/identity/CustomerAccount/ChangePassword",
                "/api/identity/CustomerAccount/ForgotPasswordResponse"
            }, new string[]{
                "/api/DocumentManagement/BytePro/View",
                "/api/DocumentManagement/Document/View",
                "/api/DocumentManagement/File/View"
            })
        {
        }
    }
}