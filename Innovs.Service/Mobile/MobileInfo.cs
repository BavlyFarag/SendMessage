using Innovs.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Innovs.Service.Mobile
{
    public class MobileInfo
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();
        protected Repository<Core.Data.MobileInfo> mobileInfoRepository;

        public MobileInfo()
        {
            mobileInfoRepository = unitOfWork.Repository<Core.Data.MobileInfo>();
        }

        #region MobileInfo
        public Core.Data.MobileInfo GetMobileInfoById(int id)
        {
            var mobileInfo = mobileInfoRepository.Table.Where(c => c.Id == id).FirstOrDefault();
            return mobileInfo;
        }
        public bool MobileInfoAdd(Core.Data.MobileInfo model)
        {
            try
            {
                mobileInfoRepository.Insert(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Core.Data.MobileInfo MobileInfoEdit(Core.Data.MobileInfo model)
        {

            var newMobileInfo = mobileInfoRepository.GetById(model.Id);
            foreach (var item in model.GetType().GetProperties())
            {
                foreach (var item2 in newMobileInfo.GetType().GetProperties())
                {
                    if (item.GetValue(model) != null)
                    {
                        if (item2.Name == item.Name)
                        {
                            item2.SetValue(newMobileInfo, item.GetValue(model));
                        }
                    }
                }
            }

            mobileInfoRepository.Update(newMobileInfo);
            return newMobileInfo;
        }

        public IQueryable<Core.Data.MobileInfo> GetAllMobileInfo()
        {
            var mobileInfos = mobileInfoRepository.Table.OrderByDescending(c => c.Id).AsQueryable();
            return mobileInfos;
        }
        public IQueryable<Core.Data.MobileInfo> SearchFor(Expression<Func<Core.Data.MobileInfo, bool>> predicate)
        {
            return mobileInfoRepository.SearchFor(predicate);
        }

        #endregion
    }
}
