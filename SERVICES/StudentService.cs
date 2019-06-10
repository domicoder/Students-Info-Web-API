using Microsoft.EntityFrameworkCore;
using MODEL;
using PERSISTENCE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES
{

    public interface IStudentService
    {
        IEnumerable<Student> GetAll(Student model);
        bool Add(Student model);
        bool Update(Student model);
        bool Delete(int id);
        Student Get(int id);

    }
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _studentDbContext;

        public StudentService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public IEnumerable<Student> GetAll(Student model)
        {
            var result = new List<Student>();
            try
            {
                result = _studentDbContext.Student.ToList();
            }
            catch(System.Exception)
            {
                
            }
            return result;
        }

        public Student Get(int id)
        {
            var student = new Student();
            try
            {
                student = _studentDbContext.Student.Single(x => x.StudentId ==  id);
            }
            catch (System.Exception)
            {

            }
            return student;
        }

        public bool Add(Student model)
        {
            try
            {
                _studentDbContext.Add(model);
                _studentDbContext.SaveChanges();

            }
            catch(System.Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(Student model)
        {
            try
            {
                var originalModel = _studentDbContext.Student.Single(x =>
                x.StudentId == model.StudentId);

                originalModel.Name = model.Name;
                originalModel.LastName = model.LastName;
                originalModel.Bio = model.Bio;

                _studentDbContext.Update(originalModel);
                _studentDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                _studentDbContext.Entry(new Student { StudentId = id }).State = EntityState.Deleted;
                _studentDbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
    }
}
