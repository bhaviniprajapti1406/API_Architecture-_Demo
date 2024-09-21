using API.DAL;
using API.DAL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IRepository<Students> _studentsRepository;

        public StudentController(IRepository<Students> studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }


        [HttpPost]
        //[Route("AddEmployee")]
        public async Task<IActionResult> Post(Students std)
        {
            var result = await _studentsRepository.AddAsync(std);            
            return Ok("Added Successfully");
        }


        [HttpGet]
        //[Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _studentsRepository.GetAllAsync());
        }

        [HttpDelete]
        public async Task<bool> Delete(Students std)
        {
            await _studentsRepository.DeleteAsync(std);
            return true;           
        }

        [HttpGet("{id}")]
        public async Task<Students> GetById(int id)
        {
            Students data =await _studentsRepository.GetByIdAsync(id);
            return data;
        }

        [HttpPut]
        public async Task<IActionResult> Update(Students std)
        {
            await _studentsRepository.UpdateAsync(std);
            return Ok("Updated Successfully!");
        }

    }
}
