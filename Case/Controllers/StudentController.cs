﻿using Case.Model;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository.StudentRepo;

namespace Case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpPost("Add")]
        public IActionResult Add(AddStudentVM vm)
        {
            try
            {
                // Her alanın değerine göre öğrencinin doluluk oranı hesaplanır.
                int rate = 0;
                if (vm.Surname != null) rate += 20;
                if (vm.Name != null) rate += 20;
                if (vm.Phone != null) rate += 20;
                if (vm.Adress != null) rate += 20;
                if (vm.TC != null) rate += 20;

                _studentRepository.Add(new Student()
                {
                    Adress = vm.Adress,
                    Name = vm.Name,
                    Phone = vm.Phone,
                    Surname = vm.Surname,
                    TC = vm.TC,
                    Rate = rate,
                });

                return Ok("Student added successfully");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Update")]
        public IActionResult Update(UpdateStudentVM vm)
        {
            try
            {
                // Her alanın değerine göre öğrencinin doluluk oranı hesaplanır.
                int rate = 0;
                if (vm.Surname != null) rate += 20;
                if (vm.Name != null) rate += 20;
                if (vm.Phone != null) rate += 20;
                if (vm.Adress != null) rate += 20;
                if (vm.TC != null) rate += 20;

                _studentRepository.Update(new Student()
                {
                    Adress = vm.Adress,
                    Name = vm.Name,
                    Phone = vm.Phone,
                    Surname = vm.Surname,
                    TC = vm.TC,
                    Rate = rate,
                });

                return Ok("Student updated successfully");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                // Tüm öğrencileri doluluk oranlarına göre azalan şekilde getirir.
                var rest = _studentRepository.GetAll().OrderByDescending(x => x.Rate).ToList();
                return Ok(rest);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("Get")]
        public IActionResult Get(long id)
        {
            try
            {
                // Belirli bir öğrenciyi getirir.
                var rest = _studentRepository.Get(x => x.Id == id);
                return Ok(rest);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        //Öğrenci Silme Methodu
        [HttpPost("Delete")]
        public IActionResult Delete(long id)
        {
            try
            {
                // Belirli bir öğrenciyi siler.
                var student = _studentRepository.Get(x => x.Id == id);
                if (student == null)
                {
                    // Öğrenci bulunamazsa 404 Not Found yanıtı döner.
                    return NotFound("Student not found");
                }

                _studentRepository.Delete(student);
                return Ok("Student deleted successfully");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("ProfileFillRate")]
        public IActionResult ProfileFillRate(long id)
        {
            try
            {
                // Belirli bir öğrencinin doluluk oranını getirir.
                var student = _studentRepository.Get(x => x.Id == id);
                if (student == null)
                {
                    // Öğrenci bulunamazsa 404 Not Found yanıtı döner.
                    return NotFound("Student not found");
                }

                var rate = student.Rate;
                return Ok($"{rate}%");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
