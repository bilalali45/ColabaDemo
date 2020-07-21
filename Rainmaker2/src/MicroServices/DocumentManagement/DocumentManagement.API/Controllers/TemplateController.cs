using System.Threading.Tasks;
using DocumentManagement.Model;
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
        public async Task<IActionResult> GetTemplates([FromQuery] GetTemplates moGetTemplates)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await templateService.GetTemplates(tenantId: moGetTemplates.tenantId,
                                                              userProfileId: userProfileId);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "GetDocuments")]
        public async Task<IActionResult> GetDocument([FromQuery] GetTemplateDocuments moGetTemplateDocuments)
        {
           
            var docQuery = await templateService.GetDocument(id: moGetTemplateDocuments.id);
            return Ok(value: docQuery);
        }


        [HttpGet(template: "GetCategoryDocument")]
        public async Task<IActionResult> GetCategoryDocument([FromQuery] GetCategoryDocument moGetCategoryDocument)
        {
            var docQuery = await templateService.GetCategoryDocument(tenantId: moGetCategoryDocument.tenantId);
            return Ok(value: docQuery);
        }

        #endregion

        #region Post

     
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> RenameTemplate(RenameTemplateModel renameTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await templateService.RenameTemplate(id: renameTemplateModel.id,
                                                                tenantid: renameTemplateModel.tenantId,
                                                                newname: renameTemplateModel.name,
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

         
        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteDocument(DeleteDocumentModel deleteDocumentModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await templateService.DeleteDocument(id: deleteDocumentModel.id,
                                                                tenantid: deleteDocumentModel.tenantId,
                                                                documentid: deleteDocumentModel.documentId,
                                                                userProfileId: userProfileId);
            if (docQuery)
                return Ok();
            return NotFound();
        }


         
        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteTemplate(DeleteTemplateModel deleteTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await templateService.DeleteTemplate(templateId: deleteTemplateModel.templateId,
                                                                tenantId: deleteTemplateModel.tenantId,
                                                                userProfileId: userProfileId);
            if (docQuery)
                return Ok();
            return NotFound();
        }

        #endregion

        #endregion
    }
}