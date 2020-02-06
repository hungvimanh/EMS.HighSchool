using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.HighSchool.Controller.DTO;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Services.MForm;
using EMS.HighSchool.Services.MStudentService;
using EMS.HighSchool.Services.MUniversity_Majors_Majors;

namespace EMS.HighSchool.Controller.form
{
    public class FormController : ApiController
    {
        private IFormService FormService;
        private IStudentService StudentService;
        public FormController( IFormService formService, IStudentService studentService )
        {
            FormService = formService;
            StudentService = studentService;
        }

        #region Save
        [Route(StudentRoute.SaveForm), HttpPost]
        public async Task<ActionResult<DTO.RegisterExam>> Save([FromBody] DTO.RegisterExam formDTO)
        {
            if (formDTO == null) formDTO = new DTO.RegisterExam();
            Entities.RegisterExam form = await ConvertDTOtoBO(formDTO);
            Student student = new Student
            {
                Id = formDTO.StudentId,
                Dob = formDTO.Dob,
                Name = formDTO.Name,
                Gender = formDTO.Gender,
                Identify = formDTO.Identify,
                Phone = formDTO.Phone,
                Address = formDTO.Address,
                EthnicId = formDTO.EthnicId,
                HighSchoolId = formDTO.HighSchoolId,
                PlaceOfBirth = formDTO.PlaceOfBirth,
                ProvinceId = formDTO.ProvinceId,
                DistrictId = formDTO.DistrictId,
                TownId = formDTO.TownId
            };
            
            form = await FormService.Save(form);
            student.Id = form.StudentId;
            student = await StudentService.Update(student);
            formDTO = new DTO.RegisterExam
            {
                Id = form.Id,
                StudentId = form.StudentId,
                Address = student.Address,
                Dob = student.Dob.Date,
                Gender = student.Gender,
                Email = student.Email,
                Identify = student.Identify,
                PlaceOfBirth = student.PlaceOfBirth,
                Name = student.Name,
                Phone = student.Phone,
                EthnicId = student.EthnicId,
                EthnicName = student.EthnicName,
                EthnicCode = student.EthnicCode,
                HighSchoolId = student.HighSchoolId,
                HighSchoolName = student.HighSchoolName,
                HighSchoolCode = student.HighSchoolCode,
                TownId = student.TownId,
                TownCode = student.TownCode,
                TownName = student.TownName,
                DistrictId = student.DistrictId,
                DistrictCode = student.DistrictCode,
                DistrictName = student.DistrictName,
                ProvinceId = student.ProvinceId,
                ProvinceCode = student.ProvinceCode,
                ProvinceName = student.ProvinceName,
                ClusterContestId = form.ClusterContestId,
                ClusterContestCode = form.ClusterContestCode,
                ClusterContestName = form.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterPlaceOfExamName,
                Biology = form.Biology,
                Chemistry = form.Chemistry,
                CivicEducation = form.CivicEducation,
                Geography = form.Geography,
                History = form.History,
                Languages = form.Languages,
                Literature = form.Literature,
                Maths = form.Maths,
                NaturalSciences = form.NaturalSciences,
                Physics = form.Physics,
                SocialSciences = form.SocialSciences,
                Graduated = form.Graduated,
                
                ExceptLanguages = form.ExceptLanguages,
                Mark = form.Mark,
                ReserveBiology = form.ReserveBiology,
                ReserveChemistry = form.ReserveChemistry,
                ReserveCivicEducation = form.ReserveCivicEducation,
                ReserveGeography = form.ReserveGeography,
                ReserveHistory = form.ReserveHistory,
                ReserveLanguages = form.ReserveLanguages,
                ReserveLiterature = form.ReserveLiterature,
                ReserveMaths = form.ReserveMaths,
                ReservePhysics = form.ReservePhysics,

                Area = form.Area,
                PriorityType = form.PriorityType,
                Status = form.Status,
                Aspirations = form.Aspirations.Select(m => new AspirationDTO
                {
                    Id = m.Id,
                    FormId = m.RegisterExamId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName,
                    Sequence = m.Sequence
                }).ToList(),
                Errors = form.Errors
            };

            if (form.HasError)
            {
                return BadRequest(formDTO);
            }
            return Ok(formDTO);
        }
        #endregion

