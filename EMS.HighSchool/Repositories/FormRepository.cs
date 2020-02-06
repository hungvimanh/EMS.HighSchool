using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.HighSchool.Common;
using EMS.HighSchool.Entities;
using EMS.HighSchool.Repositories.Models;

namespace EMS.HighSchool.Repositories
{
    public interface IFormRepository
    {
        Task<bool> Approve(RegisterExam form);
        Task<bool> Create(RegisterExam form);
        Task<RegisterExam> Get(long StudentId);
        Task<bool> Update(RegisterExam form);
        Task<bool> Delete(long Id);
    }
    public class FormRepository : IFormRepository
    {
        private readonly EMSContext context;
        private ICurrentContext CurrentContext;
        public FormRepository(EMSContext _context, ICurrentContext _currentContext)
        {
            context = _context;
            CurrentContext = _currentContext;
        }

        public async Task<bool> Create(RegisterExam form)
        {
            RegisterExamDAO formDAO = new RegisterExamDAO
            {
                Id = form.Id,
                Graduated = form.Graduated,
                ClusterContestId = form.ClusterContestId,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                Maths = form.Maths,
                Literature = form.Literature,
                Languages = form.Languages,
                NaturalSciences = form.NaturalSciences,
                SocialSciences = form.SocialSciences,
                Physics = form.Physics,
                Chemistry = form.Chemistry,
                Biology = form.Biology,
                History = form.History,
                Geography = form.Geography,
                CivicEducation = form.CivicEducation,

                ExceptLanguages = form.ExceptLanguages,
                Mark = form.Mark,
                ReserveMaths = form.ReserveMaths,
                ReserveLiterature = form.ReserveLiterature,
                ReserveLanguages = form.ReserveLanguages,
                ReservePhysics = form.ReservePhysics,
                ReserveChemistry = form.ReserveChemistry,
                ReserveBiology = form.ReserveBiology,
                ReserveHistory = form.ReserveHistory,
                ReserveGeography = form.ReserveGeography,
                ReserveCivicEducation = form.ReserveCivicEducation,

                PriorityType = form.PriorityType,
                Area = form.Area,
                Status = form.Status,
                StudentId = CurrentContext.StudentId,
            };

            context.RegisterExam.Add(formDAO);
            if (form.Aspirations.Any())
            {
            await BulkCreateAspirations(form);
            }
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BulkCreateAspirations(RegisterExam form)
        {
            await context.Aspiration.Where(d => d.RegisterExamId == form.Id).DeleteFromQueryAsync();
            if(form.Aspirations != null)
            {
                List<AspirationDAO> aspirationDAOs = form.Aspirations.Select(a => new AspirationDAO
                {
                    Id = a.Id,
                    Sequence = a.Sequence,
                    RegisterExamId = a.RegisterExamId,
                    MajorsId = a.MajorsId,
                    UniversityId = a.UniversityId,
                    SubjectGroupId = a.SubjectGroupId
                }).ToList();
                await context.Aspiration.AddRangeAsync(aspirationDAOs);
            }
            
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(long Id)
        {
            await context.Aspiration.Where(d => d.RegisterExamId == Id).DeleteFromQueryAsync();
            await context.RegisterExam.Where(b => b.Id == Id).DeleteFromQueryAsync();
            return true;
        }

        public async Task<RegisterExam> Get(long StudentId)
        {
            RegisterExam form = await context.RegisterExam.Where(f => f.StudentId == StudentId).Include(f => f.Aspirations).Select(f => new RegisterExam
            {
                Id = f.Id,
                StudentId = f.StudentId,

                Graduated = f.Graduated,
                ClusterContestId = f.ClusterContestId,
                ClusterContestCode = f.ClusterContest.Code,
                ClusterContestName = f.ClusterContest.Name,
                RegisterPlaceOfExamId = f.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = f.RegisterPlaceOfExam.Code,
                RegisterPlaceOfExamName = f.RegisterPlaceOfExam.Name,
                Biology = f.Biology,
                Chemistry = f.Chemistry,
                CivicEducation = f.CivicEducation,
                Geography = f.Geography,
                History = f.History,
                Languages = f.Languages,
                Literature = f.Literature,
                Maths = f.Maths,
                NaturalSciences = f.NaturalSciences,
                Physics = f.Physics,
                SocialSciences = f.SocialSciences,

                ExceptLanguages = f.ExceptLanguages,
                Mark = f.Mark,
                ReserveBiology = f.ReserveBiology,
                ReserveChemistry = f.ReserveChemistry,
                ReserveCivicEducation = f.ReserveCivicEducation,
                ReserveGeography = f.ReserveGeography,
                ReserveHistory = f.ReserveHistory,
                ReserveLanguages = f.ReserveLanguages,
                ReserveLiterature = f.ReserveLiterature,
                ReserveMaths = f.ReserveMaths,
                ReservePhysics = f.ReservePhysics,

                Area = f.Area,
                PriorityType = f.PriorityType,
                Status = f.Status,
                Aspirations = f.Aspirations.Select(a => new Aspiration
                {
                    Id = a.Id,
                    RegisterExamId = a.RegisterExamId,
                    MajorsId = a.MajorsId,
                    MajorsCode = a.Majors.Code,
                    MajorsName = a.Majors.Name,
                    UniversityId = a.UniversityId,
                    UniversityCode = a.University.Code,
                    UniversityName = a.University.Name,
                    UniversityAddress = a.University.Address,
                    SubjectGroupId = a.SubjectGroupId,
                    SubjectGroupCode = a.SubjectGroup.Code,
                    SubjectGroupName = a.SubjectGroup.Name,
                    Sequence = a.Sequence
                }).OrderBy(a => a.Sequence).ToList(),
            }).FirstOrDefaultAsync();

            return form;
        }

        public async Task<bool> Update(RegisterExam form)
        {
            await context.RegisterExam.Where(f => f.Id == form.Id).UpdateFromQueryAsync(f => new RegisterExamDAO
            {
                Id = f.Id,

                Graduated = form.Graduated,
                ClusterContestId = form.ClusterContestId,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                Maths = form.Maths,
                Literature = form.Literature,
                Languages = form.Languages,
                Physics = form.Physics,
                Chemistry = form.Chemistry,
                Biology = form.Biology,
                History = form.History,
                Geography = form.Geography,
                CivicEducation = form.CivicEducation,
                NaturalSciences = form.NaturalSciences,
                SocialSciences = form.SocialSciences,

                Mark = form.Mark,
                ExceptLanguages = form.ExceptLanguages,
                ReserveMaths = form.ReserveMaths,
                ReserveLiterature = form.ReserveLiterature,
                ReserveLanguages = form.ReserveLanguages,
                ReservePhysics = form.ReservePhysics,
                ReserveChemistry = form.ReserveChemistry,
                ReserveBiology = form.ReserveBiology,
                ReserveHistory = form.ReserveHistory,
                ReserveGeography = form.ReserveGeography,
                ReserveCivicEducation = form.ReserveCivicEducation,

                Area = form.Area,
                PriorityType = form.PriorityType,
                Status = form.Status
            });
            await BulkCreateAspirations(form);

            return true;
        }

        public async Task<bool> Approve(RegisterExam form)
        {
            await context.RegisterExam.Where(f => f.Id == form.Id).UpdateFromQueryAsync(f => new RegisterExamDAO
            {
                Status = form.Status
            });
            return true;
        }
    }
}
