using System.Threading.Tasks;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DocumentManagement.Model.Template;

namespace DocumentManagement.API.Controllers
{
    [Authorize(Roles = "MCU")]
    [ApiController]
    [Route(template: "api/DocumentManagement/[controller]")]
    public class TemplateController : Controller
    {
        #region Private Variables

        private readonly ITemplateService templateService;

        #endregion

        #region Constructors

        public TemplateController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        #endregion

        #region Action Methods

        #region Get

        [HttpGet(template: "GetTemplates")]
        public async Task<IActionResult> GetTemplates(int tenantId)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await templateService.GetTemplates(tenantId: tenantId,
                                                              userProfileId: userProfileId);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "GetDocuments")]
        public async Task<IActionResult> GetDocument(string id)
        {
            //todo Validations User Profile id | tenant id missing plz verify
            var docQuery = await templateService.GetDocument(id: id);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "GetCategoryDocument")]
        public async Task<IActionResult> GetCategoryDocument(int tenantId)
        {
            var docQuery = await templateService.GetCategoryDocument(tenantId: tenantId);
            return Ok(value: docQuery);
        }

        #endregion

        #region Post

        //todo Validations post actions shud receive information in ViewModels and validated
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> RenameTemplate(string id,
                                                        int tenantId,
                                                        string name)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await templateService.RenameTemplate(id: id,
                                                                tenantid: tenantId,
                                                                newname: name,
                                                                userProfileId: userProfileId);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPost(template: "[action]")]
        public async Task<IActionResult> InsertTemplate(InsertTemplateModel insertTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            var docQuery = await templateService.InsertTemplate(tenantId: insertTemplateModel.tenantId,
                                                                userProfileId: userProfileId,
                                                                name: insertTemplateModel.name);

            return Ok(value: docQuery);
        }


        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddDocument(AddDocumentModel addDocumentModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            var docQuery = await templateService.AddDocument(templateId: addDocumentModel.templateId,
                                                             tenantId: addDocumentModel.tenantId,
                                                             userProfileId: userProfileId,
                                                             typeId: addDocumentModel.typeId,
                                                             docName: addDocumentModel.docName);

            if (docQuery)
                return Ok();
            return NotFound();
        }


        [HttpPost(template: "[action]")]
        public async Task<IActionResult> SaveTemplate(AddTemplateModel model)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            return Ok(value: await templateService.SaveTemplate(model: model,
                                                                userProfileId: userProfileId));
        }

        #endregion

        #region Delete

        //todo Validations delete actions shud receive information in ViewModels and validated
        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteDocument(string id,
                                                        int tenantId,
                                                        string documentId)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await templateService.DeleteDocument(id: id,
                                                                tenantid: tenantId,
                                                                documentid: documentId,
                                                                userProfileId: userProfileId);
            if (docQuery)
                return Ok();
            return NotFound();
        }


        //todo Validations delete actions shud receive information in ViewModels and validated
        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteTemplate(string templateId,
                                                        int tenantId)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await templateService.DeleteTemplate(templateId: templateId,
                                                                tenantId: tenantId,
                                                                userProfileId: userProfileId);
            if (docQuery)
                return Ok();
            return NotFound();
        }

        #endregion

        #endregion
    }
}