using GodrejMaterialDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodrejMaterialDAL.Repository
{
    public sealed class CommonRepository
    {
        private static IDA_db_godrejEntities dbContext;

        private static readonly Lazy<CommonRepository> Repository = new Lazy<CommonRepository>(() => new CommonRepository());
        private CommonRepository()
        {
            dbContext = DBContextRepository.Instance;
        }

        public static CommonRepository Instance
        {
            get
            {
                return Repository.Value;
            }
        }

        public IEnumerable<vmManualStruct> GetAllManualStructureByModelId(int ModelId)
        {
            List<IDA_ManualStructure> lst = dbContext.IDA_ManualStructure.Where(c => c.ModelId == ModelId).ToList();

            return lst.Select(x => new vmManualStruct()
            {
                ModelId = x.ModelId,
                ModifiedDateTime = x.ModifiedDateTime,
                IsActive = x.IsActive,
                CreatedDateTime = x.CreatedDateTime,
                ManualStructueId = x.ManualStructueId,
                StructureName = x.StructureName,
                SerialNo = x.SerialNo,
                ChasisNo = x.ChasisNo,
                ProjectNo = x.ProjectNo,
                CustomerCode = x.CustomerCode
            });
        }

        public IEnumerable<vmSubManualStruct> GetAllSubManualStructureByManualStructId(int ManualStructId)
        {
            List<IDA_SubManualStructure> lst = dbContext.IDA_SubManualStructure.Where(c => c.ManualStructueId == ManualStructId).ToList();

            return lst.Select(x => new vmSubManualStruct()
            {
                ManualStructueId = x.ManualStructueId,
                CreatedDateTime = x.CreatedDateTime,
                IsActive = x.IsActive,
                ModifiedDateTime = x.ModifiedDateTime,
                SubManualStructureId = x.SubManualStructureId,
                SubManualStructureName = x.SubManualStructureName,
                SubManualStructureParentId = x.SubManualStructureParentId
            });
        }

        public IEnumerable<vmModels> GetAllModels(Guid userid)
        {
            List<IDA_Models> lst = dbContext.IDA_Models.Where(x => x.MembershipUserId == userid).ToList();

            return lst.Select(x => new vmModels()
            {
                ModelId = x.ModelId,
                CreatedDateTime = x.CreatedDateTime,
                IsActive = x.IsActive,
                ModelName = x.ModelName,
                ModifiedDateTime = x.ModifiedDateTime
            });
        }

        public IEnumerable<vmUsers> GetCustomerTypeUsers()
        {
            List<IDA_Customers> lst = dbContext.IDA_Customers.Where(c => c.TypeOfUser.ToLower() == "customer" && c.TypeOfUser != null).ToList();

            return lst.Select(x => new vmUsers()
            {
                CustomerId = x.CustomerId,
                MembershipUserId = x.MembershipUserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                CustomerCode = x.CustomerCode,
                CustomerName = x.CustomerName,
                Branch = x.Branch,
                Address = x.Address
            });
        }

        public IEnumerable<vmManualStruct> GetProjects()
        {
            List<IDA_ManualStructure> lst = dbContext.IDA_ManualStructure.Where(x => x.ProjectNo != null).ToList();

            return lst.Select(x => new vmManualStruct()
            {
                ModelId = x.ModelId,
                ManualStructueId = x.ManualStructueId,
                SerialNo = x.SerialNo,
                ChasisNo = x.ChasisNo,
                ProjectNo = x.ProjectNo,
                CustomerCode = x.CustomerCode,
                ModifiedDateTime = x.ModifiedDateTime,
                IsActive = x.IsActive,
                CreatedDateTime = x.CreatedDateTime
            });
        }

        public IEnumerable<vmProductCategories> GetAllProductCategories()
        {
            List<IDA_ProductCategories> lstProductCategory = dbContext.IDA_ProductCategories.ToList();

            return lstProductCategory.Select(x => new vmProductCategories()
            {
                ProductCategoryId = x.ProdCatId,
                ProductCategoryName = x.ProdCatName,

            });
        }

        public IEnumerable<vmProductModel> GetAllProductModelByCategoryId(int ProductCategoryId)
        {
            List<IDA_ProductModels> lstProductModel = dbContext.IDA_ProductModels.Where(x => x.ProdCatId == ProductCategoryId).ToList();

            return lstProductModel.Select(x => new vmProductModel()
            {
                ProductModelId = x.ProdModelId,
                ProductModelName = x.ProdModelName,

            });
        }

        public IEnumerable<vmSvgFiles> GetSvgFilesByManualStructueId(int ManualStructueId)
        {
            List<IDA_SvgFiles> lst = dbContext.IDA_SvgFiles.Where(c => c.ManualStructueId == ManualStructueId).ToList();

            return lst.Select(x => new vmSvgFiles()
            {
                ManualStructueId = x.ManualStructueId,
                SubManualStructureId = x.SubManualStructureId,
                SvgFileId = x.SvgFileId,
                SvgFilePath = x.SvgFilePath
            });
        }

        public IEnumerable<vmSvgFiles> GetSvgFilesBySubManualStructureId(int SubManualStructureId)
        {
            List<IDA_SvgFiles> lst = dbContext.IDA_SvgFiles.Where(c => c.SubManualStructureId == SubManualStructureId).ToList();

            return lst.Select(x => new vmSvgFiles()
            {
                ManualStructueId = x.ManualStructueId,
                SubManualStructureId = x.SubManualStructureId,
                SvgFileId = x.SvgFileId,
                SvgFilePath = x.SvgFilePath
            });
        }
        public IEnumerable<vmSvgFiles> GetAllSvgFiles()
        {
            List<IDA_SvgFiles> lst = dbContext.IDA_SvgFiles.ToList();

            return lst.Select(x => new vmSvgFiles()
            {
                ManualStructueId = x.ManualStructueId,
                SubManualStructureId = x.SubManualStructureId,
                SvgFileId = x.SvgFileId,
                SvgFilePath = x.SvgFilePath
            });
        }

        public IQueryable<IDA_Customers> GetAllCustomers()
        {
            return dbContext.IDA_Customers.AsQueryable();
        }

        public IEnumerable<vmDatagrid> GetDatagridByManualStructueId(int ManualStructueId)
        {
            List<IDA_GridData> lst = dbContext.IDA_GridData.Where(c => c.ManualStructueId == ManualStructueId).ToList();

            return lst.Select(x => new vmDatagrid()
            {
                ManualStructueId = x.ManualStructueId,
                SubManualStructureId = x.SubManualStructureId,
                SerialNo = Convert.ToString(x.SerialNo),
                ItemCode = x.ItemCode,
                UNSPC = x.UNSPC,
                PartDescription = x.PartDescription,
                Qty = x.Qty,
                Notes = x.Notes
            });
        }

        public IEnumerable<vmDatagrid> GetDataGridBySubManualStructureId(int SubManualStructureId)
        {
            List<IDA_GridData> lst = dbContext.IDA_GridData.Where(c => c.SubManualStructureId == SubManualStructureId).ToList();

            return lst.Select(x => new vmDatagrid()
            {
                ManualStructueId = x.ManualStructueId,
                SubManualStructureId = x.SubManualStructureId,
                SerialNo = Convert.ToString(x.SerialNo),
                ItemCode = x.ItemCode,
                UNSPC = x.UNSPC,
                PartDescription = x.PartDescription,
                Qty = x.Qty,
                Notes = x.Notes
            });
        }

        public IEnumerable<vmDatagrid> GetGridDatabySVGFileId(int SvgFileId)
        {
            List<IDA_GridData> lst = dbContext.IDA_GridData.Where(c => c.SvgFileId == SvgFileId).ToList();

            return lst.Select(x => new vmDatagrid()
            {
                ManualStructueId = x.ManualStructueId,
                SvgFileId = x.SvgFileId,
                SubManualStructureId = x.SubManualStructureId,
                SerialNo = Convert.ToString(x.SerialNo),
                ItemCode = x.ItemCode,
                UNSPC = x.UNSPC,
                PartDescription = x.PartDescription,
                Qty = x.Qty,
                Notes = x.Notes
            });
        }

        public int GetManualStructureId(string ProjectNo, string CustomerCode, string txtSerialNo, string txtChasisNo)
        {

            int ManualStructureId = 0;
            if (!string.IsNullOrEmpty(ProjectNo))
            {
                ManualStructureId = dbContext.IDA_ManualStructure.Where(c => c.ProjectNo == ProjectNo).FirstOrDefault().ManualStructueId;
            }
            else if (!string.IsNullOrEmpty(CustomerCode))
            {
                ManualStructureId = dbContext.IDA_ManualStructure.Where(c => c.CustomerCode == CustomerCode).FirstOrDefault().ManualStructueId;
            }
            else if (!string.IsNullOrEmpty(txtSerialNo))
            {
                ManualStructureId = dbContext.IDA_ManualStructure.Where(c => c.SerialNo == txtSerialNo).FirstOrDefault().ManualStructueId;
            }
            else if (!string.IsNullOrEmpty(txtChasisNo))
            {
                ManualStructureId = dbContext.IDA_ManualStructure.Where(c => c.ChasisNo == txtChasisNo).FirstOrDefault().ManualStructueId;
            }
            var MasterGenericBomID = dbContext.IDA_MasterGenericBOM
                                        .Where(c => c.Chapter.ToLower() == dbContext
                                                                        .IDA_ManualStructure
                                                                        .Where(x => x.ManualStructueId == ManualStructureId)
                                                                        .FirstOrDefault()
                                                                        .StructureName).ToList();
            foreach (var objMasterGenericBom in MasterGenericBomID)
            {
                var GenericIllustrationResult = dbContext.IDA_GenericBOM.Where(c => c.MasterGenericBomID == objMasterGenericBom.MasterGenericBomID).FirstOrDefault().Illustration;
                var GenericBomResult = dbContext.IDA_GenericBOM.Where(c => c.MasterGenericBomID == objMasterGenericBom.MasterGenericBomID).FirstOrDefault().BOM;
            }

            return ManualStructureId;
        }
    }
}