        #region Get
        [Route(StudentRoute.GetForm), HttpPost]
        public async Task<DTO.RegisterExam> Get()
        {
            Student student = await StudentService.Get();

            Entities.RegisterExam form = new Entities.RegisterExam { StudentId = student.Id };

            form = await FormService.Get(form.StudentId);
            return form == null ? null : new DTO.RegisterExam
            {
                Id = form.Id,
                StudentId = form.StudentId,
                Address = student.Address,
                Dob = student.Dob.Date,
                Gender = student.Gender,
                Email = student.Email,
                Identify = student.Identify,
                PlaceOfBirth = student.PlaceOfBirth,
                Name = student.Name,
                Phone = student.Phone,
                EthnicId = student.EthnicId,
                EthnicName = student.EthnicName,
                EthnicCode = student.EthnicCode,
                HighSchoolId = student.HighSchoolId,
                HighSchoolName = student.HighSchoolName,
                HighSchoolCode = student.HighSchoolCode,
                TownId = student.TownId,
                TownCode = student.TownCode,
                TownName = student.TownName,
                DistrictId = student.DistrictId,
                DistrictCode = student.DistrictCode,
                DistrictName = student.DistrictName,
                ProvinceId = student.ProvinceId,
                ProvinceCode = student.ProvinceCode,
                ProvinceName = student.ProvinceName,
                ClusterContestId = form.ClusterContestId,
                ClusterContestCode = form.ClusterContestCode,
                ClusterContestName = form.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterPlaceOfExamName,
                Biology = form.Biology,
                Chemistry = form.Chemistry,
                CivicEducation = form.CivicEducation,
                Geography = form.Geography,
                History = form.History,
                Languages = form.Languages,
                Literature = form.Literature,
                Maths = form.Maths,
                NaturalSciences = form.NaturalSciences,
                Physics = form.Physics,
                SocialSciences = form.SocialSciences,
                Graduated = form.Graduated,

                ExceptLanguages = form.ExceptLanguages,
                Mark = form.Mark,
                ReserveBiology = form.ReserveBiology,
                ReserveChemistry = form.ReserveChemistry,
                ReserveCivicEducation = form.ReserveCivicEducation,
                ReserveGeography = form.ReserveGeography,
                ReserveHistory = form.ReserveHistory,
                ReserveLanguages = form.ReserveLanguages,
                ReserveLiterature = form.ReserveLiterature,
                ReserveMaths = form.ReserveMaths,
                ReservePhysics = form.ReservePhysics,

                Area = form.Area,
                PriorityType = form.PriorityType,
                Status = form.Status,
                Aspirations = form.Aspirations.Select(m => new AspirationDTO
                {
                    Id = m.Id,
                    FormId = m.RegisterExamId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName,
                    Sequence = m.Sequence
                }).ToList()
            };
        }
        #endregion

        private async Task<Entities.RegisterExam> ConvertDTOtoBO(DTO.RegisterExam formDTO)
        {
            Entities.RegisterExam form = new Entities.RegisterExam
            {
                Id = formDTO.Id,

                Graduated = formDTO.Graduated,
                ClusterContestId = formDTO.ClusterContestId,
                ClusterContestCode = formDTO.ClusterContestCode,
                ClusterContestName = formDTO.ClusterContestName,
                RegisterPlaceOfExamId = formDTO.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = formDTO.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = formDTO.RegisterPlaceOfExamName,
                Biology = formDTO.Biology,
                Chemistry = formDTO.Chemistry,
                CivicEducation = formDTO.CivicEducation,
                Geography = formDTO.Geography,
                History = formDTO.History,
                Languages = formDTO.Languages,
                Literature = formDTO.Literature,
                Maths = formDTO.Maths,
                NaturalSciences = formDTO.NaturalSciences,
                Physics = formDTO.Physics,
                SocialSciences = formDTO.SocialSciences,

                ExceptLanguages = formDTO.ExceptLanguages,
                Mark = formDTO.Mark,
                ReserveBiology = formDTO.ReserveBiology,
                ReserveChemistry = formDTO.ReserveChemistry,
                ReserveCivicEducation = formDTO.ReserveCivicEducation,
                ReserveGeography = formDTO.ReserveGeography,
                ReserveHistory = formDTO.ReserveHistory,
                ReserveLanguages = formDTO.ReserveLanguages,
                ReserveLiterature = formDTO.ReserveLiterature,
                ReserveMaths = formDTO.ReserveMaths,
                ReservePhysics = formDTO.ReservePhysics,

                Area = formDTO.Area,
                PriorityType = formDTO.PriorityType,
                Aspirations = formDTO.Aspirations == null ? null : formDTO.Aspirations.Select(m => new Aspiration
                {
                    Id = m.Id ?? default,
                    RegisterExamId = m.FormId,
                    MajorsId = m.MajorsId,
                    MajorsCode = m.MajorsCode,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName,
                    Sequence = m.Sequence
                }).ToList()
            };
            return form;
        }
    }
}
