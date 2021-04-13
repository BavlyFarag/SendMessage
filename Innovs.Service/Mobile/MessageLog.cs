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
    public class MessageLog
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();
        protected Repository<Core.Data.MessageLog> messageLogRepository;

        public MessageLog()
        {
            messageLogRepository = unitOfWork.Repository<Core.Data.MessageLog>();
        }

        #region MessageLog
        public Core.Data.MessageLog GetMessageLogById(int id)
        {
            var messageLog = messageLogRepository.Table.Where(c => c.Id == id).FirstOrDefault();
            return messageLog;
        }
        public bool MessageLogAdd(Core.Data.MessageLog model)
        {
            try
            {
                messageLogRepository.Insert(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Core.Data.MessageLog MessageLogEdit(Core.Data.MessageLog model)
        {

            var newMessageLog = messageLogRepository.GetById(model.Id);
            foreach (var item in model.GetType().GetProperties())
            {
                foreach (var item2 in newMessageLog.GetType().GetProperties())
                {
                    if (item.GetValue(model) != null)
                    {
                        if (item2.Name == item.Name)
                        {
                            item2.SetValue(newMessageLog, item.GetValue(model));
                        }
                    }
                }
            }

            messageLogRepository.Update(newMessageLog);
            return newMessageLog;
        }

        public IQueryable<Core.Data.MessageLog> GetAllMessageLog()
        {
            var messageLogs = messageLogRepository.Table.OrderByDescending(c => c.Id).AsQueryable();
            return messageLogs;
        }
        public IQueryable<Core.Data.MessageLog> SearchFor(Expression<Func<Core.Data.MessageLog, bool>> predicate)
        {
            return messageLogRepository.SearchFor(predicate);
        }

        #endregion
    }
}
