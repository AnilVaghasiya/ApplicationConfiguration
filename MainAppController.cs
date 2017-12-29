using GodrejMaterialDAL.Model;
using GodrejMaterialDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GodrejPortal.Areas.admin.Controllers
{
    public class MainAppController : Controller
    {
        // GET: admin/MainApp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProductCategories()
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstProductCategory = _repository.GetAllProductCategories().ToList();
                res["success"] = 1;
                res["ListProductCategory"] = lstProductCategory;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductModelbyPCategoryId(int ProductCategoryId)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstProductModel = _repository.GetAllProductModelByCategoryId(ProductCategoryId).ToList();
                res["success"] = 1;
                res["ListProductModel"] = lstProductModel;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStructures()
        {
            List<vmTree> lstTree = new List<vmTree>();
            try
            {
                MembershipUser _membershipUser = Membership.GetUser(User.Identity.Name);
                Guid uid = Guid.Parse(_membershipUser.ProviderUserKey.ToString());
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetAllModels(uid).ToList();

                vmTree ovmTree;
                foreach (var itemModel in lstData)
                {
                    ovmTree = new vmTree();
                    ovmTree.folder = true;
                    ovmTree.title = itemModel.ModelName;
                    ovmTree.key = itemModel.ModelId.ToString();
                    ovmTree.children = new List<vmTree>();

                    vmTree ovmTreeManual;
                    foreach (var itemManual in _repository.GetAllManualStructureByModelId(itemModel.ModelId).ToList())
                    {
                        ovmTreeManual = new vmTree();
                        ovmTreeManual.title = itemManual.StructureName;
                        ovmTreeManual.key = itemManual.ManualStructueId.ToString();
                        ovmTreeManual.children = new List<vmTree>();
                        ovmTree.children.Add(ovmTreeManual);

                        vmTree ovmTreeSubManual;
                        foreach (var itemSubManual in _repository.GetAllSubManualStructureByManualStructId(itemManual.ManualStructueId).ToList())
                        {
                            ovmTreeSubManual = new vmTree();
                            ovmTreeSubManual.title = itemSubManual.SubManualStructureName;
                            ovmTreeSubManual.key = itemSubManual.SubManualStructureId.ToString();
                            ovmTreeSubManual.children = new List<vmTree>();
                            ovmTreeManual.children.Add(ovmTreeSubManual);
                        }
                    }

                    lstTree.Add(ovmTree);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(lstTree, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllManualStructureByModelId(int ModelId)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetAllManualStructureByModelId(ModelId).ToList();
                res["success"] = 1;
                res["ListManuals"] = lstData;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSubManualStructureByManualStructId(int ManualStructueId)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetAllSubManualStructureByManualStructId(ManualStructueId).ToList();
                res["success"] = 1;
                res["ListSubManuals"] = lstData;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetSvgFilesByManualStructueId(int ManualStructueId)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetSvgFilesByManualStructueId(ManualStructueId).ToList();
                res["success"] = 1;
                res["ListSvgFiles"] = lstData;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSvgFilesBySubManualStructureId(int SubManualStructureId)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetSvgFilesBySubManualStructureId(SubManualStructureId).ToList();
                res["success"] = 1;
                res["ListSvgFiles"] = lstData;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGridDatabyManualStructureId(int ManualStructueId)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetDatagridByManualStructueId(ManualStructueId).ToList();
                res["success"] = 1;
                res["ListDataGrid"] = lstData;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataGridBySubManualStructureId(int SubManualStructureId)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetDataGridBySubManualStructureId(SubManualStructureId).ToList();
                res["success"] = 1;
                res["ListDataGrid"] = lstData;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSvgFiles()
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetAllSvgFiles().ToList();
                res["success"] = 1;
                res["ListSvgFiles"] = lstData;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGridDatabySVGFileId(int SvgFileId = 0)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            try
            {
                var _repository = CommonRepository.Instance;
                var lstData = _repository.GetGridDatabySVGFileId(SvgFileId).ToList();
                res["success"] = 1;
                res["ListDataGrid"] = lstData;
            }
            catch (Exception ex)
            {
                res["error"] = 2;
                res["message"] = ex.Message.ToString();
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}