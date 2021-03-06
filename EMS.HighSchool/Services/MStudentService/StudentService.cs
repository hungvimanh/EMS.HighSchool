﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories;

namespace EMS.HighSchool.Services.MStudentService
{
    public interface IStudentService : IServiceScoped
    {
        Task<Student> Register(Student student);
        Task<Student> Update(Student student);
        Task<Student> UploadAvatar(Student student);
        Task<Student> MarkInput(Student student);
        Task<Student> ViewMark();
        Task<bool> ImportExcel(byte[] file);
        Task<Student> Get();
        Task<Student> GetById(long Id);
        Task<Student> GetByIdentify(string Identify);
        Task<List<Student>> List(StudentFilter studentFilter);
        Task<Student> Delete(Student student);
    }
    public class StudentService : IStudentService
    {
        private readonly IUOW UOW;
        private IStudentValidator StudentValidator;
        private ICurrentContext CurrentContext;
        public StudentService(IUOW UOW, IStudentValidator StudentValidator, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.StudentValidator = StudentValidator;
            this.CurrentContext = CurrentContext;
        }
        #region Register
        public async Task<Student> Register(Student student)
        {
            if (!await StudentValidator.Create(student))
                return student;

            try
            {
                await UOW.Begin();
                
                User user = new User()
                {
                    Username = student.Identify,
                    Password = CryptographyExtentions.GeneratePassword(),
                    IsAdmin = false,
                    Email = student.Email
                };
                student.User = user;
                await UOW.StudentRepository.Create(student);

                await UOW.Commit();
                await Utils.RegisterMail(user);
                return await UOW.StudentRepository.Get(student.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
        #endregion

        #region Update Profile/Mark
        public async Task<Student> Update(Student student)
        {
            //student Id sẽ được gán bằng Id của thí sinh đang truy cập hệ thống
            student.Id = CurrentContext.StudentId;
            if (!await StudentValidator.Update(student))
                return student;

            //Cập nhật thông tin thí sinh
            try
            {
                await UOW.Begin();
                await UOW.StudentRepository.Update(student);
                await UOW.Commit();
                return await UOW.StudentRepository.Get(student.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Student> MarkInput(Student student)
        {
            if (!await StudentValidator.Update(student))
                return student;
            //Nhập điểm thi cho thí sinh
            try
            {
                await UOW.Begin();
                await UOW.StudentRepository.MarkInput(student);
                await UOW.Commit();
                return await UOW.StudentRepository.Get(student.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Student> UploadAvatar(Student student)
        {
            //student Id sẽ được gán bằng Id của thí sinh đang truy cập hệ thống
            student.Id = CurrentContext.StudentId;
            if (!await StudentValidator.Update(student))
                return student;
            try
            {
                await UOW.Begin();
                await UOW.StudentRepository.UploadAvatar(student);
                await UOW.Commit();
                return await UOW.StudentRepository.Get(student.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
        #endregion

        #region Import From Excel
        public async Task<bool> ImportExcel(byte[] file)
        {
            //Tạo nhiều tài khoản thí sinh từ file excel
            List<Student> students = await LoadFromExcel(file);
            try
            {
                await UOW.Begin();
                List<User> users = new List<User>();
                foreach (var student in students)
                {
                    User user = new User()
                    {
                        Username = student.Identify,
                        Password = CryptographyExtentions.GeneratePassword(),
                        IsAdmin = false,
                        StudentId = student.Id,
                        Email = student.Email
                    };
                    users.Add(user);
                }

                await UOW.StudentRepository.BulkInsert(students);
                await UOW.UserRepository.BulkInsert(users);
                await UOW.Commit();
                users.ForEach(u => Utils.RegisterMail(u));
                return true;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw ex;
            }
        }

        private async Task<List<Student>> LoadFromExcel(byte[] file)
        {
            //Đọc file excel
            List<Student> excelTemplates = new List<Student>();
            using (MemoryStream ms = new MemoryStream(file))
            using (var package = new ExcelPackage(ms))
            {
                //Thông tin thí sinh phải ở sheet đầu tiên của file excel
                //Cột 1: Họ và tên
                //Cột 2: Ngày tháng năm sinh
                //Cột 3: Giới tính ( 1:Nam/0:Nữ)
                //Cột 4: Số CMND
                //Cột 5: Số điện thoại
                //Cột 6: địa chỉ Email
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    Student excelTemplate = new Student()
                    {
                        Name = worksheet.Cells[i, 1].Value?.ToString(),
                        Dob = DateTime.Parse(worksheet.Cells[i, 2].Value?.ToString()),
                        Gender = worksheet.Cells[i, 3].Value.Equals("1"),
                        Identify = worksheet.Cells[i, 4].Value?.ToString(),
                        Phone = worksheet.Cells[i, 5].Value?.ToString(),
                        Email = worksheet.Cells[i, 6].Value?.ToString(),

                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }
        #endregion

        #region Read
        public async Task<Student> Get()
        {
            return await UOW.StudentRepository.Get(CurrentContext.StudentId);
        }

        public async Task<Student> GetById(long Id)
        {
            return await UOW.StudentRepository.Get(Id);
        }

        public async Task<Student> GetByIdentify(string Identify)
        {
            if (string.IsNullOrEmpty(Identify)) return null;
            return await UOW.StudentRepository.GetByIdentify(Identify);
        }

        public async Task<List<Student>> List(StudentFilter studentFilter)
        {
            return await UOW.StudentRepository.List(studentFilter);
        }

        public async Task<Student> ViewMark()
        {
            
            var student = await UOW.StudentRepository.Get(CurrentContext.StudentId);
            if (!await StudentValidator.ViewMark(student))
            {
                return student;
            }
            //Nếu thí sinh chưa tốt nghiệp THPT
            //Tính điểm tốt nghiệp
            if (!student.Graduated.HasValue || (student.Graduated.HasValue && !student.Graduated.Value))
            {
                student.GraduationMark = await GraduationMarkCalculate(student);
            }
            return student;
        }
        #endregion

        #region Delete
        public async Task<Student> Delete(Student student)
        {
            if (!await StudentValidator.Delete(student))
                return student;

            try
            {
                await UOW.Begin();
                await UOW.StudentRepository.Delete(student.Id);
                await UOW.Commit();
                return student;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
        #endregion

        private async Task<double> GraduationMarkCalculate(Student student)
        {
            //Điểm tốt nghiệp được tính theo công thức
            //Điểm môn tổ hợp = (Lý + Hoá + Sinh)/3 hoặc (Sử + Địa + GDCD)/3  => lấy điểm của tổ hợp cao nhất
            double mark = 0;
            var NaturalSciences = (student.Physics + student.Chemistry + student.Biology) / 3;
            var SocialSciences = (student.History + student.Geography + student.CivicEducation) / 3;
            if(NaturalSciences.HasValue && SocialSciences.HasValue)
            {
                mark = NaturalSciences.Value > SocialSciences.Value ? NaturalSciences.Value : SocialSciences.Value;
            }

            if(NaturalSciences.HasValue && !SocialSciences.HasValue)
            {
                mark = NaturalSciences.Value;
            }

            if (!NaturalSciences.HasValue && SocialSciences.HasValue)
            {
                mark = SocialSciences.Value;
            }
            //Điểm tốt nghiệp = (Toán + Văn + Ngoại ngữ + điểm tổ hợp môn)/4
            mark = (mark + student.Maths.Value + student.Literature.Value + student.Languages.Value) / 4;
            //Nếu thí sinh là dân tộc thiểu số sẽ được cộng thêm 1 điểm
            if (!string.IsNullOrEmpty(student.EthnicCode) && !student.EthnicCode.Equals("01")) mark += 1;
            //Tổng điểm sau khi tính >=5 đủ điều kiện tốt nghiệp
            return mark;
        }

        
    }
}
