using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;

namespace EMS.HighSchool.Services.MForm
{
    public interface IFormService : IServiceScoped
    {
        Task<RegisterExam> ApproveAccept(RegisterExam form);
        Task<RegisterExam> ApproveDeny(RegisterExam form);
        Task<RegisterExam> Save(RegisterExam form);
        Task<RegisterExam> Get(long StudentId);
        Task<RegisterExam> Delete(RegisterExam form);
    }
    public class FormService : IFormService
    {
        private readonly IUOW UOW;
        private readonly IFormValidator FormValidator;
        private readonly ICurrentContext CurrentContext;

        public FormService(IUOW UOW, IFormValidator FormValidator, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.FormValidator = FormValidator;
            this.CurrentContext = CurrentContext;
        }

        public async Task<RegisterExam> ApproveAccept(RegisterExam form)
        {
            if(!await FormValidator.Approve(form))
                return form;

            try
            {
                await UOW.Begin();
                //Chuyển trạng thái từ chờ duyệt sang đã duyệt
                form.Status = 2;
                await UOW.FormRepository.Approve(form);
                await UOW.StudentRepository.UpdateStatus(new Student { Id = form.StudentId, Status = form.Status });
                await UOW.Commit();
                return await Get(form.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<RegisterExam> ApproveDeny(RegisterExam form)
        {
            if (!await FormValidator.Approve(form))
                return form;

            try
            {
                await UOW.Begin();
                //Chuyển trạng thái từ chờ duyệt sang đã từ chối
                form.Status = 3;
                await UOW.FormRepository.Approve(form);
                await UOW.StudentRepository.UpdateStatus(new Student { Id = form.StudentId, Status = form.Status });
                await UOW.Commit();
                return await Get(form.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<RegisterExam> Save(RegisterExam form)
        {
            //Nếu Form chưa tồn tại với user
            //Tạo mới form
            //Update nếu form đã tồn tại
            if (!await FormValidator.IsExisted(form))
                return await Create(form);
            return await Update(form);
        }

        private async Task<RegisterExam> Create(RegisterExam form)
        {
            if(!await FormValidator.Save(form))
                return form;

            try
            {
                await UOW.Begin();
                //Chuyển trạng thái từ chưa đăng ký sang chờ duyệt
                form.Status = 1;
                await UOW.FormRepository.Create(form);
                await UOW.StudentRepository.UpdateStatus(new Student { Id = CurrentContext.StudentId, Status = form.Status });
                await UOW.Commit();
                return await Get(CurrentContext.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<RegisterExam> Get(long StudentId)
        {
            RegisterExam form = await UOW.FormRepository.Get(StudentId);
            return form;
        }

        private async Task<RegisterExam> Update(RegisterExam form)
        {
            if (!await FormValidator.Save(form))
                return form;

            try
            {
                await UOW.Begin();
                //Chuyển trạng thái về chờ duyệt
                //Học sinh có thể cập nhật phiếu đăng ký dự thi kể cả khi phiếu đã được duyệt
                //Khi đó trạng thái sẽ chuyển từ đã duyệt về chờ duyệt
                form.Status = 1;
                await UOW.FormRepository.Update(form);
                await UOW.StudentRepository.UpdateStatus(new Student { Id = CurrentContext.StudentId, Status = 1 });
                await UOW.Commit();
                return await Get(CurrentContext.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<RegisterExam> Delete(RegisterExam form)
        {
            if (!await FormValidator.Delete(form))
                return form;

            try
            {
                await UOW.Begin();
                await UOW.FormRepository.Delete(form.Id);
                //Chuyển trạng thái về chưa đăng ký
                await UOW.StudentRepository.UpdateStatus(new Student { Id = CurrentContext.StudentId, Status = 0 });
                await UOW.Commit();
                return await Get(CurrentContext.StudentId);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
